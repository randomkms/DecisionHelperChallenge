import { Routes as routes } from '../config';
import type { FunctionComponent } from 'react';
import { NavLink, generatePath } from 'react-router-dom';

const Navbar: FunctionComponent = () => {
  return (
    <nav
      role="navigation"
      className="navbar"
      aria-label="main navigation"
    >
      <div className="navbar-wrapper">
        <div className="brand-wrapper">
        <p className="title is-1 has-text-white">Lobster Inc</p>
        </div>
        <div className="navbar-routes">
          {/* isLoggedIn && */
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