import React, { Component } from 'react';
import ReactPlayer from 'react-player'

export class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            items: []
        };
    }

    componentDidMount() {
        fetch("https://localhost:44321/api/Video")
            .then(res => res.json())
            .then((result) => {
                console.log(result)
                this.setState({
                    isLoaded: true,
                    items: result
                });
            }
            ).catch(console.log)
    }

    render() {
        const { error, isLoaded, items } = this.state;
        if (error) {
            return <div>Error: {error.message}</div>;
        } else if (!isLoaded) {
            return <div>Loading...</div>;
        } else {
            return (
                <>
                    {items.map(item => (
                        <video width="320" height="240" controls>
                            <source src="./Videos/video0.mp4" type="video/mp4"></source>
                        </video>
                        
                    ))}
                </>
            );
        }
    }

}
