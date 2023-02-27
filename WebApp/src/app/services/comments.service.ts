import { Injectable } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  protected baseUrl = 'http://localhost:5072/api';
  protected componentUrl = this.baseUrl + '/Comment';

  constructor(
    protected router: Router,
    protected http: HttpClient
  ) { }

  getComments(): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl, {params})
  }

  getComment(id: string | null): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl + '/' + id, {params})
  }

  addComment(comment: Comment): Observable<any>{
    return this.http.post(this.componentUrl, comment);
  }

  updateComment(comment: Comment): Observable<any>{
    return this.http.put(this.componentUrl, comment);
  }

  deleteComment(id: string | null): Observable<any>{
    return this.http.delete(this.componentUrl + '/' + id);
  }
}
