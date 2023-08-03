import React, { useEffect, useState } from 'react';
import { DeleteUser, GetUsersList } from '../../serverCalls/users';
import Grid from './Grid';
import { UserInformation } from '../../shared/types/userInformation';
import NoData from '../../components/NoData';
import { Button, Row, Spin } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';

const Users: React.FC = ()=> {
  const [pagination, setPagination] = useState({ pageSize: 5, currentPage: 1, totalItems: 0, });
  const [userData, setUserData] = useState<Array<UserInformation>>();
  const [isLoading, setIsLoading] = useState(true);
  const [hasChanged, setHasChanged] = useState(false);
  const navigate = useNavigate();
  
  useEffect(() => {
    setIsLoading(true);
    GetUsersList(pagination.pageSize, pagination.currentPage)
      .then(
        r => {
          setUserData(
            r.data.items.map(
              (e: UserInformation) => ({
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
  }, [pagination.pageSize, pagination.currentPage, setUserData, setPagination, hasChanged, setHasChanged])

  const onPaginationChange = (currentPage: number, itemsPerPage: number) => {
    setPagination(
      p=> ({
        ...p,
        pageSize: itemsPerPage,
        currentPage
      })
    );
  };

  const thereIsNoData = (!isLoading && (!userData || !userData.length));

  return(
    <>
      <h2 style={{ textAlign: 'center' }}>Usuarios</h2>
      <Row justify={'end'}>
        <Button icon={<PlusOutlined />} onClick={() => navigate('/users/create')}>
          Agregar nuevo
        </Button>
      </Row>
      <br />
      <Spin spinning={isLoading} delay={0}>
        { 
          thereIsNoData ? 
            <NoData/> : 
            <Grid 
              data={userData} 
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