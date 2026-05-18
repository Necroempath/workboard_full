import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { updateUser, changePassword } from './user.api'
import { authStorage } from '../auth/auth.storage'

type UserState = {
  loading: boolean
}

const initialState: UserState = {
  loading: false
}

export const updateUserAsync = createAsyncThunk(
  'user/updateUser',
  async (params: { name: string }) => {
    const data = await updateUser(params)

    const user = authStorage.getUser()
    if (user) {
      authStorage.setUser({ ...user, name: params.name })
    }

    return data
  }
)

export const changePasswordAsync = createAsyncThunk(
  'user/changePassword',
  async (params: { oldPassword: string; newPassword: string; confirmPassword: string}) => {
    const data = await changePassword(params)

    authStorage.clearUser()
    authStorage.clearToken()

    return data
  }
)

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(updateUserAsync.pending, (state) => {
        state.loading = true
      })
      .addCase(updateUserAsync.fulfilled, (state) => {
        state.loading = false
      })
      .addCase(updateUserAsync.rejected, (state) => {
        state.loading = false
      })
      .addCase(changePasswordAsync.pending, (state) => {
        state.loading = true
      })
      .addCase(changePasswordAsync.fulfilled, (state) => {
        state.loading = false
      })
      .addCase(changePasswordAsync.rejected, (state) => {
        state.loading = false
      })
  }
})

export default userSlice.reducer