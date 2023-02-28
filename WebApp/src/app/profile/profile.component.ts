import {Component} from '@angular/core';
import {User} from "../models/user";
import {AuthentificationService} from "../services/authentification.service";
import {Router} from "@angular/router";
import {FormBuilder, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {

  email = '';
  password = '';
  name = '';
  surname = '';
  phone: number = 0;
  wrongCredentials = false;
  userFormGroup!: FormGroup;
  errorMessage!: string;
  public currentUser: any;

  constructor(
    private authService: AuthentificationService,
    private router: Router,
    private fb: FormBuilder
  ) {
  }

  ngOnInit() {
    this.currentUser = this.authService.User();

    this.userFormGroup = this.fb.group({
      email: this.fb.control(this.currentUser.email),
      name: this.fb.control(this.currentUser.name),
      surname: this.fb.control(this.currentUser.surname),
      phone: this.fb.control(this.currentUser.phone),
    });
  }

  editProfile() {
    this.name = this.userFormGroup.value.name;
    this.surname = this.userFormGroup.value.surname;
    this.email = this.userFormGroup.value.email;
    this.phone = this.userFormGroup.value.phone;

    this.authService.editProfile(this.name, this.surname, this.phone, this.email).subscribe(result => {
      this.router.navigate(['']);
    }, error => {
      if (error.status == 401) {
        error.statusText = "Impossible de vous enregistrer, veuillez recommencer";
      }
      this.errorMessage = error.statusText;
    });

  }


}
