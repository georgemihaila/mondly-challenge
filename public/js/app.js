(function(window, document, scope){
    
    let debug = true;

    let session = window.localStorage;
    const serverUri = 'http://192.168.43.250/';

    let router = {};
    let template = {};
    let timer = {};
    let user = {data:{}};

    let game = {
        participants : [{
            name: "User 1",
            lives: 3,
            questionsLeft: 4
        }, {
            name: "user 2",
            lives: 2,
            questionsLeft: 4
        }, 
        {
            name: "User 3",
            lives: 1,
            questionsLeft: 4
        }],
        time: 10,
        initialTime: 10
    };

    let store = {
        loginFormTitle: "Enter your player name",
        questions: {
            total: 5
        }
    };

    router.dispatch = function(route, props){
        scope.innerHTML = '';
        if (this[route]){
            this[route](props);
        } else {
            this.error(404);
        }
    }

    template.render = function(template, props){
        axios
            .get('/template/' + template + '.html')
            .then(response => {
                let html = 
                    eval( '`' + response.data.replace(/`/g,'\\`') + '`');
                scope.insertAdjacentHTML('beforeend', html);
            })
            .catch(err => {
                toastr.error('An error has occurred!');
                console.error(err);
            });
    }

    template.getPartial = function(template, props){
        return axios.get('/template/' + template + '.html');
    }

    // error pages
    router.error = function(statusCode){
        template.render('error', {code: statusCode});
    }

    // landing page
    router.index = router.lobbyLogin = function(props){
        // if(user.update().isLoggedIn()){
        //     window.location.hash = '#gameInProgress';
        //     return;
        // }
        template
            .getPartial('lobbyLogin', props)
            .then(response => {
                let html = 
                    eval( '`' + response.data.replace(/`/g,'\\`') + '`');
                scope.insertAdjacentHTML('beforeend', html);

                let loginForm = document.getElementById('lobbyLoginForm');
                loginForm.addEventListener('submit', event => {
                    event.preventDefault();
                    let username = document.getElementById('username').value || '';
                    if(username !== ''){
                        axios
                            .get(serverUri + 'players/create?name=' + username)
                            .then(data => {
                                let response = JSON.parse(data.data);
                                if(response.SessionID){
                                    session.setItem('currentUser', data.data);
                                    window.location.hash = "#lobbyWait";
                                }else{
                                    window.location.hash = "#gameInProgress";
                                }
                            })
                            .catch(error => {
                                toastr.error('An error has occurred!');
                                console.error(error)
                            });
                    } else {
                        toastr.error('You have not entered a valid username!');
                    }
                });
            })
            .catch(error => {
                toastr.error('An error has occurred!');
                console.error(error)
            });
    }
    
    // lobby waiting page
    router.lobbyWait = function(props){
        // if(user.update().isLoggedIn()){
        //     window.location.hash = '#gameInProgress';
        //     return;
        // }
        props.gameTypes = [
            { name: 'Marathon' },
            { name: 'Random order' },
            { name: 'Precision mania' }
        ];

        props.classes = [
            { name: 'Messi Footballer' },
            { name: 'Armchair Historian' },
            { name: 'Nascar Driver' }
        ];

        props.classChoicesHtml = "";
        props.classes.map((choice, index) => {
            props.classChoicesHtml += `
            <li class="list-group-item">
                <div class="custom-control custom-checkbox">
                    <input type="checkbox" class="custom-control-input" id="class${index}" data-choiceName="${choice.name}">
                    <label class="custom-control-label" for="class${index}">${choice.name}</label>
                </div>
            </li>
            `;
        })

        props.gameTypeChoicesHtml = "";
        props.gameTypes.map((type, index) => {
            props.gameTypeChoicesHtml += `
            <li class="list-group-item">
                <div class="custom-control custom-checkbox">
                    <input type="checkbox" class="custom-control-input" id="gameType${index}" data-typeName="${type.name}">
                    <label class="custom-control-label" for="gameType${index}">${type.name}</label>
                </div>
            </li>
            `;
        })

        template
            .getPartial('lobbyWait', props)
            .then(response => {
                let html = 
                    eval( '`' + response.data.replace(/`/g,'\\`') + '`');
                scope.insertAdjacentHTML('beforeend', html);
                let readyButton = document.getElementById('ready');
                let checkboxes = document.querySelectorAll('input[type="checkbox"]');
                for(let checkbox of checkboxes){
                    checkbox.value = 'off';
                    checkbox.addEventListener('click', event => event.target.value = (event.target.value === 'on' ? 'off' : 'on'));
                }
                readyButton.addEventListener('click', event => {
                    let request = {};
                    for(let checkbox of checkboxes){
                        if(checkbox.value === 'on'){
                            if(checkbox.dataset.choicename){
                                request.choice = checkbox.dataset.choicename;
                            } else if(checkbox.dataset.typename) {
                                request.type = checkbox.dataset.typename;
                            }
                        }
                    }
                    if(request.type){
                        console.log(JSON.stringify(request));
                        window.location.hash = "#gameInProgress";
                    } else {
                        toastr.error('You have to choose a type of game!');
                    }
                    
                });
            })
            .catch(error => {
                toastr.error('An error has occurred!');
                console.error(error)
            });
    }

    // game main viewss
    router.gameInProgress = function(props){
        if(!user.update().isLoggedIn()){
            window.location.hash = '#lobbyLogin';
            return;
        }

        axios
            .get(serverUri + `questions/GetNextQuestion?sid=${user.data.SessionID}&token=${user.data.Token}`)
            .then(response => {
                props.statusBoxesHtml = "";

                game.participants.map((participant, index) => {
                    props.statusBoxesHtml += `
                    <div class="col-md-12 col-lg-12">
                        <div class="card" style="width: 10rem;">
                            <div class="card-body">
                                &nbsp;<div class="status-card-user-icon"><i class="fa fa-user" aria-hidden="true"></i></div>
                                <div class="status-card-user-name"><h5 class="card-title">${participant.name}</h5></div>
                                <p>Lives left:
                    `;

                    for(let i = 0; i < participant.lives; i++){
                        props.statusBoxesHtml += ' <i class="fa fa-heart" aria-hidden="true"></i> ';
                    }

                    props.statusBoxesHtml += `</p>
                                <p>Questions left: ${participant.questionsLeft} / ${store.questions.total}</p>
                            </div>
                        </div>
                    </div>
                    <div class="row vertical-spacer"></div>
                    `;
                });
                
                props.currentQuestion = response.data;
                let title = props.currentQuestion.Title;
                props.currentQuestion.type= "multipleChoice";

                props.currentQuestion.answersHtml = "";
                if(props.currentQuestion.type === 'multipleChoice'){
                    props.currentQuestion.answersHtml += '<div class="row"></div><ul>';
                    props.currentQuestion.Answers.map((answer, index) => {
                        props.currentQuestion.answersHtml += `
                        <li class="list-group-item" data-key="${index}">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="answer${index}" data-answerName="${answer}">
                                <label class="custom-control-label" for="answer${index}">${answer}</label>
                            </div>
                        </li>
                        `;
                    });
                    props.currentQuestion.answersHtml += "</ul>";
                } else {
                    props.currentQuestion.answersHtml += `
                    <div class="row"></div>
                    <label for="answer">Your answer: </label>
                    <input type="text" class="form-control-plaintext col-md-10 col-lg-10" id="answer" value="">
                    <button class="btn btn-primary" type="button">Send</button>
                    `;
                }
                
                template
                    .getPartial('gameInProgress', props)
                    .then(response1 => {
                        let html = 
                            eval( '`' + response1.data.replace(/`/g,'\\`') + '`');
                        scope.innerHTML = '';
                        scope.insertAdjacentHTML('beforeend', html);

                        let submitButton = document.getElementById('submitAnswer');

                        submitButton.addEventListener('click', event => {
                            event.preventDefault();
                            let inputs = document.querySelectorAll('input');

                            for(let input in inputs){

                            }

                            // fetch next current question
                            axios
                                .get(serverUri + `questions/GetNextQuestion?sid=${user.data.SessionID}&token=${user.data.Token}`)
                                .then(response2 => {
                                    props.currentQuestion = response2.data;
                                    router.gameInProgress(props);
                                })
                                .catch(error => {
                                    toastr.error('An error has occurred!');
                                    console.error(error)
                                });
                        });

                        // We're set, let's go
                        timer.time(game.time);
                        timer.expire();
                    });
            })
    }

    // stats view
    router.gameEnded = function(props){
        if(!user.update().isLoggedIn()){
            window.location.hash = '#lobbyLogin';
            return;
        }
        template.render('gameEnded', props);
    }

    // onload event handle
    window.addEventListener('load', event => {
        let route = event.target.location.hash.substring(1);
        router.dispatch(route || 'index', store);
    });
    
    // Main event loop
    window.addEventListener('hashchange', event => {
        let route = event.target.location.hash.substring(1);
        router.dispatch(route || 'index', store);
    });

    timer.time = function(amount){
        let secs = 1000 * parseFloat(amount);
        setTimeout(()=>{
            // router.dispatch('gameEnded', store);
            user.logout();
            window.location.hash = "#gameEnded";
        }, secs);
    };

    timer.expire = function(amount){
        let interval = setInterval(function(){
            if(game.time > 0){
                game.time -= 0.10;
                
                let progress = `<div class="progress">
                    <div class="progress-bar" style="width:${game.time / game.initialTime * 100}%"></div>
                </div>`;
                let progressContainer = document.getElementById('progressContainer');
                if(progressContainer)
                    progressContainer.innerHTML = progress;
            } else {
                clearInterval(interval);
            }
        }, 100);
    };

    user.update = function(){
        let currentUserJson = session.getItem('currentUser');
        if(currentUserJson){
            let currentUser = JSON.parse(currentUserJson);
            this.data = currentUser;
        }
        return this;
    }

    user.isLoggedIn = function(){
        return (typeof this.data.SessionID !== 'undefined');
    };

    user.logout = function(){
        session.removeItem('currentUser');
    }

})(window, document, document.getElementById('app'));