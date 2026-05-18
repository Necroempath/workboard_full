import { useEffect, useRef, useState } from 'react'
import { UserDropdown } from './UserDropdown'

export function UserMenu({ user }: { user: { name: string } }) {
  const [open, setOpen] = useState(false)
  const ref = useRef<HTMLDivElement>(null)

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (ref.current && !ref.current.contains(event.target as Node)) {
        setOpen(false)
      }
    }

    document.addEventListener('mousedown', handleClickOutside)
    return () => document.removeEventListener('mousedown', handleClickOutside)
  }, [])

  return (
    <div ref={ref} className="relative">
      <button
        onClick={() => setOpen((v) => !v)}
        className="flex items-center gap-2 px-3 py-2 rounded-lg hover:bg-gray-100"
      >
        <div className="w-8 h-8 bg-blue-500 text-white flex items-center justify-center rounded-full">
          {user.name[0].toUpperCase()}
        </div>
        <span className="text-sm font-medium">{user.name}</span>
      </button>

      {open && <UserDropdown onClose={() => setOpen(false)} />}
    </div>
  )
}