import React, {Component, useState} from 'react';
import {Collapse, UncontrolledAlert} from 'reactstrap';
import PlainEditor from "./editor/PlainEditor";
import {PostComment} from "./API";
import authService from "./api-authorization/AuthorizeService";

const style = {
    borderBottomColor: 'rgb(239,240,241)',
    borderBottomStyle: 'solid',
    borderBottomWidth: '1px',
}

export class Comments extends Component {
    constructor(props) {
        super(props);
        this.state = {comments: props.comments}
    }

    render() {
        let comments = this.props.comments
        let questionId = this.props.questionId;
        let answerId = this.props.answerId;
        let userId = this.props.userId;
        return (
            <div>
                {comments.map(comment => (
                    <div key={comment.id} style={style}>
                        <PlainEditor readOnly={true} value={JSON.parse(comment.description)} />
                         - {comment.createDateTime} by {comment.user.userName}
                    </div>
                ))}
                <CollapseCard onSubmit={(comment) => {
                    this.setState((state) => {
                        state.comments.push(comment)
                        return comment;
                    })
                }} questionId={questionId} answerId={answerId} userId={userId}>
                </CollapseCard>
            </div>
        )
    }
}


const CollapseCard = (props) => {
    const [collapse, setCollapse] = useState(false);
    // const [status, setStatus] = useState('Closed');
    // const onEntering = () => setStatus('Opening...');
    // const onEntered = () => setStatus('Opened');
    // const onExiting = () => setStatus('Closing...');
    // const onExited = () => setStatus('Closed');
    const toggle = (e) => {
        e.preventDefault();
        setCollapse(!collapse);
    }

    return (
        <div>
            <div onClick={e => toggle(e)}>Add comment</div>
            <Collapse
                isOpen={collapse}
                // onEntering={onEntering}
                // onEntered={onEntered}
                // onExiting={onExiting}
                // onExited={onExited}
            >
                <PlainEditor value={null} onChange={v => CollapseCard.value = v}>
                </PlainEditor>
                <div onClick={() => {
                    if (CollapseCard.value["blocks"] 
                        && CollapseCard.value["blocks"] instanceof Array
                        && CollapseCard.value["blocks"].length >=1
                        && CollapseCard.value["blocks"][0].text.toString().trim().length > 0) {
                        PostComment(props.questionId, props.answerId, JSON.stringify(CollapseCard.value), props.userId, (date) => {
                            props.onSubmit(date)
                            setCollapse(!collapse);
                        }, data => {
                            console.log("error")
                        })
                    }
                }}>Submit
                </div>
            </Collapse>
        </div>
    );
}

CollapseCard.value = {};