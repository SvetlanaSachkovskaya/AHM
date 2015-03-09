var app = angular.module('AhmApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "Views/Home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "Views/Login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "Views/Signup.html"
    });

    $routeProvider.when("/locations", {
        controller: "locationController",
        templateUrl: "Views/Locations.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

var serviceBase = 'http://localhost:6888/';
//var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authenticationService', function (authenticationService) {
    authenticationService.fillAuthenticationData();
}]);


