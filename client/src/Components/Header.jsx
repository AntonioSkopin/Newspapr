// Components
import { useSelector } from "react-redux";
import BlackButton from "./Buttons/BlackButton";
import AuthService from "../Services/AuthService";
import { useParams } from "react-router";

const Header = () => {
    const { user: currentUser } = useSelector((state) => state.auth);
    const { tag } = useParams();

    return (
        <div>
            <span className="w-full flex justify-between items-center pb-8">
                <p className="text-lg">👋🏻 {currentUser.email}</p>
                <div className="w-max">
                    <BlackButton event={() => AuthService.logout()} text="Sign out" />
                </div>
            </span>
            <div className="text-center">
                <h1 className="text-5xl font-semibold">
                    <span className="headline font-medium">Latest news </span>
                    of { tag || "the world." }
                </h1>
                <p className="uppercase py-4">All latest trending { tag || "news" } brought to you by newspapr.</p>
            </div>
        </div>
    );
};

export default Header;