import { GetPaginated, getBase } from "./ServerCallsBase";
import { BaseUrl } from "./constants";

const GetSessionsList: GetPaginated = async (pagesize, currentPage) => {
  return await getBase(`${BaseUrl}trainingsession?pageSize=${pagesize}&currentPage=${currentPage}`);
}

export {
  GetSessionsList
}