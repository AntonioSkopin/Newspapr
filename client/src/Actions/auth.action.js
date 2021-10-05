import { ACTION } from "../Constants/actions.constant";
import AuthService from "../Services/AuthService";

export const register = (model) => (dispatch) => {
    return AuthService.register(model).then(data => {
        dispatch({
            type: ACTION.REGISTER_SUCCESS
        });

        return data;
    }, error => {
        dispatch({
            type: ACTION.REGISTER_FAIL
        });

        dispatch({
            type: ACTION.SET_MESSAGE,
            payload: "REGISTER FAILED!"
        });

        return Promise.reject();
    });
};

export const authenticate = (model) => (dispatch) => {
    return AuthService.authenticate(model).then(data => {
        dispatch({
            type: ACTION.LOGIN_SUCCESS,
            payload: { user: data }
        });

        /* 
            If status = 401 -> data === string
            If successful login -> data === object -> redirect user
        */
        if (typeof data === "string") {
            return data;
        }
        window.location.href = "/";
    }, error => {
        dispatch({
            type: ACTION.LOGIN_FAIL
        });

        dispatch({
            type: ACTION.SET_MESSAGE,
            payload: "LOGIN FAILED!"
        });

        return Promise.reject();
    });
};

export const activateAccount = (pincode) => (dispatch) => {
    return AuthService.activateAccount(pincode).then(data => {
        dispatch({
            type: ACTION.ACTIVATION_SUCCESS
        });

        dispatch({
            type: ACTION.SET_MESSAGE,
            payload: "ACCOUNT ACTIVATED!"
        });

        return data;
    }, error => {
        dispatch({
            type: ACTION.ACTIVATION_FAIL
        });

        dispatch({
            type: ACTION.SET_MESSAGE,
            payload: "ACTIVATION FAILED!"
        });

        return Promise.reject();
    });
};

export const logout = () => (dispatch) => {
    AuthService.logout();

    dispatch({
        type: ACTION.LOGOUT
    });
};