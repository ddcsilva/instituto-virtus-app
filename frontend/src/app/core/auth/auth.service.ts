import { Injectable, inject } from '@angular/core';
import {
  Auth,
  signInWithEmailAndPassword,
  signOut,
  onAuthStateChanged,
  User,
  createUserWithEmailAndPassword,
  updateProfile,
} from '@angular/fire/auth';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { NotificationService } from '../../shared/services/notification.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private auth: Auth = inject(Auth);
  private router = inject(Router);
  private notificationService = inject(NotificationService);
  private usuarioSubject = new BehaviorSubject<User | null>(null);
  private carregandoSubject = new BehaviorSubject<boolean>(true);

  public usuario$ = this.usuarioSubject.asObservable();
  public carregando$ = this.carregandoSubject.asObservable();

  constructor() {
    onAuthStateChanged(this.auth, async (user) => {
      console.log('Estado de autenticação alterado:', user?.email);
      this.usuarioSubject.next(user);
      this.carregandoSubject.next(false);

      if (user) {
        const token = await user.getIdToken();
        localStorage.setItem('idToken', token);
      } else {
        localStorage.removeItem('idToken');
      }
    });
  }

  async entrar(email: string, senha: string): Promise<void> {
    try {
      await signInWithEmailAndPassword(this.auth, email, senha);
    } catch (error) {
      console.error('Erro ao fazer login:', error);
      throw error;
    }
  }

  async sair(): Promise<void> {
    try {
      await signOut(this.auth);
      localStorage.removeItem('idToken');
      this.notificationService.success('Você saiu com sucesso. Até logo!');
      this.router.navigateByUrl('/login');
    } catch (error) {
      console.error('Erro ao fazer logout:', error);
      throw error;
    }
  }

  async registrar(email: string, senha: string, nome: string): Promise<void> {
    try {
      const cred = await createUserWithEmailAndPassword(
        this.auth,
        email,
        senha
      );

      await updateProfile(cred.user, { displayName: nome });
      await cred.user.reload();

      const novoToken = await cred.user.getIdToken(true);
      localStorage.setItem('idToken', novoToken);

      this.usuarioSubject.next(cred.user);
      this.notificationService.success(`Cadastro realizado com sucesso!`);
    } catch (error) {
      console.error('Erro ao registrar:', error);
      this.notificationService.error('Erro ao registrar usuário.');
      throw error;
    }
  }

  get token(): string | null {
    return localStorage.getItem('idToken');
  }
}
