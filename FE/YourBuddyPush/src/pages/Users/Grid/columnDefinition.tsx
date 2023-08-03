import { ColumnsType } from "antd/es/table";
import { Popconfirm, Space } from "antd";
import { UserInformation } from "../../../shared/types/userInformation";
import { Link } from "react-router-dom";

const UserActions = (user: UserInformation) => {
  return(
    <Space key={user.id}>
      <Link to={`/users/${user.id}`}>Editar</Link>
      <Popconfirm
        title="Eliminar Usuario"
        description="Al aceptar, el usuario sera eliminado del sistema"
        okText="Si"
        cancelText="No"
        onConfirm={() => user?.onDelete ? user?.onDelete(user.id) : null}
      >
        <a>Eliminar</a>
      </Popconfirm>
    </Space>
  )
}

const columns: ColumnsType<UserInformation> = [
  {
    title: 'Nombre',
    dataIndex: 'name',
    key: 'name',
  },
  {
    title: 'Apellido',
    dataIndex: 'lastName',
    key: 'lastName',
  },
  {
    title: 'Correo Electronico',
    dataIndex: 'email',
    key: 'email',
    responsive: ['lg'],
  },
  {
    title: 'Acciones',
    key: 'action',
    render: (_, u) => UserActions(u)
  }
];


export {
  columns
}