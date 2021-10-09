import IMG2 from "../../Assets/Images/interview.jpg";
import { FiHeart, FiBookmark } from "react-icons/fi";
import CloudinaryImage from "../Image/CloudinaryImage";

const ArticleCard = props => {
    return (
        <div className="h-full flex flex-col justify-between">
            <CloudinaryImage imgID={props.imageID} alt="" className="w-full h-full object-cover" />
            <div className="md:px-2 pb-4">
                <h1 className="headline text-lg md:text-2xl py-4">{props.title.slice(0, 70)}...</h1>
                <p className="text-gray-400">by {props.fullname}.</p>
                <span className="flex items-center pt-4">
                    <span className="flex items-center mr-3">
                        <FiHeart className="text-red-500 mr-1" />
                        <p className="text-gray-600">{props.numLikes}</p>
                    </span>
                    <span className="flex items-center ml-3">
                        <FiBookmark className="text-blue-500 mr-1" />
                        <p className="text-gray-600">{props.numSaved}</p>
                    </span>
                </span>
            </div>
        </div>
    );
};

export default ArticleCard;