import { Button, Col, Divider, Form, Input, Row, Select, Spin, Tag } from 'antd';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { LeftOutlined } from '@ant-design/icons';
import './styles.css';
import NoData from '../../components/NoData';
import { useAuthStore } from '../../store/authStore';
import { PlusOutlined } from '@ant-design/icons';
import { GetAllExercises } from '../../serverCalls/exercises';
import { ExerciseInformation } from '../../shared/types/exerciseInformation';
import { trainingSessionInformation } from '../../shared/types/trainingSessionInformation';
import { AddExercise, GetSingleSession, RemoveExercise } from '../../serverCalls/sessions';

type AddExerciseInfo = {
  exerciseId: string,
  type: string,
  reps: string,
  series: string,
  weight: string
}

const inputStyles = {
  maxWidth: '300px'
}

const SessionViewEdit: React.FC = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [sessionData, setSessionData] = useState<trainingSessionInformation>();
  const [loading, setLoading] = useState(true);
  const { userInfo } = useAuthStore();
  const [addExerciseInfo, setAddExerciseInfo] = useState<AddExerciseInfo>({ exerciseId: '', reps: '0', series:'0', type:'weight', weight:'0' });
  const [exercisesList, setExercisesList] = useState<Array<ExerciseInformation>>();

  const removeExerciseFromRoutine = (exerciseId: string) => {
    setLoading(true);
    RemoveExercise(exerciseId, id ?? '').then(() => {
      setLoading(false)
      sessionData && setSessionData(
        {
          ...sessionData,
          exercises: (sessionData?.exercises.filter(e => e.exerciseId !== exerciseId) ?? [])
        }
      );
    }
    );
  }

  const addExercise = async () => {
    setLoading(true);

    await AddExercise(addExerciseInfo.exerciseId, id ?? '', addExerciseInfo.reps, addExerciseInfo.weight, addExerciseInfo.series)
    const r =await GetSingleSession(id ?? "")
    
    setSessionData(r.data);
    setAddExerciseInfo({ exerciseId: '', reps: '0', series:'0', type:'weight', weight:'0' });

    setLoading(false);
  }

  useEffect(() => {
    setLoading(true);
    GetSingleSession(id ?? "").then(
      (r) => {
        setSessionData(r.data);
      }
    ).then( () =>
    GetAllExercises().then(
        r => {
          setExercisesList(r.data);
          setLoading(false);
        }
      )
    );
  }, [id, setLoading, setSessionData, setExercisesList]);

  return (
    <Spin spinning={loading} delay={0}>
    <h2 style={{textAlign:'center'}}>Sesion del {new Date(sessionData?.startTime ?? "").getDate()}/{new Date(sessionData?.startTime ?? "").getMonth()}/{new Date(sessionData?.startTime ?? "").getFullYear()}</h2>
    <h3>Ejercicios</h3>
    {!sessionData?.exercises.length && <NoData />}      
    {(userInfo?.roles == 'admin' && sessionData?.exercises.length) && (
      <>
        <Divider />
        <ul>
          {sessionData?.exercises.map(
            e=> 
            <li key={e.exerciseId}>
              {e.name} | { e.description?.substring(0, e.description.length > 30 ? 30 : e.description.length-1)+ '...' } &nbsp;&nbsp; 
              <Tag color="red" className='removeExercise' onClick={() => removeExerciseFromRoutine(e.exerciseId)}>Eliminar</Tag><br/>
              Descripcion: {e.setsDescription}<br/><br />
            </li>
          )}
        </ul>
      </>
    )}
      <h4>Agregar ejercicios:</h4>
      <Row gutter={[16, 16]}>
        <Col xs={24} md={12}>
          <p>Seleccionar ejercicio</p>
          <Select
            style={{ width: 200 }}
            value={addExerciseInfo.exerciseId}
            options={
              exercisesList?.map(e=> ({
                label: `${e.name} | ${e.description}`,
                value: e.exerciseId
              }))
            }
            placeholder="Buscar ejercicio por nombre o descripcion"
            onChange={(c)=> {
              setAddExerciseInfo(
                addExerciseInfo && { ...addExerciseInfo, exerciseId: c }
              )
            }}
            filterOption={(inputValue, option) =>
              option?.label.toUpperCase().indexOf(inputValue.toUpperCase()) !== -1
            }
            showSearch
          />
        </Col>
        <Col xs={24} md={12}>
          <p>Repeticiones</p>
          <Input style={inputStyles}
            value={addExerciseInfo.reps}
            onChange={(c)=> {
              setAddExerciseInfo(
                addExerciseInfo && { ...addExerciseInfo, reps: c.target.value}
              )
            }} type='number' />
        </Col>
        <Col xs={24} md={12}>
          <p>Series</p>
          <Input style={inputStyles}
            value={addExerciseInfo.series}
            onChange={(c)=> {
              setAddExerciseInfo(
                addExerciseInfo && { ...addExerciseInfo, series: c.target.value}
              )
            }} type='number' />
        </Col>
        <Col xs={24} md={12}>
          <p>Peso</p>
          <Input style={inputStyles}
            value={addExerciseInfo.weight}
            onChange={(c)=> {
              setAddExerciseInfo(
                addExerciseInfo && { ...addExerciseInfo, weight: c.target.value}
              )
            }} type='number' />
        </Col>

        <Col xs={24}>
          <Button type="primary" onClick={addExercise} icon={<PlusOutlined />}>
            Agregar ejercicio
          </Button>
        </Col> 
      </Row>
      <Divider />
      
      <Divider />
      <Form.Item>
        <Row gutter={64} justify={'center'}>
          <Col xs={24} md={12} className='userEditButton'>
            <Button type='default' icon={<LeftOutlined />} onClick={() => navigate('/mysessions')}>
              Ir a mis sesiones
            </Button>
          </Col>
        </Row>
      </Form.Item>
      

    </Spin>
  )
};

export default SessionViewEdit;