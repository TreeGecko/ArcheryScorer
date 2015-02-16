angular.module('asMobile')
    .controller('ShooterListCtrl', [
        '$location', 'ShooterService',
        function($location, shooterService) {
            var self = this;

            self.data = {};

            self.getShooters = function() {
                shooterService.getShooters()
                    .then(function(result) {
                        self.data.Shooters = result;
                    });
                };

            self.getShooters();
        }
    ]);