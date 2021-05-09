import React, {Component, useState} from 'react';
import {Button, Modal, ModalHeader, ModalBody, ModalFooter} from 'reactstrap';


export default class ModalWindow extends Component {
    constructor(props) {
        super(props);
        this.state = {
            modal: false,
            message:""
        }
        if(props['controller'] instanceof Function){
            props.controller(this.controller)
        }else{
            console.warn("Please provide a controller Acceptor function")
        }
    }

    controller = (message,title)=>{
        this.setState((s)=>{
            return {
                modal: true,
                message:message,
                title:title
            }
        })
    }

    toggle = () => {
        this.setState((s)=>{
            s.modal = !s.modal
            return s
        })
    }

    render() {
        return <div>
            {/*<Button color="danger" onClick={toggle}>{buttonLabel}</Button>*/}
            {/*https://reactstrap.github.io/components/modals/*/}
            <Modal isOpen={this.state.modal}
                   backdrop='static'
                   centered={true}
                   size='xl'
                   fade={false}
                   toggle={this.toggle}
                   className={this.props.className?this.props.className:null}>
                <ModalHeader toggle={this.toggle}>{this.state.title ? this.state.title: ""}</ModalHeader>
                <ModalBody>
                    {this.state.message}
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={this.toggle}>Ok</Button>{' '}
                    <Button color="secondary" onClick={this.toggle}>Cancel</Button>
                </ModalFooter>
            </Modal>
        </div>
    }
}

