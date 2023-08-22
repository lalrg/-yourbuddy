import React, { useEffect, useState } from 'react';
import Grid from './Grid';
import NoData from '../../components/NoData';
import { Button, Row, Spin } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { GetRoutinesForUserList, GetRoutinesList } from '../../serverCalls/routines';
import { RoutineInformation } from '../../shared/types/routineInformation';
import { useAuthStore } from '../../store/authStore';

type props = {
  type: 'all' | 'mine'
}

const Routines: React.FC<props> = ({type = 'all'})=> {
  const [pagination, setPagination] = useState({ pageSize: 5, currentPage: 1, totalItems: 0, });
  const [routineData, setRoutineData] = useState<Array<RoutineInformation>>();
  const [isLoading, setIsLoading] = useState(true);
  const [hasChanged, setHasChanged] = useState(false);
  const { userInfo } = useAuthStore();
  const navigate = useNavigate();
  
  useEffect(() => {
    setIsLoading(true);
    const callBack = type === 'all' ? GetRoutinesList : GetRoutinesForUserList;

    callBack(pagination.pageSize, pagination.currentPage)
      .then(
        r => {
          setRoutineData( 
            r.data.items?.map(
              (i: RoutineInformation) => ({ ...i, actionsAllowed: type === 'all' ? userInfo?.roles : 'user'})
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
  }, [pagination.pageSize, pagination.currentPage, setRoutineData, setPagination, hasChanged, setHasChanged, userInfo?.roles, type])

  const onPaginationChange = (currentPage: number, itemsPerPage: number) => {
    setPagination(
      p=> ({
        ...p,
        pageSize: itemsPerPage,
        currentPage
      })
    );
  };

  const thereIsNoData = (!isLoading && (!routineData || !routineData.length));

  return(
    <>
      <h2 style={{ textAlign: 'center' }}>{type === 'all' ? 'Todas las rutinas' : 'Mis rutinas'}</h2>
      <Row justify={'end'}>
        {
          type === 'all' &&
            <Button icon={<PlusOutlined />} onClick={() => navigate('/routines/create')}>
              Agregar nueva
            </Button>
        }
      </Row>
      <br />
      <Spin spinning={isLoading} delay={0}>
        { 
          thereIsNoData ? 
            <NoData/> : 
            <Grid 
              data={routineData} 
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

export default Routines;