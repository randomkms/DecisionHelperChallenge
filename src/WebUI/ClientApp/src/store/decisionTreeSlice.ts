import { DecisionApi } from 'src/api';
import { createAsyncThunk, createSlice, type PayloadAction } from '@reduxjs/toolkit';

export type DecisionNode = Readonly<{
  id: string;
  question: string;
  answer: string;
  result: string;
  children: DecisionNode[]
}>;

export type DecisionTreeState = Readonly<{
  isLoading: boolean;
  treeRoot: DecisionNode | null;
  visitedNodesIds: string[];
}>;

const initialState: DecisionTreeState = {
  isLoading: true,
  treeRoot: null,
  visitedNodesIds: []
};

export const decisionTreeSlice = createSlice({
  name: 'decisionTree',
  initialState,
  reducers: {
    getDecisionTree: (state, action: PayloadAction<DecisionNode>) => {
      state.isLoading = false;
      state.treeRoot = action.payload;
    },
    setFirstVisitedNodeId: (state, action: PayloadAction<string>) => {
      state.visitedNodesIds = [action.payload];
    },
    addVisitedNodeId: (state, action: PayloadAction<string>) => {
      state.visitedNodesIds.push(action.payload);
    }
  }
});

export const getDecisionTreeAsync = createAsyncThunk(
  'decision/getDecisionTreeAsync',
  async (treeName: string, { dispatch }) => {
    try {
      const treeRoot = await DecisionApi.getDecisionTreeAsync(treeName);
      dispatch(getDecisionTree(treeRoot));
    } catch (e) {
      console.error(e);
    }
  }
);

export const { getDecisionTree, setFirstVisitedNodeId, addVisitedNodeId } = decisionTreeSlice.actions;

export default decisionTreeSlice.reducer;