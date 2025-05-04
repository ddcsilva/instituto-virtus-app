import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { VirtusLogoComponent } from '../../shared/ui/virtus-logo.component';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [MatToolbarModule, VirtusLogoComponent],
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FooterComponent {
  get anoAtual(): number {
    return new Date().getFullYear();
  }
}
