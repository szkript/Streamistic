import React, {Component} from 'react';
import ReactDOM from "react-dom";
import Hls from "hls.js";

export class Stream extends Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            items: []
        };
    }

    componentDidMount() {
        const script = document.createElement("script");
        script.async = true;
        script.src = "https://cdn.jsdelivr.net/npm/hls.js@latest";
        document.body.appendChild(script);
        let video = document.getElementById('video');
        let hls = new Hls();
        if(Hls.isSupported())
        {
            hls.loadSource('http://64.225.24.130:8080/hls/sajt3.m3u8');
            hls.attachMedia(video);
            hls.on(Hls.Events.MANIFEST_PARSED,function()
            {
                video.play();
            });
        }
        else if (video.canPlayType('application/vnd.apple.mpegurl'))
        {
            video.src = 'http://64.225.24.130:8080/hls/sajt3.m3u8';
            video.addEventListener('canplay',function()
            {
                video.play();
            });
        }
    }


    render() {
        return (

            //<video id="video" controls></video>
            <p>vmi</p>

        );
    }
}