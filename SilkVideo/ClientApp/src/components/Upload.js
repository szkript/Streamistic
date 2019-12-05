import React, { Component } from 'react';
import axios, { post } from 'axios';

export class Upload extends Component {
    static displayName = Upload.name;

    constructor(props) {
        super(props);
        this.state = {
            test: "video file uploader",
            video: "",
            value: ""
        };
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({ value: event.target.value });
    }

    handleSubmit(event) {
        this.setState({value: event.target.value})
    }

    onChange(e) {
        let files = e.target.files;

        let reader = new FileReader();
        reader.readAsDataURL(files[0]);

        reader.onload = (e) => {
            const url = "https://localhost:44321/api/Video";
            const formData = { file: e.target.result };
            const datas = {
                description: "teszt2",
                path: "tesztPath2"
            }
            return post(url, datas)
                .then(response=>console.warn("result", response))

        }
    }

    render() {
        return (
            <div onSubmit={this.onFormSubmit} >
                <h1>{this.state.test}</h1>
                <input type="file" name="file" onChange={(e) => this.onChange(e)}></input>
            </div>
        );
    }
}