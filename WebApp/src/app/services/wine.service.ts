import { Injectable } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Article} from "../models/article";

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

  getArticle(id: number): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl + '/' + id, {params})
  }

  addArticle(article: Article): Observable<any>{
    return this.http.post(this.componentUrl, article);
  }

  updateArticle(article: Article): Observable<any>{
    return this.http.put(this.componentUrl, article);
  }

  deleteArticle(id: string | null): Observable<any>{
    return this.http.delete(this.componentUrl + '/' + id);
  }
}
