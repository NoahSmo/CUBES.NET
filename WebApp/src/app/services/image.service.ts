import { Injectable } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  protected baseUrl = 'http://localhost:5072/api';
  protected componentUrl = this.baseUrl + '/Image';

  constructor(
    protected router: Router,
    protected http: HttpClient
  ) { }

  getImages(): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl, {params})
  }

    getImage(id: number): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl + '/' + id, {params})
  }
}
