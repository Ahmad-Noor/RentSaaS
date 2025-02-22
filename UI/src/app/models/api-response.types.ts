// models/api-response.types.ts
export interface APIResponse<T> {
    success: boolean;
    message: string;
    data: T;
    statusCode: number;
    timestamp: string;
    pagination: PaginationInfo;
  }
  
  export interface PaginationInfo {
    currentPage: number;
    pageSize: number;
    totalItems: number;
    hasPrevious: boolean;
    hasNext: boolean;
    firstItem: number;
    lastItem: number;
    skip: number;
  }