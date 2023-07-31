import { postBase } from "./ServerCallsBase";
import { BaseUrl } from "./constants";

const Login = async (email: string, password: string) => {
  return await postBase(`${BaseUrl}Authentication/login`, {email, password});
}


export {
  Login
}