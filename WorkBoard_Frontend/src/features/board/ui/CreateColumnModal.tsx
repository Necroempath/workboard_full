import { useState } from "react"
import { ShowNotification } from "../../../shared/ui/ShowNotification"
import { useAppDispatch } from "../../../app/hooks"
import { useParams } from "react-router-dom"
import { createColumnAsync } from "../board.slice"

export const CreateColumnModal = ({ onClose }: { onClose: () => void }) => {
  const [name, setName] = useState('')
  const dispatch = useAppDispatch()
  const { projectId } = useParams()

  const isValid = name.trim().length >= 3

  const handleCreate = async () => {
    if (!isValid) {
      ShowNotification('Min 3 chars', 'error')
      return
    }

    if (!projectId) return

    await dispatch(createColumnAsync({ projectId, name }))
    onClose()
  }

  return (
<div className="fixed inset-0 bg-black/40 flex items-center justify-center" onClick={onClose}>
      <div
        className="bg-white p-6 rounded shadow w-96"
        onClick={(e) => e.stopPropagation()}
      >
        <h2 className="mb-4 font-semibold">Add Column</h2>

        <input
          className="border p-2 w-full mb-3"
          placeholder="Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />

        <div className="flex justify-end gap-2">
          <button onClick={onClose}>Cancel</button>

          <button
            disabled={!isValid}
            className="bg-blue-600 text-white px-4 py-2 disabled:opacity-50"
            onClick={handleCreate}
          >
            Add
          </button>
        </div>
      </div>
    </div>
  )
}