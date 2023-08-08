import React from 'react';
import { Table } from 'antd';
import { columns } from './columnDefinition'
import { ExerciseInformation } from '../../../shared/types/exerciseInformation';

type props = {
  data?: Array<ExerciseInformation>;
  currentPage?: number;
  totalItems?: number;
  itemsPerPage?: number;
  onChange?: (currentPage: number, pageSize: number) => void;
}


const Grid: React.FC<props> = ({ data, currentPage, totalItems, itemsPerPage, onChange }) => {
  return (
    <Table 
      columns={columns} 
      dataSource={data}
      rowKey="id"
      pagination={{
        current: currentPage ?? 1,
        total: totalItems ?? data?.length,
        pageSize: itemsPerPage ?? 10,
        showTotal: (total, range) => `Mostrando del ${range[0]} al ${range[1]} de ${total}`,
        onChange: onChange,
        showSizeChanger: true
      }} />
  )
};


export default Grid;