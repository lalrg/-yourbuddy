import { Button, Col, Form, Input, Row, Select, Spin } from 'antd';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { LeftOutlined, CheckOutlined } from '@ant-design/icons';
import './styles.css';
import { ExerciseInformation } from '../../shared/types/exerciseInformation';
import { GetExerciseById, UpdateExercise } from '../../serverCalls/exercises';

const ExerciseEdit: React.FC = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();
  const { Option } = Select;
  const { id } = useParams();
  const [exerciseData, setExerciseData] = useState<ExerciseInformation>(); // just in case it is needed at some point

  const onFinish = async (values: ExerciseInformation) => {
    setLoading(true);
    await UpdateExercise(id ?? values.exerciseId, values.name, values.type, values.description, values.imageUrl, values.videoUrl);
    setLoading(false);
    navigate('/exercises');
  };

  useEffect(
    () => {
      if(!id)
        return;

        GetExerciseById(id)
        .then(
          r=> {
            console.log(r.data)
            setExerciseData(r.data);
            form.setFieldsValue({
              name: r.data.name,
              description: r.data.description,
              imageUrl: r.data.imageUrl || 'no definido',
              type: r.data.type
            });
          }
        ) 
        .finally(
          () => {
            setLoading(false);
          }
        );
    },
    [id, setExerciseData, setLoading, form, navigate]
  );

  return (
    <Spin spinning={loading} delay={0}> 
      <h2 style={{textAlign:'center'}}>Modificar ejercicio {exerciseData?.name || id}</h2>
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

export default ExerciseEdit;