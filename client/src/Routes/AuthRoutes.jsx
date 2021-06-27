// Router
import { Switch } from "react-router-dom";
import { Route } from "react-router";

// Lazy loading
import { lazy, Suspense } from "react";

// Path
import { PATH } from "../Constants/paths";

// Components
const LoginPage = lazy(() => import("../Pages/Auth/LoginPage/LoginPage"));
const RegisterPage = lazy(() => import("../Pages/Auth/RegisterPage/RegisterPage"));

const AuthRoutes = () => {
    return (
        <Switch>
            <Route
                exact
                path={PATH.LOGIN}
                component={() => (
                    <Suspense fallback={<div>wait</div>}>
                        <LoginPage />
                    </Suspense>
                )}
            />
            <Route
                exact
                path={PATH.REGISTER}
                component={() => (
                    <Suspense fallback={<div>wait</div>}>
                        <RegisterPage />
                    </Suspense>
                )}
            />
        </Switch>
    );
};

export default AuthRoutes;