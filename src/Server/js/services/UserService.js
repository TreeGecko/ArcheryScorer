angular.module('asMobile')
    .factory('UserService', [
        '$http', function ($http) {
            var service = {
                isLoggedIn: false,
                isDataAdmin: false,
                isUserAdmin: false,
                Username: "",
                AuthToken: "",
                session: function () {
                    service.Username = Lockr.get("Username", null);
                    service.AuthToken = Lockr.get("AuthToken", null);
                    service.isUserAdmin = Lockr.get("IsUserAdmin", false);
                    service.isDataAdmin = Lockr.get("IsDataAdmin", false);

                    if (service.AuthToken != null
                        && service.Username != null) {
                        service.isLoggedIn = true;
                    } else {
                        service.isLoggedIn = false;
                    }
                },
                login: function (user) {
                    return $http.post('/rest/login', user)
                        .then(function (response) {
                            if (response.data.Result == "Success") {
                                service.isLoggedIn = true;
                                service.isUserAdmin = response.data.IsUserAdmin;
                                service.isDataAdmin = response.data.IsDataAdmin;

                                Lockr.set("Username", response.data.Username);
                                Lockr.set("AuthToken", response.data.AuthToken);
                                Lockr.set("IsUserAdmin", service.isUserAdmin);
                                Lockr.set("IsDataAdmin", service.isDataAdmin);

                                $http.defaults.headers.common['Username'] = response.data.Username;
                                $http.defaults.headers.common['AuthToken'] = response.data.AuthToken;

                                return response;
                            } else {
                                //TODO - Add Bootstrap Dialog
                                window.alert("Username or password not correct.");
                            }
                            return null;
                        });
                },
                logout: function () {
                    service.isLoggedIn = false;
                    Lockr.flush();

                    $http.defaults.headers.common['Username'] = null;
                    $http.defaults.headers.common['AuthToken'] = null;

                }
            };
            return service;
        }
    ]);


