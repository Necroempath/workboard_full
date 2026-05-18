import { useState } from "react";
import { login, register } from "./auth.api";
import type { LoginParams, RegisterParams } from "./auth.api";
import { authStorage } from "./auth.storage";
import type { User } from "../../entities/user";

export const useAuth = () => {
  const [loading, setLoading] = useState(false);

  const signIn = async (params: LoginParams) => {
    setLoading(true);
    try {
      const { accessToken, id, name, email, roles } = await login(params);

      saveAuthData(id, name, email, roles, accessToken);

    } finally {
      setLoading(false);
    }
  };

  const signUp = async (params: RegisterParams) => {
    setLoading(true);
    try {
      const { accessToken, id, name, email, roles } = await register(params);

      saveAuthData(id, name, email, roles, accessToken);

    } finally {
      setLoading(false);
    }
  };

  const logout = () => {
    authStorage.clearToken();
    authStorage.clearUser();
  };

  return { signIn, signUp, logout, loading };
};

function saveAuthData(id: string, name: string, email: string, roles: string[], accessToken: string) {
  const user: User = { id, name, email, roles };

  authStorage.setToken(accessToken);
  authStorage.setUser(user);
}

