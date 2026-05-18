import { Link, useParams } from "react-router-dom"
import { authStorage } from "../../features/auth/auth.storage"
import { useAppSelector } from "../../app/hooks"
import { UserMenu } from "../../features/auth/ui/UserMenu"


export const Header = () => {
  const { workspaceId, projectId } = useParams()
  
  const user = authStorage.getUser()
  
  const workspaces = useAppSelector(s => s.workspaces.items)
  const project = useAppSelector(s => s.board.project)

  const currentWorkspace =
        workspaces.find(w => w.id === workspaceId) ||
        workspaces.find(w => w.id === project?.workspaceId)

  return (
        <div className="h-14 flex items-center px-6 border-b border-gray-300 bg-white justify-between">
      <Link to="/workspaces" className="font-semibold text-lg">
        WorkBoard
      </Link>
      <div className="flex items-center gap-2 text-sm text-gray-500">
         
        <Link to="/workspaces" className="hover:underline">
          Workspaces
        </Link>

        {currentWorkspace && (
          <>
            <span>/</span>
            <Link
              to={`/workspaces/${currentWorkspace.id}/projects`}
              className="hover:underline"
            >
              {currentWorkspace.name}
            </Link>
          </>
        )}

        {project && projectId && (
          <>
            <span>/</span>
            <span className="text-gray-800">
              {project.name}
            </span>
          </>
        )}
      </div>

      <UserMenu user={user}/>
    </div>
  )
}