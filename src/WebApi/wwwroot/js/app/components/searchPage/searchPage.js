define(['knockout', 'dataservice', 'postman', 'config', 'jquery'],
    function (ko, dataService, postman, config) {
        return function () {
            var searchResults = ko.observableArray([]);

            var searchAndPost = function () {
                var searchVal = $("#searchField").val();
                var radioVal = $("input[name=questionsOrAnswers]:checked").val();
                
                //var date = new Date();
                var obj = {
                    "SovaUserId": 1,
                    "SearchText": searchVal,
                    "SearchDate": "2016-12-11T20:19:55"
                };

                dataService.postHistories(obj);



                dataService.getSearchResults(searchVal, radioVal, function (data) {
                    searchResults.push(data.searchResultList);
                    //resultBox.setResults(data.searchResultList);
                    alert(searchResults());
                });


                /*
                {
                    "SovaUserId":1,
                    "SearchText":"blabllasldasd",
                    "SearchDate":"2016-12-11T20:19:55"
                }
                */
            }







            /*
            dataService.getSearchResults("java script function", "true", function (data) {
                //console.log(data);
                searchResults(data.searchResultList);
                console.log(searchResults());
            });
            */

            return {
                searchResults,
                searchAndPost

            };
        };
    });