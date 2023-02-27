import { Injectable } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  protected baseUrl = 'http://localhost:5072/api';
  protected componentUrl = this.baseUrl + '/Category';

  constructor(
    protected router: Router,
    protected http: HttpClient
  ) { }

  getCategories(): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl, {params})
  }

  getCategory(id: number): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl + '/' + id, {params})
  }
}
