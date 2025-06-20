import {
  Component,
  OnInit
} from '@angular/core';
import {
  dispatch,
  select
} from '@ngxs/store';
import {
  GuildChosen,
  LoadGuilds
} from '../../state/root.actions';
import { RootState } from '../../state/root.state';
import {
  NzColDirective,
  NzRowDirective
} from 'ng-zorro-antd/grid';
import {
  NzCardComponent,
  NzCardMetaComponent
} from 'ng-zorro-antd/card';
import { NzImageModule } from 'ng-zorro-antd/image';
import { DiscordGuildLite } from '../../models/discord-models/DiscordGuildLite';
import { Navigate } from '@ngxs/router-plugin';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { NzIconDirective } from 'ng-zorro-antd/icon';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-dashboard',
  imports: [
    NzRowDirective,
    NzColDirective,
    NzCardComponent,
    NzImageModule,
    NzCardMetaComponent,
    NzButtonComponent,
    NzIconDirective
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  loadGuilds = dispatch(LoadGuilds);
  chooseGuild = dispatch(GuildChosen)
  navigateTo = dispatch(Navigate);
  guilds = select(RootState.guilds);

  ngOnInit() {
    if(this.guilds().length === 0) {
      this.loadGuilds();
    }
  }

  guildClicked(guild: DiscordGuildLite) {
    if(!guild.registeredWithBot) {
      window.location.href = `${environment.DiscordInviteConfig.Authority}/oauth2/authorize?` +
        `client_id=${environment.DiscordInviteConfig.ClientId}&` +
        `permissions=${environment.DiscordInviteConfig.Permissions}` +
        `&scope=${environment.DiscordInviteConfig.Scopes}&` +
        `guild_id=${guild.id}&` +
        `redirect_uri=${window.location.origin}${environment.DiscordInviteConfig.RedirectUri}&` +
        `response_type=code`;
    } else {
      this.chooseGuild(guild).subscribe(() => {
        this.navigateTo(['/manage', guild.id]);
      });
    }

  }
}
