import { DecisionApi } from 'src/api';
import { createAsyncThunk, createSlice, type PayloadAction } from '@reduxjs/toolkit';

export type DecisionTreesState = Readonly<{
  decisionTreesNames: string[];
}>;

const initialState: DecisionTreesState = {
  decisionTreesNames: []
};

export const decisionTreesSlice = createSlice({
  name: 'decisionTrees',
  initialState,
  reducers: {
    getDecisionTrees: (state, action: PayloadAction<string[]>) => {
      state.decisionTreesNames = action.payload;
    }
  }
});

export const getDecisionTreesAsync = createAsyncThunk(
  'decision/getDecisionTreesAsync',
  async (_, { dispatch }) => {
    try {
      const treesNames = await DecisionApi.getDecisionTreesAsync();
      dispatch(getDecisionTrees(treesNames));
    } catch (e) {
      console.error(e);
    }
  }
);

export const { getDecisionTrees } = decisionTreesSlice.actions;

export default decisionTreesSlice.reducer;