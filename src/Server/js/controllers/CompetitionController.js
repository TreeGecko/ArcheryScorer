angular.module('asMobile')
    .controller('CompetitionCtrl', [
        '$routeParams', '$location', 'CompetitionService', 'ShooterService', 'GuidService',
        function ($routeParams, $location, competitionService, shooterService, guidService) {
            var self = this;

            self.Guid = $routeParams.guid;
            self.competition = {};
            self.organizations = {};
            self.addedShooter = {};

            self.defaultCompetitionDate = function () {
                self.competition.Date = new Date();
            };
            self.defaultCompetitionDate();

            self.clearBirthDate = function () {
                self.competition.Date = null;
            };

            self.getCompetition = function (guid) {
                competitionService.getCompetition(guid)
                    .then(function (result) {
                        self.competition = result;
                    });
            };

            self.saveCompetition = function () {
                self.competition.Guid = self.Guid;

                competitionService.saveShooter(self.competition)
                    .then(function (result) {
                        if (result.Result == "Success") {
                            $location.path("/competitions");
                        }
                    });
            };

            self.getOrganizations = function () {
                self.organizations = [{ Name: 'Joad'}];
            };

            self.getShooters = function () {
                shooterService.getShooters()
                    .then(function (result) {
                        self.shooters = result;
                    });
            };

            self.addShooter = function () {
                if (self.addedShooter != null) {
                    //verify shooter not already added
                    var found = false;

                    for (var i = 0, len = self.competition.CompetitionShooters.length; i < len; i++) {
                        var selectedShooter = self.competition.CompetitionShooters[i];

                        if (selectedShooter != null
                            && selectedShooter.ShooterGuid != null
                            && selectedShooter.ShooterGuid == self.addedShooter) {
                            found = true;
                            break;
                        }
                    }

                    if (!found) {
                        for (var j = 0; j < self.shooters.length; j++) {
                            var shooter = self.shooters[j];

                            if (shooter != null
                                && shooter.Guid != null
                                && shooter.Guid == self.addedShooter) {

                                var csGuid = guidService.getGuid(false);

                                var competitionShooter = {
                                    Guid: csGuid,
                                    ShooterGuid: shooter.Guid,
                                    ShooterFullName: shooter.FirstName + " " + shooter.LastName,
                                    Rounds: []
                                };

                                self.competition.CompetitionShooters.push(competitionShooter);
                            }
                        }
                    }

                    self.addedShooter = null;
                }
            };


            self.getShooters();
            self.getOrganizations();

            if (self.Guid != null) {
                self.getCompetition(self.Guid);
            } else {
                var cGuid = guidService.getGuid(false);

                self.competition = {
                    Guid: cGuid,
                    CompetitionShooters: []
                };

            }
        }
    ]);