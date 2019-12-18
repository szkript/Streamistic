import React, { Component } from 'react';
import axios, { post, get } from 'axios';

export class Logout extends Component {
    constructor() {
        super();
        const url = "http://localhost:54352/api/Login/Logout";
        return get(url)
            .then(response => console.warn("result", response))
            .then(window.location.href = "/")

    }
}