// Router
import { Switch } from "react-router-dom";
import { Route } from "react-router";

// Lazy loading
import { lazy, Suspense } from "react";

// Path
import { PATH } from "../Constants/paths";

// Components
import HomePage from "../Pages/Articles/Homepage";

const ArticleRoutes = () => {
    return (
        <Switch>
            <Route
                exact
                path={PATH.HOME}
                component={() => (
                    <Suspense fallback={<div>wait</div>}>
                        <HomePage />
                    </Suspense>
                )}
            />
        </Switch>
    );
};

export default ArticleRoutes;