import { deleteBase, getBase, postBase, putBase } from './ServerCallsBase';
import { BaseUrl } from './constants';

const GetUsersList = async (pagesize: number, currentpage: number) => {
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


export {
  GetUsersList,
  DeleteUser,
  GetSingleUser,
  UpdateUser,
  CreateUser
}