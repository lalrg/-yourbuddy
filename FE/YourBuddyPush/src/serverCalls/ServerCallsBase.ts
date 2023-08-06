import axios from "axios";
import { LOCALSTORAGE_TOKEN_KEY } from "../shared/constants";

const myAxios = axios;
const token = localStorage.getItem(LOCALSTORAGE_TOKEN_KEY);

if(token) {
  myAxios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
}


const getBase = (url: string) => {
  return myAxios.get(url);
}

const postBase = (url: string, body: object) => {
  return myAxios.post(url, body);
}

const putBase = (url: string, body: object) => {
  return myAxios.put(url, body);
}

const deleteBase = (url: string) => {
  return myAxios.delete(url);
}

export {
  getBase,
  postBase,
  putBase,
  deleteBase
}