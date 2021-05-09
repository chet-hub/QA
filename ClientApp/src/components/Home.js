import React, { Component } from 'react';
import {test} from "./API"


export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {log:"loading..."}
    }

    componentDidMount() {
        test().then(data=>{
            this.state.log = JSON.stringify(data)
        })
    }

    render () {
        return (
            <div>
                {this.state.log}
            </div>
        );
    }
}
