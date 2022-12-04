--USE FootballBet
declare @oddsId           int
declare @userId           varchar(64)
declare @groupId          varchar(64)
declare @matchOddsGroupId int
declare @oddsAwayGoals    int
declare @oddsHomeGoals    int
declare @matchHomeGoals   int
declare @matchAwayGoals   int
declare @matchId          int
declare @statsExactWins   int = 0
declare @statsBaseWins    int = 0
declare @statsLosses      int = 0
declare @odds decimal = 0
declare @highestWin decimal =0
declare @nickName nvarchar(max) = ''
declare @baseOdds decimal = 0

DECLARE BettingGroupMembersCursor CURSOR FOR
select bgm.UserId, bgm.BettingGroupEntityId, bgm.Nickname
from BettingGroupMembers bgm
    OPEN BettingGroupMembersCursor
FETCH NEXT FROM BettingGroupMembersCursor INTO @userId, @groupId, @nickName
    WHILE @@FETCH_STATUS = 0
BEGIN
        DECLARE bet_cursor CURSOR FOR
select b.OddsId
from BetEntities b
where b.Processed = 1
  and b.IsWinningBet = 1
  and b.UserId = @userId
  and b.BettingGroupId = @groupId
    OPEN bet_cursor
        FETCH NEXT FROM bet_cursor INTO @oddsId
    WHILE @@FETCH_STATUS = 0
BEGIN
				-- select exact score odds
select @matchOddsGroupId = eo.MatchOddsGroupId,
       @oddsAwayGoals = eo.AwayTeamGoals,
       @oddsHomeGoals = eo.HomeTeamGoals,
       @odds = eo.Odds
from ExactScoreOddsEntities eo
where Id = @oddsId
--select base odds



select @matchId = mog.MatchId from MatchOddsGroupEntities mog where Id = @matchOddsGroupId
select @matchHomeGoals = m.HomeCurrentGoals, @matchAwayGoals = m.AwayCurrentGoals
from MatchEntities m
where Id = @matchId


    IF (@matchHomeGoals = @oddsHomeGoals and @matchAwayGoals = @oddsAwayGoals)
BEGIN
                        set @statsExactWins = @statsExactWins + 1
						--IF (@odds> @highestWin)
						--BEGIN
						--	set @highestWin = @odds
						--	print(@nickName + ': ' + CAST(@highestWin as varchar(max)))
						--END
END
ELSE
BEGIN
						-- home win
						IF(@matchHomeGoals > @matchAwayGoals)
BEGIN
select @baseOdds = bo.Odds from BaseOddsEntities bo where bo.MatchOddsGroupId = @matchOddsGroupId and bo.MatchWinnerEntityEnum = 0
END
						-- away win
						IF(@matchHomeGoals < @matchAwayGoals)
BEGIN
select @baseOdds = bo.Odds from BaseOddsEntities bo where bo.MatchOddsGroupId = @matchOddsGroupId and bo.MatchWinnerEntityEnum = 1
END
						-- draw
						IF(@matchHomeGoals = @matchAwayGoals)
BEGIN
select @baseOdds = bo.Odds from BaseOddsEntities bo where bo.MatchOddsGroupId = @matchOddsGroupId and bo.MatchWinnerEntityEnum = 2
END
                        IF (@baseOdds> @highestWin)
BEGIN
							set @highestWin = @baseOdds
							print(@nickName + ': ' + CAST(@highestWin as varchar(max)))
END
END
FETCH NEXT FROM bet_cursor INTO @oddsId
end
CLOSE bet_cursor
    DEALLOCATE bet_cursor
        Fetch Next From BettingGroupMembersCursor INTO @userId, @groupId, @nickName
END
CLOSE BettingGroupMembersCursor
    DEALLOCATE BettingGroupMembersCursor