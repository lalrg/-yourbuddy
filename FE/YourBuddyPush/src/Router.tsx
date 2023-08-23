import React, { useEffect, useMemo, useState } from 'react';
import { Route, Routes, useLocation } from 'react-router-dom';
import { useJWT } from './hooks/useJWT';
import LoginPage from './pages/LoginPage';
import { useAuthStore } from './store/authStore';
import Users from './pages/Users';
import UserEdit from './pages/UserEdit';
import UserCreate from './pages/UserCreate';
import Exercises from './pages/Exercises';
import ExerciseCreate from './pages/ExerciseCreate';
import ExerciseEdit from './pages/ExerciseEdit';
import Routines from './pages/Routines';
import RoutineCreate from './pages/RoutineCreate';
import RoutineViewEdit from './pages/RoutineViewEdit';
import ForgotPasswordComponent from './pages/ForgotPassword';
import UpdatePassword from './pages/UpdatePassword';
import MySessions from './pages/MySessions';
import SessionViewEdit from './pages/SessionViewEdit';

const AppRoutes: Array<{path: string, element: React.FC, role: string}> = [
  {
    element: () => <Users/>,
    path: '/users',
    role: 'admin'
  },
  {
    element: () => <UserEdit />,
    path: '/users/:id',
    role: 'admin'
  },
  {
    element: () => <UserCreate />,
    path: '/users/create',
    role: 'admin'
  },
  {
    element: () => <Exercises />,
    path: '/exercises',
    role: 'admin'
  },
  {
    element: () => <ExerciseCreate />,
    path: '/exercises/Create',
    role: 'admin'
  },
  {
    element: () => <ExerciseEdit />,
    path: '/exercises/:id',
    role: 'admin'
  },
  {
    element: () => <Routines type='all'/>,
    path: '/routines',
    role: 'admin'
  },
  {
    element: () => <RoutineCreate />,
    path: '/routines/create',
    role: 'admin'
  },
  {
    element: () => <RoutineViewEdit />,
    path: '/routines/:id',
    role: 'admin'
  },
  {
    element: () => <Routines type='mine' />,
    path: '/myroutines',
    role: 'user'
  },
  {
    element: () => <MySessions />,
    path: '/mysessions',
    role: 'user'
  },
  {
    element: () => <SessionViewEdit />,
    path: '/mysessions/:id',
    role: 'user'
  },
  {
    element: () => <Routines type='mine' />,
    path: '/',
    role: 'user'
  },
  {
    element: () => <UpdatePassword />,
    path: '/settings',
    role: 'user'
  },
  {
    element: () => <h2>Esta pagina no existe 😔</h2>,
    path: '*',
    role: 'user'
  },

];

const Router: React.FC = ()=>{
  const location = useLocation();

  const [displayLocation, setDisplayLocation] = useState(location);
  const [transitionStage, setTransistionStage] = useState("fadeIn");

  useEffect(() => {
    if (location !== displayLocation) setTransistionStage("fadeOut");
  }, [location, displayLocation]);
  
  const { isLoggedIn } = useJWT();
  const { userInfo } = useAuthStore();

  const validRoutes = useMemo(()=>{
    if(!isLoggedIn) return;
    return AppRoutes.filter(
      r => (r.role === userInfo?.roles || userInfo?.roles === 'admin')
    )
  }, [isLoggedIn, userInfo?.roles]);

  if (!isLoggedIn) {
    return (
      <Routes>
        <Route path="/forgotpassword" element={<ForgotPasswordComponent />} />
        <Route path="*" element={<LoginPage/>} />
      </Routes>
    )
  }
  
  return (
    <div
      className={`${transitionStage}`}
      onAnimationStartCapture={() => {
        if (transitionStage === "fadeOut") {
          setTransistionStage("fadeIn");
          setDisplayLocation(location);
        }
      }}
    >
      <Routes>
        {
          validRoutes?.map(
            r=> (<Route path={r.path} element={<r.element/>} key={r.path} />)
          )
        }
      </Routes>
    </div>
  )
}

export default Router;