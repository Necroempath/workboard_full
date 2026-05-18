import { useState } from 'react'
import { useSearchParams, useNavigate } from 'react-router-dom'
import { useAppDispatch } from '../../../app/hooks'
import { resetPasswordAsync } from '../auth.slice'
import { ShowNotification } from '../../../shared/ui/ShowNotification'

export const ResetPasswordPage = () => {
  const [params] = useSearchParams()
  const token = decodeURIComponent(params.get('token') || '')

  const dispatch = useAppDispatch()
  const navigate = useNavigate()

  const [password, setPassword] = useState('')
  const [confirm, setConfirm] = useState('')
  const [loading, setLoading] = useState(false)

  const isValid = password.length >= 6 && password === confirm

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    if (!token) {
      ShowNotification('Invalid link', 'error')
      return
    }

    if (!isValid) return

    setLoading(true)

    const result = await dispatch(
      resetPasswordAsync({
        token,
        password: password
      })
    )

    setLoading(false)

    if (resetPasswordAsync.fulfilled.match(result)) {
      ShowNotification('Password successfully reset', 'success')

      setTimeout(() => {
        navigate('/login')
      }, 800)
    } else {
      ShowNotification('Invalid or expired token', 'error')
    }
  }

  if (!token) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="bg-white p-6 rounded-xl shadow text-center">
          <h2 className="text-lg font-semibold mb-2">Invalid Link</h2>
          <p className="text-sm text-gray-500">
            This password reset link is invalid or expired.
          </p>
        </div>
      </div>
    )
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <form
        onSubmit={handleSubmit}
        className="bg-white p-6 rounded-xl shadow w-80 flex flex-col gap-3"
      >
        <h1 className="text-xl font-bold">Set New Password</h1>

        <p className="text-sm text-gray-500">
          Enter a new password for your account
        </p>

        <input
          type="password"
          placeholder="New Password"
          className="p-2 border rounded"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

        <input
          type="password"
          placeholder="Confirm Password"
          className="p-2 border rounded"
          value={confirm}
          onChange={(e) => setConfirm(e.target.value)}
        />

        {confirm && password !== confirm && (
          <p className="text-sm text-red-500">
            Passwords do not match
          </p>
        )}

        <button
          disabled={!isValid || loading}
          className="bg-blue-500 text-white py-2 rounded disabled:opacity-50"
        >
          {loading ? 'Saving...' : 'Reset Password'}
        </button>
          <button
          onClick={() => navigate('/login')}
          className="bg-blue-500 text-white py-2 rounded disabled:opacity-50"
        >
           ← Back to login
        </button>
      </form>
    </div>
  )
}