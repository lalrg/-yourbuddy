import { deleteBase, getBase } from './ServerCallsBase';
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


export {
  GetUsersList,
  DeleteUser,
  GetSingleUser
}