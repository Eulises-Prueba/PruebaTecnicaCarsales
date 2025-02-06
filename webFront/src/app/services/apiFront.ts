import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Episodios } from "../models/episodios";
import { EpisodioDetalle } from "../models/episodioDetalle";
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiFront {
  private urlApi = environment.urlApiFront;

  constructor (private http: HttpClient) { }

  getListado(page: number): Observable<Episodios> {
    let queryParams = new HttpParams();
    if(page > 0) {
        queryParams = queryParams.set("page", page);
    }
    return this.http.get<Episodios>(`${this.urlApi}/episode`, { params: queryParams });
  }

  getDetalle(id: number): Observable<EpisodioDetalle> {
    return this.http.get<EpisodioDetalle>(`${this.urlApi}/episode/${id}`);
  }
}