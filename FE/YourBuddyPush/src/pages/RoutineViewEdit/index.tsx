import { AutoComplete, Button, Col, Collapse, Divider, Form, Input, Row, Select, Space, Spin, Tag } from 'antd';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { LeftOutlined } from '@ant-design/icons';
import './styles.css';
import { AddExerciseToRoutine, AssignToUser, Duplicate, GetRoutineById, RemoveExerciseFromRoutine, UpdateName } from '../../serverCalls/routines';
import { RoutineInformation } from '../../shared/types/routineInformation';
import NoData from '../../components/NoData';
import { useAuthStore } from '../../store/authStore';
import { PlusOutlined } from '@ant-design/icons';
import { GetAllExercises } from '../../serverCalls/exercises';
import { GetAll } from '../../serverCalls/users';
import { ExerciseInformation } from '../../shared/types/exerciseInformation';
import { UserInformation } from '../../shared/types/userInformation';

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

const UserCreate: React.FC = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [routineData, setRoutineData] = useState<RoutineInformation>();
  const [loading, setLoading] = useState(true);
  const { userInfo } = useAuthStore();
  const [addExerciseInfo, setAddExerciseInfo] = useState<AddExerciseInfo>({ exerciseId: '', reps: '0', series:'0', type:'weight', weight:'0' });
  const [exercisesList, setExercisesList] = useState<Array<ExerciseInformation>>();
  const [usersList, setUsersList] = useState<Array<UserInformation>>();
  const [userAssignedId, setUserAssignedId] = useState<string>();

  const updateRoutineName = (name: string) => {
    setLoading(true);
    UpdateName(id ?? '', name).then(
      () => {
        setLoading(false);
        routineData && setRoutineData({...routineData, name});
      }
    );
  }

  const removeExerciseFromRoutine = (exerciseId: string) => {
    setLoading(true);
    RemoveExerciseFromRoutine(id ?? '', exerciseId).then(() => {
      setLoading(false)
      routineData && setRoutineData(
        {
          ...routineData,
          exercises: routineData.exercises.filter(e => e.exerciseId !== exerciseId)
        }
      );
    }
    );
  }

  const addExercise = async () => {
    setLoading(true);

    await AddExerciseToRoutine(addExerciseInfo.exerciseId, id ?? '', addExerciseInfo.reps, addExerciseInfo.weight, addExerciseInfo.series)
    const r =await GetRoutineById(id ?? "")
    if(r.data.assignedToName) {
      setUserAssignedId(r.data.assignedTo);
    }
    
    setRoutineData(r.data);
    setAddExerciseInfo({ exerciseId: '', reps: '0', series:'0', type:'weight', weight:'0' });

    setLoading(false);
  }

  useEffect(() => {
    setLoading(true);
    GetRoutineById(id ?? "").then(
      (r) => {
        setRoutineData(r.data);
        if(r.data.assignedToName) {
          setUserAssignedId(r.data.assignedTo);
        }
      }
    ).then( () =>
    GetAllExercises().then(
        r => {
          setExercisesList(r.data);
        }
      )
    ).then( () => {
      GetAll().then(
        r => {
          setUsersList(r.data);
          setLoading(false);
        }
      )
    })
  }, [id, setLoading, setRoutineData, setExercisesList]);

  const assignRoutineToUser = async () => {
    setLoading(true);
    await AssignToUser(id?? '', userAssignedId ?? '');
    setLoading(false);
  }

  const duplicateRoutine = async () => {
    setLoading(true);
    const result = await Duplicate(id ?? '');
    setLoading(false);
    navigate(`/routines/${result.data}`);
  }

  return (
    <Spin spinning={loading} delay={0}>
    {userInfo?.roles == 'admin' && (
      <h3>Vista de usuarios no administradores:</h3>
    )}

      <h2 style={{textAlign:'center'}}>{routineData?.name ?? id }</h2>
      <h3>Ejercicios</h3>
      {!routineData?.exercises.length && <NoData />}
      {
        routineData?.exercises.length && (
          <Collapse items={
            routineData.exercises.map((e) => (
              { 
                label: e.name,
                key: e.exerciseId,
                children: 
                  <>
                    <p>{e.description}</p>
                    <p>{e.setsDescription}</p>
                    <img src={e.imageUrl} width={300} height="auto" />
                  </>,
              }
            )
            )
          } />
        )
      }      
      <br />
      
      {userInfo?.roles == 'admin' && (
        <>
          <h3>Acciones administrativas:</h3>
          <h4>Editar el siguiente texto y presionar enter para cambiar el nombre de la rutina</h4>
          <Input value={routineData?.name} onPressEnter={
            (e: any) => {
              updateRoutineName(e.target.value);
            }} 
            onChange={(e)=> {
              routineData && setRoutineData({ ...routineData, name: e.target.value })
            }}
            />
          <br />
          <Divider />
          <h4>Remover ejercicios:</h4>
          <ul>
            {routineData?.exercises.map(
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
      <Row>
        <Col xs={24}>
          <h4>Asignar a usuario:</h4>
        </Col>
        <Col xs={24}>
          <p>Seleccionar Usuario</p>
          <Select
            style={{ width: 200 }}
            value={userAssignedId}
            options={
              usersList?.map(e=> ({
                label: `${e.name} ${e.lastName}`,
                value: e.id
              }))
            }
            placeholder="Buscar usuario por nombre o apellido"
            onChange={(c)=> {
              setUserAssignedId(c)
            }}
            filterOption={(inputValue, option) =>
              option?.label.toUpperCase().indexOf(inputValue.toUpperCase()) !== -1
            }
            showSearch
          />
        </Col>
        <Col xs={24} style={{marginTop: '20px'}}>
          <Button type='primary' onClick={assignRoutineToUser}>
            Guardar usuario seleccionado
          </Button>
        </Col>
      </Row>
      
      <Divider />
      <Form.Item>
        <Row gutter={64} justify={'center'}>
          <Col xs={24} md={12} className='userEditButton'>
            <Button type='default' icon={<LeftOutlined />} onClick={() => navigate('/myroutines')}>
              Ir a mis rutinas
            </Button>
          </Col>
          <Col xs={24} md={12} className='userEditButton'>
            <Button type='primary' onClick={duplicateRoutine}>
              + Duplicar rutina
            </Button>
          </Col>
        </Row>
      </Form.Item>
      

    </Spin>
  )
};

export default UserCreate;