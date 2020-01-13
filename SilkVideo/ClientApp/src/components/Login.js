import React, { Component } from 'react';
import axios, { post } from 'axios';
import Cookies from 'universal-cookie';

const cookies = new Cookies();

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
    }

    handleResponse(response){
        if (response.status === 200){
            cookies.set('userToken', response.data)
        }
    }

    handleClick = () => {
        console.log('this is:', this);
        const url = "http://localhost:54352/api/Login";
        const uname = this.state.username;
        const pw = this.state.password;
        const datas = {
            UserName: `${uname}`,
            Password: `${pw}`
        };
        return post(url, datas)
            .then(response => this.handleResponse(response))
    };

    render() {
        console.log(cookies.get('userToken'));
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