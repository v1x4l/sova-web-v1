define(['knockout', 'dataservice', 'postman', 'config'],
    function (ko, dataService, postman, config) {
        return function () {
            var searchResults = ko.observableArray([]);



            
            dataService.getSearchResults("java", "true", function (data) {
                console.log(data);
                //searchResults();
            });
            

            return {
                searchResults

            };
        };
    });