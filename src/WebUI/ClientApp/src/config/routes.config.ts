import type { ComponentType } from 'react';
import type { Params } from 'react-router';
import { Dashboard, DecisionMaker } from '../containers';
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
    showInNav: true,
    path: '/',
    name: 'Home',
    Component: Dashboard,
    transition: TRANSITION_DEFAULT
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