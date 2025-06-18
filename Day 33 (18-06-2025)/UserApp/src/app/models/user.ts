export interface User {
  firstName: string;
  lastName: string;
  gender: string;
  age?: number;
  role?: string;
  address?: {
    state?: string;
  };
}
