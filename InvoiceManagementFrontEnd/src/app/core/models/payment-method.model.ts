export interface PaymentMethod {
  id: number;
  name: string;
  status: number;
  createdAt: string;
  updatedAt: string;
}

export enum PaymentMethodStatus {
  Active = 31,
  Inactive = 32
}