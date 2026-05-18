import { WorkspaceGrid } from "../../features/workspaces/ui/WorkspaceGrid"

export function WorkspacesPage() {
  
  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold">Your Workspaces</h1>

      <WorkspaceGrid />
    </div>
  )
}