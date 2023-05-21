import React, { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import './HomeStyle.css';

const PostCard = ({ item }) => {
   
    const navigate = useNavigate();

    const profilepage = (id) => {
        navigate("/" + "profile/" + id)
    }

    const handleDelete = (id) => {
        fetch("twitter/post/" + id, {
            method: 'DELETE'
        })
            .then(res => res.json())
            .then(data => console.log(data))
            .catch(error => console.error(error));
    }

    useEffect(() => {
        
    }, []);

    return (
        <div>
             <div className="userPost">
                <img className="followPhoto" src={item.profilePicture} onClick={() => { profilepage(item.userId) }} />
                <h3>{item.userName}</h3>
                <button className="deletePost" onClick={() => handleDelete(item.postId)}>Delete</button>
             </div>
            <article className="postBox" key={item.postId}>
                <p className="postText">{item.text}</p>
                <img src={item.image} />
            </article>
        </div>
        )
}
export default PostCard