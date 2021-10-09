import { Image } from "cloudinary-react";

const CloudinaryImage = (props) => {
    return (
        <Image
            className={props.className}
            cloudName="askopin"
            publicID={props.imgID}
            width="250"
        />
    );
};

export default CloudinaryImage;