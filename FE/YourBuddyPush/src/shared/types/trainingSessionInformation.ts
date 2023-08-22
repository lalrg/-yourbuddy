export type trainingSessionInformation = {
  id: string;
  startTime: Date;
  endTime: Date;
  createdById: string;
  createdByName: string;
  exerciseTrainingSessions: exerciseTrainingSession;
}

export type exerciseTrainingSession = {
  exerciseId: string;
  name: string;
  description: string;
  setsDescription: string;
  load: number;
  sets: number;
  reps: number;
}