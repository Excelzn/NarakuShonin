import {
  Component,
  OnInit
} from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import {
  createDispatchMap,
  createSelectMap,
  dispatch
} from '@ngxs/store';
import { LoadUserData } from './state/root.actions';
import { RootState } from './state/root.state';
import { NzAvatarComponent } from 'ng-zorro-antd/avatar';

@Component({
  selector: 'app-root',
  imports: [RouterLink, RouterOutlet, NzIconModule, NzLayoutModule, NzMenuModule, NzAvatarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  isCollapsed = false;
  loadUserData = dispatch(LoadUserData);
  selectors = createSelectMap({
    userName: RootState.userName,
    userAvatar: RootState.userAvatar,
    isLoggedIn: RootState.isLoggedIn
  })

  ngOnInit() {
    this.loadUserData();
  }
}
