using FootballBet.Infrastructure.Exceptions;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Mappers;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Shared.Models.Groups;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace FootballBet.Server.Data.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFootballRepository _footballRepository;

        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository,
            IEmailSender emailSender, IHttpContextAccessor httpContextAccessor, IFootballRepository footballRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _footballRepository = footballRepository;
        }

        public async Task<BettingGroupInvitationEntity> CreateInvitation(string groupId, string invitedUserEmail, string inviterUserId, CancellationToken ct)
        {
            var invitation = await _groupRepository.CreateBettingGroupInvitation(CreateBettingGroupInvitation(Guid.Parse(groupId), inviterUserId, invitedUserEmail), ct);

            var callbackUrl = $"https://{_httpContextAccessor.HttpContext.Request.Host.Value}/invitation/{invitation.BettingGroupInvitationId}/{groupId}";
            await _emailSender.SendEmailAsync(invitedUserEmail, "You are invited to join FootballBet",
                $"Accept your invitation by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            return invitation;
        }

        public async Task<BettingGroupShared> CreateBettingGroup(string creatorId, string description, string groupName, CancellationToken ct)
        {
            var user = await _userRepository.GetUserAsync(creatorId, ct);
            var league = await _footballRepository.GetLeague(1);
            if (user == null)
                throw new KeyNotFoundException();

            var newGroup = CreateBettingGroup(groupName, description, user, league);
            return GroupMapper.Map(await _groupRepository.CreateGroup(user, newGroup, ct), user.Id);
        }

        public async Task<List<BettingGroupShared>> ListGroupsForUser(string userId, CancellationToken ct)
        {
            var bettingGroupIds = (await _groupRepository.GetBettingGroupMemberByUserId(userId, ct)).Select(p => p.BettingGroupEntityId)
                .ToList();
            var groupList = new List<BettingGroupEntity>();
            foreach (var id in bettingGroupIds)
            {
                groupList.Add(await _groupRepository.GetGroupById(id, ct));
            }
            return groupList.Select(g => GroupMapper.Map(g, userId)).ToList();
        }

        public async Task ConsumeInvitation(string invitationId, string groupId, string userId, CancellationToken ct)
        {
            var invitedUser = await _userRepository.GetUserAsync(userId, ct);
            if (await ValidateInvitation(groupId, invitationId, invitedUser.Email, ct))
            {
                var bettingGroupMember = CreateBettingGroupMember(invitedUser);
                await _groupRepository.JoinGroup(Guid.Parse(groupId), bettingGroupMember, ct);
                await _groupRepository.DeleteBettingGroupInvitation(Guid.Parse(invitationId), ct);
            }
            else
                throw new BadHttpRequestException("Validation of invitation failed");
        }

        public async Task<BettingGroupShared> GetBettingGroupById(string groupId, string currentUserId, CancellationToken ct)
            => GroupMapper.Map(await _groupRepository.GetGroupById(Guid.Parse(groupId), ct), currentUserId) ?? throw new NotFoundException("Betting group not found");

        private async Task<bool> ValidateInvitation(string groupId, string invitationId, string userEmail, CancellationToken ct)
        {
            var invitation = await _groupRepository.GetBettingGroupInvitationByIdAsync(Guid.Parse(invitationId), ct);
            var group = await _groupRepository.GetGroupById(Guid.Parse(groupId), ct);

            if (invitation == null || group == null || invitation.InvitedUserEmail != userEmail || invitation.BettingGroupEntityId != Guid.Parse(groupId))
                return false;
            return true;
        }



        private static BettingGroupInvitationEntity CreateBettingGroupInvitation(Guid groupId, string invitingUserId, string invitedUserEmail)
            => new()
            {
                BettingGroupEntityId = groupId,
                InvitingUserId = invitingUserId,
                InvitedUserEmail = invitedUserEmail
            };

        private static BettingGroupEntity CreateBettingGroup(string groupName, string description, ApplicationUser creator, LeagueEntity league)
           => new()
           {
               Id = new Guid(),
               Description = description,
               Name = groupName,
               Memberships = new List<BettingGroupMemberEntity>()
                    {
                        CreateBettingGroupMember(creator)
                    },
               Creator = creator,
               CreatedAt = DateTime.UtcNow,
               League = league
           };

        private static BettingGroupMemberEntity CreateBettingGroupMember(ApplicationUser user)
            => new()
            {
                Id = new Guid(),
                Nickname = user.UserName,
                UserId = user.Id
            };


    }
}
