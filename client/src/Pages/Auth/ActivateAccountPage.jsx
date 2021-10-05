// Hooks
import { useState } from "react";

// Assets
import LoginIMG from "../../Assets/Images/activate-account.jpg";

// Router
import { Link } from "react-router-dom";

// Components
import BlackButton from "../../Components/Buttons/BlackButton";
import Input from "../../Components/Inputs/Input";

import { useDispatch } from "react-redux";
import { activateAccount } from "../../Actions/auth.action";


const ActivateAccountPage = () => {

    const [formData, setFormData] = useState();
    const [activationResponse, setActivationResponse] = useState({});
    const dispatch = useDispatch();
    const activationResponseTextColor = activationResponse.type === "success" ? "text-green-500" : "text-red-500";

    const handleInputChange = e => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async e => {
        e.preventDefault();
        console.log(formData);
        dispatch(activateAccount(formData)).then(res => {
            setActivationResponse(res);
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
                    <h1 className="headline mt-12 text-2xl font-semibold text-black tracking-ringtighter sm:text-3xl title-font">
                        Activate your account
                    </h1>
                    <form className="mt-6" method="POST">
                        <div className="mb-8">
                            <label className="block text-sm font-medium leading-relaxed tracking-tighter text-gray-700">
                                Your Activation PIN
                            </label>
                            <Input 
                                event={handleInputChange}
                                type="email"
                                placeholder="Ex. 4353"
                                name="Pincode"
                            />    
                        </div>
                        <BlackButton 
                            text="Activate"
                            event={handleSubmit} />
                    </form>
                    <p className={`pt-4 ${activationResponseTextColor}`}>{ activationResponse.message }</p>
                    <p className="mt-8 text-center">
                        Is your account already activated? &nbsp;
                        <Link to="/login" className="font-semibold text-blue-500 hover:text-blue-700">
                            Log in
                        </Link>``
                    </p>
                </div>
            </div>
        </section>
    );
};

export default ActivateAccountPage;