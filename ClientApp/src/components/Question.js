import React from 'react';
import {Tags} from "./Tags";
import {Pointer, QuestionStyle} from './Styles.css';
import {Container, Row, Col} from 'reactstrap';
import {Link} from 'react-router-dom';
import PlainEditor from "./editor/PlainEditor";


export function Question(props) {
    let question = props.question
    let onTagClick = props.onTagClick
    return (
        <Container key={question.id} style={QuestionStyle}>
            <Row>
                <Col xs={2}>{question.votes}</Col>
                <Col xs={8}>
                    <Link style={Pointer} to={{
                        pathname: "/questionDetail",
                        search: "questionId=" + question.id
                    }}>
                        {question.title}
                    </Link>
                </Col>
            </Row>
            <Row>
                <Col xs={2}>
                    <div>Votes</div>
                    <div>{question.answers}</div>
                    <div>answers</div>
                </Col>
                <Col xs={8}>
                    <Link style={Pointer} to={{
                        pathname: "/questionDetail",
                        search: "questionId=" + question.id
                    }}>
                        <PlainEditor readOnly={true} value={JSON.parse(question.description)}/>
                    </Link>
                </Col>
            </Row>
            <Row>
                <Col xs={2}>
                    {/*{question.answers}*/}
                </Col><Col xs={8}>
                <Tags tags={question.tags} onClick={(tag)=>{
                    onTagClick(tag)
                }}>
                </Tags></Col>
            </Row>
            <Row>
                <Col xs={2}>
                    {/*answers*/}
                </Col><Col xs={5}></Col><Col
                xs={3}>{question.createDateTime} by {question.user.userName}</Col>
            </Row>
        </Container>
    )
}
