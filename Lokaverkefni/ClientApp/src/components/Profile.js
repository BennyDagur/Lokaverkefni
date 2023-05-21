import React, { useState, useEffect } from 'react';
import { useParams } from "react-router-dom";
import './HomeStyle.css';
import PostMenu from './PostMenu';
import ProfileMenu from './ProfileMenu';
import FollowBox from './FollowBox';
import PostCard from './PostCard';
import TrendBox from './TrendBox';


const Profile = () => {
    const { id } = useParams()
    const [user, setUser] = useState();
    const [post, setPost] = useState([]);
    const [postMenu, setPostMenu] = useState(false);
    const [profileMenu, setProfileMenu] = useState(false);


    const twitter = async () => {
        const res = await fetch("twitter/profile/" + id);
        const body = await res.json();
        setUser(body)
    };

    const twitterPosts = async () => {
        const res = await fetch("twitter/post");
        const body = await res.json();
        setPost(body)
    }

    const handleFollow = (followerId, followingId) => {

        if (followerId == followingId) {
            return alert("You cannot follow yourself")
        }

        fetch(`twitter/profile/${followerId}/follow/${followingId}`, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ followerId, followingId })
        })
            .then(response => {
                twitter();
                response.json()
            }
                )
            .then(data => console.log(data))
            .catch(error => console.error(error));
    }

    const handleDelete = () => {
        fetch("twitter/profile/" + id, {
            method: 'DELETE'
        })
            .then(res => res.json())
            .then(data => console.log(data))
            .catch(error => console.error(error));
    }

    useEffect(() => {
        twitter();
        twitterPosts();
        window.scrollTo(0, 0)
    }, [profileMenu, id]);

    return (
        <div className="homeBody">
            <div className="hide">
                <FollowBox/>
                <button className="postButton" onClick={() => setPostMenu(true)}>POST</button>
            </div>
            <div>{user &&
                <header className="profileHead">
                    <img className="profilePhoto" src={user.profilePicture} onClick={() => setProfileMenu(true)} />
                    <h1 className="profileText">Welcome to the profile of {user.name}</h1>
                    <div className="sideToSide"><h5>{user.followingId.length} Following</h5> <h5>{user.followerId.length} Followers</h5>
                    </div><button className="followButton" onClick={() => handleFollow( 1 , id)}>Follower</button>
                </header>}
                <main className="homeMain">
                    <section>
                        <h2>Latest Tweets</h2>
                        {
                            post ? (
                                post.map((item) => (
                                    item.userId == id ? (
                                    <PostCard key={item.postId} item={item}></PostCard>
                                ): null ))
                            ) : (<p>loading...</p>)
                        }
                        <button className="postButton bigHide" onClick={() => setPostMenu(true)}>P</button>
                    </section>
                </main>
            </div>
                <TrendBox />
            <PostMenu trigger={postMenu} setTrigger={setPostMenu}>
                <div>
                </div>
            </PostMenu>
            <ProfileMenu trigger={profileMenu} setTrigger={setProfileMenu}>
                <div>
                    <button className="deleteBtn" onClick={() => handleDelete()}>Delete</button>
                </div>
            </ProfileMenu>
        </div>
    )
}
export default Profile;