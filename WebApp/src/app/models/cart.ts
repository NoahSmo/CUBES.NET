import {Auditable} from "./auditable";
import {User} from "./user";
import {Article} from "./article";

export interface Cart extends Auditable{
  id?: number;
  userId?: number;
  user?: User;
  cartItems?: Array<CartItem>;
}

export interface CartItem {
  id?: number;
  quantity?: number;
  articleId?: number;
  article?: Article;
  cartId?: number;
  cart?: Cart;
}
