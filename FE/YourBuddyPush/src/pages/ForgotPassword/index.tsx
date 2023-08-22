import React, { useState } from 'react';
import { Button, Form, Input, Spin } from 'antd';
import { ForgotPassword } from '../../serverCalls/users';
import { useNavigate } from 'react-router-dom';

const ForgotPasswordComponent: React.FC = () => {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const onFinish = (values: any) => {
    setLoading(true);
    ForgotPassword(values.email).then(v => {
      navigate('/');
    }).finally(()=>setLoading(false))
  };

  return (
  <Spin spinning={loading} delay={0}>
    <Form
      name="basic"
      labelCol={{ span: 8 }}
      style={{ maxWidth: 600 }}
      onFinish={onFinish}
    >
      <Form.Item
        label="Email"
        name="email"
        rules={[{ required: true, message: 'Por favor ingrese su email' }]}
      >
        <Input />
      </Form.Item>

      <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
        <Button type="primary" htmlType="submit">
          Reestablecer contraseña
        </Button>
      </Form.Item>
    </Form>
  </Spin>)
};

export default ForgotPasswordComponent;
