import {Address} from "./address";
import {User} from "./user";
import {Status} from "./status";
import {Auditable} from "./auditable";
import {ArticleOrder} from "./article";

export interface Order extends Auditable{
     id?: number;
     date?: Date;
     userId?: number;
     user?: User;
     addressId?: number;
     address?: Address;
     statusId?: number;
     status?: Status;
     articleOrders?: Array<ArticleOrder>;
}
