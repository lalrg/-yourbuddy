import React, { useEffect, useState } from 'react';
import { DeleteUser } from '../../serverCalls/users';
import Grid from './Grid';
import NoData from '../../components/NoData';
import { Button, Row, Spin } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { GetExercisesList } from '../../serverCalls/exercises';
import { ExerciseInformation } from '../../shared/types/exerciseInformation';

const Users: React.FC = ()=> {
  const [pagination, setPagination] = useState({ pageSize: 5, currentPage: 1, totalItems: 0, });
  const [exercisesData, setExercisesData] = useState<Array<ExerciseInformation>>();
  const [isLoading, setIsLoading] = useState(true);
  const [hasChanged, setHasChanged] = useState(false);
  const navigate = useNavigate();
  
  useEffect(() => {
    setIsLoading(true);
    GetExercisesList(pagination.pageSize, pagination.currentPage)
      .then(
        r => {
          setExercisesData(
            r.data.items.map(
              (e: object) => ({
                ...e,
                onDelete: async (id: string) => {
                  await DeleteUser(id);
                  setHasChanged(true);
                  return;
                }
              })
            )
          );

          setPagination( p=> ({
            ...p,
            pageSize: r.data.pageSize,
            currentPage: r.data.currentPage,
            totalItems: r.data.totalCount
          }));

        }
      ).finally(
        () => setIsLoading(false)
      );
      setHasChanged(false);
  }, [pagination.pageSize, pagination.currentPage, setExercisesData, setPagination, hasChanged, setHasChanged])

  const onPaginationChange = (currentPage: number, itemsPerPage: number) => {
    setPagination(
      p=> ({
        ...p,
        pageSize: itemsPerPage,
        currentPage
      })
    );
  };

  const thereIsNoData = (!isLoading && (!exercisesData || !exercisesData.length));

  return(
    <>
      <h2 style={{ textAlign: 'center' }}>Ejercicios 🏋️</h2>
      <Row justify={'end'}>
        <Button icon={<PlusOutlined />} onClick={() => navigate('/exercises/create')}>
          Agregar nuevo
        </Button>
      </Row>
      <br />
      <Spin spinning={isLoading} delay={0}>
        { 
          thereIsNoData ? 
            <NoData/> : 
            <Grid 
              data={exercisesData} 
              onChange={onPaginationChange}
              totalItems={pagination.totalItems}
              currentPage={pagination.currentPage}
              itemsPerPage={pagination.pageSize}
              />
        }
      </Spin>
    </>    
  ) 
  
}

export default Users;