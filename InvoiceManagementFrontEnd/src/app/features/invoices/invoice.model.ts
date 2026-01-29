export interface Invoice {
  id: number;
  customerId: number;
  customerName: string;
  userId: number;
  userName: string;
  date: string;
  paymentMethodId: number;
  paymentMethodName: string;
  status: number;
  total: number;
  observations?: string;
  isDeleted?: boolean;
  createdAt?: string;
  updatedAt?: string;
  details?: any[];
}


export enum InvoiceStatus {
  Active = 41,
  Inactive = 42
}
