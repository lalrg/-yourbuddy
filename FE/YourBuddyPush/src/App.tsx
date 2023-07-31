import './App.css'
import AppLayout from './components/AppLayout';

import { BrowserRouter } from 'react-router-dom';
import Router from './Router';

function App() {
  return (
    <BrowserRouter>
      <AppLayout>
        <Router/>
      </AppLayout>
    </BrowserRouter>
  )
}

export default App
