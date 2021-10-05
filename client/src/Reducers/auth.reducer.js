import { ACTION } from "../Constants/actions.constant";

const user = JSON.parse(localStorage.getItem("user"));
const initialState = user
    ? { isLoggedIn: true, user }
    : { isLoggedIn: false, user: null };

export default function (state = initialState, action) {
    const { type, payload } = action;

    switch(type) {
        case ACTION.REGISTER_SUCCESS:
            return {
                ...state,
                isLoggedIn: false
            };
        case ACTION.REGISTER_FAIL:
            return {
                ...state,
                isLoggedIn: false,
            };
        case ACTION.LOGIN_SUCCESS:
            return {
                ...state,
                isLoggedIn: true,
                user: payload.user,
            };
        case ACTION.LOGIN_FAIL:
            return {
                ...state,
                isLoggedIn: false,
                user: null,
            };
        case ACTION.ACTIVATION_SUCCESS:
            return {
                ...state,
                isLoggedIn: false
            };
        case ACTION.ACTIVATION_FAIL:
            return {
                ...state,
                isLoggedIn: false
            };
        case ACTION.LOGOUT:
            return {
                ...state,
                isLoggedIn: false,
                user: null,
            };
        default:
            return state;
    }
}