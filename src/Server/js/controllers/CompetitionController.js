angular.module('asMobile')
    .controller('CompetitionCtrl', [
        '$routeParams', '$location', 'CompetitionService', 'ShooterService', 'OrganizationService', 'GuidService',
        function ($routeParams, $location, competitionService, shooterService, organizationService, guidService) {
            var self = this;

            self.competition = {};
            self.competition.Guid = $routeParams.guid;
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

            self.buildCompetitionStructure = function (competition) {
                var shooters = competition.CompetitionShooters;
                var rounds = competition.Rounds;
                var ends = competition.Ends;
                var arrows = competition.Arrows;

                for (var h = 0; h < shooters.length; h++) {
                    var shooter = shooters[h];

                    //Skip shooters that already have rounds assigned.
                    if (shooter.Rounds.length == 0) {
                        for (var i = 1; i <= rounds; i++) {
                            var round = { Sequence: i, Ends: [] };
                            shooter.Rounds.push(round);

                            for (var j = 1; j < ends; j++) {
                                var end = { Sequence: j, Arrows: [] };
                                round.Ends.push(end);

                                for (var k = 1; k < arrows; k++) {
                                    var arrow = { Sequence: k };
                                    end.Arrows.push(arrow);
                                }
                            }
                        }
                    }
                }
            };

            self.saveCompetition = function () {
                self.buildCompetitionStructure(self.competition);

                competitionService.saveCompetition(self.competition)
                    .then(function (result) {
                        if (result.Result == "Success") {
                            $location.path("/competitions");
                        }
                    });
            };

            self.getOrganizations = function () {
                self.organizations = organizationService.getOrganizations()
                    .then(function(result) {
                        self.organizations = result;
                    });
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

            if (self.competition.Guid != null) {
                self.getCompetition(self.competition.Guid);
            } else {
                var cGuid = guidService.getGuid(false);

                self.competition = {
                    Guid: cGuid,
                    CompetitionShooters: []
                };

            }
        }
    ]);