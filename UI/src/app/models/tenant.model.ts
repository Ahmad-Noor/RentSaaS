import { UUID } from "crypto";
export interface TenantCreate
{
id?: UUID;



}

export interface Tenant {
    name ?: string;
    email?:string;
    phone?:string;
    address?:string;
  }