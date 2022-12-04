USE FootballBet
declare @oddsId           int
declare @userId           varchar(64)
declare @groupId          varchar(64)
declare @matchOddsGroupId int
declare @oddsAwayGoals    int
declare @oddsHomeGoals    int
declare @matchHomeGoals   int
declare @matchAwayGoals   int
declare @matchId          int
declare @odds             decimal = 0
declare @nickName         nvarchar(max) = ''
declare @baseOdds         decimal = 0
declare @matchDate        datetime

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
                select @matchHomeGoals = m.HomeCurrentGoals, @matchAwayGoals = m.AwayCurrentGoals, @matchDate = m.Date
                from MatchEntities m
                where Id = @matchId

                -- exact score
                IF (@matchHomeGoals = @oddsHomeGoals and @matchAwayGoals = @oddsAwayGoals)
                    BEGIN
                        INSERT INTO WinEntities (UserId, MatchId, Amount, IsExactScoreWin, WinDate)
                        VALUES (@userId, @matchId, (@odds * 100), 1, @matchDate)
                        --IF (@odds> @highestWin)
                        --BEGIN
                        --	set @highestWin = @odds
                        --	print(@nickName + ': ' + CAST(@highestWin as varchar(max)))
                        --END
                    END
                    -- base odds
                ELSE
                    BEGIN
                        -- home win
                        IF (@matchHomeGoals > @matchAwayGoals)
                            BEGIN
                                select @baseOdds = bo.Odds
                                from BaseOddsEntities bo
                                where bo.MatchOddsGroupId = @matchOddsGroupId
                                  and bo.MatchWinnerEntityEnum = 0
                            END
                        -- away win
                        IF (@matchHomeGoals < @matchAwayGoals)
                            BEGIN
                                select @baseOdds = bo.Odds
                                from BaseOddsEntities bo
                                where bo.MatchOddsGroupId = @matchOddsGroupId
                                  and bo.MatchWinnerEntityEnum = 1
                            END
                        -- draw
                        IF (@matchHomeGoals = @matchAwayGoals)
                            BEGIN
                                select @baseOdds = bo.Odds
                                from BaseOddsEntities bo
                                where bo.MatchOddsGroupId = @matchOddsGroupId
                                  and bo.MatchWinnerEntityEnum = 2
                            END
                        INSERT INTO WinEntities (UserId, MatchId, Amount, IsExactScoreWin, WinDate)
                        VALUES (@userId, @matchId, (@baseOdds * 100), 0, @matchDate)
                    END
                FETCH NEXT FROM bet_cursor INTO @oddsId
            end
        CLOSE bet_cursor
        DEALLOCATE bet_cursor
        Fetch Next From BettingGroupMembersCursor INTO @userId, @groupId, @nickName
    END
CLOSE BettingGroupMembersCursor
DEALLOCATE BettingGroupMembersCursor