import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../../../core/auth/auth.service';
import { Router } from '@angular/router';
import { VirtusLogoComponent } from '../../../shared/ui/virtus-logo.component';

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
    VirtusLogoComponent,
  ],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginPageComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  esconderSenha = true;

  formulario = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    senha: ['', [Validators.required]],
  });

  async entrar(): Promise<void> {
    if (this.formulario.invalid) return;

    const { email, senha } = this.formulario.value;
    try {
      await this.authService.entrar(email!, senha!);
      this.router.navigateByUrl('/');
    } catch (error) {
      console.error('Erro ao logar:', error);
    }
  }

  get currentYear(): number {
    return new Date().getFullYear();
  }
}
