import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from "react-router-dom";
import "./ProfileStyle.css";

const ProfileMenu = (props) => {
    const { id } = useParams()
    const [updateUser, setUpdateUser] = useState({
        userId: id,
        profilePicture: null,
        name: "",
        password: "",
        email: ""
    });

    const handleUpdate = (event) => {
        event.preventDefault();
        const form = new FormData();
        form.append('userId', updateUser.userId)
        if (updateUser.name.trim() !== "") {
            form.append('name', updateUser.name)
        }
        if (updateUser.email.trim() !== "") {
            form.append('email', updateUser.email)
        }
        if (updateUser.password.trim() !== "") {
            form.append('password', updateUser.password)
        }
        if (updateUser.profilePicture != null) {
            form.append('image', updateUser.profilePicture)
        }

        fetch("twitter/profile/" + id, {
            method: "PATCH",
            body: form
        })
            .then(response => {
                if (response.status === 204) {
                    console.log("Okay")
                } else { response.json() }
            })
            .then(data => console.log(data))
            .catch(error => console.error(error));
    }

    const handleInputChange = (e) => {
        setUpdateUser(prevState => ({ ...prevState, [e.target.name]: e.target.value }));
    }

    return (props.trigger) ? (
        <div className="mainProfile">
            <div className="innerProfile">
                <button className="closeProfile" onClick={() => props.setTrigger(false)}>Close</button>
                <form className="formProfile" onSubmit={handleUpdate}>
                    <input type="file" accept="image/*" name="profilePicture" onChange={(event) => setUpdateUser({ ...updateUser, profilePicture: event.target.files[0]})} />
                    <label htmlFor="Name"> Name: </label>
                    <input type="text" minLength="4" maxLength="16" className="profileInput" id="Name" name="name" onChange={handleInputChange} value={updateUser.name} />
                    <label htmlFor="Password"> Password: </label>
                    <input type="password" minLength="8" maxLength="20" className="profileInput" id="Password" name="password" onChange={handleInputChange} value={updateUser.password} />
                    <label htmlFor="Email"> Email: </label>
                    <input type="email" minLength="3" className="profileInput" id="Email" name="email" onChange={handleInputChange} value={updateUser.email} />
                    <input className="buttonProfile" type="submit" value="Submit" />
                </form>
                {props.children}
            </div>
        </div>
    ) : ""
}
export default ProfileMenu;