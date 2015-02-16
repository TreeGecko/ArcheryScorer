angular.module('asMobile')
    .factory('ShooterService', [
        '$http', 'UserService', function ($http, userService) {
            var service = {
                getShooters : function() {
                    var req = {
                        method: 'GET',
                        url: '/rest/shooters',
                        headers: {
                            'Username': userService.Username,
                            'AuthToken': userService.AuthToken
                        }
                    };

                    return $http(req)
                        .then(function (result) {
                            return result.data;
                        });
                    },
                getShooter : function(guid) {
                    var req = {
                        method: 'GET',
                        url: '/rest/shooter/' + guid,
                        headers: {
                            'Username': userService.Username,
                            'AuthToken': userService.AuthToken
                        },
                        data: user
                    };

                    return $http(req)
                        .then(function (result) {
                            return result.data;
                        });
                },
                updateShooter : function(shooter) {
                    var req = {
                        method: 'POST',
                        url: '/rest/shooter',
                        headers: {
                            'Username': userService.Username,
                            'AuthToken': userService.AuthToken
                        },
                        data: shooter
                    };

                    return $http(req)
                        .then(function (result) {
                            return result.data;
                        });
                }

            };
            return service;
        }
    ]);


