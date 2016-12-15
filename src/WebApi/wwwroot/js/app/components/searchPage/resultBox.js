define(['knockout', 'dataservice', 'postman', 'config'],
    function (ko, dataService, postman, config) {
        return function () {
            var questions = ko.observableArray([]);

            
            dataService.getQuestions(function (data) {
                questions(data.questionList);
            });

            
            return {
                questions
            };
        };
    });