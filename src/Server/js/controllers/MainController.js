angular.module('asMobile')
    .controller('MainCtrl', [
        'UserService',
        function (userService) {
            var self = this;
            self.userService = userService;

            // Check if the user is logged in when the application
            // loads
            // User Service will automatically update isLoggedIn
            // after this call finishes
            self.userService.session();
        }
    ]);
