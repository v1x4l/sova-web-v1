define(['knockout', 'dataservice', 'postman', 'config', 'jqcloud'],
    function (ko, dataService, postman, config) {
        return function () {
            var wordList = ko.observableArray([]);
            /*
            wordList = [
          { word: "Lorem", frequency: 15},
          { word: "Ipsum", frequency: 9},
          { word: "Dolor", frequency: 6},
          { word: "Sit", frequency: 7},
          { word: "Amet", frequency: 5}
            ];
            */
            makeCloud = function () {
                $("#wordCloud").jQCloud(wordList);
            }

            
            dataService.getWordList("java", "true", function (data) {
                var temp = [];
                temp = data.frequentWordList;
                temp.sort(function (a, b) {
                    return parseFloat(b.frequency) - parseFloat(a.frequency);
                });
                console.log("sorted" + temp);
                temp = temp.slice(0, 6);
                console.log("sliced" + temp);
                wordList(temp);
                //wordList.push(temp.slice(0, 6));
                console.log(wordList);
            });
            

            return {
                makeCloud,
                wordList
            };
        };
    });