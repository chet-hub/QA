import React, {Component} from 'react';
import RichEditor from "./editor/RichEditor";
import {Button, Form, FormGroup, Label, Input, Col, Row} from 'reactstrap';
import {PostAnswer, PostQuestion} from "./API";
import authService from "./api-authorization/AuthorizeService";
// import Alert from './Alert';
import {Alert} from 'reactstrap';

import TagsInput from 'react-tagsinput'
import './react-tagsinput.css'

//https://stackoverflow.com/questions/33923/what-is-tail-recursion
export class PostContent extends Component {

    constructor(props) {
        super(props);
        this.state = {
            submit: {content: null, title: null}, 
            message: null,
            tags:[]
        };
        this.titleValue = null;
        this.editorValue = null;
        this.user = authService.getUser()
    }

    setMessage(msg) {
        this.setState((state) => {
            state.message = msg;
            return state
        })
    }

    setTags(tags) {
        this.setState((state) => {
            state.tags = tags;
            return state
        })
    }
    
    submit(e) {
        e.preventDefault()
        e.persist();
        this.user.then(user => {
                let that = this;
                if (this.props.questionId !== undefined) {
                    if (!this.editorValue || !notEmpty(this.editorValue)) {
                        this.setMessage("Please input content of the question")
                    } else {
                        PostAnswer(this.props.questionId,
                            user.sub,
                            JSON.stringify(that.editorValue), data => {
                                that.setMessage("Post successfully")
                                that.props.onSubmit(data)
                            }, data => {
                                that.setMessage(data)
                            })
                    }
                } else {
                    if (!this.titleValue || this.titleValue.toString().trim().length === 0) {
                        this.setMessage("Please input title of the question")
                    } else if (!this.editorValue || !notEmpty(this.editorValue)) {
                        this.setMessage("Please input content of the question")
                    } else {
                        PostQuestion(user.sub,
                            that.titleValue,
                            JSON.stringify(that.editorValue),
                            that.state.tags.join(","),
                            data => {
                                that.props.onSubmit(data)
                                that.setMessage("Post successfully")
                            }, data => {
                                that.setMessage(data)
                            })
                    }
                }
            }
        )
    }

    renderQuestionDetail(question, answers) {
        return (
            <Form>
                {this.props.questionId ? "" :
                    <>
                        <FormGroup>
                            <Label>Title</Label>
                            <Input onChange={v => {
                                this.titleValue = v.target.value
                            }}/>
                        </FormGroup>
                        <FormGroup>
                            <Label>Tags</Label>
                            <TagsInput value={this.state.tags} onChange={(a) => {
                                this.setTags(a)
                            }}/>
                        </FormGroup>
                    </>
                    }
                <FormGroup>
                    {this.props.questionId ? "" : <Label>Content</Label>}
                    <RichEditor onChange={v => {
                        this.editorValue = v
                    }}>
                    </RichEditor>
                </FormGroup>

                <Alert color="info" isOpen={!!this.state.message} toggle={() => {
                    this.setMessage(null)
                }}>
                    {this.state.message}
                </Alert>

                <FormGroup>
                    <Row>
                        <Col sm={1}>
                            <div onClick={(e) => {
                                this.submit(e)
                            }}>
                                <Button>Submit</Button>
                            </div>
                        </Col>
                    </Row>
                </FormGroup>
            </Form>
        );
    }

    render() {
        return (
            <div>
                {this.renderQuestionDetail(this.state.question, this.state.answers)}
            </div>
        );
    }


}

function notEmpty(value) {
    if (value instanceof String) {
        value = JSON.parse(value)
    }
    return value
        && value instanceof Object
        && value["blocks"] instanceof Array
        && value["blocks"].length >= 1
        && value["blocks"][0].text.toString().trim().length > 0
}