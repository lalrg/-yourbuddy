import { Button, Col, Form, Input, Row, Spin } from 'antd';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { UserInformation } from '../../shared/types/userInformation';
import { GetSingleUser } from '../../serverCalls/users';
import { LeftOutlined, CheckOutlined } from '@ant-design/icons';
import './styles.css';

const UserEdit: React.FC = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  const [userData, setUserData] = useState<UserInformation>();
  const [form] = Form.useForm();

  useEffect(
    () => {
      if(!id)
        return;

      GetSingleUser(id)
        .then(
          r=> {
            setUserData(r.data)
            console.log(userData)
          }
        )
        .finally(
          () => setLoading(false)
        );
    },
    [id, userData]
  );
  
  return (
    <Spin spinning={loading} delay={0}> 
      <h2 style={{textAlign:'center'}}>Editar usuario {userData?.email ?? id}</h2>
      <Form
        name="wrap"
        labelCol={{ flex: '110px' }}
        labelAlign="left"
        labelWrap
        wrapperCol={{ flex: 1 }}
        colon={false}
        form={form}
      >
        <Form.Item label="Normal label" name="username" rules={[{ required: true, message: 'Este campo es requerido' }]}>
          <Input />
        </Form.Item>

        <Form.Item label="A super long label text" name="password" rules={[{ required: true, message: 'Este campo es requerido' }]}>
          <Input />
        </Form.Item>

        <Form.Item label=" ">
          <Row gutter={64} justify={'center'}>
            <Col xs={24} md={12} className='userEditButton'>
              <Button type='default' icon={<LeftOutlined />} onClick={() => navigate('/users')}>
                Regresar a lista de usuarios
              </Button>
            </Col>
            <Col xs={24} md={12} className='userEditButton'>
              <Button type='primary' icon={<CheckOutlined />} htmlType="submit">
                Guardar cambios
              </Button>
            </Col>
          </Row>
        </Form.Item>
      </Form>
    


    </Spin>
  )
}

export default UserEdit;