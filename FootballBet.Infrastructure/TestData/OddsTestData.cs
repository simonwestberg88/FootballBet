using FootballBet.Infrastructure.ApiResponses.Odds;
using FootballBet.Server.Data.Repositories.Interfaces;

namespace FootballBet.Infrastructure.TestData;

public class OddsTestData
{
    private readonly IFootballRepository _footballRepository;

    public OddsTestData(IFootballRepository footballRepository)
    {
        _footballRepository = footballRepository;
    }

    public OddsRoot GenerateData()
    {
        var matchIdList = _footballRepository.GetAllMatchesForLeagueId(1).Select(m => m.Id).ToList();
        return new OddsRoot
        {
            Response = GenerateMatchData(matchIdList).ToList()
        };
    }

    private static IEnumerable<OddsResponse> GenerateMatchData(IEnumerable<int> matchIds)
    {
        return matchIds.Select(id => new OddsResponse
        {
            Match = new Match
            {
                Date = DateTime.Now,
                Id = 1,
                Timestamp = 0,
                Timezone = "gmt"
            },
            Bookmakers = new List<Bookmaker>
            {
                new ()
                {
                    Id = 8,
                    Name = "bet365",
                    Bets = new List<Bet>
                    {
                        GenerateMatchWinnerData(),
                        GenerateExactScoreData()
                    }
                }
            }
        });
    }

    private static Bet GenerateMatchWinnerData()
    {
        return new Bet
        {
            Id = 1,
            Name = "Match Winner",
            Values = new List<Value>
            {
                new()
                {
                    Odd = "3.25",
                    Prediction = "Home"
                },
                new()
                {
                    Odd = "2.25",
                    Prediction = "Draw"
                },
                new()
                {
                    Odd = "2.75",
                    Prediction = "Away"
                }
            }
        };
    }

    private static Bet GenerateExactScoreData()
    {
        return new Bet
        {
            Id = 10,
            Name = "Exact Score",
            Values = new List<Value>
            {
                new()
                {
                    Prediction = "1=0",
                    Odd = "9.00"
                },
                new()
                {
                    Prediction = "2=0",
                    Odd = "15.00"
                },
                new()
                {
                    Prediction = "2=1",
                    Odd = "12.00"
                },
                new()
                {
                    Prediction = "3=0",
                    Odd = "34.00"
                },
                new()
                {
                    Prediction = "3=1",
                    Odd = "29.00"
                },
                new()
                {
                    Prediction = "3=2",
                    Odd = "34.00"
                },
                new()
                {
                    Prediction = "4=0",
                    Odd = "81.00"
                },
                new()
                {
                    Prediction = "4=1",
                    Odd = "51.00"
                },
                new()
                {
                    Prediction = "4=2",
                    Odd = "81.00"
                },
                new()
                {
                    Prediction = "4=3",
                    Odd = "126.00"
                },
                new()
                {
                    Prediction = "5=0",
                    Odd = "251.00"
                },
                new()
                {
                    Prediction = "5=1",
                    Odd = "151.00"
                },
                new()
                {
                    Prediction = "5=2",
                    Odd = "251.00"
                },
                new()
                {
                    Prediction = "5=3",
                    Odd = "501.00"
                },
                new()
                {
                    Prediction = "6=1",
                    Odd = "501.00"
                },
                new()
                {
                    Prediction = "6=2",
                    Odd = "501.00"
                },
                new()
                {
                    Prediction = "0=0",
                    Odd = "9.00"
                },
                new()
                {
                    Prediction = "1=1",
                    Odd = "6.50"
                },
                new()
                {
                    Prediction = "2=2",
                    Odd = "17.00"
                },
                new()
                {
                    Prediction = "3=3",
                    Odd = "51.00"
                },
                new()
                {
                    Prediction = "4=4",
                    Odd = "301.00"
                },
                new()
                {
                    Prediction = "0=1",
                    Odd = "7.00"
                },
                new()
                {
                    Prediction = "0=2",
                    Odd = "10.00"
                },
                new()
                {
                    Prediction = "0=3",
                    Odd = "21.00"
                },
                new()
                {
                    Prediction = "0=4",
                    Odd = "41.00"
                },
                new()
                {
                    Prediction = "0=5",
                    Odd = "101.00"
                },
                new()
                {
                    Prediction = "0=6",
                    Odd = "301.00"
                },
                new()
                {
                    Prediction = "1=2",
                    Odd = "9.00"
                },
                new()
                {
                    Prediction = "1=3",
                    Odd = "19.00"
                },
                new()
                {
                    Prediction = "1=4",
                    Odd = "41.00"
                },
                new()
                {
                    Prediction = "1=5",
                    Odd = "81.00"
                },
                new()
                {
                    Prediction = "1=6",
                    Odd = "251.00"
                },
                new()
                {
                    Prediction = "2=3",
                    Odd = "29.00"
                },
                new()
                {
                    Prediction = "2=4",
                    Odd = "51.00"
                },
                new()
                {
                    Prediction = "2=5",
                    Odd = "151.00"
                },
                new()
                {
                    Prediction = "2=6",
                    Odd = "451.00"
                },
                new()
                {
                    Prediction = "3=4",
                    Odd = "101.00"
                },
                new()
                {
                    Prediction = "3=5",
                    Odd = "301.00"
                },
                new()
                {
                    Prediction = "4=5",
                    Odd = "501.00"
                },
                new()
                {
                    Prediction = "3=6",
                    Odd = "501.00"
                }
            }
        };
    }
}