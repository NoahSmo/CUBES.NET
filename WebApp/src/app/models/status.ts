import {Auditable} from "./auditable";

export interface Status extends Auditable {
  id: number;
  message: string;
}
