import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import { requestPasswordReset, resetPassword } from './auth.api'

type AuthState = {
  loading: boolean
}

const initialState: AuthState = {
  loading: false
}

export const forgotPasswordAsync = createAsyncThunk(
  'auth/forgotPassword',
  async (email: string) => {
    return await requestPasswordReset(email)
  }
)

export const resetPasswordAsync = createAsyncThunk(
  'auth/resetPassword',
  async (params: { token: string; password: string }) => {
    return await resetPassword(params)
  }
)

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder

      .addCase(forgotPasswordAsync.pending, (s) => {
        s.loading = true
      })
      .addCase(forgotPasswordAsync.fulfilled, (s) => {
        s.loading = false
      })
      .addCase(forgotPasswordAsync.rejected, (s) => {
        s.loading = false
      })

      .addCase(resetPasswordAsync.pending, (s) => {
        s.loading = true
      })
      .addCase(resetPasswordAsync.fulfilled, (s) => {
        s.loading = false
      })
      .addCase(resetPasswordAsync.rejected, (s) => {
        s.loading = false
      })
  }
})

export default authSlice.reducer