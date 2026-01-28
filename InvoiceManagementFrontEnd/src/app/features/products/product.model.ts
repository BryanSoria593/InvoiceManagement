export interface Product {
  id: number;
  code: string;
  name: string;
  description: string;
  salePrice: number;
  status: ProductStatus;
  createdAt: string;
  updatedAt?: string;
}

export enum ProductStatus {
  Active = 11,
  Inactive = 12
}
