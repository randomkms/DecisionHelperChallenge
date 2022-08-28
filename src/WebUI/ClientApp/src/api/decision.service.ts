import { BaseService } from './base.service';
import type { Decision } from 'src/store/decisionSlice';
import type { DecisionNode } from 'src/store/decisionTreeSlice';

class DecisionService extends BaseService {
  private static _decisionService: DecisionService;
  private static _controller: string = 'DecisionTree';

  private constructor(name: string) {
    super(name);
  }

  public static get Instance(): DecisionService {
    return this._decisionService || (this._decisionService = new this(this._controller));
  }

  public async getFirstDecisionAsync(treeName: string): Promise<Decision> {
    const url = `firstDecision?treeName=${treeName}`;
    const { data } = await this.$http.get<Decision>(url);

    return data;
  }

  public async getDecisionByIdAsync(decisionId: string): Promise<Decision> {
    const url = `decision?decisionId=${decisionId}`;
    const { data } = await this.$http.get<Decision>(url);

    return data;
  }

  public async getDecisionTreeAsync(treeName: string): Promise<DecisionNode> {
    const url = `decisionTree?treeName=${treeName}`;
    const { data } = await this.$http.get<DecisionNode>(url);

    return data;
  }

  public async getDecisionTreesAsync(): Promise<string[]> {
    const url = `decisionTrees`;
    const { data } = await this.$http.get<string[]>(url);

    return data;
  }
}

export const DecisionApi = DecisionService.Instance;
