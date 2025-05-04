import { Component, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-carregando',
  standalone: true,
  imports: [CommonModule, MatProgressSpinnerModule],
  templateUrl: './carregando.component.html',
  styleUrls: ['./carregando.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CarregandoComponent {}
