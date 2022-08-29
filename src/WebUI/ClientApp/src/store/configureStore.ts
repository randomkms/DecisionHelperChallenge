import decisionReducer from './decisionSlice';
import decisionTreeReducer from './decisionTreeSlice';
import decisionTreesReducer from './decisionTreesSlice';

import { configureStore } from '@reduxjs/toolkit'

export const store = configureStore({
  reducer: {
    decision: decisionReducer,
    decisionTree: decisionTreeReducer,
    decisionTrees: decisionTreesReducer
  }
});

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;

// Inferred type: {auth: AuthState, form: FormState, weather: WeatherState}
export type AppDispatch = typeof store.dispatch;