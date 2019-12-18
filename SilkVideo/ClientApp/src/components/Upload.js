import React from 'react'
import { post } from 'axios';

export class Upload extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            id: 1,
            file: null,
        };
    }

    async submit(e) {
        e.preventDefault();

        const url = `http://localhost:54352/api/Video`;
        const formData = new FormData();
        formData.append('body', this.state.file);
        const config = {
            headers: {
                'content-type': 'multipart/form-data',
            },
        };
        return post(url, formData, config);
    }

    setFile(e) {
        this.setState({ file: e.target.files[0] });
    }

    render() {
        return (
            <form onSubmit={e => this.submit(e)}>
                <h1>File Upload</h1>
                <input type="file" onChange={e => this.setFile(e)} />
                <button type="submit">Upload</button>
            </form>
        );
    }
}

//import React, { Component } from 'react';
//import axios, { post } from 'axios';

//export class Upload extends Component {
//    static displayName = Upload.name;

//    constructor(props) {
//        super(props);
//        this.state = {
//            test: "video file uploader",
//            video: "",
//            value: ""
//        };
//        this.handleChange = this.handleChange.bind(this);
//        this.handleSubmit = this.handleSubmit.bind(this);
//    }

//    handleChange(event) {
//        this.setState({ value: event.target.value });
//    }

//    handleSubmit(event) {
//        this.setState({value: event.target.value})
//    }

//    onChange(e) {
//        let files = e.target.files;

//        let reader = new FileReader();
//        reader.readAsDataURL(files[0]);

//        reader.onload = (e) => {
//            const url = "https://localhost:44321/api/Video";
//            const formData = { file: e.target.result };
//            const datas = {
//                description: "teszt2",
//                path: "tesztPath2"
//            }
//            return post(url, datas)
//                .then(response=>console.warn("result", response))

//        }
//    }

//    render() {
//        return (
//            <div onSubmit={this.onFormSubmit} >
//                <h1>{this.state.test}</h1>
//                <input type="file" name="file" onChange={(e) => this.onChange(e)}></input>
//            </div>
//        );
//    }
//}
