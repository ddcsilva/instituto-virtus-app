import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { filter, firstValueFrom } from 'rxjs';

export const authGuard: CanActivateFn = async () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  await firstValueFrom(authService.carregando$.pipe(filter((c) => !c)));

  const token = authService.token;

  if (!token) {
    router.navigateByUrl('/login');
    return false;
  }

  return true;
};
