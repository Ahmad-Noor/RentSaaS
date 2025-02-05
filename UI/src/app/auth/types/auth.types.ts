export interface LoginCredentials {
  email: string;
  password: string;
  rememberMe: boolean;
}

export interface LoginResponse {
  success: boolean;
  error?: string;
  token?: string;
  user?: {
    id: string;
    email: string;
  };
}

export interface SocialLoginResponse extends LoginResponse {
  provider: 'google' | 'facebook' | 'apple';
}