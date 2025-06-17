import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserClaim } from '../../models/user-models/UserClaim';

@Injectable({ providedIn: 'root' })
export class UserHttpService {
  constructor(private httpService: HttpClient) { }

  getUserInfo() {
    return this.httpService.get<UserClaim[]>('/api/user');
  }
}
