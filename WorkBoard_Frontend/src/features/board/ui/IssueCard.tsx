import type { Issue } from '../../../entities/issue'
import { useDraggable, useDroppable } from '@dnd-kit/core'
import { CSS } from '@dnd-kit/utilities'
import { useState } from 'react';
import { useAppDispatch } from '../../../app/hooks';
import { deleteIssueAsync, updateIssueAsync } from '../board.slice';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPenToSquare, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { EditIssueModal } from './EditIssueModal';
import { DeleteConfirmationModal } from '../../../shared/ui/DeleteConfirmationModal';

export const IssueCard = ({ issue }: { issue: Issue }) => {
  const { setNodeRef: setDragRef, listeners, attributes, transform } = useDraggable({
    id: issue.id,
    data: { columnId: issue.columnId }
  })

  const priorityColor : any = {
    0: 'bg-green-500',   
    1: 'bg-yellow-400',  
    2: 'bg-orange-400',  
    3: 'bg-red-500'      
}

  const { setNodeRef: setDropRef } = useDroppable({
    id: issue.id,
    data: { columnId: issue.columnId }
  })

  const dispatch = useAppDispatch()

  const [hovered, setHovered] = useState(false)
  const [confirmOpen, setConfirmOpen] = useState(false)
  const [editOpen, setEditOpen] = useState(false)

  const handleDelete = () => {
    dispatch(deleteIssueAsync(issue.id))
    setConfirmOpen(false)
  }

  const handleEdit = (data: any) => {
  dispatch(updateIssueAsync({
    id: issue.id,
    ...data
  }))
  setEditOpen(false)
}

  const style = {
    transform: CSS.Translate.toString(transform),
  }

  return (
    <>
      <div
        ref={(node) => {
          setDragRef(node)
          setDropRef(node)
        }}
        style={style}
        {...listeners}
        {...attributes}
        onMouseEnter={() => setHovered(true)}
        onMouseLeave={() => setHovered(false)}
        className="bg-white p-3 rounded shadow cursor-grab relative group"
      >
         <button
            onClick={(e) => {
              e.stopPropagation()
              setEditOpen(true)
            }}
            className={`
              absolute top-2 right-8 
              text-gray-400 hover:text-gray-600
              transition-opacity duration-200
              ${hovered ? 'opacity-100' : 'opacity-0'}
            `}
          >
            <FontAwesomeIcon icon={faPenToSquare} className="text-gray-400 hover:text-gray-600 cursor-pointer"/>
        </button>
  
        <button
          onClick={(e) => {
            e.stopPropagation()     
            setConfirmOpen(true)
          }}
          className={`
            absolute top-2 right-2 
            transition-opacity duration-200
            ${hovered ? 'opacity-100' : 'opacity-0'}
          `}
        >
          <FontAwesomeIcon icon={faTrashCan} className="text-gray-400 hover:text-gray-600 cursor-pointer" />
        </button>
  <span
    className={`inline-block w-2.5 h-2.5 me-2 rounded-full shrink-0 ${priorityColor[issue.priority]}`}
  ></span>
        <span>{issue.title}</span>
      </div>
      
        {editOpen && (
          <EditIssueModal
            issue={issue}
            onClose={() => setEditOpen(false)}
            onSubmit={handleEdit}
          />
        )}
      {confirmOpen && (
        <DeleteConfirmationModal
          message={`Are you sure you want to delete the issue [${issue.title}]?`}
          onConfirm={handleDelete}
          onClose={() => setConfirmOpen(false)}
        />
      )}
    </>
  )
}