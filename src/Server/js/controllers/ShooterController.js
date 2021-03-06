﻿angular.module('asMobile')
    .controller('ShooterCtrl', [
        '$routeParams', '$location', 'ShooterService',
        function ($routeParams, $location, shooterService) {
            var self = this;
            
            self.Guid = $routeParams.guid;
            self.shooter = {};

            self.defaultBirthDate = function () {
                self.shooter.BirthDate = new Date();
            };
            self.defaultBirthDate();

            self.clearBirthDate = function () {
                self.shooter.BirthDate = null;
            };

            self.getShooter = function (guid) {
                shooterService.getShooter(guid)
                    .then(function (result) {
                        self.shooter = result;
                    });
                };

            self.saveShooter = function() {
                self.shooter.Guid = self.Guid;

                shooterService.saveShooter(self.shooter)
                    .then(function(result) {
                        if (result.Result == "Success") {
                            $location.path("/shooters");
                        }
                    });
            };

            if (self.Guid != null) {
                self.getShooter(self.Guid);
            }
        }
    ]);