import React, {Component} from 'react';
import ReactDOM from "react-dom";
import Hls from "hls.js";
import {post} from "axios";

export class Stream extends Component {
    constructor(props) {
        super(props);
        this.state = {
            streamUrl: "rtmp://64.225.24.130:1935/show",
            baseUrl: "/liveStream",
            streamName: null,
            isLoaded: false,
            constructedUrl: null,
            streamKey: null
        };
        this.handleChange = this.handleChange.bind(this);
    }

    getStreamKey(){
        let url = this.props.location.pathname;
        let asd = url.split("/");
        console.log(asd);
        //username from url as streamKey
        return asd[2];
    }

    componentDidMount() {
        const videoTag = document.getElementById("video");
        videoTag.style.display = "none";
        this.getStreamKey();
        const streamKey = this.getStreamKey();
        if (this.props.location.pathname !== this.state.baseUrl) {
            //hide and show the elements
            const formTag = document.getElementById("submitForm");
            formTag.style.display = "none";
            videoTag.style.display = "block";
            //stream video player
            const script = document.createElement("script");
            script.async = true;
            script.src = "https://cdn.jsdelivr.net/npm/hls.js@latest";
            document.body.appendChild(script);
            let video = document.getElementById('video');
            let hls = new Hls();
            if (Hls.isSupported()) {
                hls.loadSource(`http://64.225.24.130:8080/hls/${streamKey}.m3u8`);
                hls.attachMedia(video);
                hls.on(Hls.Events.MANIFEST_PARSED, function () {
                    video.play();
                });
            } else if (video.canPlayType('application/vnd.apple.mpegurl')) {
                video.src = `http://64.225.24.130:8080/hls/${streamKey}.m3u8`;
                video.addEventListener('canplay', function () {
                    video.play();
                });
            }
        }

    }

    handleChange(evt) {
        // check it out: we get the evt.target.name (which will be either "email" or "password")
        // and use it to target the key on our `state` object with the same name, using bracket syntax
        this.setState({[evt.target.name]: evt.target.value});
        //console.warn("userField: ", this.state.username, "pwField: ", this.state.password);
    }

    constructUrl = (response) => {
        let result;
        const info = document.createElement("p");
        const streamUrlParagraph = document.createElement("p");
        let infoText = document.createTextNode("You have to login for stream key");
        const anchor = document.createElement("a");
        const linkText = document.createTextNode("Your streaming will be available here");
        anchor.appendChild(linkText);
        const componentDiv = document.getElementById('switch');
        let finalUrl = this.state.baseUrl + "/" + response.data;
        this.setState({
            constructedUrl: finalUrl
        });
        anchor.href = finalUrl;
        if (response.status === 200) {
            const streamUrlInfo = document.createTextNode("stream url is : " + this.state.streamUrl);
            streamUrlParagraph.appendChild(streamUrlInfo);
            componentDiv.appendChild(streamUrlParagraph);
            infoText = document.createTextNode("your stream key is : " + response.data);
            this.setState({streamKey: response.data});
            componentDiv.appendChild(anchor);
            result = true;
        } else {
            result = false;
        }
        info.appendChild(infoText);
        componentDiv.appendChild(info);
        return result;
    };

    handleClick = () => {
        console.log('this is:', this);
        const url = "http://localhost:54352/api/Streaming";
        const streamName = this.state.streamName;
        const datas = {
            Description: `${streamName}`
        };
        post(url + "/record");
        return post(url, datas)
            .then(response => this.constructUrl(response));
    };

    render() {
        return (
            <div id='switch'>
                <video id="video" controls style={{maxWidth: 600, maxHeight: 500}}></video>
                <form id="submitForm">
                    <input type="button" value="Start stream" onClick={this.handleClick}/>
                </form>
            </div>
        );
    }
}