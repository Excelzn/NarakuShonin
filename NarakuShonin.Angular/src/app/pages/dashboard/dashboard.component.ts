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
import { NzImageViewComponent } from 'ng-zorro-antd/experimental/image';
import { NzImageModule } from 'ng-zorro-antd/image';
import { DiscordGuildLite } from '../../models/discord-models/DiscordGuildLite';
import { Navigate } from '@ngxs/router-plugin';

@Component({
  selector: 'app-dashboard',
  imports: [
    NzRowDirective,
    NzColDirective,
    NzCardComponent,
    NzImageModule,
    NzCardMetaComponent
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
    this.loadGuilds();
  }

  guildClicked(guild: DiscordGuildLite) {
    this.chooseGuild(guild);
    this.navigateTo(['/manage', guild.id])
  }
}
