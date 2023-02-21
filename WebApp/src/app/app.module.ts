import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {NavbarComponent} from './components/navbar/navbar.component';
import {HeaderComponent} from './components/header/header.component';
import {HomeComponent} from './home/home.component';
import {ButtonModule} from 'primeng/button';
import {FooterComponent} from './components/footer/footer.component';
import {LoginComponent} from './login/login.component';
import {MenuModule} from "primeng/menu";
import {RegisterComponent} from './register/register.component';
import {CarouselModule} from "primeng/carousel";
import {MenubarModule} from 'primeng/menubar';
import {InputTextModule} from "primeng/inputtext";
import {CarouselHomeComponent} from './carousel-home/carousel-home.component';
import {CardModule} from "primeng/card";
import {WineService} from "./services/wine.service";
import {HttpClient, HttpClientModule} from "@angular/common/http";


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HeaderComponent,
    HomeComponent,
    FooterComponent,
    LoginComponent,
    RegisterComponent,
    CarouselHomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ButtonModule,
    CarouselModule,
    MenubarModule,
    InputTextModule,
    CardModule,
    HttpClientModule
  ],
  providers: [WineService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
