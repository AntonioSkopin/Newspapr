const BlackButton = (props) => {
    return (
        <button onClick={props.event} className="block w-full px-4 py-3 font-semibold text-white transition duration-500 ease-in-out transform bg-black rounded-lg hover:bg-blueGray-800 focus:shadow-outline focus:outline-none focus:ring-2 ring-offset-current ring-offset-2 ">
            {props.text}
        </button>
    );
};

export default BlackButton;