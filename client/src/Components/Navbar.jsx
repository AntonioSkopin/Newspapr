import { Link } from "react-router-dom";

const Navbar = () => {
    const tags = [
        "Business",
        "Sports",
        "Education",
        "Crime",
        "Art",
        "Environment",
        "Tech"
    ];

    return (
        <nav className="my-8 w-full h-12 border-gray-100 border-t-2 border-b-2">
            <ul className="w-full h-full flex justify-between items-center px-6">
                { 
                    tags.map(tag => {
                        return (
                            <li>
                                <Link to={`/articles/${tag}`}>
                                    { tag }
                                </Link>
                            </li>
                        )
                    })
                }
            </ul>
        </nav>
    );
};

export default Navbar;