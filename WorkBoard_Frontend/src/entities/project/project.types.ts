import type { Issue } from "../issue/issue.types";

export type Column = {
  id: string;
  name: string;
  order: number;
  issues: Issue[];
};

export type Project = {
  id: string;
  name: string;
  columns: Column[];
  workspaceId: string
};
