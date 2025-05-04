import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { filter, firstValueFrom, map } from 'rxjs';

export const authGuard: CanActivateFn = async () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  try {
    await firstValueFrom(authService.carregando$.pipe(filter((c) => !c)));

    const usuario = await firstValueFrom(authService.usuario$);

    if (!usuario) {
      router.navigateByUrl('/login');
      return false;
    }

    return true;
  } catch (error) {
    console.error('Erro ao verificar autenticação:', error);
    router.navigateByUrl('/login');
    return false;
  }
};
