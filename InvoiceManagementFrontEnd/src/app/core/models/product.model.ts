export interface Product {
  id: number;
  code: string;
  name: string;
  description: string;
  salePrice: number;
  createdAt: string;
  updatedAt?: string;
}