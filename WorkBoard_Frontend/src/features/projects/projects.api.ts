import type { Project } from '../../entities/project'
import { api } from '../../shared/api/api'

//export type CreateProjectParams = { workspaceId: string; name: string }

export const getProjects = async (workspaceId: string): Promise<Project[]> => {
  const res = await api.get<Project[]>(`/projects/${workspaceId}/all`)
  return res.data
}

export const createProject = async (workspaceId: string, name: string): Promise<Project> => {
  const res = await api.post<Project>(`/projects`, { workspaceId, name })
  return res.data
}
