import axios from "axios";

axios.defaults.headers.post['Content-Type'] = 'application/json';
axios.defaults.withCredentials = true;

export const BACK_URL = "http://localhost:5027/";

export const apiEndpoint = endpoint => {
    let url = `${BACK_URL}api/${endpoint}`;
    return {
        fetch: () => axios.get(url),
        fetchById: id => axios.get(url + `/${id}`),
        post: newRecord => axios.post(url, newRecord),
        put: updatedRecord => axios.put(url, updatedRecord),
        delete: () => axios.delete(url),
        deleteById: id => axios.delete(url + `/${id}`),
    }
};

