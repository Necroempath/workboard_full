import { useState } from 'react'
import type { Column as ColumnType } from '../../../entities/project'
import { IssueCard } from './IssueCard'
import { useDroppable } from '@dnd-kit/core'
import { CreateIssueModal } from './CreateIssueModal'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPenToSquare, faPlus, faXmark } from '@fortawesome/free-solid-svg-icons'
import { useAppDispatch } from '../../../app/hooks'
import { deleteColumnAsync, updateColumnAsync } from '../board.slice'
import { ShowNotification } from '../../../shared/ui/ShowNotification'
import { EditNameModal } from '../../../shared/ui/EditNameModal'
import { DeleteConfirmationModal } from '../../../shared/ui/DeleteConfirmationModal'

export const Column = ({
  column,
  activeId,
  overId
}: {
  column: ColumnType
  activeId: string | null
  overId?: string
}) => {
  const { setNodeRef } = useDroppable({
  id: column.id,
  data: { columnId: column.id }
})
const iconClass =
  'text-gray-400 hover:text-gray-600 cursor-pointer transition-colors'
  const [openIssueModal, setOpenIssueModal] = useState(false)
  const [editOpen, setEditOpen] = useState(false)
  const [confirmOpen, setConfirmOpen] = useState(false)
  
  const [hovered, setHovered] = useState(false)

  const dispatch = useAppDispatch()

const handleEdit = (name: string) => {
  dispatch(updateColumnAsync({
    id: column.id,
    name
  }))
  setEditOpen(false)
}

const handleDelete = () => {
   if (column.issues.length > 0) {
    ShowNotification(
      'Cannot delete column with existing issues',
      'error'
    )
  }
  else dispatch(deleteColumnAsync(column.id))

  setConfirmOpen(false)
}

  return (
    <div
      ref={setNodeRef}
        onMouseEnter={() => setHovered(true)}
        onMouseLeave={() => setHovered(false)}
      className="bg-gray-100 rounded-xl p-4 w-72 shrink-0 shadow-sm"
    >
      <div className="flex justify-between items-center mb-3">
  <h2>{column.name}</h2>

  <div className="flex items-center gap-2">

    <FontAwesomeIcon
      icon={faPlus}
      className={`${iconClass} transition-opacity duration-200 ${
      hovered ? 'opacity-100' : 'opacity-0'
    }`}
      onClick={() => setOpenIssueModal(true)}
    />

    <FontAwesomeIcon
      icon={faPenToSquare}
      className={`${iconClass} transition-opacity duration-200 ${
      hovered ? 'opacity-100' : 'opacity-0'
    }`}
      onClick={(e) => { 
        e.stopPropagation()
        setEditOpen(true)
      }}
    />

    <FontAwesomeIcon
      icon={faXmark}
      className={`${iconClass} transition-opacity duration-200 ${
      hovered ? 'opacity-100' : 'opacity-0'
    }`}
      onClick={() => setConfirmOpen(true)}
    />
  </div>
</div>

      <div className="space-y-2 min-h-12.5">
        {column.issues.map(issue => {
  const isActive = issue.id === activeId
  const isOver = issue.id === overId

  return (
    <div key={issue.id}>
      {isOver && activeId !== issue.id && (
        <div className="h-10 bg-blue-100 rounded mb-2" />
      )}

      {!isActive && (
        <IssueCard issue={issue} />
      )}
    </div>
  )
})}
      </div>

      {openIssueModal && (
        <CreateIssueModal
          columnId={column.id}
          onClose={() => setOpenIssueModal(false)}
        />
      )}
      {editOpen && (
        
      <EditNameModal
        title='Edit Column'
        initialValue={column.name}
        onClose={() => setEditOpen(false)}
        onSubmit={handleEdit}
      />
    )}

    {confirmOpen && (
      <DeleteConfirmationModal
        message={`Are you sure you want to delete the column [${column.name}]?`}
        onConfirm={handleDelete}
        onClose={() => setConfirmOpen(false)}
      />
    )}
    </div>
  )
}