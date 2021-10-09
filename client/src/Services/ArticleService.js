// API library
import axios from "axios";

const getSpotlightArticles = async tag => {
    try {
        return await axios.get("/api/Article/GetSpotlightArticles?tag=" + tag)
            .then(articles => {
                return articles;
            }
            ).catch(error => {
                return error.response.data.message;
            });
    } catch (error) {
        console.log(error);
    }
};

const GetTop3ArticlesOfTag = async tag => {
    try {
        return await axios.get("/api/Article/GetTop3ArticlesOfTag?tag=" + tag)
            .then(articles => {
                return articles;
            }
            ).catch(error => {
                return error.response.data.message;
            });
    } catch (error) {
        console.log(error);
    }
};

const getAllArticlesOfTag = async tag => {
    try {
        return await axios.get("/api/Article/GetAllArticlesOfTag?tag=" + tag)
            .then(articles => {
                return articles;
            }
            ).catch(error => {
                return error.response.data.message;
            });
    } catch (error) {
        console.log(error);
    }
};

const createArticle = async article => {
    try {
        return await axios.post("/api/Article/CreateArticle", article);
    } catch (error) {
        console.log(error);
    }
};

export default {
    getSpotlightArticles,
    GetTop3ArticlesOfTag,
    getAllArticlesOfTag,
    createArticle
};