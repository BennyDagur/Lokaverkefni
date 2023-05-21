import React, { useState, useEffect } from 'react';
import "./PostStyle.css";

const PostMenu = (props) => {
    const [post, setPost] = useState(
    {
        userId: 1,
        text: "",
        image: null,
    });

    const handleCreate = (event) => {
        event.preventDefault();
        const form = new FormData();

        form.append("userId", post.userId)

        if (post.text) {
            form.append('text', post.text);
        }
        if (post.image) {
            form.append('image', post.image);
        }

        fetch("twitter/post", {
            method: "POST",
            body: form
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(error => console.error(error));
    }

    const handleInputChange = (e) => {
        setPost(prevState => ({ ...prevState, [e.target.name]: e.target.value }));
    }

    return (props.trigger) ? (
        <div className="mainPost">
            <div className="innerPost">
                <button className="closePost" onClick={() => props.setTrigger(false)}>Close</button>
                <form className="formPost" onSubmit={handleCreate}>
                    <label htmlFor="Text" className="postLabel"> Text: </label>
                    <textarea type="text" minLength="1" maxLength="200" className="postInput" id="Text" name="text" onChange={handleInputChange} value={post.text} />
                    <input type="file" accept="image/*" name="image" onChange={(event) => setPost({ ...post, image: event.target.files[0] })} />
                    <input className="buttonPost" type="submit" value="Submit" />
                </form>
                {props.children}
            </div>
        </div>
        ) : ""
}
export default PostMenu;