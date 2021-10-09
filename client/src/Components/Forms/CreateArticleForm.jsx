import axios from "axios";
import { useState } from "react";
import { useSelector } from "react-redux";
import ArticleService from "../../Services/ArticleService";
import Input from "../Inputs/Input";
import BlackButton from "../Buttons/BlackButton";

const CreateArticleForm = () => {

    const [formData, setFormData] = useState({});
    const [previewSource, setPreviewSource] = useState('');
    const { user: currentUser } = useSelector((state) => state.auth);

    const handleFileInputChange = (e) => {
        const file = e.target.files[0];
        previewFile(file);
    }

    const previewFile = (file) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            setPreviewSource(reader.result);
        }
    }
    
    const handleFormChange = e => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };

    const uploadImage = async img => {
        const imgObj = {
            file: img,
            upload_preset: "Newspapr"
        };
        let image_id;
        
        await axios.post("https://api.cloudinary.com/v1_1/askopin/image/upload", imgObj).then(res => {
            image_id = res.data.public_id;
        });

        return image_id;
    };

    const handleSubmit = async e => {
        e.preventDefault();

        // Create article object
        formData.ImageID = await uploadImage(previewSource);
        formData.PostedBy = currentUser.gd;

        await ArticleService.createArticle(formData);
    };

    return (
        <div className="container mx-auto px-4 py-12">
            <h1 className="headline mt-12 text-2xl font-semibold text-black tracking-ringtighter sm:text-3xl title-font">Create a new article</h1>
            <form className="mt-6" method="POST">
                <div>
                    <label className="block text-sm font-medium leading-relaxed tracking-tighter text-gray-700">Title</label>
                    <Input 
                        event={handleFormChange}
                        type="text"
                        placeholder="Enter your email"
                        name="Title"
                    />    
                </div>
                <div className="mt-4">
                    <label className="block text-sm font-medium leading-relaxed tracking-tighter text-gray-700">Content</label>
                    <Input
                        event={handleFormChange}
                        type="text"
                        name="Content"
                        placeholder="Your Password"
                    />
                </div>
                <div className="mt-4">
                    <label className="block text-sm font-medium leading-relaxed tracking-tighter text-gray-700">Tag</label>
                    <Input
                        event={handleFormChange}
                        type="text"
                        name="Tag"
                        placeholder="Your Password"
                    />
                </div>
                <div className="mt-4 mb-6">
                    <label className="block text-sm font-medium leading-relaxed tracking-tighter text-gray-700">Content</label>
                    <input onChange={handleFileInputChange} type="file" name="Img" id="" />
                </div>
                <BlackButton 
                    text="Create"
                    event={handleSubmit} 
                />
            </form>
        </div>
    );
};

export default CreateArticleForm;