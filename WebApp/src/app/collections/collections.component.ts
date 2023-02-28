import {Component, OnInit} from '@angular/core';
import {Article} from "../models/article";
import {WineService} from "../services/wine.service";
import {ImageService} from "../services/image.service";
import {Image} from "../models/image";
import {Cart, CartItem} from "../models/cart";
import {CartService} from "../services/cart.service";
import {SelectItem} from "primeng/api";

@Component({
  selector: 'app-collections',
  templateUrl: './collections.component.html',
  styleUrls: ['./collections.component.scss']
})
export class CollectionsComponent implements OnInit {
  sortOptions: SelectItem[] = [ {label: 'Price High to Low', value: '!price'}, {label: 'Price Low to High', value: 'price'}];
  sortOrder: number | undefined;
  sortField: string | undefined;


  articles: Article[] = [];
  article: Article | undefined;
  images: Image[] = [];
  cart: Cart[] = [];

  constructor(private wineService: WineService, private imageService: ImageService, private cartService: CartService) {
  }

  ngOnInit(): void {
    this.wineService.getArticles().subscribe((wine: Article[]) => {
      this.articles = wine;
    });

    this.imageService.getImages().subscribe((images: Image[]) => {
      this.images = images;
    });

    this.sortOptions = [
      {label: 'Price High to Low', value: '!price'},
      {label: 'Price Low to High', value: 'price'}
    ];
  }

  onSortChange(event: any) {
    let value = event.value;

    if (value.indexOf('!') === 0) {
      this.sortOrder = -1;
      this.sortField = value.substring(1, value.length);
    }
    else {
      this.sortOrder = 1;
      this.sortField = value;
    }
  }

  getUrlImageByArticleId(id: number | undefined): string {
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
