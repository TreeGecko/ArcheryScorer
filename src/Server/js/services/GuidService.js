angular.module('asMobile')
    .factory('GuidService', [
        '$http', 'uuid2', 'UserService', 'LocalDBService', function($http, uuid2, userService, localDB) {
            var service = {
                isLoggedIn: false,
                Username: "",
                AuthToken: "",
                fetchNewGuids: function() {
                    var req = {
                        method: 'GET',
                        url: '/rest/guids/1000',
                        headers: {
                            'Username': userService.Username,
                            'AuthToken': userService.AuthToken
                        }
                    };

                    return $http(req)
                        .then(function (result) {
                            //Store to localdb
                            var list = result.data;
                            
                            if (list != null) {
                                for (var i = 0; i < list.length; i++) {
                                    var item = {};
                                    item.Guid = list[i];
                                    localDB.persistItem('Guids', item);
                                }
                            }
                        });
                    },
                verifyGuidSupply : function() {
                    var count = localDB.getCount("Guids");
                    
                    if (count < 100) {
                        service.fetchNewGuids();
                    }
                },
                getGuid : function(recursed) {
                    var item = localDB.findFirstItem("Guids");
                    
                    if (item != null) {
                        localDB.deleteItem("Guids", item);

                        return item.Guid;
                    } else {
                        if (recursed != true) {
                            service.fetchNewGuids()
                            .then(function() {
                                return service.getGuid(true);
                            });
                        } else {
                            return service.getLocalGuid();
                        }
                    }

                    return service.getLocalGuid();
                },
                getLocalGuid: function () {
                    //Inferior, but may be needed.
                    return uuid2.newguid();
                }
            };

            return service;
        }
    ]);
        