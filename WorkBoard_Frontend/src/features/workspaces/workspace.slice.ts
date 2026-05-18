import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { addMember, createWorkspace, getWorkspace, getWorkspaceMembers, getWorkspaces, removeMember, updateMemberRole, workspaceRole } from './workspace.api'
import type { Workspace, WorkspaceMember } from '../../entities/workspace'

export const fetchWorkspaces = createAsyncThunk('workspaces/fetch', async () => {
  return await getWorkspaces()
})

export const fetchWorkspaceDetails = createAsyncThunk('workspace/fetch', async (workspaceId: string) => {
  return await getWorkspace(workspaceId)
})

export const addWorkspace = createAsyncThunk('workspaces/add', async (name: string) => {
  return await createWorkspace({ name })
})

export const addMemberAsync = createAsyncThunk(
  'workspaces/addMember',
  async (
    params: {
      workspaceId: string
      email: string
      role: number
    },
    { rejectWithValue }
  ) => {
    try {
      return await addMember(params.workspaceId, {
        email: params.email,
        role: params.role,
      })
    } catch (e: any) {
      return rejectWithValue(e.response?.data || 'Error')
    }
  }
)

export const fetchMembersAsync = createAsyncThunk(
  'workspaces/fetchMembers',
  async (workspaceId: string) => {
    return await getWorkspaceMembers(workspaceId)
  }
)

export const updateMemberRoleAsync = createAsyncThunk(
  'workspaces/updateRole',
  async (params: { workspaceId: string; userId: string; role: number }) => {
    return await updateMemberRole(params)
  }
)

export const removeMemberAsync = createAsyncThunk(
  'workspaces/removeMember',
  async (params: { workspaceId: string; userId: string }) => {
    await removeMember(params)
    return params.userId 
  }
)

export const fetchWorkspaceRole = createAsyncThunk(
  "workspaceRole/fetch",
  async (workspaceId: string) => {
    return await workspaceRole(workspaceId);
  },
);

type WorkspaceState = {
  items: Workspace[]
  members: WorkspaceMember[]
  role: number
  currentUserRole: number
  loading: boolean
  error: string | null
}

const initialState: WorkspaceState = {
  items: [],
  members: [],
  role: 0,
  currentUserRole: 2,
  loading: false,
  error: null
}

export const workspacesSlice = createSlice({
  name: 'workspaces',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchWorkspaces.pending, (state) => { state.loading = true; state.error = null })
      .addCase(fetchWorkspaces.fulfilled, (state, action: PayloadAction<Workspace[]>) => {
        state.loading = false
        state.items = action.payload
      })
      .addCase(fetchWorkspaces.rejected, (state, action) => {
        state.loading = false
        state.error = action.error.message || 'Failed to fetch workspaces'
      })

      .addCase(addWorkspace.pending, (state) => { state.loading = true; state.error = null })
      .addCase(addWorkspace.fulfilled, (state, action: PayloadAction<Workspace>) => {
        state.loading = false
        state.items.push(action.payload)
      })
      .addCase(addWorkspace.rejected, (state, action) => {
        state.loading = false
        state.error = action.error.message || 'Failed to add workspace'
      }).addCase(fetchMembersAsync.fulfilled, (state, action) => {
      state.members = action.payload.members
      state.currentUserRole = action.payload.currentUserRole
    })
    .addCase(updateMemberRoleAsync.fulfilled, (state, action) => {
      const { userId, role } = action.meta.arg

      const member = state.members.find(m => m.userId === userId)
      if (member) {
        member.role = role
      }
    })
    .addCase(removeMemberAsync.fulfilled, (state, action) => {
      state.members = state.members.filter(
        m => m.userId !== action.payload
      )
    })
          .addCase(fetchWorkspaceRole.fulfilled, (state, action) => {
            state.role = action.payload
      });
  },
})

export default workspacesSlice.reducer