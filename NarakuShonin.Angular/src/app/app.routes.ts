import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ReturnComponent } from './pages/return/return.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', component: HomeComponent },
  { path: 'return', component: ReturnComponent },
  { path: 'dashboard', loadChildren: () => import('./pages/dashboard/dashboard.routes').then(m => m.DASHBOARD_ROUTES) },
  { path: 'manage/:guildId', loadChildren: () => import('./pages/manage/manage.routes').then(m => m.MANAGE_ROUTES) },
];
