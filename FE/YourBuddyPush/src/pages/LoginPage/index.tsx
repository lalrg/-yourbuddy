import React, { useState } from 'react';
import { Button, Form, Input, Spin } from 'antd';
import { Login } from '../../serverCalls/login';
import { useAuthStore } from '../../store/authStore';
import { LOCALSTORAGE_TOKEN_KEY } from '../../shared/constants';
import { decodeToken } from '../../shared/security';

const LoginPage: React.FC = () => {
  const { login } = useAuthStore();
  const [loading, setLoading] = useState(false);

  const onFinish = (values: any) => {
    setLoading(true);
    Login(values.email, values.password).then(v => {
      const jsonParsed = decodeToken(v.data.token);
      localStorage.setItem(LOCALSTORAGE_TOKEN_KEY, v.data.token);

      login(jsonParsed.email, jsonParsed.name, jsonParsed.roles, jsonParsed.exp, v.data.token);
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
        rules={[{ required: true, message: 'Por favor ingrese un email' }]}
      >
        <Input />
      </Form.Item>

      <Form.Item
        label="Contraseña"
        name="password"
        rules={[{ required: true, message: 'Por favor ingrese su contraseña' }]}
      >
        <Input.Password />
      </Form.Item>

      <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
        <Button type="primary" htmlType="submit">
          Acceder
        </Button>
      </Form.Item>
    </Form>
  </Spin>)
};

export default LoginPage;
