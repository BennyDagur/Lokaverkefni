import React, { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import './HomeStyle.css';

const FollowBox = () => {
    const [other, setOther] = useState([]);
    const navigate = useNavigate();

    const profilepage = (id) => {
        navigate("/" + "profile/" + id)
    }

    const twitterUsers = async () => {
        const res = await fetch("twitter/profile");
        const body = await res.json();
        setOther(body)
        console.log(body)
    };

    useEffect(() => {
        twitterUsers();
    }, [])

    return (
        <aside className="followBox">
            <h2 className="boxFont">Who to follow</h2>
            {other ? (
                other.slice(0, 3).map((item) => (
                    <div key={item.userId} onClick={() => { profilepage(item.userId) }}>
                        <img className="followPhoto" src={item.profilePicture} />
                        <p className="followText">{item.name}</p>
                    </div>
                ))
            ) : (<p>loading...</p>)}
        </aside>
        )
}
export default FollowBox;