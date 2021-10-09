import { useEffect, useState } from "react";
import { FiBookmark, FiHeart } from "react-icons/fi";
import { useParams } from "react-router";
import ArticleService from "../../Services/ArticleService";
import CloudinaryImage from "../Image/CloudinaryImage";

const SpotlightSection = props => {

    const [spotlightArticles, setSpotlightArticles] = useState();
    let { tag } = useParams();

    useEffect(() => {
        ArticleService.getSpotlightArticles(tag || props.tag).then(res => {
            setSpotlightArticles(res.data);
        });
    }, [tag]);

    return (
        <div className="w-full h-auto flex flex-col md:flex-row justify-between border-b-2 border-gray-100 py-4">
            <div className="md:w-1/2 h-full pb-12 md:py-0 md:pr-12 md:border-r-2 border-gray-100">
                {
                    spotlightArticles !== undefined ? <div className="h-full text-center relative">
                        <span className="flex items-center justify-between border-b-2 border-gray-100">
                            <h1 className="text-2xl pb-2 font-bold">
                                Best of {spotlightArticles[0].tag}
                            </h1>
                        </span>
                        <CloudinaryImage className="w-max object-cover pb-2 h-1/2 mt-4" imgID={spotlightArticles[0].imageID} />
                        <p className="uppercase text-sm py-4 font-semibold">{spotlightArticles[0].tag}</p>
                        <h1 className="text-3xl headline leading-relaxed">
                            {spotlightArticles[0].title}
                        </h1>
                        <p className="text-gray-400 py-4">by {spotlightArticles[0].fullname}.</p>
                        <span className="flex items-center justify-center pt-2">
                            <span className="flex items-center mr-3">
                                <FiHeart className="text-red-500 mr-1" />
                                <p className="text-gray-600">{spotlightArticles[0].numLikes}</p>
                            </span>
                            <span className="flex items-center ml-3">
                                <FiBookmark className="text-blue-500 mr-1" />
                                    <p className="text-gray-600">{spotlightArticles[0].numSaved}</p>
                            </span>
                        </span>
                    </div> : <div></div>
                }
            </div>
                <div className="md:w-1/2 h-full md:pl-12">
                    <div className="h-full">
                        <span className="flex items-center justify-between">
                            <h1 className="text-2xl pb-2 font-bold">
                                Spotlight
                            </h1>
                            <p className="text-gray-400">{new Date().toLocaleString() + ""}</p>
                        </span>
                        {
                            spotlightArticles !== undefined ? spotlightArticles?.slice(1,4)?.map(article => {
                                return (
                                    <div className="py-4 w-full flex justify-between items-center border-t-2 border-gray-100">
                                        <div className="w-2/3">
                                            <p className="uppercase text-sm py-1 font-semibold">{article.tag}</p>
                                            <h1 className="text-3xl headline leading-relaxed">
                                                {article.title.slice(0, 42)}...
                                            </h1>
                                            <p className="text-gray-400 py-1">by {article.fullname}.</p>
                                            <span className="flex items-center pt-2">
                                                <span className="flex items-center mr-3">
                                                    <FiHeart className="text-red-500 mr-1" />
                                                    <p className="text-gray-600">{article.numLikes}</p>
                                                </span>
                                                <span className="flex items-center ml-3">
                                                    <FiBookmark className="text-blue-500 mr-1" />
                                                    <p className="text-gray-600">{article.numSaved}</p>
                                                </span>
                                            </span>
                                        </div>
                                        <div className="w-1/2">
                                            {/* <img src={IMG} alt="" className="w-1/1" /> */}
                                            <CloudinaryImage imgID={article.imageID} />
                                        </div>
                                    </div>
                                )
                            }) : <div></div>
                        }
                    </div>
                </div>
        </div>
    );
};

export default SpotlightSection;