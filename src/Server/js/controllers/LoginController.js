angular.module('asMobile')
    .controller('LoginCtrl', [
        'UserService', '$location',
        function (userService, $location) {
            var self = this;
            self.user = { username: '', password: '' };

            self.login = function () {
                userService.login(self.user).then(function (success) {
                    $location.path('/');
                }, function (error) {
                    self.errorMessage = error.data.msg;
                });
            };
        }
    ]);