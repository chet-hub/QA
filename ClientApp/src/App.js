import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { QuestionList } from './components/QuestionList';
import { QuestionDetail } from './components/QuestionDetail';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'
import {AskQuestion} from "./components/AskQuestion";

export default class App extends Component {
    static displayName = App.name;

    render () {
        return (
            <Layout>
                {/*<Route exact path='/' component={Home} />*/}
                <Route exact path='/' component={QuestionList} />
                <Route path='/counter' component={Counter} />
                <Route path='/questionList' component={QuestionList} />
                <Route path='/questionDetail' component={QuestionDetail} />
                <AuthorizeRoute path='/askQuestion' component={AskQuestion} />
                <AuthorizeRoute path='/fetch-data' component={FetchData} />
                <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
            </Layout>
        );
    }
}
