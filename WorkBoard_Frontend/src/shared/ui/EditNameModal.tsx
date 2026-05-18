import { createPortal } from 'react-dom'
import { useState } from 'react'

export const EditNameModal = ({
  title,
  initialValue,
  onClose,
  onSubmit
}: {
  title: string
  initialValue: string
  onClose: () => void
  onSubmit: (value: string) => void
}) => {
  const [value, setValue] = useState(initialValue)

  const isValid = value.trim().length >= 3

  return createPortal(
    <div
      className="fixed inset-0 bg-black/40 flex items-center justify-center z-9999"
      onClick={onClose}
    >
      <div
        className="bg-white rounded-xl p-6 w-80"
        onClick={(e) => e.stopPropagation()}
      >
        <h2 className="mb-4 font-semibold">{title}</h2>

        <input
          className="w-full border p-2 mb-4 rounded"
          value={value}
          onChange={(e) => setValue(e.target.value)}
        />

        <div className="flex justify-end gap-2">
          <button
            onClick={onClose}
            className="bg-gray-200 px-4 py-2 rounded"
          >
            Cancel
          </button>

          <button
            disabled={!isValid}
            onClick={() => onSubmit(value)}
            className="bg-blue-500 text-white px-4 py-2 rounded disabled:opacity-50"
          >
            Submit
          </button>
        </div>
      </div>
    </div>,
    document.body
  )
}