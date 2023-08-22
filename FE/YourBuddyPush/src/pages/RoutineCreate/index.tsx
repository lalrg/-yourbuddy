import { Button, Col, Form, Input, Row, Spin } from 'antd';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { LeftOutlined, CheckOutlined } from '@ant-design/icons';
import './styles.css';
import { ExerciseInformation } from '../../shared/types/exerciseInformation';
import { CreateRoutine } from '../../serverCalls/routines';

const ExerciseCreate: React.FC = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();

  const onFinish = async (values: ExerciseInformation) => {
    setLoading(true);
    const id = (await CreateRoutine(values.name)).data;
    setLoading(false);
    navigate(`/routines/${id}`);
  };

  return (
    <Spin spinning={loading} delay={0}> 
      <h2 style={{textAlign:'center'}}>Crear rutina</h2>
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
        <Form.Item label="Nombre" name="name" 
          rules={[
            { required: true, message: 'Este campo es requerido' }
          ]}
        >
          <Input />
        </Form.Item>
        <p>En los siguientes pasos podra agregar los ejercicios a la rutina</p>

        <Form.Item label=" ">
          <Row gutter={64} justify={'center'}>
            <Col xs={24} md={12} className='EditButton'>
              <Button type='default' icon={<LeftOutlined />} onClick={() => navigate('/myroutines')}>
                Ir a mis rutinas
              </Button>
            </Col>
            <Col xs={24} md={12} className='EditButton'>
              <Button type='primary' icon={<CheckOutlined />} htmlType="submit">
                Crear rutina
              </Button>
            </Col>
          </Row>
        </Form.Item>
      </Form>
    


    </Spin>
  )
};

export default ExerciseCreate;