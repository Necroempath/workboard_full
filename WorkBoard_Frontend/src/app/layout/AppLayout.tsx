import { Outlet } from 'react-router-dom'
import { Header } from '../../shared/ui/Header'

export function AppLayout() {
  return (
    <div className="d min-h-screen bg-gray-100">
        <Header />  

      <main className="p-4 max-w-7xl mx-auto">
        <Outlet />
      </main>
    </div>
  )
}