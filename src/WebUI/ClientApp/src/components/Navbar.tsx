import { Routes as routes } from '../config';
import type { FunctionComponent } from 'react';
import { NavLink, generatePath } from 'react-router-dom';
import LogoSrc from '../assets/image/lobsterLogo.png';

const Navbar: FunctionComponent = () => {
  return (
    <nav
      role="navigation"
      className="navbar"
      aria-label="main navigation"
    >
      <div className="navbar-wrapper">
        <div className="brand-wrapper">
          <img src={LogoSrc} style={{height: "3rem"}} />
          <p className="title is-1 has-text-white">Lobster Inc</p>
        </div>
        <div className="navbar-routes">
          {
            routes
              .filter(({ showInNav }) => showInNav)
              .map(({ path, name, params }) => (
                <NavLink
                  key={name}
                  to={generatePath(path, params)}
                  className={({ isActive }) => 'navbar-item' + (isActive ? ' is-active' : '')}
                >
                  {name}
                </NavLink>
              ))}
        </div>
      </div>
    </nav>
  );
};

export default Navbar;