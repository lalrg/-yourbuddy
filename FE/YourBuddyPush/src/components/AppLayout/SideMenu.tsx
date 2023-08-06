import React from 'react';
import { Menu } from "antd"
import Sider from "antd/es/layout/Sider"
import { 
  UserOutlined, 
  LogoutOutlined, 
  FireOutlined,
  BarsOutlined,
  SmileOutlined,
  SettingOutlined
} from '@ant-design/icons';
import { useJWT } from '../../hooks/useJWT';
import { useNavigate } from 'react-router-dom';
import { useAuthStore } from '../../store/authStore';

type MenuOptionsType = {
  icon: any;
  text: string;
  onClick?: (() => void);
  role: string;
}

const SideMenu: React.FC = () => {
  const { LogOut } = useJWT();
  const navigate = useNavigate();
  const { userInfo } = useAuthStore();
  const MenuOptions: Array<MenuOptionsType> = [
    {
      icon: UserOutlined,
      text: 'Usuarios',
      onClick: ()=> navigate('/users'),
      role: 'admin'
    },
    {
      icon: SmileOutlined,
      text: 'Ejercicios',
      onClick: ()=> navigate('/exercises'),
      role: 'admin'
    },
    {
      icon: BarsOutlined,
      text: 'Todas las Rutinas',
      onClick: ()=> navigate('/routines'),
      role: 'admin'
    },
    {
      icon: BarsOutlined,
      text: 'Mis Rutinas',
      onClick: ()=> navigate('/myroutines'),
      role: 'user'
    },
    {
      icon: FireOutlined,
      text: 'Todas las Sesiones',
      onClick: ()=> navigate('/sessions'),
      role: 'admin'
    },
    {
      icon: FireOutlined,
      text: 'Mis Sesiones',
      onClick: ()=> navigate('/mysessions'),
      role: 'user'
    },
    {
      icon: SettingOutlined,
      text: 'Ajustes',
      onClick: ()=> navigate('/settings'),
      role: 'user'
    },
    {
      icon: LogoutOutlined,
      text: 'Cerrar sesion',
      onClick: LogOut,
      role: 'user'
    }
  ];

  return (
    <Sider
      defaultCollapsed={false}
      collapsible={true}
      collapsedWidth="0"
      theme='light'
      style={{minHeight:'100vh'}}
    >
      <Menu
        mode="inline"
        items={
          MenuOptions
          .filter(
            e=> e.role == 'user' || e.role == userInfo?.roles
          )
          .map(
          (opt) => ({
            key: opt.text,
            icon: React.createElement(opt.icon),
            label: opt.text,
            onClick: opt.onClick,
          }),
        )}
      />
    </Sider>)
  }

export default SideMenu;