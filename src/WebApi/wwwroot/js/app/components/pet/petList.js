define(['knockout', 'dataservice', 'postman', 'config'],
    function (ko, dataService, postman, config) {
        return function () {
            var users = ko.observableArray([]);
            
            var users2 = [];

            var goToPersons = function() {
                postman.publish(
                    config.events.changeMenu,
                    config.menuItems.persons);
            }

            dataService.getUsers(function (data) {
                //data.userList.forEach(user => users.push(user))
                //users = ko.observableArray(data.userList);
                users(data.userList);
                console.log(users());
            });
         
            return {
                users,
                goToPersons,
                
            };
        };
    });