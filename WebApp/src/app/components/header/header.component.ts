import { Component } from '@angular/core';
import {AuthentificationService} from "../../services/authentification.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

  isAuth: boolean = false;

  constructor(
    private authService: AuthentificationService,
    private router: Router
  ) {
  }

  ngOnInit(){
    this.canDisplayLogin();
  }

  canDisplayLogin(): boolean{
    if (this.authService.isAuthenticated()){
      this.isAuth = true;
    }
    return this.isAuth;
  }

  logout(){
    this.authService.logout();
  }

}
