define(['knockout', 'postman', 'config', 'toastr'], function (ko, postman, config, toastr) {
    return function (params) {
        var person = ko.observable(params.person);

        var savePerson = function() {
            postman.publish(config.events.savePerson, ko.toJS(person));
            toastr.success("Person saved!");
        }

        postman.subscribe(config.events.selectPerson, function (p) {
            person(p);
        });



        return {
            person,
            savePerson
        };
    };
});
