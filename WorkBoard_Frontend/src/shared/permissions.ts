export const canManageUser = (
  currentRole: number,
  targetRole: number
) => {
  if (currentRole === 0) return targetRole !== 0

  if (currentRole === 1) {
    return targetRole === 2 || targetRole === 3
  }

  return false
}