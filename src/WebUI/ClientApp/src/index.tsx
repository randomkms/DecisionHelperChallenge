import { Provider } from 'react-redux';
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import './assets/style/scss/site.scss';
import { store } from './store';
import { ToastContainer } from 'react-toastify';
import { toastifyProps, registerIcons } from './config';
import * as serviceWorkerRegistration from './serviceWorkerRegistration';

registerIcons();

const container = document.getElementById('root');
const root = createRoot(container as HTMLElement);

function AppRenderer() {
  return (
    <>
      <BrowserRouter>
        <Provider store={store}>
          <App />
        </Provider>
      </BrowserRouter>
      <ToastContainer {...toastifyProps} />
    </>
  );
}

root.render(<AppRenderer />);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister();