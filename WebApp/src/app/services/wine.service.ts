import { Injectable } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class WineService {

  protected baseUrl = 'http://localhost:38387/api';
  protected componentUrl = this.baseUrl + '/Article';

  constructor(
    protected router: Router,
    protected http: HttpClient
  ) {
  }

  getArticles(): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl, {params})
  }

}
