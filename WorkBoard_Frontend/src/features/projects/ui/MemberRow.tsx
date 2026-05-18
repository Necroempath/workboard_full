import { useState } from "react";
import { useAppDispatch } from "../../../app/hooks";
import { roleEnum } from "../../../entities/workspace";
import { canManageUser } from "../../../shared/permissions";
import { DeleteConfirmationModal } from "../../../shared/ui/DeleteConfirmationModal";
import { ShowNotification } from "../../../shared/ui/ShowNotification";
import { roleColor } from "../../../shared/ui/styles";
import { removeMemberAsync, updateMemberRoleAsync } from "../../workspaces/workspace.slice";

type MemberRowProps = {
  member: {
    userId: string;
    name: string;
    email: string;
    role: number;
  };
  currentUserRole: number;
  workspaceId: string | undefined;
};

export const MemberRow = ({ member, currentUserRole, workspaceId }: MemberRowProps) => {
 
  const dispatch = useAppDispatch();
    
  const canManage = canManageUser(currentUserRole, member.role);
  const [confirmOpen, setConfirmOpen] = useState(false)

  const handleRoleChange = async (userId: string, role: number) => {
    const result = await dispatch(
      updateMemberRoleAsync({ workspaceId: workspaceId!, userId, role })
    );

    if (updateMemberRoleAsync.rejected.match(result)) {
      ShowNotification("Failed to update role", "error");
    }
  };

  const userId = member.userId;

  const handleRemove = async () => {
    const result = await dispatch(removeMemberAsync({ workspaceId: workspaceId!, userId }));

    if (removeMemberAsync.fulfilled.match(result)) {
      ShowNotification("User removed", "success");
    } else {
      ShowNotification("Failed to remove user", "error");
    }

    setConfirmOpen(false)
  };
  return (
    <div
      className="grid grid-cols-[1fr_1.5fr_auto_auto_auto] items-center gap-3 p-3 border border-gray-200 rounded-lg hover:bg-gray-50 transition"
    >
      <div className="min-w-0 truncate font-medium">{member.name}</div>

      <div className="min-w-0 truncate text-gray-500 text-sm">{member.email}</div>

      <div className={`font-semibold text-sm ${roleColor[member.role]}`}>
        {roleEnum[member.role]}
      </div>

      <select
        value={member.role}
        disabled={!canManage || member.role === 0}
        onChange={(e) => handleRoleChange(member.userId, Number(e.target.value))}
        className="border border-gray-200 text-sm p-1.5 rounded-md disabled:opacity-50"
      >
        <option value={1}>Admin</option>
        <option value={2}>Member</option>
        <option value={3}>Viewer</option>
      </select>

      <button
        disabled={!canManage}
        onClick={() => setConfirmOpen(true)}
        className={`bg-red-500 ${
          canManage ? "hover:bg-red-600 cursor-pointer" : ""
        } text-sm rounded-md disabled:opacity-50 px-3 py-1.5 text-white transition`}
      >
        Delete
      </button>
      
          {confirmOpen && (
            <DeleteConfirmationModal
              message={`Are you sure you want to remove user [${member.name}]?`}
              onConfirm={handleRemove}
              onClose={() => setConfirmOpen(false)}
            />
          )}
    </div>
  );
};

