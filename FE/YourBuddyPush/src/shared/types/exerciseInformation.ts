export type ExerciseInformation = {
  exerciseId: string;
  name: string;
  description: string;
  imageUrl: string;
  videoUrl: string;
  type: "time" | "weight"
}