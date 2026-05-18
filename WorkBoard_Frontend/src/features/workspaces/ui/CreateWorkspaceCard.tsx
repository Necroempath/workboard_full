import { useState } from 'react'
import { CreateWorkspaceModal } from './CreateWorkspaceModal'

type Props = {
  onCreated?: (workspaceName: string) => void
}

export const CreateWorkspaceCard = ({ onCreated }: Props) => {
  const [open, setOpen] = useState(false)

  return (
    <>
      <div
        className="border border-dashed rounded-xl p-6 text-center cursor-pointer hover:bg-gray-100"
        onClick={() => setOpen(true)}
      >
        + Create Workspace
      </div>

      {open && (
        <CreateWorkspaceModal
          onClose={() => setOpen(false)}
          onCreated={(name) => {
            onCreated?.(name)
            setOpen(false)
          }}
        />
      )}
    </>
  )
}