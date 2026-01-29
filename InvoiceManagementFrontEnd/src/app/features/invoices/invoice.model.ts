import { PaymentMethod } from "../../core/models/payment-method.model";

export class Invoice {
  id = 0;
  date = new Date();
  status: InvoiceStatus = InvoiceStatus.Active;
  total = 0;
  observations?: string;
  isDeleted = false;
  createdAt = new Date();
  updatedAt?: Date;

  customerId = 0;
  customerName: string = '';

  userId = 0;
  userName: string = '';

  paymentMethodId = 0;

  invoiceDetails: InvoiceDetail[] = [];
}

export class InvoiceDetail {
  id = 0;
  invoiceId = 0;
  productId = 0;
  productName: string = '';
  prouctCode: string = '';
  quantity = 0;
  unitPrice = 0;
  total = 0;

  description?: string;
  isDeleted = false;

}

export enum InvoiceStatus {
  Active = 41,
  Inactive = 42
}
