import { Injectable, inject } from '@angular/core';
import {
  Auth,
  signInWithEmailAndPassword,
  signOut,
  onAuthStateChanged,
  User,
} from '@angular/fire/auth';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private auth = inject(Auth);
  private router = inject(Router);
  private usuarioSubject = new BehaviorSubject<User | null>(null);
  private carregandoSubject = new BehaviorSubject<boolean>(true);

  public usuario$ = this.usuarioSubject.asObservable();
  public carregando$ = this.carregandoSubject.asObservable();

  constructor() {
    onAuthStateChanged(this.auth, async (user) => {
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
    await signInWithEmailAndPassword(this.auth, email, senha);
  }

  async sair(): Promise<void> {
    await signOut(this.auth);
    localStorage.removeItem('idToken');
    this.router.navigateByUrl('/login');
  }

  get token(): string | null {
    return localStorage.getItem('idToken');
  }
}
