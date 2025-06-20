import { DiscordPermissions } from './Permissions';

export interface DiscordGuildLite {
  id: string;
  name: string;
  icon: string;
  banner: string;
  owner: boolean;
  permissions: DiscordPermissions[];
  features: string[];
  approximateMemberCount: number;
  approximatePresenceCount: number;
  registeredWithBot: boolean;
}
