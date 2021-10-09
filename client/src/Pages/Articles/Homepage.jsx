// Components
import FeaturedArticles from "../../Components/Articles/FeaturedArticles";
import SpotlightSection from "../../Components/Articles/SpotlightSection";
import Snackbar from "../../Components/Snackbars/Snackbar";
import Navbar from "../../Components/Navbar";
import Header from "../../Components/Header";

const HomePage = () => {
    const tags = [
        "Business",
        "Sports",
        "Education",
        "Crime",
        "Art",
        "Environment",
        "Tech"
    ];

    return (
        <section className="container mx-auto px-4 py-12">
            <Header />
            <Navbar />

            {/* ARTICLES */}
            <SpotlightSection tag="Business" />
            { tags.map(tag => { return <FeaturedArticles tag={tag} /> }) }
            {/* ARTICLES */}
            
            {/* <Snackbar type="succes" text="Successfully logged in!" canBeClosed={true} /> */}
        </section>
    );
};

export default HomePage;