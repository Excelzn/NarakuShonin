import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DiscordGuildLite } from '../../models/discord-models/DiscordGuildLite';

@Injectable({ providedIn: 'root' })
export class DiscordHttpService {
  constructor(private http: HttpClient) {}

  getGuilds() {
    return this.http.get<DiscordGuildLite[]>('/api/discord/guilds');
  }
}
