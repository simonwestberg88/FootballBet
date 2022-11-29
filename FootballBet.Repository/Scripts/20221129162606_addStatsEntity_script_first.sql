USE FootballBet
insert into StatEntities (UserId, GroupId, BaseWins, ExactWins, Losses)
select bgm.UserId, bgm.BettingGroupEntityId, 0, 0, 0
from BettingGroupMembers bgm