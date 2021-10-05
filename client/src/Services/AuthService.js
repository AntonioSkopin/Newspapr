// API library
import axios from "axios";

const authenticate = (model) => {
    try {
        return axios.post("/api/Auth/Authenticate", model)
            .then(userResponse => {
                console.log(userResponse);
                
                if (userResponse.data) {
                    // Store user object in localstorage
                    localStorage.setItem("user", JSON.stringify(userResponse.data));
                }

                return userResponse;
            }
            ).catch(error => {
                return error.response.data.message;
            });
    } catch (error) {
        console.log(error);
    }
}

const register = (model) => {
    try {
        return axios.post("/api/Auth/Register", model)
            .then(res => {
                return {
                    message: res.data.message,
                    type: "success"
                };
            }).catch(error => {
                return {
                    message: error.response.data.message,
                    type: "error"
                };
            });
    } catch (error) {
        console.log(error);
    }
}

const activateAccount = (pincode) => {
    try {
        return axios.post("/api/Auth/ActivateAccount", pincode)
            .then(res => {
                return {
                    message: res.data.message,
                    type: "success"
                };
            }).catch(error => {
                return {
                    message: error.response.data.message,
                    type: "error"
                };
            });
    } catch (error) {
        console.error(error);
    }
}

const logout = () => {
    localStorage.removeItem("user");
    window.location.href = "/login";
}

export default {
    authenticate,
    register,
    activateAccount,
    logout
};