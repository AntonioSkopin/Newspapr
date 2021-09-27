// Router
import { BrowserRouter } from "react-router-dom";

// Routes
import AuthRoutes from "./AuthRoutes";
import ArticleRoutes from "./ArticleRoutes";

const Routes = () => {
    return (
        <BrowserRouter>
            <AuthRoutes />
            <ArticleRoutes />
        </BrowserRouter>
    );
};

export default Routes;