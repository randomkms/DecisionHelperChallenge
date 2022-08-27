import type { ComponentType } from 'react';
import type { Params } from 'react-router';
import { Login, Dashboard, FetchData, Form, DecisionMaker } from '../containers';
import type { IconProp } from '@fortawesome/fontawesome-svg-core';
import DecisionTree from 'src/containers/DecisionTree';

export const TRANSITION_DEFAULT = {
  classNames: 'fade',
  timeout: { enter: 250, exit: 250 }
};

export type RouteComponent = ComponentType<any>;
export type TransitionMetaData = typeof TRANSITION_DEFAULT;

export type Route = Readonly<{
  name: string;
  path: string;
  icon?: IconProp;
  showInNav?: boolean;
  Component: RouteComponent;
  transition: TransitionMetaData;
  params?: Readonly<Params<string>>;
}>;

export const Routes: Route[] = [
  {
    path: '/',
    icon: 'sign-out-alt',
    name: 'Logout',
    Component: Login,
    transition: TRANSITION_DEFAULT
  },
  {
    path: '/form',
    showInNav: true,
    name: 'Form',
    Component: Form,
    transition: {
      classNames: 'page-slide-left',
      timeout: { enter: 350, exit: 250 }
    }
  },
  {
    showInNav: true,
    path: '/home',
    name: 'Home',
    Component: Dashboard,
    transition: TRANSITION_DEFAULT
  },
  {
    showInNav: true,
    name: 'Fetch',
    path: '/fetch/:startDateIndex',
    Component: FetchData,
    transition: {
      classNames: 'page-slide-right',
      timeout: { enter: 350, exit: 250 }
    },
    params: {
      startDateIndex: '0'
    }
  },
  {
    showInNav: true,
    name: 'Decision Maker',
    path: '/decisionTrees',
    Component: DecisionMaker,
    transition: {
      classNames: 'page-slide-right',
      timeout: { enter: 350, exit: 250 }
    }
  },
  {
    showInNav: false,
    name: 'DecisionChooser',
    path: '/decisionTrees/:treeName',
    Component: DecisionMaker,
    transition: {
      classNames: 'page-slide-right',
      timeout: { enter: 350, exit: 250 }
    }
  },
  {
    showInNav: false,
    name: 'DecisionTreeResult',
    path: '/decisionTreeResult/:treeName',
    Component: DecisionTree,
    transition: {
      classNames: 'page-slide-right',
      timeout: { enter: 350, exit: 250 }
    }
  }
];
