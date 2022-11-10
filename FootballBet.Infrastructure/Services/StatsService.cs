﻿using FootballBet.Infrastructure.Interfaces;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Shared.Models.Stats;
using FootballBet.Shared.Models.Users;

namespace FootballBet.Infrastructure.Services
{

    public class StatsService : IStatsService
    {
        //    public List<ChartSeries> Series = new List<ChartSeries>()
        //{
        //    new ChartSeries() { Name = "Series 1", Data = new double[] { 90, 79, 72, 69, 62, 62, 55, 65, 70 } },
        //    new ChartSeries() { Name = "Series 2", Data = new double[] { 10, 41, 35, 51, 49, 62, 69, 91, 148 } },
        //};
        //    public string[] XAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" };

        private readonly IGroupRepository _groupRepository;
        private readonly IBetRepository _betRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IOddsRepository _oddsRepository;

        public StatsService(
            IGroupRepository groupRepository,
            IBetRepository betRepository,
            IMatchRepository matchRepository,
            IOddsRepository oddsRepository)
        {
            _groupRepository = groupRepository;
            _betRepository = betRepository;
            _matchRepository = matchRepository;
            _oddsRepository = oddsRepository;
        }

        public async Task<GameDayStatsContainerShared> GetStatsForGroupAsync(string groupId)
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
                var winningGameDayBets = betsForGroup.Where(x => x.IsWinningBet == true && gameDayMatches.Select(gdm => gdm.Id).Contains(x.MatchId));
                foreach (var member in members)
                {
                    var totalMatchDayWinnings = 0m;
                    var personalWinningBets = winningGameDayBets.Where(b => b.UserId == member.UserId);
                    foreach (var winningBet in personalWinningBets)
                    {
                        var odds = await _oddsRepository.GetOddsAsync(winningBet.OddsId);
                        var match = gameDayMatches.FirstOrDefault(m => m.Id == winningBet.MatchId);
                        var exactWin = odds.HomeTeamGoals == match.HomeFulltimeGoals && odds.AwayTeamGoals == match.AwayFulltimeGoals;
                        var oddsMultiplier = odds.Odds;
                        if (!exactWin)
                            oddsMultiplier = (await _oddsRepository.GetBaseOddsAsync(odds.Id, GetMatchWinner(match))).Odds;
                        totalMatchDayWinnings += winningBet.WagerAmount * oddsMultiplier;
                    }

                    memberGameDayStats.Add(new MemberGameDayStatsShared()
                    {
                        User = new() { Id = member.UserId, UserName = member.Nickname},
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
                Members = members.Select(x => new UserDto() { Id = x.UserId, UserName = x.Nickname}).ToList() 
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
