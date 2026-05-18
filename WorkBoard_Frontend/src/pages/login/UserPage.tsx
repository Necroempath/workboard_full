import { useState } from 'react'
import { useAppDispatch } from '../../app/hooks'
import { authStorage } from '../../features/auth/auth.storage'
import { ShowNotification } from '../../shared/ui/ShowNotification'
import { changePasswordAsync, updateUserAsync } from '../../features/user/user.slice'
import { useNavigate } from 'react-router-dom'

export const UserPage = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const user = authStorage.getUser();

  const [name, setName] = useState(user?.name || "");
  const [initialName] = useState(user?.name || "");

  const [oldPassword, setOldPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const [loadingProfile, setLoadingProfile] = useState(false);
  const [loadingPassword, setLoadingPassword] = useState(false);

  const [errors, setErrors] = useState<{
    name?: string;
    password?: string;
    confirmPassword?: string;
  }>({});

  const isNameChanged = name !== initialName;

  const isPasswordChanged =
    oldPassword.length > 0 ||
    newPassword.length > 0 ||
    confirmPassword.length > 0;

  const clearError = (field: keyof typeof errors) => {
    setErrors((prev) => {
      const copy = { ...prev };
      delete copy[field];
      return copy;
    });
  };

  const validateName = () => {
    if (name.trim().length < 2) {
      setErrors({ name: "Name must be at least 2 characters" });
      return false;
    }
    return true;
  };

  const validatePassword = () => {
    const newErrors: typeof errors = {};

    if (oldPassword.length < 6) {
      newErrors.password = "Old password must be at least 6 characters";
    }

    if (newPassword.length < 6) {
      newErrors.password = "New password must be at least 6 characters";
    }

    if (newPassword !== confirmPassword) {
      newErrors.confirmPassword = "Passwords do not match";
    }

    setErrors(newErrors);

    return Object.keys(newErrors).length === 0;
  };

  const handleSaveProfile = async () => {
    if (!isNameChanged) return;
    if (!validateName()) return;

    setLoadingProfile(true);

    const result = await dispatch(updateUserAsync({ name }));

    setLoadingProfile(false);

    if (updateUserAsync.fulfilled.match(result)) {
      ShowNotification("Profile updated", "success");
    } else {
      ShowNotification("Failed to update profile", "error");
    }
  };

  const handleChangePassword = async () => {
    if (!isPasswordChanged) return;
    if (!validatePassword()) return;

    setLoadingPassword(true);

    const result = await dispatch(
      changePasswordAsync({
        oldPassword,
        newPassword,
        confirmPassword,
      })
    );

    setLoadingPassword(false);

    if (changePasswordAsync.fulfilled.match(result)) {
      ShowNotification("Password changed. Please login again", "success");

      setTimeout(() => {
        navigate("/login");
      }, 800);
    } else {
      ShowNotification("Incorrect password", "error");
    }
  };

  return (
    <div className="max-w-2xl mx-auto space-y-6">
      <div className="bg-white p-4 rounded-xl shadow">
        <h1 className="text-xl font-semibold">Profile Settings</h1>
      </div>

      <div className="bg-white p-6 rounded-xl shadow space-y-4">
        <h2 className="text-lg font-semibold">Personal Info</h2>

        <div className="space-y-2">
          <label className="text-sm text-gray-500">Name</label>

          <input
            value={name}
            onChange={(e) => setName(e.target.value)}
            onFocus={() => clearError("name")}
            className="w-full border rounded p-2 focus:outline-none focus:ring-2 focus:ring-blue-400"
          />

          {errors.name && (
            <p className="text-sm text-red-500">{errors.name}</p>
          )}
        </div>

        <div className="flex justify-end">
          <button
            onClick={handleSaveProfile}
            disabled={!isNameChanged || loadingProfile}
            className="px-4 py-2 bg-blue-600 text-white rounded disabled:opacity-50"
          >
            Save
          </button>
        </div>
      </div>

      {/* PASSWORD */}
      <div className="bg-white p-6 rounded-xl shadow space-y-4">
        <h2 className="text-lg font-semibold">Change Password</h2>

        <div className="space-y-2">
          <label className="text-sm text-gray-500">Old Password</label>
          <input
            type="password"
            value={oldPassword}
            onChange={(e) => setOldPassword(e.target.value)}
            onFocus={() => clearError("password")}
            className="w-full border rounded p-2 focus:outline-none focus:ring-2 focus:ring-blue-400"
          />
        </div>

        <div className="space-y-2">
          <label className="text-sm text-gray-500">New Password</label>
          <input
            type="password"
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
            onFocus={() => clearError("password")}
            className="w-full border rounded p-2 focus:outline-none focus:ring-2 focus:ring-blue-400"
          />
        </div>

        <div className="space-y-2">
          <label className="text-sm text-gray-500">Confirm Password</label>
          <input
            type="password"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            onFocus={() => clearError("confirmPassword")}
            className="w-full border rounded p-2 focus:outline-none focus:ring-2 focus:ring-blue-400"
          />
        </div>

        {errors.password && (
          <p className="text-sm text-red-500">{errors.password}</p>
        )}

        {errors.confirmPassword && (
          <p className="text-sm text-red-500">{errors.confirmPassword}</p>
        )}

        <div className="flex justify-end">
          <button
            onClick={handleChangePassword}
            disabled={!isPasswordChanged || loadingPassword}
            className="px-4 py-2 bg-blue-600 text-white rounded disabled:opacity-50"
          >
            Save
          </button>
        </div>
      </div>
    </div>
  );
};