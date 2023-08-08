import { ColumnsType } from "antd/es/table";
import { Space } from "antd";
import { Link } from "react-router-dom";
import { ExerciseInformation } from "../../../shared/types/exerciseInformation";

const ExerciseActions = (exercise: ExerciseInformation) => {
  return(
    <Space key={exercise.exerciseId}>
      <Link to={`/exercises/${exercise.exerciseId}`}>Editar</Link>
    </Space>
  )
}

const columns: ColumnsType<ExerciseInformation> = [
  {
    title: 'Nombre',
    dataIndex: 'name',
    key: 'name',
  },
  {
    title: 'Descripcion',
    dataIndex: 'description',
    key: 'description',
    responsive: ['lg'],
  },
  {
    title: 'Tipo',
    dataIndex: 'email',
    key: 'email',
    responsive: ['lg'],
    render: (_, e) => e.type == "time" ? "Tiempo" : "Peso"
  },
  {
    title: 'Acciones',
    key: 'action',
    render: (_, u) => ExerciseActions(u)
  }
];


export {
  columns
}