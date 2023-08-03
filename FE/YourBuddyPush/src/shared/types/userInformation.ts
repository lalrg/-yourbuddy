export type UserInformation = {
  id: string;
  name: string;
  lastName: string;
  email: string;
  isDeleted: boolean;
  roles: string;
  onDelete?: (id: string) => void 
};