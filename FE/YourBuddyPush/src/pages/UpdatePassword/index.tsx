import { Button, Col, Form, Input, Row, Spin } from 'antd';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { CheckOutlined } from '@ant-design/icons';
import './styles.css';
import { CreateUser, UpdatePassword } from '../../serverCalls/users';

const UpdatePasswordComponent: React.FC = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();

  const onFinish = async (values: { newPassword: string, currentPassword: string }) => {
    setLoading(true);
    console.log(values)
    await UpdatePassword(values.newPassword, values.currentPassword)
    setLoading(false);
    navigate('/myroutines');
  };

  return (
    <Spin spinning={loading} delay={0}> 
      <h2 style={{textAlign:'center'}}>Actualizar contraseña</h2>
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
        <Form.Item label="Contraseña actual" name="currentPassword" 
          rules={[
            { required: true, message: 'Este campo es requerido' }, 
          ]}
        >
          <Input type='password'/>
        </Form.Item>

        <Form.Item label="Nueva contraseña" name="newPassword" rules={[{ required: true, message: 'Este campo es requerido' }]}>
          <Input type='password' />
        </Form.Item>

        <Form.Item label=" ">
          <Row gutter={64} justify={'center'}>
            <Col xs={24} className='userEditButton'>
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

export default UpdatePasswordComponent;