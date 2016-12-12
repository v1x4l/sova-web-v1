define(['knockout', 'dataservice', 'postman', 'config'], function (ko, dataService, postman, config) {
    return function () {
        var persons = ko.observableArray([]);
        var selectedPerson = ko.observable();

        var selectPerson = function(person) {
            selectedPerson(person);
            postman.publish(config.events.selectPerson, person);
        }

        var isSelected = function(person) {
            return person === selectedPerson();
        }

        postman.subscribe(config.events.savePerson, function(person) {
            var personArray = persons();
            for (var i = 0; i < personArray.length; i++) {
                if (personArray[i].id === person.id) {
                    personArray[i] = person;
                    break;
                }
            }
            persons(personArray);
            selectedPerson(person);
        });
        /*
        dataService.getPersons(function (data) {
            persons(data);
        });
        */
        return {
            persons,
            selectPerson,
            isSelected
        };
    };
});
