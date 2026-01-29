export interface Configuration {
  id: number;
  companyName: string;
  phone: string;
  email: string;
  address: string;
  city: string;
  region: string;
  postalCode: string;
  vatPercentage: number;
  currencySymbol: string;
  logoUrl: string;
  updatedAt: string;
  base64LogoImage?: string;
}
