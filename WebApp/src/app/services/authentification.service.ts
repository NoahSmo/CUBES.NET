import {Injectable} from '@angular/core';
import {User} from "../models/user";
import {Observable} from "rxjs";
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {

  protected baseUrl = 'http://localhost:38387/api';
  protected componentUrl = this.baseUrl + '/Login';

  users: User[] = [];
  authenticatedUser: string | undefined;
  public currentUser!: User;
  private loggedIn = false;

  constructor(
    protected router: Router,
    protected http: HttpClient
  ) {
  }

  login(email: any, password: any){
    const loginData = {
      email: email,
      password: password
    }

    return new Observable<boolean>((observer)=>{
      this.http.post(this.componentUrl, loginData).subscribe(result => {
        observer.next(true);
        observer.complete();
      }, error => {
        observer.error(false);
        observer.complete();
        });
    });
  }



  // login(email: string, password: string): Observable<any> {
  //   const headers = new HttpHeaders().set('Content-Type', 'application/json');
  //   const body = {email1: email, password1: password};
  //   // return this.http.post(this.componentUrl, {credentials}, {headers, observe: 'response'});
  //   return this.http.post(this.componentUrl, body, {headers, observe: 'response'}).pipe(
  //     map((response: HttpResponse<User>) => {
  //       if (response.status !== 200 || response.ok !== true) {
  //         return false;
  //       }
  //       const responseUser: User | null = response.body;
  //       console.log('username', responseUser, email);
  //       if (responseUser?.email === email) {
  //         this.currentUser = responseUser;
  //         // this.token = response.headers.get('Token') ?? '';
  //
  //         // this.currentUser.token = this.token;
  //         // set token property
  //         // store username and jwt token in local storage to keep user logged in between page refreshes
  //         sessionStorage.setItem('currentUser', JSON.stringify(this.currentUser));
  //         sessionStorage.setItem('LastConnectionDate', JSON.stringify(new Date));
  //         this.loggedIn = true;
  //
  //         // return true to indicate successful login
  //         return true;
  //       } else {
  //         // return false to indicate failed login
  //         return false;
  //       }
  //     }),
  //     // @ts-ignore
  //     catchError((error: any) => error(error, 'login.msgs.error')));
  // }
  //
  // logout() {
  //   localStorage.removeItem('token');
  // }
  //
  // isAuthenticated(): boolean {
  //   const token = localStorage.getItem('token');
  //   return token !== null;
  // }


    // authenticated(cle: string): Observable<boolean> {
    //   this.authenticatedUser = cle;
    //   localStorage.setItem("authUser", this.authenticatedUser);
    //   return of(true);
    // }

    // isAuthenticated(){
    //   return this.authenticatedUser!=undefined;
    // }

    //
    // getAuthorizationHeader(): string {
    //   const token = localStorage.getItem('token');
    //   return `Bearer ${token}`;
    // }
  }
