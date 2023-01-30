import {Article} from "./article";
import {Auditable} from "./auditable";

export interface Category extends Auditable {
  id?: number;
  name?: string;
  description?: string;
  articles?: Array<Article>;
}
