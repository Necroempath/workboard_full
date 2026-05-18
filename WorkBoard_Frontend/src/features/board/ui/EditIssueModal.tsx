import { useState } from "react"
import type { Issue } from "../../../entities/issue"
import { createPortal } from "react-dom"

export const EditIssueModal = ({
  issue,
  onClose,
  onSubmit
}: {
  issue: Issue
  onClose: () => void
  onSubmit: (data: {
    title: string
    description?: string
    priority: number
  }) => void
}) => {
  const [title, setTitle] = useState(issue.title)
  const [description, setDescription] = useState(issue.description || '')
  const [priority, setPriority] = useState(issue.priority)

  const isValid = title.trim().length >= 3

  return createPortal(
    <div
      className="fixed inset-0 bg-black/40 flex items-center justify-center z-9999"
      onClick={onClose}
    >
      <div
        className="bg-white rounded-xl p-6 w-96"
        onClick={(e) => e.stopPropagation()}
      >
        <h2 className="text-lg font-semibold mb-4">
          Edit issue
        </h2>

        <input
          className="w-full border p-2 mb-3 rounded"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />

        <textarea
          className="w-full border p-2 mb-3 rounded"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />

        <select
          className="w-full border p-2 mb-4 rounded"
          value={priority}
          onChange={(e) => setPriority(Number(e.target.value))}
        >
          <option value={0}>Low</option>
          <option value={1}>Medium</option>
          <option value={2}>High</option>
          <option value={3}>Critical</option>
        </select>

        <div className="flex justify-end gap-2">
          <button
            className="px-4 py-2 bg-gray-200 rounded cursor-pointer"
            onClick={onClose}
          >
            Cancel
          </button>

          <button
            disabled={!isValid}
            className="px-4 py-2 bg-blue-500 text-white rounded cursor-pointer disabled:opacity-50"
            onClick={() =>
              onSubmit({ title, description, priority })
            }
          >
            Submit
          </button>
        </div>
      </div>
    </div>,
    document.body
  )
}