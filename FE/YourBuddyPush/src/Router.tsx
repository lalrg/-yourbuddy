import React, { useEffect, useMemo, useState } from 'react';
import { Route, Routes, useLocation } from 'react-router-dom';
import { useJWT } from './hooks/useJWT';
import LoginPage from './pages/LoginPage';
import { useAuthStore } from './store/authStore';
import Users from './pages/Users';
import UserEdit from './pages/UserEdit';
import UserCreate from './pages/UserCreate';
import Exercises from './pages/Exercises';

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
    element: () => <h2>routines 😁</h2>,
    path: '/routines',
    role: 'admin'
  },
  {
    element: () => <h2>My routines 😁</h2>,
    path: '/myroutines',
    role: 'user'
  },
  {
    element: () => <h2>sessions 😁</h2>,
    path: '/sessions',
    role: 'admin'
  },
  {
    element: () => <h2>my sessions 😁</h2>,
    path: '/mysessions',
    role: 'user'
  },
  {
    element: () => <h2>Home page 😁</h2>,
    path: '/',
    role: 'user'
  },
  {
    element: () => <h2>Settings 😁</h2>,
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