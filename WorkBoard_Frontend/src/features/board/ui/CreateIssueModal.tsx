import { useState } from "react"
import { useAppDispatch, useAppSelector } from "../../../app/hooks"
import { ShowNotification } from "../../../shared/ui/ShowNotification"
import { createIssueAsync } from "../board.slice"

export const CreateIssueModal = ({
  columnId,
  onClose,
}: {
  columnId: string
  onClose: () => void
}) => {
  const dispatch = useAppDispatch()
  const projectId = useAppSelector((s) => s.board.project?.id)

  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')
  const [priority, setPriority] = useState(0)

  const isValid = title.trim().length >= 3

  const handleCreate = async () => {
    if (!isValid) {
      ShowNotification('Title must be at least 3 characters', 'error')
      return
    }

    await dispatch(
      createIssueAsync({
        columnId,
        projectId,
        title,
        description,
        priority,
      })
    )

    onClose()
  }

  return (
    <div className="fixed inset-0 bg-black/40 flex items-center justify-center z-9999" onClick={onClose}>
      <div
        className="bg-white p-6 rounded shadow w-96"
        onClick={(e) => e.stopPropagation()}
      >
        <h2 className="mb-4 font-semibold">Create Issue</h2>

        <input
          className="border p-2 w-full mb-3"
          placeholder="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />

        <textarea
          className="border p-2 w-full mb-3"
          placeholder="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />

        <select
          className="border p-2 w-full mb-4"
          value={priority}
          onChange={(e) => setPriority(Number(e.target.value))}
        >
          <option value={0}>Low</option>
          <option value={1}>Medium</option>
          <option value={2}>High</option>
          <option value={3}>Critical</option>
        </select>

        <div className="flex justify-end gap-2">
          <button onClick={onClose}>Cancel</button>

          <button
            disabled={!isValid}
            className="bg-blue-600 rounded text-white px-4 py-2 disabled:opacity-50"
            onClick={handleCreate}
          >
            Create
          </button>
        </div>
      </div>
    </div>
  )
}