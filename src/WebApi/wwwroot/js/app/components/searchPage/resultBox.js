define(['knockout', 'dataservice', 'config', 'bootstrap'],
    function (ko, dataService, config) {
        return function () {
            var searchResults = ko.observableArray([]);



            $("#myButton").click(function () {
                var searchVal = $("#searchField").val();
                var radioVal = $("input[name=questionsOrAnswers]:checked").val();

                dataService.getSearchResults(searchVal, radioVal, function (data) {
                    searchResults(data.searchResultList);
                });
            });

            $("#saveButton").click(function () {
                //var thisId = $(this).parent(".invisibleDiv").html();
                var thisId = $("#postIdContainer").text();
                var description = $("#description").val();
                alert(thisId);
                //var date = new Date();
                /*
                "markedId":1,"postId":1,"note":"lorem ipsum ...","date":"2016-10-05T20:19:50"
                */
                
                var obj = {
                    "PostId": thisId,
                    "SovaUserId ":1,
                    "Note": description,
                    "Date": "2016-12-11T20:19:55"
                };

                dataService.postMarked(obj);

            });

            return {
                searchResults

            };
        };
    });