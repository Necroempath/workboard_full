export type Workspace = {
  id: string;
  name: string;
  role: number;
};

export type WorkspaceDetails = {
  id: string;
  name: string;
  members: { id: string; name: string; email: string; role: string }[];
};

export type WorkspaceMember = {
  userId: string;
  name: string;
  email: string;
  role: number;
};

export const roleEnum = ["Owner", "Admin", "Member", "Viewer"];

export type roleEnum = (typeof roleEnum)[keyof typeof roleEnum];
