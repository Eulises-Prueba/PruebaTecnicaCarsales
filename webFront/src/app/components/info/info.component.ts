import { Component, input } from '@angular/core';
import { EpisodioDetalle } from '../../models/episodioDetalle';

@Component({
  selector: 'app-info',
  imports: [],
  templateUrl: './info.component.html',
  styleUrl: './info.component.css'
})
export class InfoComponent {
  detalle = input.required<EpisodioDetalle>();
}
