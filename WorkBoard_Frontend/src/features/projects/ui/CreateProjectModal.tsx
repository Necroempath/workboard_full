import { useState } from 'react'
import { useParams } from 'react-router-dom'
import { useAppDispatch } from '../../../app/hooks'
import { addProject } from '../projects.slice'
import { ShowNotification } from '../../../shared/ui/ShowNotification'

type Props = {
  onClose: () => void
}

export const CreateProjectModal = ({ onClose }: Props) => {
  const [name, setName] = useState('')
  const [loading, setLoading] = useState(false)

  const { workspaceId } = useParams()
  const dispatch = useAppDispatch()

  const isValid = name.trim().length >= 3

  const handleCreate = async () => {
    if (!isValid) {
      ShowNotification('Project name must be at least 3 characters', 'error')
      return
    }

    if (!workspaceId) return

    setLoading(true)
    try {
      await dispatch(addProject({ workspaceId, name: name.trim() }))

      ShowNotification('Project created😁', 'success')

      onClose()
    } catch {
      ShowNotification('Failed to create project', 'error')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div
      className="fixed inset-0 bg-black/40 flex items-center justify-center"
      onClick={onClose}
    >
      <div
        className="bg-white p-6 rounded shadow-lg w-96"
        onClick={(e) => e.stopPropagation()}
      >
        <h2 className="text-lg font-semibold mb-4">Create Project</h2>

        <input
          className="border p-2 w-full mb-4"
          placeholder="Project name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />

        <div className="flex justify-end gap-2">
          <button onClick={onClose}>Cancel</button>

          <button
            className="bg-blue-600 text-white px-4 py-2 disabled:opacity-50"
            disabled={!isValid || loading}
            onClick={handleCreate}
          >
            Create
          </button>
        </div>
      </div>
    </div>
  )
}