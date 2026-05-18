import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { createProject, getProjects } from "./projects.api";
import type { Project } from "../../entities/project";

export const fetchProjects = createAsyncThunk(
  "projects/fetch",
  async (workspaceId: string) => {
    return await getProjects(workspaceId);
  },
);

export const addProject = createAsyncThunk(
  "projects/add",
  async ({ workspaceId, name }: { workspaceId: string; name: string }) => {
    return await createProject(workspaceId, name);
  },
);

type ProjectsState = {
  items: Project[];
  loading: boolean;
};

const initialState: ProjectsState = {
  items: [],
  loading: false,
};

const projectsSlice = createSlice({
  name: "projects",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchProjects.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchProjects.fulfilled, (state, action) => {
        state.loading = false;
        state.items = action.payload;
      })
      .addCase(fetchProjects.rejected, (state) => {
        state.loading = false;
      })
      .addCase(addProject.fulfilled, (state, action) => {
        state.items.push(action.payload);
      })
  },
});

export default projectsSlice.reducer;
