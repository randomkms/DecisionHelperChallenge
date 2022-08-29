import { DecisionApi } from 'src/api';
import { createAsyncThunk, createSlice, type PayloadAction } from '@reduxjs/toolkit';

export type DecisionTreeInfo = Readonly<{
  name: string;
  description: string;
  imageUrl: string;
}>;

export type DecisionTreesState = Readonly<{
  decisionTreesInfos: DecisionTreeInfo[];
}>;

const initialState: DecisionTreesState = {
  decisionTreesInfos: []
};

export const decisionTreesSlice = createSlice({
  name: 'decisionTrees',
  initialState,
  reducers: {
    getDecisionTrees: (state, action: PayloadAction<DecisionTreeInfo[]>) => {
      state.decisionTreesInfos = action.payload;
    }
  }
});

export const getDecisionTreesAsync = createAsyncThunk(
  'decision/getDecisionTreesAsync',
  async (_, { dispatch }) => {
    try {
      const treesInfos = await DecisionApi.getDecisionTreesAsync();
      dispatch(getDecisionTrees(treesInfos));
    } catch (e) {
      console.error(e);
    }
  }
);

export const { getDecisionTrees } = decisionTreesSlice.actions;

export default decisionTreesSlice.reducer;