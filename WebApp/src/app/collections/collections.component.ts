import {Component, OnInit} from '@angular/core';
import {Article} from "../models/article";
import {WineService} from "../services/wine.service";
import {ImageService} from "../services/image.service";
import {Image} from "../models/image";
import {Cart, CartItem} from "../models/cart";
import {CartService} from "../services/cart.service";

@Component({
  selector: 'app-collections',
  templateUrl: './collections.component.html',
  styleUrls: ['./collections.component.scss']
})
export class CollectionsComponent implements OnInit {

  articles: Article[]= [];
  article: Article | undefined;
  images: Image[] = [];
  cart: Cart[] = [];

  constructor(private wineService: WineService, private imageService: ImageService, private cartService: CartService) { }

  ngOnInit(): void {
    this.wineService.getArticles().subscribe((wine: Article[]) => {
      this.articles = wine;
    });

    this.imageService.getImages().subscribe((images: Image[]) => {
      this.images = images;
    });
  }

  getUrlImageByArticleId(id: number | undefined ): string {
    let url: string | undefined = '';
    this.images.forEach((image: Image) => {
      if (image.articleId === id) {
        url = image.url;
      }
    });
    return url;
  }

  addToCart(article: Article): void {
    let cartItem: CartItem = {
      articleId: article.id,
      quantity: 1
    };
    let cart: Cart = {
      cartItems: [cartItem]
    };
    this.cartService.addCart(cart).subscribe((cart: Cart) => {
      this.cart.push(cart);
    });
  }

  getCart(): void {
    this.cartService.getCarts().subscribe((cart: Cart[]) => {
      this.cart = cart;
      console.log(this.cart);
    });
  }

}
