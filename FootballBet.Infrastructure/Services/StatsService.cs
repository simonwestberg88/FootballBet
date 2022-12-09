using FootballBet.Infrastructure.Interfaces;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;
using FootballBet.Repository.Repositories;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Shared.Models.Stats;
using FootballBet.Shared.Models.Users;

namespace FootballBet.Infrastructure.Services
{
    public class StatsService : IStatsService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IBetRepository _betRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IOddsRepository _oddsRepository;
        private readonly IStatRepository _statRepository;
        private readonly IUserRepository _userRepository;

        public StatsService(
            IGroupRepository groupRepository,
            IBetRepository betRepository,
            IMatchRepository matchRepository,
            IOddsRepository oddsRepository,
            IStatRepository statRepository,
            IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _betRepository = betRepository;
            _matchRepository = matchRepository;
            _oddsRepository = oddsRepository;
            _statRepository = statRepository;
            _userRepository = userRepository;
        }

        [Obsolete("a bit too heavy, would need a different implementation that reduces SQL requests")]
        public async Task<GameDayStatsContainerShared> GetAdvancedStatsForGroupAsync(string groupId)
        {
            var members = await _groupRepository.GetBettingGroupMembersAsync(groupId);
            var matches = await _matchRepository.GetMatches(1);
            var finishedMatches = await _matchRepository.GetFinishedMatches(); //hardcoded for world cup
            var gameDays = matches.Select(x => x.Date.Date).Distinct();
            var betsForGroup = await _betRepository.GetBetsForGroupAsync(groupId);
            var gameDayStats = new List<GameDayStatsShared>();
            foreach (var gameday in gameDays)
            {
                var memberGameDayStats = new List<MemberGameDayStatsShared>();
                var gameDayMatches = finishedMatches.Where(x => x.Date.Date == gameday.Date.Date).ToList();
                var winningGameDayBets = betsForGroup.Where(x =>
                    x.IsWinningBet == true && gameDayMatches.Select(gdm => gdm.Id).Contains(x.MatchId));
                foreach (var member in members)
                {
                    var totalMatchDayWinnings = 0m;
                    var personalWinningBets = winningGameDayBets.Where(b => b.UserId == member.UserId);
                    foreach (var winningBet in personalWinningBets)
                    {
                        var odds = await _oddsRepository.GetOddsAsync(winningBet.OddsId);
                        var match = gameDayMatches.FirstOrDefault(m => m.Id == winningBet.MatchId);
                        var exactWin = odds.HomeTeamGoals == match.HomeFulltimeGoals &&
                                       odds.AwayTeamGoals == match.AwayFulltimeGoals;
                        var oddsMultiplier = odds.Odds;
                        if (!exactWin)
                            oddsMultiplier = (await _oddsRepository.GetBaseOddsAsync(odds.Id, GetMatchWinner(match)))
                                .Odds;
                        totalMatchDayWinnings += winningBet.WagerAmount * oddsMultiplier;
                    }

                    memberGameDayStats.Add(new MemberGameDayStatsShared()
                    {
                        User = new() { Id = member.UserId, UserName = member.Nickname },
                        TotalWinningsForDay = totalMatchDayWinnings
                    });
                }

                gameDayStats.Add(new GameDayStatsShared()
                {
                    Date = gameday,
                    MemberGameDayStats = memberGameDayStats
                });
            }

            return new GameDayStatsContainerShared()
            {
                GameDayStats = gameDayStats,
                GameDates = gameDays.ToList(),
                Members = members.Select(x => new UserDto()
                {
                    Id = x.UserId,
                    UserName = x.Nickname,
                    Balance = (_betRepository.GetUserBalanceForGroupAsync(x.UserId, groupId)).Result.Balance
                }).ToList()
            };
        }

