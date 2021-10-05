// Hooks
import { useState } from "react";

// Assets
import RegisterIMG from "../../Assets/Images/register-img.jpg";

// Router
import { Link } from "react-router-dom";
import BlackButton from "../../Components/Buttons/BlackButton";
import Input from "../../Components/Inputs/Input";

import { register } from "../../Actions/auth.action";
import { useDispatch } from "react-redux";

const RegisterPage = () => {
    const [formData, setFormData] = useState({});
    const [responseMessage, setResponseMessage] = useState({});
    const dispatch = useDispatch();
    const responseTextColor = responseMessage.type === "success" ? "text-green-500" : "text-red-500";

    const handleFormChange = e => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async e => {
        e.preventDefault();
        
        dispatch(register(formData)).then(res => {
            setResponseMessage(res);
        });
    };

    return (
        <section className="flex flex-col items-center h-screen bg-blueGray-100 md:flex-row ">
            <div className="relative hidden w-full h-screen bg-blueGray-400 lg:block md:w-1/3 lg:w-2/3">
                <img src={RegisterIMG} alt="" className="absolute object-cover w-full h-full" />
            </div>
            <div className="flex items-center justify-center w-full h-screen px-6 bg-white md:max-w-md lg:max-w-full md:mx-auto md:w-1/2 xl:w-1/3 lg:px-16 xl:px-12">
                <div className="w-full h-100">
                    <div className="flex items-center w-32 mb-4 font-medium text-blueGray-900 title-font md:mb-0">
                        <div className="w-2 h-2 p-2 mr-2 rounded-full" style={{ backgroundColor: "#F6C158" }}>
                        </div>
                        <h2 className="headline text-lg font-bold tracking-tighter text-black uppercase duration-500 ease-in-out transform ttransition hover:text-lightBlue-500 dark:text-blueGray-400"> Newspapr. </h2>
                    </div>
                    <h1 className="headline mt-12 text-2xl font-semibold text-black tracking-ringtighter sm:text-3xl title-font">Create your free account</h1>
                    <form className="mt-6" method="POST">
                        <div>
                            <label className="block text-sm font-medium leading-relaxed tracking-tighter text-gray-700">Full name</label>
                            <Input
                                event={handleFormChange}
                                type="text"
                                name="Fullname"
                                placeholder="Your Full name"
                                className="w-full px-4 py-2 mt-2 text-base text-black transition duration-500 ease-in-out transform border-transparent rounded-lg bg-gray-100 focus:border-gray-500 focus:bg-white focus:outline-none focus:shadow-outline focus:ring-2 ring-offset-current ring-offset-2 " />
                        </div>
                        <div className="mt-4">
                            <label className="block text-sm font-medium leading-relaxed tracking-tighter text-gray-700">Email</label>
                            <Input
                                event={handleFormChange}
                                type="email"
                                name="Email"
                                placeholder="Your Email"
                            />
                        </div>
                        <div className="mt-4 mb-8">
                            <label className="block text-sm font-medium leading-relaxed tracking-tighter text-gray-700">Password</label>
                            <Input
                                event={handleFormChange}
                                type="password"
                                name="Password"
                                placeholder="Your Password"
                                minLength="6"
                            />         
                       </div>
                        <BlackButton 
                            event={handleSubmit}
                            text="Create your account" />
                    </form>
                    <p className={`mt-4 ${responseTextColor}`}>{ responseMessage.message }</p>
                    <p className="mt-8 text-center">Already an account? <Link to="/login" className="font-semibold text-blue-500 hover:text-blue-700">Sign In</Link></p>
                </div>
            </div>
        </section>
    );
};

export default RegisterPage;