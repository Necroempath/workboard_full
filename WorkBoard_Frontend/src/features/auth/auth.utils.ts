import { refresh } from "../auth/auth.api";
import { authStorage } from "./auth.storage";

export const tryRefresh = async () => {
  try {
    const accessToken = await refresh();
    authStorage.setToken(accessToken);
    return true;
  } catch {
    authStorage.clearToken();
    return false;
  }
};
