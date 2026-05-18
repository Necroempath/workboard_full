import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { useAppDispatch, useAppSelector } from '../../app/hooks'
import { fetchBoard, updateProjectAsync } from '../../features/board/board.slice'
import { BoardView } from '../../features/board/ui/BoardView'
import { CreateColumnModal } from '../../features/board/ui/CreateColumnModal'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPenToSquare, faPlus } from '@fortawesome/free-solid-svg-icons'
import { ShowNotification } from '../../shared/ui/ShowNotification'
import { EditNameModal } from '../../shared/ui/EditNameModal'

export const BoardPage = () => {
  const { projectId } = useParams()
  const dispatch = useAppDispatch()

  const project = useAppSelector((s) => s.board.project)
  const loading = useAppSelector((s) => s.board.loading)

  const [editProjectOpen, setEditProjectOpen] = useState(false)
  const [createColumnOpen, setCreateColumnOpen] = useState(false)

  const handleAddColumnClick = () => {
  if (!project) return

  if (project.columns.length >= 7) {
    ShowNotification(
      'Maximum 7 columns allowed',
      'error'
    )
    return
  }

  setCreateColumnOpen(true)
}

const handleEditProject = (name: string) => {
  if(projectId)
  dispatch(updateProjectAsync({
    id: projectId,
    name
  }))

  setEditProjectOpen(false)
}
  const [openColumnModal, setOpenColumnModal] = useState(false)

  useEffect(() => {
    if (projectId) {
      dispatch(fetchBoard(projectId))
    }
  }, [projectId, dispatch])

  if (loading || !project) return <div>Loading...</div>

  const iconClass =
    'text-gray-400 hover:text-gray-600 cursor-pointer transition-colors'
  return (
    <div className="p-6 h-full bg-gray-50">
      
      <div className="flex items-center gap-5 mb-6">
        <h1 className="text-xl font-semibold">{project.name}</h1>

<div className="flex gap-2">
    <FontAwesomeIcon
      icon={faPlus}
      className={iconClass}
      onClick={handleAddColumnClick}
    />

    <FontAwesomeIcon
      icon={faPenToSquare}
      className={iconClass}
      onClick={() => setEditProjectOpen(true)}
    />
  </div>
  
      </div>  
      <BoardView columns={project.columns} />

      {openColumnModal && (
        <CreateColumnModal onClose={() => setOpenColumnModal(false)} />
      )}
          
      {createColumnOpen && (
        <CreateColumnModal
          onClose={() => setCreateColumnOpen(false)}
        />
    )}
      {editProjectOpen && (
      <EditNameModal
        title="Edit project"
        initialValue={project.name}
        onClose={() => setEditProjectOpen(false)}
        onSubmit={handleEditProject}
  />
)}
    </div>
  )
}