
  export enum DiscordPermissions {
    CreateInstantInvite,
    KickMembers ,
    BanMembers,
    Administrator,
    ManageChannels,
    ManageGuild,
    AddReactions,
    ViewAuditLog,
    PrioritySpeaker,
    Stream,
    ViewChannel ,
    SendMessages ,
    SendTTSMessages ,
    ManageMessages ,
    EmbedLinks ,
    AttachFiles ,
    ReadMessageHistory ,
    MentionEveryone ,
    UseExternalEmojis ,
    ViewGuildInsights ,
    Connect ,
    Speak ,
    MuteMembers ,
    DeafenMembers ,
    MoveMembers ,
    UseVAD ,
    ChangeNickname ,
    ManageNicknames ,
    ManageRoles ,
    ManageWebhooks ,
    ManageEmojisAndStickers ,
    UseApplicationCommands ,
    RequestToSpeak ,
    ManageEvents ,
    ManageThreads ,
    CreatePublicThreads ,
    CreatePrivateThreads ,
    UseExternalStickers ,
    SendMessagesInThreads ,
    UseEmbeddedActivities ,
    ModerateMembers ,
    ViewCreatorMonetizationInsights ,
    UseSoundboard ,
    CreateGuildExpressions ,
    UseExternalSounds ,
    SendVoiceMessages ,
  }

  export class DiscordPermissionHelper {
    static hasPermission(permissions: number, check: DiscordPermissions): boolean {
      return (permissions & check) === check;
    }

    static printPermissions(permissions: number): void {
      Object.values(DiscordPermissions)
        .filter((value) => typeof value === 'number')
        .forEach((perm) => {
          if (this.hasPermission(permissions, perm as unknown as DiscordPermissions)) {
            console.log(DiscordPermissions[perm as unknown as keyof typeof DiscordPermissions]);
          }
        });
    }
  }
