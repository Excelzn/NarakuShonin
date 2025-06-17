import { DiscordGuildLite } from '../models/discord-models/DiscordGuildLite';

export class LoadUserData {
  static readonly type = '[Root] Load User Data';
}

export class LoadGuilds {
  static readonly type = '[Root] Load Guilds';
}

export class GuildChosen {
  static readonly type = '[Root] Guild Chosen';
  constructor(public guild: DiscordGuildLite|null) {}
}
