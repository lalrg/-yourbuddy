export type ExerciseFromRoutine = {
  exerciseId: string;
  name: string;
  description: string;
  imageUrl: string;
  videoUrl: string;
  setsDescription: string;
  load: number;
  sets: number;
  reps: number;
  type: "weight" | "time"
};

export type RoutineInformation = {
  id: string;
  createdBy: string;
  createdByName: string;
  name: string;
  isEnabled: boolean;
  exercises: Array<ExerciseFromRoutine>;
  actionsAllowed?: string;
}