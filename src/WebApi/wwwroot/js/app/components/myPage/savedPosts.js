define(['knockout', 'dataservice', 'config'],
    function (ko, dataService, config) {
        return function () {
            var marked = ko.observableArray([]);

            /*
            marked = [
                    { url: "gratisting.dk", markedId: 1, postId: 1, note: "marked1" },
                    { url: "gratisting.dk", markedId: 2, postId: 2, note: "marked2" },
                    { url: "gratisting.dk", markedId: 3, postId: 3, note: "marked3" }

            ]
            */

            dataService.getMarked(function (data) {
                marked(data.markedList);
            });

            return {
                marked
                
            };
        };
    });