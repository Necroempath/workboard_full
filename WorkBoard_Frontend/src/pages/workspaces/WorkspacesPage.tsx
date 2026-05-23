import { WorkspaceGrid } from "../../features/workspaces/ui/WorkspaceGrid"

export function WorkspacesPage() {
  
  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-amber-300">Your Wishlist</h1>

      <WorkspaceGrid />
    </div>
  )
}