        public async Task<GameDayStatsContainerShared> GetStatsForGroupAsync(string groupId)
        {
            var users = await _betRepository.GetUserBalancesForGroupAsync(groupId);
            var members = await _groupRepository.GetBettingGroupMembersAsync(groupId);
            var betStats = await _betRepository.GetBetStatsAsync(groupId);
            return new GameDayStatsContainerShared()
            {
                Members = members.Select(x => new UserDto()
                {
                    Id = x.UserId,
                    UserName = x.Nickname,
                    Balance = users.FirstOrDefault(u => u.UserId == x.UserId)?.Balance ?? 0,
                    Stats = new BetStatsDto
                    {
                        Balance = users.FirstOrDefault(u => u.UserId == x.UserId)?.Balance ?? 0,
                        Losses = betStats.FirstOrDefault(s => s.UserId == x.UserId)?.Losses ?? 0,
                        BaseWins = betStats.FirstOrDefault(s => s.UserId == x.UserId)?.BaseWins ?? 0,
                        ExactWins = betStats.FirstOrDefault(s => s.UserId == x.UserId)?.ExactWins ?? 0
                    }
                }).ToList()
            };
        }

        public async Task<BetStatsDto> GetAppBarStatsAsync(string groupId, string userId)
        {
            var stats = await _betRepository.GetBetStatsAsync(groupId, userId);
            var balance = (await _betRepository.GetUserBalanceForGroupAsync(userId, groupId)).Balance;
            return new BetStatsDto
            {
                ExactWins = stats.ExactWins,
                BaseWins = stats.BaseWins,
                Losses = stats.Losses,
                Balance = balance
            };
        }

        public async Task<ChartResponse> GetChartStatsAsync(string groupId)
        {
            var wins = (await _statRepository.GetWinsAsync(groupId)).ToList();
            var groupUsers = wins.GroupBy(g => g.UserId).ToList();
            var userData = groupUsers.Select(g =>
            {
                var userName = _userRepository.GetUserNickNameAsync(g.Key, groupId).Result;
                decimal total = 0;
                var userWinsSorted = g.OrderBy(w => w.WinDate).ToList();
                List<WinData> winData = new ();
                foreach (var winEntity in userWinsSorted)
                {
                    total += winEntity.Amount;
                    winData.Add(new WinData
                    {
                        Amount = total,
                        Date = winEntity.WinDate
                    });
                }
                return new UserData
                {
                    Username = userName,
                    Wins = winData
                };
            });
            return new ChartResponse
            {
                UserData = userData
            };
        }

        public async Task<WinStatsResponse> GetTop10WinStatsAsync(string groupId)
        {
            var wins = (await _statRepository.GetTop10WinsAsync(groupId)).ToList();
            // map wins to WinStats object
            var winStats = wins.Select(w => new WinStats
            {
                NickName = _userRepository.GetUserNickNameAsync(w.UserId ?? "", groupId).Result,
                Date = w.WinDate,
                WinAmount = w.Amount,
                IsExactWin = w.IsExactScoreWin
            }).ToList();
            return new WinStatsResponse
            {
                WinStats = winStats
            };
        }
        
        public async Task<WinStatsResponse> GetLatestWinsAsync(string groupId)
        {
            var wins = (await _statRepository.GetLatestWinsAsync(groupId)).ToList();
            // map wins to WinStats object
            var winStats = wins.Select(w => new WinStats
            {
                NickName = _userRepository.GetUserNickNameAsync(w.UserId ?? "", groupId).Result,
                Date = w.WinDate,
                WinAmount = w.Amount,
                IsExactWin = w.IsExactScoreWin
            }).ToList();
            return new WinStatsResponse
            {
                WinStats = winStats
            };
        }

        public async Task<WinStatsResponse> GetWinStatsAsync(string groupId, string userId)
        {
            var wins = await _statRepository.GetWinsAsync(groupId, userId);
            // map wins to WinStats object
            var winStats = wins.Select(w => new WinStats
            {
                NickName = _userRepository.GetUserNickNameAsync(w.UserId ?? "", groupId).Result,
                Date = w.WinDate,
                WinAmount = w.Amount,
                IsExactWin = w.IsExactScoreWin
            }).ToList();
            return new WinStatsResponse
            {
                WinStats = winStats
            };
        }

        private static MatchWinnerEntityEnum GetMatchWinner(MatchEntity match)
        {
            if (match.HomeFulltimeGoals > match.AwayFulltimeGoals)
            {
                return MatchWinnerEntityEnum.Home;
            }

            if (match.AwayFulltimeGoals > match.HomeFulltimeGoals)
            {
                return MatchWinnerEntityEnum.Away;
            }

            return MatchWinnerEntityEnum.Draw;
        }
    }
}