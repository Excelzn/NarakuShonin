import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class GuildConfigurationHttpService {
  constructor(private http: HttpClient) {}

  saveGuildConfiguration(guildId: string) {
    return this.http.post(`api/guildconfiguration`, {id: guildId});
  }


}
