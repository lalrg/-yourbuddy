import { ColumnsType } from "antd/es/table";
import { Popconfirm, Space } from "antd";
import { Link } from "react-router-dom";
import { RoutineInformation } from "../../../shared/types/routineInformation";

const RoutineActions = (routine: RoutineInformation) => {
  return(
    <Space key={routine.id}>
      <Link to={`/routines/${routine.id}`}>Ver</Link>
      { routine.actionsAllowed === 'admin' && 
        <Popconfirm
          title="Eliminar rutina"
          description="Al aceptar, la rutina sera eliminada del sistema"
          okText="Si"
          cancelText="No"
          onConfirm={() => console.log(routine)}
        >
          <a>Eliminar</a>
        </Popconfirm>
      }
    </Space>
  )
}

const columns: ColumnsType<RoutineInformation> = [
  {
    title: 'Nombre',
    dataIndex: 'name',
    key: 'name',
  },
  {
    title: 'Cantidad de ejercicios',
    key: 'exercises',
    responsive: ['lg'],
    render: (_, r) => r.exercises.length
  },
  {
    title: 'Creada por',
    key: 'createdBy',
    responsive: ['lg'],
    render: (_, r) => r.createdByName
  },
  {
    title: 'Acciones',
    key: 'action',
    render: (_, r) => RoutineActions(r)
  }
];


export {
  columns
}