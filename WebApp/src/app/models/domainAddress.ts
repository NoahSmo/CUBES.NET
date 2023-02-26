import {Domain} from "./domain";
import {Auditable} from "./auditable";

export interface DomainAddress extends Auditable {
  id?: number;
  street?: string;
  city?: string;
  country?: string;
  zipCode?: number;
  domainId?: number;
  domain?: Domain;
}
