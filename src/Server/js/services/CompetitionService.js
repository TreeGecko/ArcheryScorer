angular.module('asMobile')
    .factory('CompetitionService', [
        '$http', 'UserService', function ($http, userService) {
            var service = {
                getCompetitions : function() {
                    var req = {
                        method: 'GET',
                        url: '/rest/competitions',
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
                getCompetition : function(guid) {
                    var req = {
                        method: 'GET',
                        url: '/rest/competition/' + guid,
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
                saveCompetition : function(competition) {
                    var req = {
                        method: 'POST',
                        url: '/rest/competition',
                        headers: {
                            'Username': userService.Username,
                            'AuthToken': userService.AuthToken
                        },
                        data: competition
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


