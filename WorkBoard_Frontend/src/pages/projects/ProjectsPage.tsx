import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { useAppDispatch, useAppSelector } from '../../app/hooks'
import { fetchProjects } from '../../features/projects/projects.slice'
import { ProjectCard } from '../../features/projects/ui/ProjectCard'
import { CreateProjectCard } from '../../features/projects/ui/CreateProjectCard'
import { AddMemberModal } from '../../features/projects/ui/AddMemberModal'
import { ManageMembersModal } from '../../features/projects/ui/ManageMembersModal'
import { fetchWorkspaceRole } from '../../features/workspaces/workspace.slice'
import { roleEnum } from '../../entities/workspace'
import { roleColor } from '../../shared/ui/styles'


export const ProjectsPage = () => {
  const { workspaceId } = useParams()
  const dispatch = useAppDispatch()

  const role = useAppSelector((state) => state.workspaces.role)

  const [openAddMember, setOpenAddMember] = useState(false)
  const [openManageMember, setOpenManageMember] = useState(false)

  const workspaceName = useAppSelector(
    (s) => s.workspaces.items.find(w => w.id == workspaceId)?.name
  )

  const projects = useAppSelector((s) => s.projects.items)
  const loading = useAppSelector((s) => s.projects.loading)

  const canManage =
    role === 0 || role === 1

  useEffect(() => {
    if (workspaceId) {
      dispatch(fetchProjects(workspaceId))
      dispatch(fetchWorkspaceRole(workspaceId))
    }
  }, [workspaceId, dispatch])

  return (
    <div className="space-y-4">

      {/* HEADER */}
      <div className="p-4 bg-white shadow rounded flex justify-between items-center">

        <div>
          <div className="text-sm text-gray-500">
            Workspace
          </div>

          <div className="font-semibold">
            {workspaceName}
          </div>

          {/* ROLE */}
          <div className="text-xs text-gray-400 mt-1">
            Your Role: <span className={`${roleColor[role]}`}>{roleEnum[role]}</span>
          </div>
        </div>

        {/* ACTIONS */}
        <div className="flex items-center gap-3">

          <button
            onClick={() => canManage && setOpenAddMember(true)}
            disabled={!canManage}
            className={`text-gray-500 hover:text-gray-700 ${
              !canManage ? "opacity-40 cursor-not-allowed" : "cursor-pointer"
            }`}
          >
            + Member
          </button>

          <button
            onClick={() => setOpenManageMember(true)}
            className='text-gray-500 cursor-pointer hover:text-gray-700'
          >
            Manage
          </button>

        </div>
      </div>

      {loading ? (
        <div>Loading...</div>
      ) : (
        <div className="grid grid-cols-3 gap-4">

          {projects.map((p) => (
            <ProjectCard key={p.id} project={p} />
          ))}

          {canManage && <CreateProjectCard />}

        </div>
      )}
      {openAddMember && (
        <AddMemberModal onClose={() => setOpenAddMember(false)} />
      )}

      {openManageMember && (
        <ManageMembersModal onClose={() => setOpenManageMember(false)} />
      )}

    </div>
  )
}