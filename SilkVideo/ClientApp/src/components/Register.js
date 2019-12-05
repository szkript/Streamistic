import React, { Component } from 'react';
import axios, { post } from 'axios';

export class Register extends Component {
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
    }

    handleClick = () => {
        const url = "https://localhost:44321/api/Registration";
        const uname = this.state.username;
        const pw = this.state.password;
        //const formData = new FormData();
        //formData.set('UserName', { uname });
        //formData.set('Password', { pw });
        //const config = {
        //    headers: {
        //        'contentType': 'application/json',
        //        'charSet':'utf-8'
        //    },
        //};
        console.log('this is:', this);
        //var querystring = require('querystring');
        //return axios.post(url, querystring.stringify({
        //    UserName: { uname },
        //    Password: { pw }
        //}));
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