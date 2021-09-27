import ArticleCard from "./ArticleCard";
import {BsArrowRight} from "react-icons/bs";

const FeaturedArticles = props => {
    return (
        <div className="w-full py-8">
            <span className="flex justify-between items-center">
                <h1 className="text-2xl font-semibold">{props.title}</h1>
                <a className="flex items-center">
                    More from {props.title} &nbsp;&nbsp;
                    <BsArrowRight className="text-xl" />
                </a>
            </span>
            <div className="w-full grid grid-cols-3 gap-x-6 py-8 border-b-2 border-gray-100">
                <ArticleCard />
                <ArticleCard />
                <ArticleCard />
            </div>
        </div>
    );
};

export default FeaturedArticles;