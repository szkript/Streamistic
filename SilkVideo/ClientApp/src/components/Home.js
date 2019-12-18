import React, { Component } from 'react';

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
        fetch("http://localhost:54352/api/Video")
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
                            <source src={item.path} type="video/mp4"></source>
                        </video>
                        
                    ))}
                </>
            );
        }
    }

}
