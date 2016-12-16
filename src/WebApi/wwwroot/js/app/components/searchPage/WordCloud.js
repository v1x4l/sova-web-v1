define(['knockout', 'dataservice', 'postman', 'config', 'jqcloud'],
    function (ko, dataService, postman, config) {
        return function () {
            var wordList = ko.observableArray([]);
            makeCloud = function () {
                $("#wordCloud").jQCloud(wordList);
            }

            
            
            $("#myButton").click(function () {
                var searchVal = $("#searchField").val();
                var radioVal = $("input[name=questionsOrAnswers]:checked").val();

                dataService.getWordList(searchVal, radioVal, function (data) {
                    var temp = [];
                    temp = data.frequentWordList;
                    temp.sort(function (a, b) {
                        return parseFloat(b.frequency) - parseFloat(a.frequency);
                    });
                    temp = temp.slice(0, 6);
                    wordList(temp);
                });
                });
            

            return {
                makeCloud,
                wordList
            };
        };
    });