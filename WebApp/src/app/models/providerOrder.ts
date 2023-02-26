import {Provider} from "./provider";
import {Status} from "./status";
import {ArticleOrder} from "./article";
import {Auditable} from "./auditable";

export interface ProviderOrder extends Auditable{
  id?: number;
  date?: Date;
  providerId?: number;
  provider?: Provider;
  statusId?: number;
  status?: Status;
  articleOrders?: Array<ArticleOrder>;
}
