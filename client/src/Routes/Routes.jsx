// Router
import { BrowserRouter } from "react-router-dom";

// Routes
import AuthRoutes from "./AuthRoutes";
import ArticleRoutes from "./ArticleRoutes";
import { useEffect } from "react";

const Routes = () => {
    const checkIfUserLoggedIn = () => {
        return localStorage.getItem("user") ? true : false;
    };
    let isLoggedIn = checkIfUserLoggedIn();

    useEffect(() => {
        isLoggedIn = checkIfUserLoggedIn();
    }, []);

    return (
        <BrowserRouter>
            <AuthRoutes />
            <ArticleRoutes loggedIn={isLoggedIn} />
        </BrowserRouter>
    );
};

export default Routes;