import { GetPaginated, deleteBase, getBase, postBase, putBase } from './ServerCallsBase';
import { BaseUrl } from './constants';

const GetUsersList: GetPaginated = async (pagesize: number, currentpage: number) => {
  return await getBase(`${BaseUrl}user?pageSize=${pagesize}&currentPage=${currentpage}`);
}

const GetSingleUser = async (id: string) => {
  return await getBase(`${BaseUrl}user/${id}`);
}

const DeleteUser = async (id: string) => {
  return await deleteBase(`${BaseUrl}user/${id}`);
}

const UpdateUser = async (id: string, Name: string, LastName: string, Email:string, Role: string) => {
  return await putBase(`${BaseUrl}user/${id}`, { Name, LastName, Email, Role});
}

const CreateUser = async (Name: string, LastName: string, Email:string, Role: string) => {
  return await postBase(`${BaseUrl}user`, { Name, LastName, Email, Role });
}

const GetAll = async () => {
  return await getBase(`${BaseUrl}user/getAll`)
}

const ResetPassword = async (UserId: string) => {
  return await postBase(`${BaseUrl}authentication/resetpasssword`, {UserId});
}

const ForgotPassword = async (Email: string) => {
  return await postBase(`${BaseUrl}authentication/forgotPassword`, {Email});
}

const UpdatePassword = async (NewPassword: string, OldPassword: string) => {
  return await postBase(`${BaseUrl}authentication/updatePassword`, { NewPassword, OldPassword })
}


export {
  GetUsersList,
  DeleteUser,
  GetSingleUser,
  UpdateUser,
  CreateUser,
  GetAll,
  ResetPassword,
  ForgotPassword,
  UpdatePassword
}