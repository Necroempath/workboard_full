import { useNavigate } from "react-router-dom"
import type { Project } from "../../../entities/project"

export const ProjectCard = ({ project }: { project: Project }) => {
    const navigate = useNavigate()
     console.log(project)
  return (
    <div 
     onClick={() => navigate(`/workspaces/${project.workspaceId}/projects/${project.id}/board`)}

     className="p-4 bg-white rounded shadow truncate hover:bg-gray-50 cursor-pointer">
      {project.name}
    </div>
  )
}