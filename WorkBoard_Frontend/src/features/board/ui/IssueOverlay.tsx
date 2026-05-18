export const IssueOverlay = ({ issueId, columns }: any) => {
  for (const col of columns) {
    const issue = col.issues.find((i: { id: string }) => i.id === issueId)
    if (issue) {
      return (
        <div className="bg-white rounded p-2 shadow-lg opacity-90">
          {issue.title}
        </div>
      )
    }
  }
  return null
}