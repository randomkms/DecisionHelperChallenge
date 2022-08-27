import { DecisionApi } from 'src/api';
import { createAsyncThunk, createSlice, type PayloadAction } from '@reduxjs/toolkit';
import { addVisitedNodeId, setFirstVisitedNodeId } from './decisionTreeSlice';

export type Decision = Readonly<{
  id: string;
  question: string;
  result: string;
  possibleAnswers: PossibleAnswer[];
}>;

export type PossibleAnswer = Readonly<{
  id: string;
  answer: string;
}>;

export type DecisionState = Readonly<{
  isLoading: boolean;
  currentDecision: Decision | null;
}>;

const initialState: DecisionState = {
  isLoading: true,
  currentDecision: null
};

export const decisionSlice = createSlice({
  name: 'decision',
  initialState,
  reducers: {
    getFirstDecision: (state, action: PayloadAction<Decision>) => {
      state.isLoading = false;
      state.currentDecision = action.payload;
    },
    getDecisionById: (state, action: PayloadAction<Decision>) => {// TODO mb add setLoading or something like it
      state.isLoading = false;
      state.currentDecision = action.payload;
    }
  }
});

export const getFirstDecisionAsync = createAsyncThunk(
  'decision/getFirstDecisionAsync',
  async (treeName: string, { dispatch }) => {
    try {
      const firstDecision = await DecisionApi.getFirstDecisionAsync(treeName);
      dispatch(getFirstDecision(firstDecision));
      dispatch(setFirstVisitedNodeId(firstDecision.id));
    } catch (e) {
      console.error(e);
    }
  }
);

export const getDecisionByIdAsync = createAsyncThunk(
  'decision/getDecisionByIdAsync',
  async (decisionId: string, { dispatch }) => {
    try {
      const decision = await DecisionApi.getDecisionByIdAsync(decisionId);
      dispatch(getDecisionById(decision));
      dispatch(addVisitedNodeId(decision.id));
    } catch (e) {
      console.error(e);
    }
  }
);

export const { getFirstDecision, getDecisionById } = decisionSlice.actions;

export default decisionSlice.reducer;