import { useState } from 'react'
import { ShowNotification } from '../../../shared/ui/ShowNotification'
import { useAppDispatch } from '../../../app/hooks'
import { addWorkspace } from '../workspace.slice'


type Props = {
  onClose: () => void
  onCreated?: (workspaceName: string) => void
}

export const CreateWorkspaceModal = ({ onClose, onCreated }: Props) => {
  const [name, setName] = useState('')
  const [loading, setLoading] = useState(false)
  
    const dispatch = useAppDispatch()

  const isValid = name.trim().length >= 3

  const handleCreate = async () => {
    if (!isValid) {
      ShowNotification('Workspace name must be at least 3 characters', 'error')
      return
    }

    setLoading(true)
    try {
      await dispatch(addWorkspace(name))
      onCreated?.(name.trim())
      onClose()
    } catch (err: any) {
      ShowNotification(err?.message || 'Failed to create workspace', 'error')
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
        <h2 className="text-lg font-semibold mb-4">Create Workspace</h2>
        <input
          className="border p-2 w-full mb-4"
          placeholder="Workspace name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <div className="flex justify-end gap-2">
          <button className="px-4 py-2" onClick={onClose}>Cancel</button>
          <button
            className="px-4 py-2 bg-blue-600 text-white disabled:opacity-50"
            onClick={handleCreate}
            disabled={name.length < 1 || loading}
          >
            Create
          </button>
        </div>
      </div>
    </div>
  )
}