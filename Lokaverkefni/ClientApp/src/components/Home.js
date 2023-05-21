import React, { useState, useEffect } from 'react';
import './HomeStyle.css';
import PostMenu from './PostMenu';
import FollowBox from './FollowBox';
import PostCard from './PostCard';
import TrendBox from './TrendBox';


const Home = () => {
    const [post, setPost] = useState([]);
    const [postMenu, setPostMenu] = useState(false);

    const twitterPosts = async () => {
        const res = await fetch("twitter/post");
        const body = await res.json();
        setPost(body)
    }

    useEffect(() => {
        twitterPosts();
        window.scrollTo(0, 0)
    }, []);

    return (
        <div className="homeBody">
            <div className="hide">
                <FollowBox />
                <button className="postButton" onClick={() => setPostMenu(true)}>POST</button>
            </div>
            <div>
                <header className="pageHead">
                    <h1>Welcome to Twitter</h1>
                </header>
                <main className="homeMain">
                    <section>
                        <h2>Latest Tweets</h2>

                        {
                            post ? (
                                post.map((item) => (
                                    <PostCard key={item.postId} item={item}></PostCard>
                                ))
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
        </div>
        )
}
export default Home;