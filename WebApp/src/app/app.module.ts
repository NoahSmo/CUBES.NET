import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './home/home.component';
import {ButtonModule} from 'primeng/button';
import { FooterComponent } from './components/footer/footer.component';
import { LoginComponent } from './login/login.component';
import {MenuModule} from "primeng/menu";
import { RegisterComponent } from './register/register.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HeaderComponent,
    HomeComponent,
    FooterComponent,
    LoginComponent,
    RegisterComponent,
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        ButtonModule,
        MenuModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
