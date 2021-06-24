// API library
import axios from "axios";

export const Authenticate = async (model) => {
    try {
        console.log("MODEL: ", model);
        return await axios.post("/api/Auth/Authenticate", model);
    } catch (error) {
        console.error(error);
    }
}

export const Register = async (model) => {
    try {
        return await axios.post("/api/Auth/Register", model);
    } catch (error) {
        console.error(error);
    }
}

export const ActivateAccount = async (pincode) => {
    try {
        return await axios.post("/api/Auth/ActivateAccount", pincode);
    } catch (error) {
        console.error(error);
    }
}