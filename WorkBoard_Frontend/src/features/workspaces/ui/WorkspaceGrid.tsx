import { useAppDispatch, useAppSelector } from '../../../app/hooks'
import { useEffect } from 'react'
import { fetchWorkspaces } from '../workspace.slice'
import { WorkspaceCard } from './WorkspaceCard'
import { CreateWorkspaceCard } from './CreateWorkspaceCard'

export const WorkspaceGrid = () => {
  const dispatch = useAppDispatch()

  const workspaces = useAppSelector((state) => state.workspaces.items)

  useEffect(() => {
    dispatch(fetchWorkspaces())
  }, [dispatch])

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4">
      {workspaces.map((w) => (
        <WorkspaceCard key={w.id} workspace={w} />
      ))}

      <CreateWorkspaceCard />
    </div>
  )
}