angular.module('asMobile')
    .controller('MainCtrl', [
        'UserService', 'GuidService',
        function (userService, guidService) {
            var self = this;
            self.userService = userService;

            guidService.verifyGuidSupply();
        }
    ]);
