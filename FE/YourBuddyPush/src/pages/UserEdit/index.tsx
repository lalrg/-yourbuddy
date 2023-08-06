import { Button, Col, Form, Input, Row, Select, Spin } from 'antd';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { UserInformation } from '../../shared/types/userInformation';
import { GetSingleUser, UpdateUser } from '../../serverCalls/users';
import { LeftOutlined, CheckOutlined } from '@ant-design/icons';
import './styles.css';

const UserEdit: React.FC = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  const [userData, setUserData] = useState<UserInformation>();
  const [form] = Form.useForm();
  const { Option } = Select;

  useEffect(
    () => {
      if(!id)
        return;

      GetSingleUser(id)
        .then(
          r=> {
            setUserData(r.data);
            form.setFieldsValue({
              email: r.data.email,
              lastname: r.data.lastName,
              name: r.data.name,
              role: r.data.roles[0]
            });
          }
        )
        .finally(
          () => {
            setLoading(false);
          }
        );
    },
    [id, setUserData, setLoading, form, navigate]
  );
  const onFinish = async (values: { email: string, name: string, lastname: string, role: string }) => {
    await UpdateUser(id ?? '', values.name, values.lastname, values.email, values.role);
    navigate('/users');
  };

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
        onFinish={onFinish}
      >
        <Form.Item label="Correo Electronico" name="email" 
          rules={[
            { required: true, message: 'Este campo es requerido' }, 
            { type:"email", message: 'Debe ingresar un email valido' }
          ]}
        >
          <Input />
        </Form.Item>

        <Form.Item label="Nombre" name="name" rules={[{ required: true, message: 'Este campo es requerido' }]}>
          <Input />
        </Form.Item>

        <Form.Item label="Apellido" name="lastname" rules={[{ required: true, message: 'Este campo es requerido' }]}>
          <Input />
        </Form.Item>

        <Form.Item name="role" label="Rol" rules={[{ required: true, message: 'Debe seleccionar un rol' }]}>
          <Select
            placeholder="Seleccione un rol"
          >
            <Option value="admin">Administrador</Option>
            <Option value="user">Usuario</Option>
          </Select>
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