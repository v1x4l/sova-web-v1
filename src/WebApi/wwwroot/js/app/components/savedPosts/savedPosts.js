define(['knockout', 'dataservice', 'postman', 'config'],
    function (ko, dataService, postman, config) {
        return function () {
            var marked = ko.observableArray([]);

        /*
            public string Url { get; set; }
            public int MarkedId { get; set; }
            public int PostId { get; set; }
            public string Note { get; set; }
            public DateTime Date { get; set; }
        */

            marked = [
                    { url: "gratisting.dk", markedId: 1, postId: 1, note: "marked1" },
                    { url: "gratisting.dk", markedId: 2, postId: 2, note: "marked2" },
                    { url: "gratisting.dk", markedId: 3, postId: 3, note: "marked3" },

            ]

            //Use this ?
            var goToPersons = function () {
                postman.publish(
                    config.events.changeMenu,
                    config.menuItems.persons);
            }


            dataService.getMarked(function (data) {
                //marked(data.markedList);
                console.log(marked);
            });

            return {
                marked,
                goToPersons,

            };
        };
    });