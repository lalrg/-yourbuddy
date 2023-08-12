import { Button, Col, Form, Input, Row, Select, Spin } from 'antd';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { LeftOutlined, CheckOutlined } from '@ant-design/icons';
import './styles.css';
import { ExerciseInformation } from '../../shared/types/exerciseInformation';
import { CreateExercise } from '../../serverCalls/exercises';

const ExerciseCreate: React.FC = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();
  const { Option } = Select;

  const onFinish = async (values: ExerciseInformation) => {
    setLoading(true);
    await CreateExercise(values.name, values.type, values.description, values.imageUrl, values.videoUrl);
    setLoading(false);
    navigate('/exercises');
  };

  return (
    <Spin spinning={loading} delay={0}> 
      <h2 style={{textAlign:'center'}}>Crear ejercicio</h2>
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

        <Form.Item label="Descripcion" name="description" rules={[{ required: true, message: 'Este campo es requerido' }]}>
          <Input />
        </Form.Item>

        <Form.Item label="URL de la imagen" name="imageUrl" rules={[{ required: true, message: 'Este campo es requerido' }]}>
          <Input />
        </Form.Item>

        <Form.Item label="URL del video" name="videoUrl" rules={[{ required: true, message: 'Este campo es requerido' }]}>
          <Input />
        </Form.Item>

        <Form.Item name="type" label="Tipo de ejercicio" rules={[{ required: true, message: 'Debe seleccionar un tipo de ejercicio' }]}>
          <Select
            placeholder="Seleccione un tipo"
          >
            <Option value="weight">Peso</Option>
            <Option value="time">Tiempo</Option>
          </Select>
        </Form.Item>

        <Form.Item label=" ">
          <Row gutter={64} justify={'center'}>
            <Col xs={24} md={12} className='EditButton'>
              <Button type='default' icon={<LeftOutlined />} onClick={() => navigate('/exercises')}>
                Regresar a lista de ejercicios
              </Button>
            </Col>
            <Col xs={24} md={12} className='EditButton'>
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

export default ExerciseCreate;