import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { DataProvider } from './context/DataContext';
import './index.css';
import App from './App.jsx';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <DataProvider>
            <App />
        </DataProvider>
  </StrictMode>
)
