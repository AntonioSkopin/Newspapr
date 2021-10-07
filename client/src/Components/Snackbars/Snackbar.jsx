import { useEffect, useState } from "react";
import { FiCheck } from "react-icons/fi";
import { MdErrorOutline } from "react-icons/md";

const Snackbar = (props) => {
    const snackbarBgColor = props.type === "succes" ? "bg-green-500" : "bg-red-500";
    const snackbarIcon = props.type === "succes" ? <FiCheck /> : <MdErrorOutline />;
    const [visibility, setVisibility] = useState("visible");

    return (
        <div className={`absolute bottom-0 left-1/2 transform -translate-x-1/2 -translate-y-1/2 text-white px-6 py-4 border-0 rounded w-max mb-4 ${visibility} ${snackbarBgColor}`}>
            <span className="text-xl inline-block mr-5 align-middle">
                { snackbarIcon }
            </span>
            <span className="inline-block align-middle mr-8">
                <b className="capitalize">{props.type}!</b> {props.text}
            </span>
            <button className="absolute bg-transparent text-2xl font-semibold leading-none right-0 top-0 mt-4 mr-6 outline-none focus:outline-none">
                {
                    props.canBeClosed === true && <span onClick={() => setVisibility("hidden")}>×</span>
                }
            </button>
        </div>
    );
};

export default Snackbar;