// Router
import { Switch } from "react-router-dom";
import { Redirect, Route } from "react-router";

// Path
import { PATH } from "../Constants/paths";

// Components
import HomePage from "../Pages/Articles/Homepage";
import ArticlesPage from "../Pages/Articles/ArticlesPage";
import CreateArticleForm from "../Components/Forms/CreateArticleForm";

const ArticleRoutes = (props) => {
    return (
        <Switch>
            <Route
                exact
                path={PATH.HOME}
                render={() => {
                    return props.loggedIn ? <HomePage /> : <Redirect to="/login" />;
                }}
            />
            <Route
                exact
                path={PATH.ARTICLE_PAGE}
                render={() => {
                    return props.loggedIn ? <ArticlesPage /> : <Redirect to="/login" />;
                }}
            />
            <Route
                exact
                path="/admin/article-form"
                render={() => {
                    return props.loggedIn ? <CreateArticleForm /> : <Redirect to="/login" />;
                }}
            />
        </Switch>
    );
};

export default ArticleRoutes;