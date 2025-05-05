import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../core/auth/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from '../../../shared/services/notification.service';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    RouterModule,
  ],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginPageComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private notificationService = inject(NotificationService);

  esconderSenha = true;

  formulario = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    senha: ['', [Validators.required]],
  });

  async entrar(): Promise<void> {
    if (this.formulario.invalid) {
      if (this.formulario.get('email')?.hasError('required')) {
        this.notificationService.error('E-mail é obrigatório');
      } else if (this.formulario.get('email')?.hasError('email')) {
        this.notificationService.error('E-mail inválido');
      } else if (this.formulario.get('senha')?.hasError('required')) {
        this.notificationService.error('Senha é obrigatória');
      } else {
        this.notificationService.error(
          'Por favor, preencha todos os campos corretamente'
        );
      }
      return;
    }

    const { email, senha } = this.formulario.value;
    try {
      await this.authService.entrar(email!, senha!);
      this.router.navigateByUrl('/');
    } catch (error) {
      console.error('Erro ao logar:', error);
      this.notificationService.error('Usuário ou senha incorretos.');
    }
  }

  get currentYear(): number {
    return new Date().getFullYear();
  }
}
