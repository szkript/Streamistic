import React, { Component } from 'react';

export class Upload extends Component {
    static displayName = Upload.name;

    constructor(props) {
        super(props);
        this.state = {
            test: "video file uploader",
            image: ""
        };
    }

    onChange(e) {
        let files = e.target.files;
        console.warn("datafile", files)
    }

    render() {
        return (

            <div onSubmit={this.onFormSubmit} >
                <h1>{this.state.test}</h1>
                <input type="file" name="file" onChange={(e) => this.onchange(e)}></input>
            </div>
        );
    }
}
