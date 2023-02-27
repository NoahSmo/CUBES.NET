import { Injectable } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Cart} from "../models/cart";

@Injectable({
  providedIn: 'root'
})
export class CartService {

  protected baseUrl = 'http://localhost:5072/api';
  protected componentUrl = this.baseUrl + '/Cart';

  constructor(
    protected router: Router,
    protected http: HttpClient
  ) {
  }

  getCarts(): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl, {params})
  }

  getCart(id: number): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl + '/' + id, {params})
  }

  addCart(cart: Cart): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.post(this.componentUrl, cart, {params})
  }

  updateCart(cart: Cart): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.put(this.componentUrl + '/' + cart.id, cart, {params})
  }

  deleteCart(id: number): Observable<any>{
    const params: HttpParams = new HttpParams();
    return this.http.delete(this.componentUrl + '/' + id, {params})
  }

}
