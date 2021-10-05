// Router
import { Switch } from "react-router-dom";
import { Redirect, Route } from "react-router";

// Path
import { PATH } from "../Constants/paths";

// Components
import HomePage from "../Pages/Articles/Homepage";

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
        </Switch>
    );
};

export default ArticleRoutes;