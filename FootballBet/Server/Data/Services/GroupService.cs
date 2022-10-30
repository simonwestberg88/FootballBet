﻿using FootballBet.Server.Data.Mappers;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Data.Services.Exceptions;
using FootballBet.Server.Data.Services.Interfaces;
using FootballBet.Server.Models;
using FootballBet.Server.Models.Groups;
using FootballBet.Shared.Models.Groups;
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

        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BettingGroupInvitation> CreateInvitation(string groupId, string invitedUserEmail, string inviterUserId, CancellationToken ct)
        {
            var invitation = await _groupRepository.CreateBettingGroupInvitation(CreateBettingGroupInvitation(Guid.Parse(groupId), inviterUserId, invitedUserEmail), ct);

            var callbackUrl = $"https://{_httpContextAccessor.HttpContext.Request.Host.Value}/invitation/{invitation.BettingGroupInvitationId}/{groupId}";
            await _emailSender.SendEmailAsync(invitedUserEmail, "You are invited to join FootballBet",
                $"Accept your invitation by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            return invitation;
        }

        public async Task<BettingGroup> CreateBettingGroup(string creatorId, string description, string groupName, CancellationToken ct)
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
            var groupList = new List<BettingGroup>();

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

        public async Task<BettingGroup> GetBettingGroupById(string groupId, CancellationToken ct)
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



        private static BettingGroupInvitation CreateBettingGroupInvitation(Guid groupId, string invitingUserId, string invitedUserEmail)
            => new()
            {
                BettingGroupInvitationId = new Guid(),
                BettingGroupId = groupId,
                InvitingUserId = invitingUserId,
                InvitedUserEmail = invitedUserEmail
            };

        private static BettingGroup CreateBettingGroup(string groupName, string description, ApplicationUser creator)
           => new()
           {
               Description = description,
               Name = groupName,
               Memberships = new List<BettingGroupMember>()
                    {
                        CreateBettingGroupMember(creator)
                    },
               CreatedAt = DateTime.UtcNow
           };

        private static BettingGroupMember CreateBettingGroupMember(ApplicationUser user)
            => new()
            {
                Nickname = user.UserName,
                UserId = user.Id
            };


    }
}
