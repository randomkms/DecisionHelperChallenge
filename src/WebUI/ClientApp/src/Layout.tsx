import { Footer, Navbar } from './components';
import { Fragment, type FunctionComponent, type PropsWithChildren } from 'react';

type LayoutProps = PropsWithChildren<unknown>;

const Layout: FunctionComponent<LayoutProps> = ({ children }) => (
  <Fragment>
    <Navbar />
    {children}
    <Footer />
  </Fragment>
);

export default Layout;