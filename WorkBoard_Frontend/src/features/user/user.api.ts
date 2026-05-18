import { api } from "../../shared/api/api"

export const updateUser = async (params: { name: string }) => {
  const res = await api.put('/users/name', params)
  return res.data
}

export const changePassword = async (params: {
  oldPassword: string
  newPassword: string
  confirmPassword: string
}) => {
  const res = await api.put('/users/password', params)
  return res.data
}