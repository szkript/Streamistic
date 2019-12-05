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
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(evt) {
        // check it out: we get the evt.target.name (which will be either "email" or "password")
        // and use it to target the key on our `state` object with the same name, using bracket syntax
        this.setState({ [evt.target.name]: evt.target.value });
        console.warn("userField: ",this.state.username, "pwField: ", this.state.password);
    }

    handleSubmit(event) {
        event.PreventDefault();
        const url = "https://localhost:44321/api/Login";
        //const uname = event.target.username;
        //const userData = {
        //    UserName: { uname},
        //    //Password: { }
        //}
        console.warn(url);
        //return post(url, datas)
        //    .then(response => console.warn("result", response))
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>

                <label>Username</label>
                <input type="text" name="username" onChange={this.handleChange} />

                <label>Password</label>
                <input type="password" name="password" onChange={this.handleChange} />
                <input type="submit" value="submit" />
            </form>
        );
    }
}