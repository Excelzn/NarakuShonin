﻿namespace NarakuShonin.Web.Shared.Models.DiscordApiModels;

using System;

[Flags]
public enum DiscordPermissions : ulong
{
    CreateInstantInvite = 1UL << 0,
    KickMembers = 1UL << 1,
    BanMembers = 1UL << 2,
    Administrator = 1UL << 3,
    ManageChannels = 1UL << 4,
    ManageGuild = 1UL << 5,
    AddReactions = 1UL << 6,
    ViewAuditLog = 1UL << 7,
    PrioritySpeaker = 1UL << 8,
    Stream = 1UL << 9,
    ViewChannel = 1UL << 10,
    SendMessages = 1UL << 11,
    SendTTSMessages = 1UL << 12,
    ManageMessages = 1UL << 13,
    EmbedLinks = 1UL << 14,
    AttachFiles = 1UL << 15,
    ReadMessageHistory = 1UL << 16,
    MentionEveryone = 1UL << 17,
    UseExternalEmojis = 1UL << 18,
    ViewGuildInsights = 1UL << 19,
    Connect = 1UL << 20,
    Speak = 1UL << 21,
    MuteMembers = 1UL << 22,
    DeafenMembers = 1UL << 23,
    MoveMembers = 1UL << 24,
    UseVAD = 1UL << 25,
    ChangeNickname = 1UL << 26,
    ManageNicknames = 1UL << 27,
    ManageRoles = 1UL << 28,
    ManageWebhooks = 1UL << 29,
    ManageEmojisAndStickers = 1UL << 30,
    UseApplicationCommands = 1UL << 31,
    RequestToSpeak = 1UL << 32,
    ManageEvents = 1UL << 33,
    ManageThreads = 1UL << 34,
    CreatePublicThreads = 1UL << 35,
    CreatePrivateThreads = 1UL << 36,
    UseExternalStickers = 1UL << 37,
    SendMessagesInThreads = 1UL << 38,
    UseEmbeddedActivities = 1UL << 39,
    ModerateMembers = 1UL << 40,
    ViewCreatorMonetizationInsights = 1UL << 41,
    UseSoundboard = 1UL << 42,
    CreateGuildExpressions = 1UL << 43,
    UseExternalSounds = 1UL << 44,
    SendVoiceMessages = 1UL << 45
    // Remaining bits up to 53 can be added when new permissions are introduced
}

public static class DiscordPermissionHelper
{
    public static bool HasPermission(ulong permissions, DiscordPermissions check)
    {
        return (permissions & (ulong)check) == (ulong)check;
    }

    public static void PrintPermissions(ulong permissions)
    {
        foreach (DiscordPermissions perm in Enum.GetValues(typeof(DiscordPermissions)))
        {
            if (HasPermission(permissions, perm))
            {
                Console.WriteLine(perm);
            }
        }
    }
}
