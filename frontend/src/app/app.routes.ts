import { Routes } from '@angular/router';
import { LoginPageComponent } from './features/auth/pages/login-page.component';
import { LayoutComponent } from './layout/layout.component';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginPageComponent,
  },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [
      () => import('./core/auth/auth.guard').then((m) => m.authGuard),
    ],
    loadChildren: () =>
      import('./features/dashboard/dashboard.routes').then((m) => m.routes),
  },
  {
    path: '**',
    redirectTo: 'login',
  },
];
