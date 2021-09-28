// Components
import BlackButton from "../../Components/Buttons/BlackButton";
import Input from "../../Components/Inputs/Input";
import FeaturedArticles from "../../Components/Articles/FeaturedArticles";
import SpotlightSection from "../../Components/Articles/SpotlightSection";

const HomePage = () => {
    return (
        <section className="container mx-auto px-4 py-12">
            <div className="text-center">
                <h1 className="text-5xl font-semibold">
                    <span className="headline font-medium">Latest news </span>
                    for Everyone
                </h1>
                <p className="uppercase py-4">All latest trending news in one website</p>
            </div>
            <nav className="my-8 w-full h-12 border-gray-100 border-t-2 border-b-2">
                <ul className="w-full h-full flex justify-between items-center px-6">
                    <li>Business</li>
                    <li>Sports</li>
                    <li>Education</li>
                    <li>Crime</li>
                    <li>Art</li>
                    <li>Environment</li>
                    <li>Tech</li>
                </ul>
            </nav>
            <SpotlightSection />
            <FeaturedArticles title="Business" />
            <FeaturedArticles title="Sports" />
            <FeaturedArticles title="Education" />
            <FeaturedArticles title="Crime" />
            <FeaturedArticles title="Art" />
            <FeaturedArticles title="Environment" />
            <FeaturedArticles title="Tech" />
        </section>
    );
};

export default HomePage;