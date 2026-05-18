import { useState } from 'react'
import { useParams } from 'react-router-dom'
import { useAppDispatch } from '../../../app/hooks'
import { addMemberAsync } from '../../workspaces/workspace.slice'
import { ShowNotification } from '../../../shared/ui/ShowNotification'

const InviteRolesEnum = {
  Admin: 1,
  Member: 2,
  Viewer: 3,
} as const

export const AddMemberModal = ({ onClose }: { onClose: () => void }) => {
  const { workspaceId } = useParams()
  const dispatch = useAppDispatch()

  const [email, setEmail] = useState('')
  const [role, setRole] = useState<number>(InviteRolesEnum.Member)
  const [loading, setLoading] = useState(false)

  const isValid = email.length >= 5 && email.includes('@')

  const handleSubmit = async () => {
    if (!workspaceId || !isValid) return

    setLoading(true)

    const result = await dispatch(
      addMemberAsync({ workspaceId, email, role })
    )

    setLoading(false)

if (addMemberAsync.fulfilled.match(result)) {
  ShowNotification('User added', 'success')
  onClose()
} else {
  const error = result.payload as {code: string; message: string; }

  if (error['code'] === 'ALREADY_MEMBER') {
    ShowNotification('User already in workspace', 'error')
    setEmail('')
    setRole(2)
    onClose()
  } else if (error['code'] === 'USER_NOT_FOUND') {
    ShowNotification('User not found', 'error')
  } else {
    ShowNotification('Something went wrong', 'error')
  }
}}

  return (
    <div className="fixed inset-0 bg-black/30 flex items-center justify-center z-50">
      <div className="bg-white p-6 rounded-xl w-96 space-y-4">
        
        <h2 className="text-lg font-semibold">Add Member</h2>

        <input
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          placeholder="Email"
          className="w-full border p-2 rounded"
        />

        <select
          value={role}
          onChange={(e) => setRole(Number(e.target.value))}
          className="w-full border p-2 rounded"
        >
          <option value={InviteRolesEnum.Admin}>Admin</option>
          <option value={InviteRolesEnum.Member}>Member</option>
          <option value={InviteRolesEnum.Viewer}>Viewer</option>
        </select>

        <div className="flex justify-end gap-2">
          <button
            onClick={onClose}
            className="px-4 py-2 bg-gray-200 rounded"
          >
            Cancel
          </button>

          <button
            onClick={handleSubmit}
            disabled={!isValid || loading}
            className="px-4 py-2 bg-blue-600 text-white rounded disabled:opacity-50"
          >
            Invite
          </button>
        </div>
      </div>
    </div>
  )
}
