import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Episodios } from "../models/episodios";
import { EpisodioDetalle } from "../models/episodioDetalle";

@Injectable({
  providedIn: 'root',
})
export class ApiFront {
  constructor (private http: HttpClient) { }

  getListado(page: number): Observable<Episodios> {
    let queryParams = new HttpParams();
    if(page > 0) {
        queryParams = queryParams.set("page", page);
    }
    return this.http.get<Episodios>('api/episode', { params: queryParams });
  }

  getDetalle(id: number): Observable<EpisodioDetalle> {
    let queryParams = new HttpParams();
    return this.http.get<EpisodioDetalle>(`api/episode/${id}`);
  }
}