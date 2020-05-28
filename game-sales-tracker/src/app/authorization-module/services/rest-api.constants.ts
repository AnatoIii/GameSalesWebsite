const AUTH = "/auth";
const ACCOUNT = "/account";

export const LIST_URI = {
    login: `${AUTH}/login`,
    register: `${AUTH}/create`,
    getAllUsers: `${ACCOUNT}/getAll`,
    getById: `${ACCOUNT}/?userId=`,
    updateUser: `${ACCOUNT}/update`,
    uploadPhoto: `${ACCOUNT}/upload`
};
