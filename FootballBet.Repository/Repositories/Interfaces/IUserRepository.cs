using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<ApplicationUser?> GetUserAsync(string userId, CancellationToken ct);
    public Task<decimal> GetBalanceAsync(string userId, string groupId, CancellationToken ct);
    public Task ChangeNicknameForGroupMemberAsync(string userId, string newNickname, string groupId);
    public Task<string> GetBettingGroupUserNicknameByUserIdAsync(string userId);
    public Task<string> GetUserNickNameAsync(string userId, string groupId);
}