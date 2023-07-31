import React from 'react';
import { Divider, Layout } from 'antd';
import SideMenu from './SideMenu';
import './layoutstyles.css';
import { useAuthStore } from '../../store/authStore';

const { Content, Footer } = Layout;

type Props = {
  children: JSX.Element;
}

const AppLayout: React.FC<Props> = ({children}) => {

  const { userInfo } = useAuthStore();
  return (
    <Layout>
      {userInfo && <SideMenu />}
      <Layout>
        <section className='title'>
          <h1>YourBuddy Push</h1>
          <Divider />
        </section>
        <Content className='mainContent'>
          {children}
        </Content>
        <Footer style={{ textAlign: 'center' }}>{`YourBuddy App ©${new Date().getFullYear()} Created with ♥️ by Luis Richmond`}</Footer>
      </Layout>
    </Layout>
  );
};

export default AppLayout;