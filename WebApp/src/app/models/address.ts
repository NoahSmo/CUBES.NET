import {User} from "./user";
import {Auditable} from "./auditable";

export interface Address extends Auditable {
  id?: number;
  street?: string;
  city?: string;
  country?: string;
  zipCode?: number;
  userId?: number;
  user?: User;
}
