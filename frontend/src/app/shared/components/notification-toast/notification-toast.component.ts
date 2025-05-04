import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { animate, style, transition, trigger } from '@angular/animations';

export type NotificationType = 'success' | 'error' | 'info';

@Component({
  selector: 'app-notification-toast',
  standalone: true,
  imports: [CommonModule, MatIconModule],
  template: `
    <div
      *ngIf="visible"
      class="notification-toast"
      [@fadeSlideInOut]
      [class.success]="type === 'success'"
      [class.error]="type === 'error'"
      [class.info]="type === 'info'"
      role="alert"
      [attr.aria-live]="'polite'"
    >
      <mat-icon class="notification-icon">
        {{ getIcon() }}
      </mat-icon>
      <span class="notification-message">{{ message }}</span>
    </div>
  `,
  styles: [
    `
      .notification-toast {
        position: fixed;
        bottom: 24px;
        right: 24px;
        padding: 16px 24px;
        border-radius: 8px;
        display: flex;
        align-items: center;
        gap: 12px;
        min-width: 300px;
        max-width: 400px;
        box-shadow: var(--sombra-media);
        z-index: var(--z-flutuante);
        color: var(--virtus-preto);
      }

      .notification-icon {
        font-size: 24px;
        width: 24px;
        height: 24px;
      }

      .notification-message {
        font-size: 14px;
        line-height: 1.4;
      }

      .success {
        background-color: var(--virtus-amarelo);
      }

      .error {
        background-color: var(--virtus-vermelho);
        color: white;
      }

      .info {
        background-color: var(--virtus-cinza-claro);
      }

      @media (max-width: 768px) {
        .notification-toast {
          left: 16px;
          right: 16px;
          bottom: 16px;
          min-width: auto;
        }
      }
    `,
  ],
  animations: [
    trigger('fadeSlideInOut', [
      transition(':enter', [
        style({ transform: 'translateY(100%)', opacity: 0 }),
        animate(
          '300ms ease-out',
          style({ transform: 'translateY(0)', opacity: 1 })
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ transform: 'translateY(100%)', opacity: 0 })
        ),
      ]),
    ]),
  ],
})
export class NotificationToastComponent implements OnInit, OnDestroy {
  @Input() message = '';
  @Input() type: NotificationType = 'info';
  @Input() duration = 3000;

  visible = false;
  private timeoutId?: number;

  ngOnInit() {
    this.show();
  }

  ngOnDestroy() {
    if (this.timeoutId) {
      window.clearTimeout(this.timeoutId);
    }
  }

  show() {
    this.visible = true;
    if (this.duration > 0) {
      this.timeoutId = window.setTimeout(() => {
        this.visible = false;
      }, this.duration);
    }
  }

  getIcon(): string {
    switch (this.type) {
      case 'success':
        return 'check_circle';
      case 'error':
        return 'error';
      case 'info':
      default:
        return 'info';
    }
  }
}
