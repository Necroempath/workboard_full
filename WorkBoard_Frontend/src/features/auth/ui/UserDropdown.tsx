import { useNavigate } from "react-router-dom";
import { useAuth } from "../auth.hooks";

type Props = {
  onClose: () => void;
};

export function UserDropdown({ onClose }: Props) {
  const navigate = useNavigate();
  const { logout } = useAuth()

  const handleLogout = () => {
    logout();
    onClose();
    navigate("/login");
  };

  return (
    <div className="absolute right-0 mt-2 w-48 bg-white border border-gray-400 rounded-xl shadow-lg p-2 z-50">
      <div className="px-3 py-2 text-sm text-gray-500 border-b">Signed in</div>

      <button
        onClick={() => navigate("/profile")}
        className="w-full text-left px-3 py-2 mt-2 text-sm text-gray-700 hover:bg-gray-100 cursor-pointer rounded-lg"
      >
        Profile
      </button>
      <button
        onClick={handleLogout}
        className="w-full text-left px-3 py-2 text-sm text-red-500 cursor-pointer hover:bg-gray-100 rounded-lg"
      >
        Logout
      </button>
    </div>
  );
}
