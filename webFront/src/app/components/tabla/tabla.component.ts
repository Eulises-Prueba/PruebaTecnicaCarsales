import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Episodios } from '../../models/episodios';
import { HighlightDirective } from '../../directives/highlight.directive';

@Component({
  selector: 'app-tabla',
  imports: [ RouterLink, HighlightDirective ],
  templateUrl: './tabla.component.html',
  styleUrl: './tabla.component.css'
})
export class TablaComponent {
  listado = input.required<Episodios>();
}
