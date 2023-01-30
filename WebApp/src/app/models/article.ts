import {Domain} from "./domain";
import {Provider} from "./provider";
import {Category} from "./category";
import {Image} from "./image";
import {Comment} from "./comment";
import {Auditable} from "./auditable";

export interface Article extends Auditable{
  id?: number;
  name?: string;
  description?: string;
  price?: number;
  year?: number;
  alcohol?: number;
  stock?: number;
  domainId?: number;
  domain?: Domain;
  categoryId?: number;
  category?: Category;
  articleOrders?: Array<ArticleOrder>;
  providers?: Array<Provider>;
  images?: Array<Image>;
  comments?: Array<Comment>;
}

export interface ArticleOrder{
  id?: number;
  articleId?: number;
  orderId?: number;
  quantity?: number;
}
