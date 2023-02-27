import {Auditable} from "./auditable";

export interface Provider extends Auditable{
     id?: number;
     name?: string;
     email?: string;
     phone?: number;
}
