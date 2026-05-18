import type { User } from "../../entities/user";
import { STORAGE_KEYS } from "../../shared/config/storageKeys";


export const authStorage = {
  setToken(token: string) {
    localStorage.setItem(STORAGE_KEYS.ACCESS_TOKEN, token);
  },
  getToken() {
    return localStorage.getItem(STORAGE_KEYS.ACCESS_TOKEN);
  },
  setUser(user: User) {
    localStorage.setItem(STORAGE_KEYS.USER, JSON.stringify(user));
  },
  getUser() {
    const user = localStorage.getItem(STORAGE_KEYS.USER);
    return user ? JSON.parse(user) : { name: 'User', email: '', role: ['User'] }
  },
  clearToken() {
    localStorage.removeItem(STORAGE_KEYS.ACCESS_TOKEN);
  },
  clearUser() {
    
    localStorage.removeItem(STORAGE_KEYS.USER);
  }
};
