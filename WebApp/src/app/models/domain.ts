import {DomainAddress} from "./domainAddress";
import {Auditable} from "./auditable";

export interface Domain extends Auditable{
  id?: number;
  name?: string;
  description?: string;
  domainAddresses?: Array<DomainAddress>;
}
