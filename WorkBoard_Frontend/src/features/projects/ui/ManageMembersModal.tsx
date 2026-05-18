import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../../app/hooks";
import { fetchMembersAsync } from "../../workspaces/workspace.slice";
import { MemberRow } from "./MemberRow";

export const ManageMembersModal = ({ onClose }: { onClose: () => void }) => {
  const { workspaceId } = useParams();
  const dispatch = useAppDispatch();

  const members = useAppSelector((s) => s.workspaces.members);
  const currentUserRole = useAppSelector((s) => s.workspaces.currentUserRole);

  useEffect(() => {
    if (workspaceId) {
      dispatch(fetchMembersAsync(workspaceId));
    }
  }, [workspaceId]);

  return (
    <div className="fixed inset-0 bg-black/30 flex items-center justify-center z-40">
      <div className="bg-white p-6 rounded-2xl w-162.5 shadow-lg">
        <h2 className="text-lg font-semibold mb-4">Members</h2>

        <div className="space-y-2">
          {members.map((m) => (
            <MemberRow
              key={m.userId}
              member={m}
              currentUserRole={currentUserRole}
              workspaceId={workspaceId}
            />
          ))}
        </div>

        <div className="flex justify-end mt-5">
          <button
            onClick={onClose}
            className="px-4 py-2 bg-gray-200 hover:bg-gray-300 rounded-md transition"
          >
            Close
          </button>
        </div>
      </div>
    </div>
  );
};
