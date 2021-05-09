import React , { useState } from 'react';
import {Comments} from "./Comments";
import {Col, Container, Row} from "reactstrap";
import {AnswerStyle} from "./Styles.css";
import {VoteComponent} from "./Vote";
import PlainEditor from "./editor/PlainEditor";

export function Answer(props) {
    const [votes,setVotes] = useState(props.answer.votes)
    let answer = props.answer
    const up = (
        <svg aria-hidden="true" className="m0 svg-icon iconArrowUpLg" width="36" height="36" viewBox="0 0 36 36">
            <path d="M2 26h32L18 10 2 26z">
            </path>
        </svg>);
    const down = (
        <svg aria-hidden="true" className="m0 svg-icon iconArrowDownLg" width="36" height="36" viewBox="0 0 36 36">
            <path d="M2 10h32L18 26 2 10z">
            </path>
        </svg>);
    return (
        <Container key={answer.id} style={AnswerStyle}>
            <Row>
                <Col xs={3}>
                    <Row>
                        <VoteComponent icon={up} votes={votes} voteType={"answer"} vote={1} id={answer.id}
                                       onChange={(votes) => {
                                           setVotes(votes)
                                       }}>
                        </VoteComponent>
                    </Row>
                    <Row>{votes}</Row>
                    <Row>
                        <VoteComponent icon={down} votes={votes} voteType={"answer"} vote={-1} id={answer.id}
                                       onChange={(votes) => {
                                           setVotes(votes)
                                       }}>
                        </VoteComponent>
                    </Row>
                </Col>
                <Col xs={7}>
                    <Row><PlainEditor readOnly={true} value={JSON.parse(answer.description)} /></Row>
                    <Row>{answer.createDateTime} by {answer.user.userName}</Row>
                    <Comments comments={answer.comments} questionId={0} answerId={answer.id} userId={answer.user.id}>
                    </Comments>
                </Col>
            </Row>
        </Container>
    )
}

