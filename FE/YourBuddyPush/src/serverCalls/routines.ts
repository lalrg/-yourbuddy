import { GetPaginated, getBase, postBase } from "./ServerCallsBase";
import { BaseUrl } from "./constants";

const GetRoutinesList: GetPaginated = async (pagesize: number, currentpage: number) => {
  return await getBase(`${BaseUrl}routine?pageSize=${pagesize}&currentPage=${currentpage}`);
}

const GetRoutinesForUserList = async (pagesize: number, currentpage: number) => {
  return await getBase(`${BaseUrl}routine/GetByUserId?pageSize=${pagesize}&currentPage=${currentpage}`);
}

const CreateRoutine = async (Name: string) => {
  return await postBase(`${BaseUrl}routine`, { Name });
}

const GetRoutineById = async (id: string) => {
  return await getBase(`${BaseUrl}routine/${id}`);
}

const UpdateName = async (Id: string, Name: string) => {
  return await postBase(`${BaseUrl}routine/updateName`, { Id, Name })
}

const RemoveExerciseFromRoutine = async (routineId: string, exerciseId: string) => {
  return await postBase(`${BaseUrl}routine/RemoveExercise`, { routineId, exerciseId });
}

const AddExerciseToRoutine = async (ExerciseId: string, RoutineId: string, reps: string, load: string, sets: string) => {
  return await postBase(`${BaseUrl}routine/AddExercise`, {ExerciseId, RoutineId, reps, load, sets})
}

const AssignToUser = async (routineId: string, userId: string) => {
  return await postBase(`${BaseUrl}routine/AssignToUser`, { userId, routineId });
}

const Duplicate = async (id: string) => {
  return await postBase(`${BaseUrl}routine/Duplicate`, {id})
}

export {
  GetRoutinesList,
  CreateRoutine,
  GetRoutineById,
  UpdateName,
  RemoveExerciseFromRoutine,
  AddExerciseToRoutine,
  AssignToUser,
  Duplicate,
  GetRoutinesForUserList
}