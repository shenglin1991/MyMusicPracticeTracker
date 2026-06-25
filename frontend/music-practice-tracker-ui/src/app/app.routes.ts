import { Routes } from '@angular/router';
import { DashboardPage } from './pages/dashboard-page/dashboard-page';
import { InstrumentsPage } from './pages/instruments-page/instruments-page';
import { SessionsPage } from './pages/sessions-page/sessions-page';
import { SettingsPage } from './pages/settings-page/settings-page';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'dashboard' },
  { path: 'dashboard', component: DashboardPage },
  { path: 'instruments', component: InstrumentsPage },
  { path: 'sessions', component: SessionsPage },
  { path: 'settings', component: SettingsPage },
  { path: '**', redirectTo: 'dashboard' },
];
