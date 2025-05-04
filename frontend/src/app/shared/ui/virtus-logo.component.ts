import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-virtus-logo',
  standalone: true,
  imports: [NgClass],
  template: `
    <!-- Placeholder para o logo futuro -->
    <div
      class="virtus-logo-placeholder"
      [ngClass]="{ inverted: inverted }"
      [style.width.px]="size"
      [style.height.px]="size"
    ></div>
  `,
  styles: [
    `
      .virtus-logo-placeholder {
        display: inline-block;
        /* Sem conteúdo visual, apenas estrutura */
      }

      .inverted {
        /* Para manter a estrutura do código para estilo futuro */
      }
    `,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class VirtusLogoComponent {
  @Input() size = 24;
  @Input() inverted = false;
}
