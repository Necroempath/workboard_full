import { createBrowserRouter, Navigate } from "react-router-dom";
import { AppLayout } from "../layout/AppLayout";
import { LoginPage } from "../../pages/login/LoginPage";
import { ProtectedRoute } from "./ProtectedRoute";
import { WorkspacesPage } from "../../pages/workspaces/WorkspacesPage";
import { ProjectsPage } from "../../pages/projects/ProjectsPage";
import { BoardPage } from "../../pages/board/BoardPage";
import { UserPage } from "../../pages/login/UserPage";
import { ResetPasswordPage } from "../../features/auth/ui/ResetPasswordPage";

export const router = createBrowserRouter([
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/",
    element: (
      <ProtectedRoute>
        <AppLayout />
      </ProtectedRoute>
    ),
    children: [
      {
        path: "workspaces",
        element: <WorkspacesPage />,
      },
      {
        path: "profile",
        element: <UserPage />,
      },
      {
        index: true,
        element: <Navigate to="/workspaces" replace />,
      },
      {
        path: "workspaces/:workspaceId/projects",
        element: <ProjectsPage />,
      },
      {
        path: "workspaces/:workspaceId/projects/:projectId/board",
        element: <BoardPage />,
      },
    ],
  },
  {
    path: "/reset-password",
    element: <ResetPasswordPage />,
  },
]);
