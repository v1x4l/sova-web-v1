define(['knockout', 'dataservice', 'postman', 'config', 'jqcloud'],
    function (ko, dataService, postman, config) {
        return function () {
            var wordList = ko.observableArray([]);

            wordList = [
          { word: "Lorem", frequency: 15},
          { word: "Ipsum", frequency: 9},
          { word: "Dolor", frequency: 6},
          { word: "Sit", frequency: 7},
          { word: "Amet", frequency: 5}
            ];

            makeCloud = function () {
                $("#wordCloud").jQCloud(wordList);
            }
            /*
            dataService.searchResults(function (data) {
                
            });
            */

            return {
                makeCloud,
                wordList
            };
        };
    });