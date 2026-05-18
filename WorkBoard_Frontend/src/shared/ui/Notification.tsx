import { useEffect } from 'react'

type Props = {
  message: string
  type?: 'error' | 'success'
  onClose: () => void
  duration?: number
}

export const Notification = ({ message, type = 'error', onClose, duration = 3000 }: Props) => {
  useEffect(() => {
    const timer = setTimeout(onClose, duration)
    return () => clearTimeout(timer)
  }, [onClose, duration])

  return (
    <div className={`fixed top-0 left-0 right-0 p-4 mx-6 my-2 rounded-2xl text-white font-medium shadow-md z-50
      ${type === 'error' ? 'bg-red-600' : 'bg-green-600'}`}>
      {message}
    </div>
  )
}