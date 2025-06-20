import {
  Component,
  inject,
  OnInit
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GuildConfigurationHttpService } from '../../services/http-clients/guild-configuration-http.service';
import {
  dispatch,
  select
} from '@ngxs/store';
import {
  GuildChosen,
  LoadGuilds
} from '../../state/root.actions';
import { RootState } from '../../state/root.state';
import { Navigate } from '@ngxs/router-plugin';

@Component({
  selector: 'app-return',
  imports: [],
  templateUrl: './return.component.html',
  styleUrl: './return.component.scss'
})
export class ReturnComponent implements OnInit {
  private route= inject(ActivatedRoute);
  private guildConfigurationService = inject(GuildConfigurationHttpService);
  private loadGuilds = dispatch(LoadGuilds);
  private chooseGuild = dispatch(GuildChosen);
  private guilds = select(RootState.guilds);
  private navigateTo = dispatch(Navigate);

  ngOnInit() {
    const guildId = this.route.snapshot.queryParamMap.get('guild_id');
    if(guildId) {
      this.guildConfigurationService.saveGuildConfiguration(guildId).subscribe(() => {
        this.loadGuilds().subscribe(() => {
          const guilds = this.guilds();
          const chosenGuild = guilds.find(g => g.id === guildId);
          if (chosenGuild) {
            this.chooseGuild(chosenGuild).subscribe(() => {
              this.navigateTo(['/manage', guildId]);
            });
          } else {
            console.error('Chosen guild not found in the list of guilds.');
            this.navigateTo(['/dashboard']);
          }
        })
      })
    }
  }
}
