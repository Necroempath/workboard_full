import { createRoot } from 'react-dom/client'
import { Notification } from './Notification'

export const ShowNotification = (message: string, type?: 'error' | 'success') => {
  const container = document.createElement('div')
  document.body.appendChild(container)
  const root = createRoot(container)

  const handleClose = () => {
    root.unmount()
    container.remove()
  }

  root.render(<Notification message={message} type={type} onClose={handleClose} />)
}