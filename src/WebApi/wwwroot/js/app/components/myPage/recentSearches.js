define(['knockout', 'dataservice', 'postman', 'config'],
    function (ko, dataService, postman, config) {
        return function () {
            var histories = ko.observableArray([]);

            /*
            histories = [
                    { url: "gratisting.dk", historyId: 1, sovaUserId: 1, searchText: "glædelig hjul1" },
                    { url: "gratisting.dk", historyId: 2, sovaUserId: 2, searchText: "glædelig hjul2" },
                    { url: "gratisting.dk", historyId: 3, sovaUserId: 3, searchText: "glædelig hjul3" },

            ]
            */

            dataService.getHistories(function (data) {

                histories(data.historyList);
            });

            return {
                histories

            };
        };
    });