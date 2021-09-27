const Input = props => {
    return (
        <input
            type={props.type}
            name={props.name}
            placeholder={props.placeholder}
            className="w-full px-4 py-3 text-base text-black transition duration-500 ease-in-out transform border-transparent rounded-lg bg-gray-100 focus:border-gray-500 focus:bg-white focus:outline-none focus:shadow-outline focus:ring-2 ring-offset-current ring-offset-2 " />
    );
};

export default Input;