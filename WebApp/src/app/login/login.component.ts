import {Component} from '@angular/core';
import {AuthentificationService} from "../services/authentification.service";
import {Router} from "@angular/router";
import {FormBuilder, FormGroup} from "@angular/forms";
import {User} from "../models/user";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  email = '';
  password = '';
  wrongCredentials = false;
  userFormGroup! : FormGroup;
  currentUser: User | undefined;

  constructor(
    private authService: AuthentificationService,
    private router: Router,
    private fb: FormBuilder
  ) {
  }

  ngOnInit() {
    this.userFormGroup = this.fb.group({
      email : this.fb.control(""),
      password : this.fb.control("")
    });

  }

  show: boolean = false;

  login() {
    this.wrongCredentials = false;
    this.email = this.userFormGroup.value.email;
    this.password = this.userFormGroup.value.password;
    this.authService.loginDesktop(this.email, this.password).subscribe(result => {
      this.router.navigate(['/']);

    }, error => {
      this.wrongCredentials = true;
    });
  }
}
