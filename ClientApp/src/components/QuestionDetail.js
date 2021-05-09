import React, {Component} from 'react';
import {Answer} from "./Answer";
import RichEditor from "./editor/RichEditor";
import {Col, Container, Row} from "reactstrap";
import {QuestionStyle, AnswerContainerStyle} from "./Styles.css";
import {Tags} from "./Tags";
import {Comments} from "./Comments";
import {GetAnswersList, GetQuestionsList} from "./API";
import {PostContent} from "./PostContent";
import ModalWindow from "./editor/ModalWindow";
import PlainEditor from "./editor/PlainEditor";
import {VoteComponent} from "./Vote";

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


//https://stackoverflow.com/questions/33923/what-is-tail-recursion
export class QuestionDetail extends Component {

    constructor(props) {
        super(props);
        this.state = {question: {}, answers: [], loading: true};
        let arr = props.location.search.split("=");
        if (arr instanceof Array
            && arr.length === 2
            && arr[0] === "?questionId") {
            let id = parseInt(arr[1]);
            if (!isNaN(id)) {
                this.getData(id)
            } else {
                console.log("ID is not a number")
            }
        } else {
            console.log("params error")
        }
    }

    componentDidMount() {
    }

    renderQuestionDetail(question, answers) {
        return (
            <div>
                <Container style={QuestionStyle}>
                    <Row>
                        <Col xs={2}>
                            <VoteComponent icon={up} votes={question.votes} voteType={"question"} vote={1} id={question.id}
                                           onChange={(votes) => {
                                               this.setState((state)=>{
                                                   question.votes = votes
                                                   return state
                                               })
                                           }}>
                            </VoteComponent>
                        </Col><Col xs={8}>{question.title}</Col>
                    </Row>
                    <Row>
                        <Col xs={2}>
                            <div>{question.votes}</div>
                            <VoteComponent icon={down} votes={question.votes} voteType={"question"} vote={-1} id={question.id}
                                           onChange={(votes) => {
                                               this.setState((state)=>{
                                                   question.votes = votes
                                                   return state
                                               })
                                           }}>
                            </VoteComponent>
                        </Col>
                        <Col xs={8}>
                            <PlainEditor readOnly={true} value={JSON.parse(question.description)} />
                        </Col>
                    </Row>
                    <Row>
                        <Col xs={2}>
                            <div>{question.answers}</div>
                        </Col><Col xs={8}>
                        <Tags tags={question.tags} onClick={(tag)=>{
                            console.log(JSON.stringify(tag))
                        }} />
                    </Col>
                    </Row>
                    <Row>
                        <Col xs={2}>
                            <div>answers</div>
                        </Col>
                        <Col xs={5}>
                        </Col>
                        <Col xs={3}>{question.createDateTime} by {question.user.userName}</Col>
                    </Row>
                    <Row>
                        <Col xs={2}>
                        </Col>
                        <Col xs={8}>
                            <Comments comments={question.comments} questionId={question.id} answerId={0}
                                      userId={question.user.id}>
                            </Comments>
                        </Col>
                    </Row>
                </Container>
                <div style={AnswerContainerStyle}>{answers.map(answer =>
                    <Answer key={answer.id} answer={answer}>
                    </Answer>
                )}
                </div>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderQuestionDetail(this.state.question, this.state.answers);

        return (
            <>
                <div>
                    {/*<h1 id="tabelLabel">Questions Detail</h1>*/}
                    {contents}
                </div>
                <div style={{paddingTop: '25px', paddingBottom: '25px'}}>
                    {/*<RichEditor id={this.state.question.id}>*/}
                    {/*</RichEditor>*/}
                    <PostContent questionId={this.state.question.id} onSubmit={(data)=>{
                        this.setState({question: data.question, answers: data.answers, loading: false});
                    }}>
                    </PostContent>
                </div>
            </>
        );
    }

    async getData(questionId) {
        return GetAnswersList(questionId, data => {
            this.setState({question: data.question, answers: data.answers, loading: false});
        }, data => {

        })
    }


}