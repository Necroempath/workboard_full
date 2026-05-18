import { useNavigate } from "react-router-dom"
import { roleEnum, type Workspace } from "../../../entities/workspace"
import { roleColor } from "../../../shared/ui/styles"

export function WorkspaceCard({ workspace }: { workspace: Workspace }) {

  const navigate = useNavigate()

  return (
    <div
      onClick={() => navigate(`/workspaces/${workspace.id}/projects`)}
      className="bg-white rounded-xl shadow p-4 cursor-pointer hover:bg-gray-50 transition"
    >
      <h2 className="font-semibold text-lg truncate">{workspace.name}</h2>
      <div className={`text-sm ${roleColor[workspace.role]}`}>
  {roleEnum[workspace.role]}
</div>
    </div>
  )
}