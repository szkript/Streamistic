import React, { Component } from 'react';
import axios, { post } from 'axios';

export class Login extends Component {
    constructor() {
        super();
        this.state = {
            username: '',
            password: ''
        };
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(evt) {
        // check it out: we get the evt.target.name (which will be either "email" or "password")
        // and use it to target the key on our `state` object with the same name, using bracket syntax
        this.setState({ [evt.target.name]: evt.target.value });
        //console.warn("userField: ", this.state.username, "pwField: ", this.state.password);
    }

    handleClick = () => {
        console.log('this is:', this);
        const url = "https://localhost:44321/api/Login";
        const uname = this.state.username;
        const pw = this.state.password;
        const datas = {
            UserName: `${uname}`,
            Password: `${pw}`
        }
        return post(url, datas)
            .then(response => console.warn("result", response))
    }

    render() {
        return (
            <form>

                <label>Username</label>
                <input type="text" name="username" onChange={this.handleChange} />

                <label>Password</label>
                <input type="password" name="password" onChange={this.handleChange} />
                <input type="button" value="submit" onClick={this.handleClick} />
            </form>
        );
    }
}