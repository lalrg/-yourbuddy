import { create } from 'zustand'

type UserInfoType = {
  email: string,
  name: string,
  roles: string,
  exp: Date,
  token: string
} 


type AuthStateType = {
  userInfo: UserInfoType | null,
  login: (email: string, name: string, roles: string, exp: Date, token: string) => void,
  logout: () => void
}

export const useAuthStore = create<AuthStateType>()((set) => ({
  userInfo: null,
  login: (email, name, roles, exp, token) => set((state) => ({ 
    ...state,
    userInfo: {
      email,
      exp,
      name,
      roles,
      token
    }
   })),
   logout: () => {
    set((state) => ({ ...state, userInfo: null }))
  }
}));

