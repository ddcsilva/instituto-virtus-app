import { Component, ChangeDetectionStrategy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../core/auth/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from '../../../shared/services/notification.service';

@Component({
  selector: 'app-registro-page',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    RouterModule,
  ],
  templateUrl: './registro-page.component.html',
  styleUrl: './registro-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegistroPageComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private notificationService = inject(NotificationService);

  esconderSenha = true;
  currentYear = new Date().getFullYear();
  carregando = false;

  formulario = this.fb.group({
    nome: ['', [Validators.required, Validators.minLength(3)]],
    email: ['', [Validators.required, Validators.email]],
    senha: ['', [Validators.required, Validators.minLength(6)]],
  });

  async registrar(): Promise<void> {
    if (this.formulario.invalid) {
      this.notificationService.error(
        'Por favor, preencha todos os campos corretamente.'
      );
      return;
    }

    this.carregando = true;

    try {
      const { nome, email, senha } = this.formulario.value;
      await this.authService.registrar(email!, senha!, nome!);
      this.notificationService.success('Cadastro realizado com sucesso!');
      this.router.navigateByUrl('/');
    } catch (error: any) {
      console.error('Erro ao registrar:', error);

      let mensagem = 'Erro ao registrar usuário.';
      if (error.code === 'auth/email-already-in-use') {
        mensagem = 'Este e-mail já está em uso.';
      } else if (error.code === 'auth/weak-password') {
        mensagem = 'A senha deve ter pelo menos 6 caracteres.';
      }

      this.notificationService.error(mensagem);
    } finally {
      this.carregando = false;
    }
  }
}
