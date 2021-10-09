import ArticleCard from "./ArticleCard";
import {BsArrowRight} from "react-icons/bs";
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import ArticleService from "../../Services/ArticleService";

const FeaturedArticles = props => {
    const [featuredArticles, setFeaturedArticles] = useState();

    useEffect(() => {
        ArticleService.GetTop3ArticlesOfTag(props.tag).then(res => {
            setFeaturedArticles(res.data);
        });
        console.log(featuredArticles)
    }, []);
    
    return (
        <div className="w-full h-full py-8">
            <span className="flex justify-between items-center">
                <h1 className="text-2xl font-semibold">{props.tag}</h1>
                <Link className="flex items-center" to={`/articles/${props.tag}`}>
                    More from {props.tag} &nbsp;&nbsp;
                    <BsArrowRight className="text-xl" />
                </Link>
            </span>
            <div className="w-full h-full grid grid-cols-3 gap-x-6 py-8 border-b-2 border-gray-100">
                {
                    featuredArticles != undefined && featuredArticles.map(article => {
                        return (
                            <Link className="cursor-pointer" to={`/articles/${article.tag}`}>
                                <ArticleCard 
                                    title={article.title} 
                                    fullname={article.fullname} 
                                    imageID={article.imageID} 
                                    numLikes={article.numLikes} 
                                    numSaved={article.numSaved} />
                            </Link>
                        )
                    })
                }
            </div>
        </div>
    );
};

export default FeaturedArticles;