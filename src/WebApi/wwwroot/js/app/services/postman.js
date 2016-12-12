define([], function () {
    var subscribers = [];

    var subscribe = function(topic, callback) {
        var subscriber = { topic, callback };
        subscribers.push(subscriber);
        return function() {
            subscribers = subscribers.forEach(s => s !== subscriber);
        }
    }

    var publish = function (topic, data)
    {
        subscribers.forEach(function(s) {
            if (s.topic === topic) s.callback(data);
        });
    }

    return {
        subscribe,
        publish
    };
});
