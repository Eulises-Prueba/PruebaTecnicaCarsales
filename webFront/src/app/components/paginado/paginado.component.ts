import { Component, input, output } from '@angular/core';
import { PaginaDirective } from '../../directives/pagina.directive';

@Component({
  selector: 'app-paginado',
  imports: [ PaginaDirective ],
  templateUrl: './paginado.component.html'
})
export class PaginadoComponent {
  totalPaginas = input.required<number>();
  change = output<any>();

  changePage(page: number) {
    this.change.emit(page);
  }
}
