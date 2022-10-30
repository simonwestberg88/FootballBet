using FootballBet.Infrastructure.Exceptions;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Mappers;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Models;
using FootballBet.Server.Models.Groups;
using FootballBet.Shared.Models.Groups;
using Microsoft.AspNetCore.Http;

namespace FootballBet.Server.Data.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task<BettingGroupInvitationEntity> CreateInvitation(string groupId, string invitedUserEmail, string inviterUserId, CancellationToken ct)
        {
            //TODO: send email to invited user

            var invitation = await _groupRepository.CreateBettingGroupInvitation(CreateBettingGroupInvitation(Guid.Parse(groupId), inviterUserId, invitedUserEmail), ct);
            return invitation;
        }

        public async Task<BettingGroupEntity> CreateBettingGroup(string creatorId, string description, string groupName, CancellationToken ct)
        {
            var user = await _userRepository.GetApplicationUserById(creatorId, ct);
            if (user == null)
                throw new KeyNotFoundException(); //better exception needed

            var newGroup = CreateBettingGroup(groupName, description, user);
            return await _groupRepository.CreateGroup(user, newGroup, ct);
        }

        public async Task<List<BettingGroupShared>> ListGroupsForUser(string userId, CancellationToken ct)
        {
            var bettingGroupIds = (await _groupRepository.GetBettingGroupMemberByUserId(userId, ct)).Select(p => p.BettingGroupId)
                .ToList();
            var groupList = new List<BettingGroupEntity>();

            foreach (var id in bettingGroupIds)
            {
                //wanted to do this by making a task list and awaiting all, but could not be done 
                //this close to the context... will have to get back to it later
                groupList.Add(await _groupRepository.GetGroupById(id, ct));
            }
            return groupList.Select(g => GroupMapper.Map(g)).ToList();
        }

        public async Task ConsumeInvitation(string invitationId, string groupId, string userId, CancellationToken ct)
        {
            var invitedUser = await _userRepository.GetApplicationUserById(userId, ct);
            if (await ValidateInvitation(groupId, invitationId, invitedUser.Email, ct))
            {
                var bettingGroupMember = CreateBettingGroupMember(invitedUser);
                await _groupRepository.JoinGroup(Guid.Parse(groupId), bettingGroupMember, ct);
                await _groupRepository.DeleteBettingGroupInvitation(Guid.Parse(invitationId), ct);
            }
            else
                throw new BadHttpRequestException("Validation of invitation failed");
        }

        public async Task<BettingGroupEntity> GetBettingGroupById(string groupId, CancellationToken ct)
            => await _groupRepository.GetGroupById(Guid.Parse(groupId), ct) ?? throw new NotFoundException("Betting group not found");

        private async Task<bool> ValidateInvitation(string groupId, string invitationId, string userEmail, CancellationToken ct)
        {
            var invitation = await _groupRepository.GetBettingGroupInvitationByIdAsync(Guid.Parse(invitationId), ct);
            var group = await _groupRepository.GetGroupById(Guid.Parse(groupId), ct);
            //can make above calls at the same time and await both tasks
            if (invitation == null || group == null || invitation.InvitedUserEmail != userEmail || invitation.BettingGroupId != Guid.Parse(groupId))
                return false;
            return true;
        }



        private static BettingGroupInvitationEntity CreateBettingGroupInvitation(Guid groupId, string invitingUserId, string invitedUserEmail)
            => new()
            {
                BettingGroupInvitationId = new Guid(),
                BettingGroupId = groupId,
                InvitingUserId = invitingUserId,
                InvitedUserEmail = invitedUserEmail
            };

        private static BettingGroupEntity CreateBettingGroup(string groupName, string description, ApplicationUser creator)
           => new()
           {
               Description = description,
               Name = groupName,
               Memberships = new List<BettingGroupMemberEntity>()
                    {
                        CreateBettingGroupMember(creator)
                    },
               CreatedAt = DateTime.UtcNow
           };

        private static BettingGroupMemberEntity CreateBettingGroupMember(ApplicationUser user)
            => new()
            {
                Nickname = user.UserName,
                UserId = user.Id
            };


    }
}
