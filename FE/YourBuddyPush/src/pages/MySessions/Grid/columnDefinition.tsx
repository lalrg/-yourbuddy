import { ColumnsType } from "antd/es/table";
import { Popconfirm, Space } from "antd";
import { Link } from "react-router-dom";
import { trainingSessionInformation } from "../../../shared/types/trainingSessionInformation";

const SessionActions = (session: trainingSessionInformation) => {
  return(
    <Space key={session.id}>
      <Link to={`/sessions/${session.id}`}>Ver o actualizar</Link>
    </Space>
  )
}

const columns: ColumnsType<trainingSessionInformation> = [
  {
    title: 'Fecha',
    dataIndex: 'startTime',
    render: (_, item) => {
      const date = new Date(item.startTime);
      return <p>{date.getDate()}/{date.getMonth()}/{date.getFullYear()}</p>
    }
  },
  {
    title: 'Hora inicio',
    dataIndex: 'startTime',
    responsive: ['lg'],
    render: (_, item) => {
      const date = new Date(item.startTime);
      const minutes = date.getMinutes();
      return <p>{date.getHours()}:{minutes < 10 ? '0' + minutes.toString() : minutes}</p>
    }
  },
  {
    title: 'Hora fin',
    dataIndex: 'endTime',
    responsive: ['lg'],
    render: (_, item) => {
      const date = new Date(item.endTime);
      const minutes = date.getMinutes();
      return <p>{date.getHours()}:{minutes < 10 ? '0' + minutes.toString() : minutes}</p>
    }
  },
  {
    title: 'Acciones',
    key: 'action',
    render: (_, r) => SessionActions(r)
  }
];


export {
  columns
}