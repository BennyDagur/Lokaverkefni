import React, { useState, useEffect } from 'react';
import './HomeStyle.css';

const CreateUser = () => {
    const [user, setUser] = useState(
        {
            name: "",
            password: "",
            phoneNumber: 12345,
            email: "",
            followers: [],
            following: [],
        });

    const handleCreate = (event) => {
        event.preventDefault();

        if (user.name.trim() == "" || user.password.trim() == "" || user.email.trim() == "")
        { return alert("One or more fields are empty, please fill them out") }

        fetch("twitter/profile", {
            method: "POST",
            headers: {
                'Content-Type' : 'application/json'
            },
            body: JSON.stringify(user)
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(error => console.error(error));
    }

    const handleInputChange = (e) => {
        setUser(prevState => ({ ...prevState, [e.target.name]: e.target.value }));
    }

    return (
        <div className="createBody">
            <div className="createBox">
                <form className="createForm" onSubmit={handleCreate}>
                    <label htmlFor="Name"> Name: </label>
                    <input type="text" minLength="4" maxLength="16" className="createInput" id="Name" name="name" onChange={handleInputChange} value={user.name} />
                <label htmlFor="Password"> Password: </label>
                    <input type="password" minLength="8" maxLength="20" className="createInput" id="Password" name="password" onChange={handleInputChange} value={user.password} />
                <label htmlFor="Email"> Email: </label>
                    <input type="email" minLength="3" className="createInput" id="Email" name="email" onChange={handleInputChange} value={user.email} />
                    <input className="createButton" type="submit" value="Submit" />
                </form>
            </div>
        </div>
        )
}
export default CreateUser;