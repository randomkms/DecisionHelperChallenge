import axios, { type AxiosInstance } from 'axios';

/**
 * Service API base class - configures default settings/error handling for inheriting class
 */
export abstract class BaseService {
  protected readonly $http: AxiosInstance;

  protected constructor(controller: string, timeout: number = 50000) {
    this.$http = axios.create({
      timeout,
      baseURL: `https://localhost:5001/api/${controller}/`
    });
  }
}
