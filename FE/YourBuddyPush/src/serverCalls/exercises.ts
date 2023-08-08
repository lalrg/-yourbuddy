import { GetPaginated, getBase } from "./ServerCallsBase";
import { BaseUrl } from "./constants";

const GetExercisesList: GetPaginated = async (pagesize, currentPage) => {
  return await getBase(`${BaseUrl}exercise?pageSize=${pagesize}&currentPage=${currentPage}`);
}

export {
  GetExercisesList
}