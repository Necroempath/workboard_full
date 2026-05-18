import { Navigate } from "react-router-dom";
import { authStorage } from "../../features/auth/auth.storage";
import type { ReactNode } from "react";

type Props = {
  children: ReactNode;
};

export function ProtectedRoute({ children }: Props) {
  const token = authStorage.getToken();

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  return <>{children}</>;
}
