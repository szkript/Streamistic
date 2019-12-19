import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Upload } from './components/Upload';
import { Login } from './components/Login';
import { Register } from './components/Register';
import { Logout } from './components/Logout';
import {Stream} from "./components/Stream";

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/upload' component={Upload} />
                <Route path='/login' component={Login} />
                <Route path='/register' component={Register} />
                <Route path='/logout' component={Logout} />
                <Route path='/liveStream' component={Stream}/>
            </Layout>
        );
    }
}
