import { GetPaginated, getBase, postBase, putBase } from "./ServerCallsBase";
import { BaseUrl } from "./constants";

const GetExercisesList: GetPaginated = async (pagesize, currentPage) => {
  return await getBase(`${BaseUrl}exercise?pageSize=${pagesize}&currentPage=${currentPage}`);
}

const CreateExercise = async(ExerciseName: string, ExerciseType: string, ExerciseDescription: string, ImageUrl: string, VideoUrl: string) => {
  return await postBase(`${BaseUrl}exercise`, {ExerciseName, ExerciseType, ExerciseDescription, ImageUrl, VideoUrl});
}

const UpdateExercise = async (exerciseId: string, ExerciseName: string, ExerciseType: string, ExerciseDescription: string, ImageUrl: string, VideoUrl: string) => {
  return await putBase(`${BaseUrl}exercise/${exerciseId}`, { ExerciseName,  ExerciseType, ExerciseDescription, ImageUrl, VideoUrl});
}

const GetExerciseById = async (exerciseId: string) => {
  return await getBase(`${BaseUrl}exercise/${exerciseId}`);
}

export {
  GetExercisesList,
  CreateExercise,
  UpdateExercise,
  GetExerciseById
}