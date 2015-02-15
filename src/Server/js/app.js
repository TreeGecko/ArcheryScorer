angular.module('asMobile', ['ngRoute'])
    .config(function($routeProvider) {

        $routeProvider.when('/', {
            templateUrl: 'views/home.html'
            })
            .when('/login', {
                templateUrl: 'views/login.html'
            })
            .when('/logout', {
                templateUrl: 'views/logout.html'
            });

        $routeProvider.otherwise({
            redirectTo: '/'
        });
    });
