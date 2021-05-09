import React, {Component, useState} from 'react';
import {Vote} from "./API";
import authService from "./api-authorization/AuthorizeService";
import ModalWindow from "./editor/ModalWindow";
import {Redirect} from 'react-router-dom';

// <VoteComponent onChange={} icon={"UP"} votes={1} voteType={"question"} id={-1} vote={-1}></VoteComponent>
export class VoteComponent extends Component {
    constructor(props) {
        super(props);
        this.state = {votes: props.votes ? props.votes : 0, message: true}
        this.modalController = null
    }

    setMessage(msg) {
        this.setState((state) => {
            state.message = msg;
            return state
        })
    }

    render() {
        return (
            <a onClick={() => {
                let that = this;
                authService.getUser().then(user => {
                    if (user != null) {
                        Vote(user.sub, this.props.vote, this.props.voteType, this.props.id, data => {
                            this.setState((state) => {
                                state.votes = data;
                                return state
                            })
                            this.props.onChange(data)
                        }, data => {
                            if (data === 406 || data === 403) {
                                console.log("you can't vote for yourself")
                                this.modalController("You can't vote for yourself")
                            }else if(data === 401){ //login
                                that.setMessage("login")
                            }
                        })
                    } else {
                        console.log("error when get user")
                        that.setMessage("login")
                    }
                })
            }}>
                <span>{this.props.icon}
                    {this.state.message === "login" ? <Redirect to={"/authentication/login"}/> :""}
                    <ModalWindow controller={(contr) => {
                        this.modalController = contr
                    }}/>
                </span>
            </a>
        )
    }
}
