export interface Customer {
  id: number;
  name: string;
  email: string;
  phone: string;
  address: string;
  status: CustomerStatus;
  createdAt: string;
  updatedAt?: string;
}

export enum CustomerStatus {
  Active = 21,
  Inactive = 22
}
