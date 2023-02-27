import {Component} from '@angular/core';
import {User} from "../models/user";
import {AuthentificationService} from "../services/authentification.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {

  public currentUser: any;

  constructor(
    private authService: AuthentificationService,
    private router: Router
  ) {
  }

  ngOnInit() {
    this.currentUser = this.authService.User();
  }

  // public getSessionStorageUserInfo(){
  //   const value = sessionStorage.getItem("currentUser");
  //   const objet =
  //   return value;
  // }

}
