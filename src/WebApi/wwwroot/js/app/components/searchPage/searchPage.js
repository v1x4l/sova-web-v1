define(['knockout', 'dataservice', 'postman', 'config'],
    function (ko, dataService, postman, config) {
        return function () {
            var searchResults = ko.observableArray([]);



            
            dataService.getSearchResults("java script function", "true", function (data) {
                //console.log(data);
                searchResults(data.searchResultList);
                console.log(searchResults);
            });
            

            return {
                searchResults

            };
        };
    });