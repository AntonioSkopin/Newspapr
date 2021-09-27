import IMG from "../../Assets/Images/article-img.webp";

const SpotlightSection = props => {
    return (
        <div className="w-full h-auto flex flex-col md:flex-row justify-between border-b-2 border-gray-100 pb-4">
                <div className="md:w-1/2 h-full pb-12 md:py-0 md:pr-12 md:border-r-2 border-gray-100">
                    <div className="h-full text-center relative">
                        <img src={IMG} alt="" className="w-max object-cover pb-2 h-1/2" />
                        <p className="uppercase text-sm py-4 font-semibold">Business</p>
                        <h1 className="text-3xl headline leading-relaxed">
                            Like a Boss: "I have to have my first tough
                            conversation with an employee who's underperforming."
                        </h1>
                        <p className="text-gray-400 py-4">by Antonio Skopin.</p>
                    </div>
                </div>
                <div className="md:w-1/2 h-full md:pl-12">
                    <div className="h-full">
                        <span className="flex items-center justify-between">
                            <h1 className="text-2xl pb-2 font-bold">
                                Spotlight
                            </h1>
                            <p className="text-gray-400">{new Date().toLocaleString() + ""}</p>
                        </span>
                        <div className="py-4 w-full flex justify-between items-center border-b-2 border-gray-100">
                            <div className="w-2/3">
                                <p className="uppercase text-sm py-2 font-semibold">Business</p>
                                <h1 className="text-3xl headline leading-relaxed">
                                    How to ask your manager for feedback
                                </h1>
                                <p className="text-gray-400 py-2">by Antonio Skopin.</p>
                            </div>
                            <div className="w-1/2">
                                <img src={IMG} alt="" className="w-1/1" />
                            </div>
                        </div>
                        <div className="py-4 w-full flex justify-between items-center border-b-2 border-gray-100">
                            <div className="w-2/3">
                                <p className="uppercase text-sm py-2 font-semibold">Business</p>
                                <h1 className="text-3xl headline leading-relaxed">
                                    How to ask your manager for feedback
                                </h1>
                                <p className="text-gray-400 py-2">by Antonio Skopin.</p>
                            </div>
                            <div className="w-1/2">
                                <img src={IMG} alt="" className="w-1/1" />
                            </div>
                        </div>
                        <div className="py-4 w-full flex justify-between items-center">
                            <div className="w-2/3">
                                <p className="uppercase text-sm py-2 font-semibold">Business</p>
                                <h1 className="text-3xl headline leading-relaxed">
                                    How to ask your manager for feedback
                                </h1>
                                <p className="text-gray-400 py-2">by Antonio Skopin.</p>
                            </div>
                            <div className="w-1/2">
                                <img src={IMG} alt="" className="w-1/1" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    );
};

export default SpotlightSection;