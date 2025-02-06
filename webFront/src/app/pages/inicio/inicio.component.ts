import { Component, OnInit } from '@angular/core';
import { ApiFront } from '../../services/apiFront';
import { CommonModule } from '@angular/common';
import { TablaComponent } from '../../components/tabla/tabla.component';
import { PaginadoComponent } from '../../components/paginado/paginado.component';

@Component({
  selector: 'app-inicio',
  imports: [ CommonModule, TablaComponent, PaginadoComponent ],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})
export class InicioComponent implements OnInit {
  paginas: number = 0;
  episodios: any;

  constructor(private apiFront: ApiFront) { }

  ngOnInit(): void {
    this.loadPage(0);
  }

  onPageChange(pagina: number) {
    this.loadPage(pagina);
  }

  private loadPage(page: number) {
    this.apiFront.getListado(page).subscribe({
      next: (data: any) => {
        this.episodios = data;
        if(data != undefined) {
          this.paginas = data.info.pages;
        }
      },
      error: (e) => console.error(e)
    });
  }
}
