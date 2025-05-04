import { Injectable, inject } from '@angular/core';
import {
  NotificationToastComponent,
  NotificationType,
} from '../components/notification-toast/notification-toast.component';
import {
  ComponentRef,
  createComponent,
  ApplicationRef,
  Injector,
  Type,
} from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private appRef = inject(ApplicationRef);
  private injector = inject(Injector);

  show(message: string, type: NotificationType = 'info', duration = 3000) {
    const componentRef = this.createComponent(NotificationToastComponent);

    componentRef.instance.message = message;
    componentRef.instance.type = type;
    componentRef.instance.duration = duration;

    // Adiciona o componente ao DOM
    const element = componentRef.location.nativeElement;
    document.body.appendChild(element);

    // Atualiza a view
    this.appRef.attachView(componentRef.hostView);

    // Remove o componente após a duração especificada
    setTimeout(() => {
      this.appRef.detachView(componentRef.hostView);
      element.remove();
      componentRef.destroy();
    }, duration + 300); // Adiciona 300ms para a animação de saída
  }

  private createComponent<T>(component: Type<T>): ComponentRef<T> {
    return createComponent(component, {
      environmentInjector: this.appRef.injector,
      elementInjector: this.injector,
    });
  }

  success(message: string, duration = 3000) {
    this.show(message, 'success', duration);
  }

  error(message: string, duration = 3000) {
    this.show(message, 'error', duration);
  }

  info(message: string, duration = 3000) {
    this.show(message, 'info', duration);
  }
}
