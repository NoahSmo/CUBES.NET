import {Address} from "./address";
import {Order} from "./order";
import {Comment} from "./comment";
import {Auditable} from "./auditable";

export interface User extends Auditable {
  id?: number;
  username?: string;
  name?: string;
  surname?: string;
  email?: string;
  phone?: number;
  role?: string;
  password?: string;
  addresses?: Array<Address>;
  orders?: Array<Order>;
  comments?: Array<Comment>;
}

export interface UserLogin {
  email: string;
  password: string;
}

