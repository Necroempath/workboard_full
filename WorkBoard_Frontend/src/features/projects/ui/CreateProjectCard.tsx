import { useState } from 'react'
import { CreateProjectModal } from './CreateProjectModal'

export const CreateProjectCard = () => {
  const [open, setOpen] = useState(false)

  return (
    <>
      <div
        className="border border-dashed p-6 text-center cursor-pointer hover:bg-gray-100"
        onClick={() => setOpen(true)}
      >
        + Create Project
      </div>

      {open && <CreateProjectModal onClose={() => setOpen(false)} />}
    </>
  )
}