import Navbar from "../../Components/Navbar";
import Header from "../../Components/Header";
import SpotlightSection from "../../Components/Articles/SpotlightSection";
import Articles from "../../Components/Articles/Articles";

const ArticlesPage = () => {
    return (
        <section className="container mx-auto px-4 py-12">
            <Header />
            <Navbar />
            <SpotlightSection />
            <Articles />
        </section>
    );
};

export default ArticlesPage;