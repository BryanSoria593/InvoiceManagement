export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  status: UserStatus;
  createdAt: string;
  updatedAt?: string;
}

export enum UserStatus {
  Active = 1,
  Inactive = 2
}
