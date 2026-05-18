using WorkBoard.Domain.Entities;
using WorkBoard.Domain.Enums;

namespace WorkBoard.Domain.Extensions;

public static class DomainExtensions
{
    public static bool CanInviteMembers(this WorkspaceRole role) => role is WorkspaceRole.Owner or WorkspaceRole.Admin;

    public static bool CanMoveIssues(this WorkspaceRole role) => role is WorkspaceRole.Owner or WorkspaceRole.Admin or WorkspaceRole.Member;

    public static bool CanManageProjects(this WorkspaceRole role) => role is WorkspaceRole.Owner or WorkspaceRole.Admin;

    public static bool CanAssignRole(this WorkspaceRole role, WorkspaceRole roleToAssign)
    {
        if (role is WorkspaceRole.Owner && roleToAssign is not WorkspaceRole.Owner) return true;
        if (role is WorkspaceRole.Admin)
        {
            if (roleToAssign is WorkspaceRole.Member || roleToAssign is WorkspaceRole.Viewer) return true;
            else return false;
        }

        return false;
    }

    public static bool CanDeleteMembers(this WorkspaceRole role, WorkspaceRole roleToDelete)
    {
        if (role is WorkspaceRole.Owner) return true;
        if (role is WorkspaceRole.Admin)
        {
            if (roleToDelete is WorkspaceRole.Member || roleToDelete is WorkspaceRole.Viewer) return true;
            else return false;
        }

        return false;
    }
}
