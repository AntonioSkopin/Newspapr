import IMG from "../../Assets/Images/article-img.webp";
import IMG2 from "../../Assets/Images/interview.jpg";
import {BsArrowRight} from "react-icons/bs";
import {AiFillPlayCircle} from "react-icons/ai";

const ArticleCard = props => {
    return (
        <div>
            <img src={IMG2} alt="" className="w-full" />
            <h1 className="headline text-lg md:text-2xl py-4">Lebron James emotional after big win and LA Lakers win finals.</h1>
            <p className="flex items-center text-lg text-gray-700">
                <AiFillPlayCircle className="mr-4" />
                Watch now
            </p>
        </div>
    );
};

export default ArticleCard;