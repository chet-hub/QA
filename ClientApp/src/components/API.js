import authService from "./api-authorization/AuthorizeService";


export async function post(path, params, done, error) {
    const token = await authService.getAccessToken();
    const tokenHead = !token ? {} : {'Authorization': `Bearer ${token}`};
    const header = Object.assign({'Content-Type': 'application/json'}, tokenHead)
    const requestOptions = {
        method: 'POST',
        headers: header,
        body: JSON.stringify(params)
    };
    console.log("fetch --> body: " + JSON.stringify(params))
    console.log("fetch --> path: " + path)
    const response = await fetch(path, requestOptions)
    if (!response.ok) {
        error instanceof Function ? error(response.status) : console.log(JSON.stringify(response));
        if(response.status === 401){
            window.location = "/authentication/login"
        }
    } else {
        let result = await response.json();
        done(result);
        console.log("fetch --> return: " + JSON.stringify(result))
    }
    return response
}


export function GetQuestionsList(orderBy, from, to, tag, done, error) {
    return post("GetQuestionsList", {
        orderBy: orderBy,
        from: from,
        to: to,
        tag: tag
    }, done, error)
}

export function PostQuestion(userId, title, description, tags, done, error) {
    return post("PostQuestion", {
        userId: userId,
        title: title,
        description: description,
        tags: tags
    }, done, error)
}

export function VoteQuestion(questionId, vote, done, error) {
    return post("VoteQuestion", {
        questionId: questionId,
        vote: vote
    }, done, error)
}

Vote.Type = {question:"question",answer:"answer",user:"user"}
export function Vote(userId, vote, type, id, done, error) {
    return post("Vote", {
        userId: userId,
        vote: vote,
        type: type,
        id: id,
    }, done, error)
}

export function GetAnswersList(questionId, done, error) {
    return post("GetAnswersList", {
        questionId: questionId
    }, done, error)
}

export function PostAnswer(questionId, userId, description, done, error) {
    return post("PostAnswer", {
        questionId: questionId,
        userId: userId,
        description: description
    }, done, error)
}

export function PostComment(questionId, answerId, description, userId, done, error) {
    return post("PostComment", {
        questionId: questionId,
        answerId: answerId,
        userId: userId,
        description: description
    }, done, error)
}


export function test() {
    const description = JSON.stringify({"blocks":[{"key":"a7jmu","text":"test","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}})
    let result = []
    return Promise.all([

        // GetQuestionsList("CreateDateTime", 0, 5, null, data => {
        //     result.push({'GetQuestionsList': data})
        // }, data => {
        //     result.push({'GetQuestionsList:': data})
        // }),
        //
        // PostQuestion('f984066c-816d-4531-bd9a-c63256ca7000',
        //     'PostQuestion_title',
        //     description,
        //     'a,b,c', data => {
        //         result.push({'PostQuestion:': data})
        //     }, data => {
        //         result.push({'PostQuestion:': data})
        //     }),
        //
        // VoteQuestion(-1, 1, data => {
        //     result.push({'VoteQuestion:': data})
        // }, data => {
        //     result.push({'VoteQuestion:': data})
        // }),
        //
        // Vote('f984066c-816d-4531-bd9a-c63256ca7000', 1, Vote.Type.user,-1,data => {
        //     result.push({'Vote:': data})
        // }, data => {
        //     result.push({'Vote:': data})
        // }),
        //
        //
        // GetAnswersList(-1, data => {
        //     result.push({'GetAnswersList:': data})
        // }, data => {
        //     result.push({'GetAnswersList:': data})
        // }),
        //
        // PostAnswer(-1, 'f984066c-816d-4531-bd9a-c63256ca7000', description, data => {
        //     result.push({'PostAnswer:': data})
        // }, data => {
        //     result.push({'PostAnswer:': data})
        // }),
        //
        // PostComment(-1, 0, description, 'f984066c-816d-4531-bd9a-c63256ca7000', data => {
        //     result.push({'PostComment:': data})
        // }, data => {
        //     result.push({'PostComment:': data})
        // })

    ]).then(() => {
        return "test pass"
    })
}
