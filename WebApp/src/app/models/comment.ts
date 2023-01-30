import {User} from "./user";
import {Auditable} from "./auditable";

export interface Comment extends Auditable{
  id?: number;
  rating?: number;
  message?: string;
  articleId?: number;
  userId?: number;
  user?: User;
}
