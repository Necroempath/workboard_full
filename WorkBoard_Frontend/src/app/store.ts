import { configureStore } from '@reduxjs/toolkit'
import workspacesReducer  from '../features/workspaces/workspace.slice'
import projectsReducer  from '../features/projects/projects.slice'
import boardReducer  from '../features/board/board.slice'

export const store = configureStore({
  reducer: {
    workspaces: workspacesReducer,
    projects: projectsReducer,
    board: boardReducer,
  },
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch 