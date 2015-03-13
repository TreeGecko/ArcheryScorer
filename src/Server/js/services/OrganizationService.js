angular.module('asMobile')
    .factory('OrganizationService', [
        '$http', 'UserService', function ($http, userService) {
            var service = {
                getOrganizations: function () {
                    var req = {
                        method: 'GET',
                        url: '/rest/organizations',
                        headers: {
                            'Username': userService.Username,
                            'AuthToken': userService.AuthToken
                        }
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


