define(['knockout', 'dataservice', 'postman', 'config'],
    function (ko, dataService, postman, config) {
        return function () {
            var histories = ko.observableArray([]);

            histories = [
                    { url: "gratisting.dk", historyId: 1, sovaUserId: 1, searchText: "glædelig hjul1" },
                    { url: "gratisting.dk", historyId: 2, sovaUserId: 2, searchText: "glædelig hjul2" },
                    { url: "gratisting.dk", historyId: 3, sovaUserId: 3, searchText: "glædelig hjul3" },

            ]

            //Use this ?
            var goToPersons = function () {
                postman.publish(
                    config.events.changeMenu,
                    config.menuItems.persons);
            }
            /*
            public string Url { get; set; }
            public int HistoryId { get; set; }
            public int SovaUserId { get; set; }
            public string SearchText { get; set; }
            public DateTime SearchDate { get; set; }
            */  

            dataService.getHistories(function (data) {
                //data.userList.forEach(user => users.push(user))
                //users = ko.observableArray(data.userList);

                //histories(data.historyList);

                //Has no dateTime attribute
                
                console.log(histories);
            });

            return {
                histories,
                goToPersons,

            };
        };
    });