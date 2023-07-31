import React, { useEffect, useState } from 'react';
import { GetUsersList } from '../../serverCalls/users';
import Grid from './Grid';

const Users: React.FC = ()=> {
  const [pagination, setPagination] = useState({ pageSize: 5, currentPage: 1 });
  const [totalItems, setTotalItems] = useState(0);
  const [userData, setUserData] = useState([]);
  
  useEffect(() => {
    GetUsersList(pagination.pageSize, pagination.currentPage)
      .then(
        r => {
          setUserData(r.data.items);
          setTotalItems(r.data.totalCount);
        }
      );
  }, [pagination, setUserData, setTotalItems, setPagination])

  


  return(
    <>
      <h2>Usuarios</h2>
      <Grid />
    </>    
  ) 
  
}

export default Users;