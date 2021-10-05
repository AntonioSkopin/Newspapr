// Router
import { Switch } from "react-router-dom";
import { Route } from "react-router";

// Lazy loading
import { lazy, Suspense } from "react";

// Path
import { PATH } from "../Constants/paths";

// Components
const LoginPage = lazy(() => import("../Pages/Auth/LoginPage"));
const RegisterPage = lazy(() => import("../Pages/Auth/RegisterPage"));
const ActivateAccountPage = lazy(() => import("../Pages/Auth/ActivateAccountPage"));

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
            <Route
                exact
                path={PATH.ACTIVATE_ACCOUNT}
                component={() => (
                    <Suspense fallback={<div>wait</div>}>
                        <ActivateAccountPage />
                    </Suspense>
                )}
            />
        </Switch>
    );
};

export default AuthRoutes;