import { Routes } from '@angular/router';
import { LoginPageComponent } from './features/auth/pages/login-page.component';
import { LayoutComponent } from './layout/layout.component';
import { RegistroPageComponent } from './features/auth/pages/registro-page.component';
import { authGuard, authLoginGuard } from './core/auth/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginPageComponent,
    canActivate: [authLoginGuard],
  },
  {
    path: 'cadastro',
    component: RegistroPageComponent,
    canActivate: [authLoginGuard],
  },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    loadChildren: () =>
      import('./features/dashboard/dashboard.routes').then((m) => m.routes),
  },
  {
    path: '**',
    redirectTo: '',
  },
];
