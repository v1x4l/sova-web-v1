define(['knockout', 'dataservice', 'config', 'jquery'],
    function (ko, dataService, config) {
        return function () {
            var searchResults = ko.observableArray([]);

            var searchAndPost = function () {
                var searchVal = $("#searchField").val();
                var radioVal = $("input[name=questionsOrAnswers]:checked").val();

                var obj = {
                    "SovaUserId": 1,
                    "SearchText": searchVal,
                    "SearchDate": "2016-12-11T20:19:55"
                };

                dataService.postHistories(obj);



                dataService.getSearchResults(searchVal, radioVal, function (data) {
                    searchResults.push(data.searchResultList);
                });

            };







 

            return {
                searchResults,
                searchAndPost

            };
        };
    });