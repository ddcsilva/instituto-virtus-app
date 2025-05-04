import { Component, inject } from '@angular/core';
import { AsyncPipe, NgIf } from '@angular/common';
import { AuthService } from './core/auth/auth.service';
import { RouterOutlet } from '@angular/router';
import { CarregandoComponent } from './shared/ui/carregando.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgIf, AsyncPipe, CarregandoComponent],
  template: `
    <app-carregando *ngIf="authService.carregando$ | async" />
    <router-outlet *ngIf="!(authService.carregando$ | async)" />
  `,
})
export class AppComponent {
  authService = inject(AuthService);
}
