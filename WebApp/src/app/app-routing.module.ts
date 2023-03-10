import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {CollectionsComponent} from "./collections/collections.component";
import {ArticleComponent} from "./article/article.component";
import {CartComponent} from "./cart/cart.component";
import {ProfileComponent} from "./profile/profile.component";
import {PresentationComponent} from "./presentation/presentation.component";
import {CategoryComponent} from "./category/category.component";

const routes: Routes = [
  {path: '', component: HomeComponent },
  {path: 'login', component: LoginComponent },
  {path: 'register', component: RegisterComponent },
  {path: 'profile', component: ProfileComponent },
  {path: 'collections', component: CollectionsComponent },
  {path: 'article/:id', component: ArticleComponent },
  {path: 'cart', component: CartComponent },
  {path: 'presentation', component: PresentationComponent},
  {path: 'category/:id', component: CategoryComponent},


];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
