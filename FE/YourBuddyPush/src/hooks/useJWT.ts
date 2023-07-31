import { LOCALSTORAGE_TOKEN_KEY } from '../shared/constants'
import { decodeToken } from '../shared/security';
import { useAuthStore } from '../store/authStore';

type useJWTType = () => { 
  isLoggedIn: boolean
  LogOut: () => void
}


export const useJWT: useJWTType = () => {
  const { login, logout, userInfo } = useAuthStore();
  
  const LogOut = () => {
    localStorage.removeItem(LOCALSTORAGE_TOKEN_KEY);
    logout();
  }

  if(userInfo) { return { isLoggedIn: true, LogOut } }

  const token = localStorage.getItem(LOCALSTORAGE_TOKEN_KEY);

  if(!token)
    return { isLoggedIn: false, LogOut }
  
  const tokenInfo = decodeToken(token);
  
  if(tokenInfo.exp < new Date()){
    LogOut();
    return { isLoggedIn: false, LogOut };
  }
  
  login(tokenInfo.email, tokenInfo.name, tokenInfo.roles, tokenInfo.exp, token);
  return { isLoggedIn: true, LogOut }

}