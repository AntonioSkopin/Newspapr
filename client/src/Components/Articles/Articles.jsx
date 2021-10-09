import { useEffect, useState } from "react";
import { useParams } from "react-router";
import ArticleService from "../../Services/ArticleService";
import ArticleCard from "./ArticleCard";

const Articles = props => {
    const [articles, setArticles] = useState();
    const { tag } = useParams();

    useEffect(() => {
        ArticleService.getAllArticlesOfTag(tag).then(res => {
            setArticles(res.data);
        });
    }, [tag]);

    return (
        <div className="container mx-auto px-4 py-12">
            <div className="grid grid-cols-3 gap-x-6 gap-y-9">
                {
                    articles !== undefined && articles.map(article => {
                        return (
                            <ArticleCard 
                                title={article.title} 
                                fullname={article.fullname} 
                                imageID={article.imageID} 
                                numLikes={article.numLikes} 
                                numSaved={article.numSaved} />
                        )
                    })
                }
            </div>
        </div>
    );
};

export default Articles;