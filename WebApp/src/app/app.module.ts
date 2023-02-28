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
import {RegisterComponent} from './register/register.component';
import {CarouselModule} from "primeng/carousel";
import {MenubarModule} from 'primeng/menubar';
import {InputTextModule} from "primeng/inputtext";
import {CarouselHomeComponent} from './carousel-home/carousel-home.component';
import {CardModule} from "primeng/card";
import {WineService} from "./services/wine.service";
import {HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {CollectionsComponent} from './collections/collections.component';
import {CartComponent} from './cart/cart.component';
import {ArticleComponent} from './article/article.component';
import {ProfileComponent} from './profile/profile.component';
import {DataViewModule} from "primeng/dataview";
import {DropdownModule} from "primeng/dropdown";
import {RatingModule} from "primeng/rating";
import { PresentationComponent } from './presentation/presentation.component';
import { CategoryComponent } from './category/category.component';


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
    CollectionsComponent,
    ProfileComponent,
    CartComponent,
    ArticleComponent,
    PresentationComponent,
    CategoryComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ButtonModule,
    CarouselModule,
    MenubarModule,
    InputTextModule,
    CardModule,
    HttpClientModule,
    ReactiveFormsModule,
    HttpClientModule,
    DataViewModule,
    DropdownModule,
    FormsModule,
    RatingModule
  ],
  providers: [WineService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
