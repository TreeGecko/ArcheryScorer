angular.module('asMobile')
    .controller('CompetitionListCtrl', [
        '$location', 'CompetitionService',
        function($location, competitionService) {
            var self = this;

            self.data = {};

            self.getCompetitions = function() {
                competitionService.getCompetitions()
                    .then(function(result) {
                        self.data.Competitions = result;
                    });
            };

            self.getCompetitions();
        }
    ]);