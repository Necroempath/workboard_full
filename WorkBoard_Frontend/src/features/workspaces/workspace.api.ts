import type { Workspace, WorkspaceDetails } from '../../entities/workspace'
import { api } from '../../shared/api/api'

export type CreateWorkspaceParams = { name: string }

export const createWorkspace = async (params: CreateWorkspaceParams): Promise<Workspace> => {
  const res = await api.post<Workspace>('/workspaces', params)
  return res.data
}

export const addMember = async (workspaceId: string, data: {
  email: string
  role: number
}) => {
  const res = await api.post(`/memberships/${workspaceId}`, data)
  return res.data
}

export const workspaceRole = async (workspaceId: string): Promise<number> => {
  const res = await api.get<number>(`/workspaces/role/${workspaceId}`)
  return res.data
}


export const getWorkspace = async(workspaceId: string): Promise<WorkspaceDetails> => {
    const res = await api.get<WorkspaceDetails>(`/workspaces/${workspaceId}`)
    return res.data
}

export const getWorkspaceMembers = async (workspaceId: string) => {
  const res = await api.get(`/memberships/${workspaceId}`)
  return res.data
}

export const updateMemberRole = async (params: {
  workspaceId: string
  userId: string
  role: number
}) => {
  const res = await api.put(`/memberships/${params.workspaceId}/role`, {
    userId: params.userId,
    role: params.role
  })
  return res.data
}

export const removeMember = async (params: {
  workspaceId: string
  userId: string
}) => {
  const res = await api.delete(`/memberships/${params.workspaceId}/${params.userId}`)
  return res.data
}

export const getWorkspaces = async (): Promise<Workspace[]> => {
  const res = await api.get<Workspace[]>('/workspaces')
  return res.data
}