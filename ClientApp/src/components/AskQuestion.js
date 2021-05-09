import React, {Component} from 'react';
import {PostContent} from "./PostContent";
import {Redirect} from "react-router-dom";

export class AskQuestion extends Component {
    constructor(props) {
        super(props);
        this.state = {goto: null}
    }

    render() {
        if (this.state.goto) {

            return <Redirect to={this.state.goto}/>
        } else {
            return (
                <PostContent onSubmit={(result) => {
                    this.setState({goto: "/questionDetail?questionId="+result})
                }}>
                </PostContent>
            );
        }
    }
}
