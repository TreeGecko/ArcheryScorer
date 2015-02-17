var module = angular.module('asMobile', ['ngRoute', 'ui.bootstrap', 'angularUUID2']);

module.config(function ($routeProvider) {
        $routeProvider.when('/', {
            templateUrl: 'views/home.html'
            })
            .when('/login', {
                templateUrl: 'views/login.html'
            })
            .when('/logout', {
                templateUrl: 'views/logout.html'
            })
            .when('/shooter', {
                templateUrl: 'views/shooter_add.html'
            })
            .when('/shooter/:guid', {
                templateUrl: 'views/shooter_add.html'
            })
            .when('/shooters', {
                templateUrl: 'views/shooter_list.html'
            })
            .when('/competition', {
                templateUrl: 'views/competition_add.html'
            })
            .when('/competition/:guid', {
                templateUrl: 'views/competition_add.html'
            })
            .when('/competitions', {
                templateUrl: 'views/competition_list.html'
            });

        $routeProvider.otherwise({
            redirectTo: '/'
        });
    });
