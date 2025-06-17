import { Injectable } from '@angular/core';
import {
  Action,
  Selector,
  State,
  StateContext
} from '@ngxs/store';
import { UserHttpService } from '../services/http-clients/user-http.service';
import {
  GuildChosen,
  LoadGuilds,
  LoadUserData
} from './root.actions';
import {
  catchError,
  of,
  tap
} from 'rxjs';
import { DiscordGuildLite } from '../models/discord-models/DiscordGuildLite';
import { DiscordHttpService } from '../services/http-clients/discord-http.service';
import { DiscordPermissions } from '../models/discord-models/Permissions';

export interface IRootState {
  userId: string;
  userName: string;
  userAvatarHash: string;
  isLoggedIn: boolean;
  guilds: DiscordGuildLite[];
  chosenGuild: DiscordGuildLite|null;
}

@State<IRootState>({
  name: 'root',
  defaults: {
    userId: '',
    userName: '',
    userAvatarHash: '',
    isLoggedIn: false,
    guilds: [],
    chosenGuild: null
  }
})
@Injectable()
export class RootState {
  constructor(private userService: UserHttpService, private discordService: DiscordHttpService) { }

  @Selector()
  static userName(state: IRootState): string {
    return state.userName;
  }
  @Selector()
  static userAvatar(state: IRootState): string {
    return `https://cdn.discordapp.com/avatars/${state.userId}/${state.userAvatarHash}.png`;
  }
  @Selector()
  static isLoggedIn(state: IRootState): boolean {
    return state.isLoggedIn;
  }
  @Selector()
  static guilds(state: IRootState): DiscordGuildLite[] {
    return state.guilds.filter(x => x.permissions.includes(DiscordPermissions.Administrator));
  }
  @Selector()
  static hasChosenGuild(state: IRootState): boolean {
    return state.chosenGuild !== null;
  }
  @Selector()
  static chosenGuild(state: IRootState): DiscordGuildLite|null {
    return state.chosenGuild;
  }

  // You can add actions and selectors here if needed

  @Action(LoadUserData)
  loadUserData(ctx: StateContext<IRootState>) {
    return this.userService.getUserInfo().pipe(
      catchError((err) => {
        if(err.status !== 401) {
          console.log(err);
        }
        return of([])
      }),
      tap((userClaims) => {
        let avatarHash = '';
        const hashClaim = userClaims
          .find(c => c.type === 'urn:discord:avatar:hash');
        if (hashClaim) {
          avatarHash = hashClaim.value;
        }
        let userId = '';
        const nameClaim = userClaims
          .find(c => c.type === 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier');
        if (nameClaim) {
          userId = nameClaim.value;
        }
        let userName = '';
        const nameClaim2 = userClaims
          .find(c => c.type === 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name');
        if (nameClaim2) {
          userName = nameClaim2.value;
        }
        ctx.patchState({
          userId: userId,
          userName: userName,
          userAvatarHash: avatarHash,
          isLoggedIn: userId !== '' && avatarHash !== ''
        });
      })
    );
  }

  @Action(LoadGuilds)
  loadGuilds(ctx: StateContext<IRootState>) {
    return this.discordService.getGuilds().pipe(
      tap((guilds) => {
        ctx.patchState({
          guilds: guilds,
          chosenGuild: null
        });
      })
    );
  }

  @Action(GuildChosen)
  guildChosen(ctx: StateContext<IRootState>, action: GuildChosen) {
    ctx.patchState({
      chosenGuild: action.guild
    });
  }
}
