declare @datePlayed date = DATEADD(day, -1, GETDATE());

update MatchEntities
set Date = @datePlayed,
    MatchStatus = 7,
    BetsPayed = 0,
    HomeFulltimeGoals = 1,
    AwayFulltimeGoals = 0
    
where Id in (select top 2 m.Id
             from MatchEntities m
             where m.MatchStatus = 1
             order by m.Date)

select * from MatchEntities m order by m.Date;
update MatchEntities set BetsPayed = 0 where Id = 855734

select * from BetEntities b order by b.MatchId;

select * from UserBalanceEntities