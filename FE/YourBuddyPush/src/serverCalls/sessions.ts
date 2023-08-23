import { GetPaginated, getBase, postBase } from "./ServerCallsBase";
import { BaseUrl } from "./constants";

const GetSessionsList: GetPaginated = async (pagesize: number, currentPage: number) => {
  return await getBase(`${BaseUrl}trainingsession?pageSize=${pagesize}&currentPage=${currentPage}`);
}

const GetSingleSession = async (id: string) => {
  return await getBase(`${BaseUrl}trainingsession/${id}`)
}

const AddExercise = async (exerciseId: string, sessionId: string, reps: string, sets: string, load: string) => {
  return await postBase(`${BaseUrl}trainingsession/addExercise`, { exerciseId, sessionId, reps, sets, load });
}

const RemoveExercise = async (exerciseId: string, sessionId: string) => {
  return await postBase(`${BaseUrl}trainingsession/removeExercise`, { exerciseId, sessionId });
}

export {
  GetSessionsList,
  GetSingleSession,
  AddExercise,
  RemoveExercise
}