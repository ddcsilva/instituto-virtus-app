import { Component, ChangeDetectionStrategy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/auth/auth.service';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [CommonModule, MatMenuModule, MatButtonModule, MatIconModule],
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UsuarioComponent {
  authService = inject(AuthService);

  async sair(): Promise<void> {
    await this.authService.sair();
  }
}
