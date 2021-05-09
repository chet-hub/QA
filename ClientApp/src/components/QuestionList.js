import React, {Component} from 'react';
import {Question} from "./Question";
import {GetQuestionsList} from "./API";
import {UncontrolledAlert, ButtonGroup, Button} from "reactstrap"
import {Redirect} from "react-router-dom";
import ReactPaginate from 'react-paginate';
import TagsInput from 'react-tagsinput'
import './react-tagsinput.css'

//https://stackoverflow.com/search?q=lisp
export class QuestionList extends Component {

    constructor(props) {
        super(props);
        this.state = {
            pageCount: 10,
            orderBy: "CreateDateTime",
            offset: 0,
            tag: null,
            questions: [],
            loading: true,
            error: false
        };
    }

    componentDidMount() {
        this.getData("CreateDateTime", 0, null);
    }

    handlePageClick = (data) => {
        let selected = data.selected;
        let offset = Math.ceil(selected * this.state.pageCount);
        this.getData(this.state.orderBy, offset, this.state.tag)
    };


    setTags(tags) {
        this.setState((state) => {
            state.tag = tags.join(",");
            return state
        })
    }
    
    renderQuestions(questions) {
        if (this.state.error) {
            return (
                <UncontrolledAlert color="info" fade={false}>
                    Error, can not get the result
                </UncontrolledAlert>
            )
        }
        return (
            <div>
                {/*<TagsInput style={{display:'inline'}} value={["tag1","tag2"]} onChange={(a) => {*/}
                {/*    this.setTags(a)*/}
                {/*}}/>*/}
                
                <button type="button" size="sm" className="btn btn-outline-secondary" onClick={() => {
                    this.getData("CreateDateTime", 0, this.state.tag)
                }}>Order by time
                </button>

                <button type="button" size="sm" className="btn btn-outline-secondary" onClick={() => {
                    this.getData("Answers", 0, this.state.tag)
                }}>Order by answers
                </button>

                {questions.map(question =>
                    <Question
                        onTagClick={(tag) => {
                            this.getData("CreateDateTime", 0, tag.id);
                        }}
                        key={question.id}
                        question={question}>
                    </Question>
                )}
                
                <ReactPaginate
                    previousLabel={'previous'}
                    nextLabel={'next'}
                    breakLabel={'...'}
                    pageCount={this.state.pageCount}
                    marginPagesDisplayed={2}
                    pageRangeDisplayed={5}
                    onPageChange={this.handlePageClick}

                    breakClassName={'page-item'}
                    breakLinkClassName={'page-link'}
                    containerClassName={'pagination'}
                    pageClassName={'page-item'}
                    pageLinkClassName={'page-link'}
                    previousClassName={'page-item'}
                    previousLinkClassName={'page-link'}
                    nextClassName={'page-item'}
                    nextLinkClassName={'page-link'}
                    activeClassName={'active'}
                />
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderQuestions(this.state.questions);
        return (<div> {contents}</div>);
    }

    async getData(orderBy, offset, tag, done) {
        GetQuestionsList(orderBy, offset, offset + (10*this.state.pageCount), tag, data => {
            
            this.setState(
                {
                    pageCount: Math.ceil(data.total/10),
                    orderBy: orderBy,
                    offset: offset,
                    tag: tag,
                    questions: data.questions,
                    loading: false,
                    error: false
                }, () => {
                    if (done instanceof Function) {
                        done(data.length)
                    }
                }
            );

        }, data => {
            console.log("get data with error")
        })


    }
}