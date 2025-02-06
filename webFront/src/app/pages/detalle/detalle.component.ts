import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ApiFront } from '../../services/apiFront';
import { InfoComponent } from "../../components/info/info.component";

@Component({
  selector: 'app-detalle',
  imports: [ RouterLink, InfoComponent ],
  templateUrl: './detalle.component.html',
  styleUrl: './detalle.component.css'
})
export class DetalleComponent implements OnInit {
  id: number = 0;
  item: any; 

  constructor(
    private route: ActivatedRoute,
    private apiFront: ApiFront
  ) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.apiFront.getDetalle(this.id).subscribe({
        next: (data: any) => {
          if(data != undefined) {
            this.item = data;
            this.item.created = this.getFormatedDate(data.created, "dd-MM-yyyy hh:mm");
          }
        },
        error: (e) => console.error(e)
      });
    });
  }

  private getFormatedDate(date: Date, format: string) {
    const datePipe = new DatePipe('en-US');
    return datePipe.transform(date, format);
  }
}
