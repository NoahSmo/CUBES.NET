import {Component} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {User} from "../models/user";
import {Router} from "@angular/router";
import {AuthentificationService} from "../services/authentification.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  email = '';
  password = '';
  name = '';
  surname = '';
  phone: number = 0;
  wrongCredentials = false;
  userFormGroup!: FormGroup;
  currentUser: User | undefined;
  errorMessage!: string;

  constructor(
    private authService: AuthentificationService,
    private router: Router,
    private fb: FormBuilder
  ) {
  }

  ngOnInit() {
    this.userFormGroup = this.fb.group({
      email: this.fb.control(""),
      password: this.fb.control(""),
      name: this.fb.control(""),
      surname: this.fb.control(""),
      phone: this.fb.control(0),

    });
  }

  register() {
    this.name = this.userFormGroup.value.name;
    this.surname = this.userFormGroup.value.surname;
    this.email = this.userFormGroup.value.email;
    this.phone = this.userFormGroup.value.phone;
    this.password = this.userFormGroup.value.password;

    this.authService.register(this.name, this.surname, this.phone, this.email, this.password).subscribe(result => {
      this.router.navigate(['/login']);
    }, error => {
      if (error.status == 401) {
        error.statusText = "Impossible de vous enregistrer, veuillez recommencer";
      }
      this.errorMessage = error.statusText;
    });

  }

}
