import { getBase } from './ServerCallsBase';
import { BaseUrl } from './constants';

const GetUsersList = async (pagesize: number, currentpage: number) => {
  return await getBase(`${BaseUrl}user?pageSize=${pagesize}&currentPage=${currentpage}`);
}

export {
  GetUsersList
}