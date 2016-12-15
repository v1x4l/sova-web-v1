define(['jquery'], function ($) {
    
    var getPersons = function (callback) {
        var url = "api/persons";
        $.getJSON(url, function (data) {
            callback(data);
        });
    }

    var getUsers = function (callback) { 
        var url = "api/pets";
        var url3 = "api/users";
        var url2 = "http://localhost:51234/api/users";
        $.getJSON(url3, function (data) {
            callback(data);
        })
    }

    var getAnswers = function (callback) {
        var url = "http://localhost:51234/api/answers"
        $.getJSON(url, function (data) {
            callback(data)
        })
    }

    var getComments = function (callback) {
        var url = "http://localhost:51234/api/comments"
        $.getJSON(url, function (data) {
            callback(data)
        })
    }

    var getHistories = function (callback) {
        var url = "http://localhost:51234/api/historys"
        $.getJSON(url, function (data) {
            callback(data)
        })
    }

    var getMarked = function (callback) {
        var url = "http://localhost:51234/api/markeds"
        $.getJSON(url, function (data) {
            callback(data)
        })
    }

    //No idea if this follows the proper methods call for $post, maybe third param, the callback function, is unnecessary?
    var postMarked = function (marked, callback) {
        var url = "http://localhost:51234/api/markeds"
        $.post(url, marked, function (data) {
            callback(data)
        })
    }

    var getPostTopics = function (callback) {
        var url = "http://localhost:51234/api/postTopics"
        $.getJSON(url, function (data) {
            callback(data)
        })
    }

    var getQuestions = function (callback) {
        var url = "http://localhost:51234/api/questions"
        $.getJSON(url, function (data) {
            callback(data)
        })
    }

    var getSovaUser= function (callback) {
        var url = "http://localhost:51234/api/sovaUser"
        $.getJSON(url, function (data) {
            callback(data)
        })
    }

    var getTopic = function (callback) {
        var url = "http://localhost:51234/api/topics"
        $.getJSON(url, function (data) {
            callback(data)
        })
    }

    var getSearchResults = function(searchString, isQuestion, callback) {
        var _searchString = searchString.replace(/ /g, "_");
        //var _isQuestion = isQuestion;
        var url = "http://localhost:51234/api/searchresults/" + _searchString + "&" + isQuestion;
        $.getJSON(url, function (data) {
            callback(data)
        })
    }
    return {
        getPersons,
        getUsers,
        getAnswers,
        getComments,
        getHistories,
        getMarked,
        getPostTopics,
        getQuestions,
        getSovaUser,
        getTopic,
        getSearchResults
    };
});




        