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

DECLARE BettingGroupMembersCursor CURSOR FOR
    select bgm.UserId, bgm.BettingGroupEntityId
    from BettingGroupMembers bgm
OPEN BettingGroupMembersCursor
FETCH NEXT FROM BettingGroupMembersCursor INTO @userId, @groupId
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
                select @matchOddsGroupId = eo.MatchOddsGroupId,
                       @oddsAwayGoals = eo.AwayTeamGoals,
                       @oddsHomeGoals = eo.HomeTeamGoals
                from ExactScoreOddsEntities eo
                where Id = @oddsId
                select @matchId = mog.MatchId from MatchOddsGroupEntities mog where Id = @matchOddsGroupId
                select @matchHomeGoals = m.HomeCurrentGoals, @matchAwayGoals = m.AwayCurrentGoals
                from MatchEntities m
                where Id = @matchId
                IF (@matchHomeGoals = @oddsHomeGoals and @matchAwayGoals = @oddsAwayGoals)
                    BEGIN
                        set @statsExactWins = @statsExactWins + 1
                    END
                ELSE
                    BEGIN
                        set @statsBaseWins = @statsBaseWins + 1
                    END
                FETCH NEXT FROM bet_cursor INTO @oddsId
            end
        CLOSE bet_cursor
        DEALLOCATE bet_cursor

        select @statsLosses =  count(*) from BetEntities b where b.UserId = @userId AND b.BettingGroupId = @groupId AND b.Processed = 1 and b.IsWinningBet = 0

        update StatEntities
        set ExactWins = @statsExactWins,
            BaseWins  = @statsBaseWins,
            Losses    = @statsLosses
        where UserId = @userId
          and GroupId = @groupId
        SET @statsExactWins = 0
		SET @statsBaseWins = 0
		SET @statsLosses = 0
        Fetch Next From BettingGroupMembersCursor INTO @userId, @groupId
    END
CLOSE BettingGroupMembersCursor
DEALLOCATE BettingGroupMembersCursor