angular.module('asMobile')
    .factory('CompetitionService', [
        '$http', 'UserService', 'LocalDBService', function ($http, userService, localDBService) {
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
                            return result.data.CompetitionRows;
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
                saveCompetition: function (competition) {
                    localDBService.persistItem("Competitions", competition);

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


