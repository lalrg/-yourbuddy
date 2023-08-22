import React, { useEffect, useState } from 'react';
import Grid from './Grid';
import NoData from '../../components/NoData';
import { Button, Row, Spin } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { GetRoutinesList } from '../../serverCalls/routines';
import { useAuthStore } from '../../store/authStore';
import { GetSessionsList } from '../../serverCalls/sessions';
import { trainingSessionInformation } from '../../shared/types/trainingSessionInformation';

const MySessions: React.FC = ()=> {
  const [pagination, setPagination] = useState({ pageSize: 5, currentPage: 1, totalItems: 0, });
  const [sessionData, setSessionData] = useState<Array<trainingSessionInformation>>();
  const [isLoading, setIsLoading] = useState(true);
  const [hasChanged, setHasChanged] = useState(false);
  const { userInfo } = useAuthStore();
  const navigate = useNavigate();
  
  useEffect(() => {
    setIsLoading(true);
    GetSessionsList(5, 1);

    GetSessionsList(pagination.pageSize, pagination.currentPage)
      .then(
        r => {
          setSessionData( 
            r.data.items?.map(
              (i: trainingSessionInformation) => ({ ...i, actionsAllowed: userInfo?.roles })
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
  }, [pagination.pageSize, pagination.currentPage, setSessionData, setPagination, hasChanged, setHasChanged, userInfo?.roles])

  const onPaginationChange = (currentPage: number, itemsPerPage: number) => {
    setPagination(
      p=> ({
        ...p,
        pageSize: itemsPerPage,
        currentPage
      })
    );
  };

  const thereIsNoData = (!isLoading && (!sessionData || !sessionData.length));

  return(
    <>
      <h2 style={{ textAlign: 'center' }}>Mis sesiones de entrenamiento 🔥</h2>
      <Row justify={'end'}>
        <Button icon={<PlusOutlined />} onClick={() => navigate('/routines/create')}>
          Agregar nueva
        </Button>
      </Row>
      <br />
      <Spin spinning={isLoading} delay={0}>
        { 
          thereIsNoData ? 
            <NoData/> : 
            <Grid 
              data={sessionData} 
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

export default MySessions;