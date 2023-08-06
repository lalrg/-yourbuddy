import { Button, Col, Form, Input, Row, Select, Spin } from 'antd';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { LeftOutlined, CheckOutlined } from '@ant-design/icons';
import './styles.css';
import { CreateUser } from '../../serverCalls/users';

const UserCreate: React.FC = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();
  const { Option } = Select;

  const onFinish = async (values: { email: string, name: string, lastname: string, role: string }) => {
    setLoading(true);
    await CreateUser(values.name, values.lastname, values.email, values.role);
    setLoading(false);
    navigate('/users');
  };

  return (
    <Spin spinning={loading} delay={0}> 
      <h2 style={{textAlign:'center'}}>Crear usuario</h2>
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
};

export default UserCreate;