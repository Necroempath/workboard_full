import { useAppDispatch } from '../../../app/hooks'
import type { Column as ColumnType } from '../../../entities/project'
import { Column } from './Column'
import { moveIssueAsync } from '../board.slice'
import { DndContext, DragOverlay, PointerSensor, useSensor, useSensors, type DragEndEvent, type DragStartEvent } from '@dnd-kit/core'
import { useState } from 'react'
import { IssueOverlay } from './IssueOverlay'

export const BoardView = ({ columns }: { columns: ColumnType[] }) => {
  const dispatch = useAppDispatch()
  let overId : string | undefined = undefined;
  const [activeId, setActiveId] = useState<string | null>(null)
const sensors = useSensors(
  useSensor(PointerSensor, {
    activationConstraint: {
      distance: 8,
    },
  })
)
const handleDragStart = (event: DragStartEvent) => {
  setActiveId(event.active.id as string)
}

const handleDragEnd = (event: DragEndEvent) => {
  setActiveId(null)

  const { active, over } = event
  if (!over) return

  const issueId = active.id as string

  const overData = over.data.current

  const toColumnId =
    overData?.columnId ||
    findColumnId(over.id as string, columns)

  if (!toColumnId) return

  const targetColumn = columns.find(c => c.id === toColumnId)
  if (!targetColumn) return

  overId = over.id as string

  const overIndex = targetColumn.issues.findIndex(i => i.id === overId)

  const targetIndex =
    overIndex === -1
      ? targetColumn.issues.length
      : overIndex

  dispatch(moveIssueAsync({
    issueId,
    targetColumnId: toColumnId,
    targetIndex
  }))
}
    const sortedColumns = [...columns].sort((a, b) => a.order - b.order)
  return (
    <DndContext  
      sensors={sensors} 
      onDragStart={handleDragStart}
      onDragEnd={handleDragEnd}>
      <div className="flex gap-6 overflow-x-auto pb-4">
        {sortedColumns.map((col) => (
          <Column
            key={col.id}
            column={col}
            activeId={activeId}
            overId={overId}/>
        ))}
      </div>
        <DragOverlay>
          {activeId ? <IssueOverlay issueId={activeId} columns={columns} /> : null}
        </DragOverlay>
    </DndContext>
  )
}

const findColumnId = (issueId: string, columns: ColumnType[]) => {
  for (const col of columns) {
    if (col.issues.some(i => i.id === issueId)) {
      return col.id
    }
  }
  return null
}