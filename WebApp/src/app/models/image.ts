import {Article} from "./article";
import {Auditable} from "./auditable";

export interface Image extends Auditable {
  id?: number;
  url?: string;
  articleId?: number;
  article?: Article;
}
