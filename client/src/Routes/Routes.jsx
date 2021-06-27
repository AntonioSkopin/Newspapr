// Router
import { BrowserRouter } from "react-router-dom";

// Routes
import AuthRoutes from "./AuthRoutes";

const Routes = () => {
    return (
        <BrowserRouter>
            <AuthRoutes />
        </BrowserRouter>
    );
};

export default Routes;