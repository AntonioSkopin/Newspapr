// Hooks
import { useState } from "react";

// Assets
import LoginIMG from "../../Assets/Images/login-img.jpg";

// Router
import { Link } from "react-router-dom";

// Components
import BlackButton from "../../Components/Buttons/BlackButton";
import Input from "../../Components/Inputs/Input";

import { useDispatch } from "react-redux";
import { authenticate } from "../../Actions/auth.action";

const LoginPage = (props) => {
    const [formData, setFormData] = useState({});
    const [loginResponse, setLoginResponse] = useState();
    const dispatch = useDispatch();

    const handleFormChange = e => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async e => {
        e.preventDefault();

        dispatch(authenticate(formData)).then(loginRes => {
            console.log(loginRes)
            setLoginResponse(loginRes);
        });
    };

    return (
        <section className="flex flex-col items-center h-screen md:flex-row">
            <div className="hidden w-full h-screen bg-white lg:block md:w-1/3 lg:w-2/3">
                <img src={LoginIMG} alt="" className="object-cover w-full h-full" />
            </div>
            <div className="flex items-center justify-center w-full h-screen px-6 bg-white md:max-w-md lg:max-w-full md:mx-auto md:w-1/2 xl:w-1/3 lg:px-16 xl:px-12">
                <div className="w-full h-100">
                    <div className="flex items-center w-32 mb-4 font-medium text-blueGray-900 title-font md:mb-0">
                        <div className="w-2 h-2 p-2 mr-2 rounded-full" style={{ backgroundColor: "#F6C158" }}>
                        </div>
                        <h2 className="headline text-lg font-bold tracking-tighter text-black uppercase">
                            Newspapr.
                        </h2>
                    </div>
                    <h1 className="headline mt-12 text-2xl font-semibold text-black tracking-ringtighter sm:text-3xl title-font">Log in to your account</h1>
                    <form className="mt-6" method="POST">
                        <div>
                            <label className="block text-sm font-medium leading-relaxed tracking-tighter text-gray-700">Email Address</label>
                            <Input 
                                event={handleFormChange}
                                type="email"
                                placeholder="Enter your email"
                                name="Email"
                            />    
                        </div>
                        <div className="mt-4">
                            <label className="block text-sm font-medium leading-relaxed tracking-tighter text-gray-700">Password</label>
                            <Input
                                event={handleFormChange}
                                type="password"
                                name="Password"
                                placeholder="Your Password"
                            />
                        </div>
                        <div className="mt-2 mb-8 text-right">
                            <a href="#" className="text-sm font-semibold leading-relaxed text-blueGray-700 hover:text-black focus:text-blue-700">Forgot Password?</a>
                        </div>
                        <BlackButton 
                            text="Log In"
                            event={handleSubmit} />
                    </form>
                    <p className="text-red-500 pt-4">{ loginResponse }</p>
                    <p className="mt-8 text-center">Need an account? <Link to="/register" className="font-semibold text-blue-500 hover:text-blue-700">Sign Up</Link></p>
                </div>
            </div>
        </section>
    );
};

export default LoginPage;