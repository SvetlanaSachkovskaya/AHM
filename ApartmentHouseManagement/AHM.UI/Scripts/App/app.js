var app = angular.module('AhmApp', ['ui.router', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap', 'ngPDFViewer']);

app.config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/landing');

    $urlRouterProvider.when('/building', 'landing/apartments');
    $urlRouterProvider.when('/post', 'landing/packagesBoard');
    $urlRouterProvider.when('/utilities', 'landing/billsBoard');

    $stateProvider
        .state("login", {
            url: '/login',
            templateUrl: 'Views/Authorization/Login.html',
            controller: 'loginController'
        })
        .state('landing', {
            url: '/landing',
            templateUrl: 'Views/Landing.html'
        })
        .state('landing.locations', {
            url: '/locations',
            templateUrl: 'Views/BuildingInfo/Locations.html',
            controller: 'locationsController'
        })
        .state('landing.home', {
            url: '/home',
            templateUrl: 'Views/Home.html',
            controller: 'homeController'
        })
        .state('landing.packageTypes', {
            url: '/packageTypes',
            templateUrl: 'Views/BuildingInfo/PackageTypes.html',
            controller: 'packageTypesController'
        })
        .state('landing.apartments', {
            url: '/apartments',
            templateUrl: 'Views/BuildingInfo/Apartments.html',
            controller: 'apartmentsController'
        })
        .state('landing.editApartment', {
            url: '/editApartment:apartmentId',
            templateUrl: 'Views/BuildingInfo/EditApartment.html',
            controller: 'editApartmentController'
        })
        .state('landing.occupants', {
            url: '/occupants',
            templateUrl: 'Views/BuildingInfo/Occupants.html',
            controller: 'occupantsController'
        })
        .state('landing.createOccupant', {
            url: '/createOccupant:occupantId',
            templateUrl: 'Views/BuildingInfo/CreateOccupant.html',
            controller: 'createOccupantController'
        })
        .state('landing.createPackage', {
            url: '/createPackage',
            templateUrl: 'Views/Post/CreatePackage.html',
            controller: 'createPackageController'
        })
        .state('landing.editPackage', {
            url: '/editPackage:packageId',
            templateUrl: 'Views/Post/EditPackage.html',
            controller: 'editPackageController'
        })
        .state('landing.packagesBoard', {
            url: '/packagesBoard',
            templateUrl: 'Views/Post/PackagesBoard.html',
            controller: 'packagesBoardController'
        })
        .state('landing.instructions', {
            url: '/instructions',
            templateUrl: 'Views/Instructions/Instructions.html',
            controller: 'instructionsController'
        })
        .state('landing.createInstruction', {
            url: '/createInstruction',
            templateUrl: 'Views/Instructions/CreateInstruction.html',
            controller: 'createInstructionController'
        })
        .state('landing.editInstruction', {
            url: '/editInstruction:instructionId',
            templateUrl: 'Views/Instructions/EditInstruction.html',
            controller: 'editInstructionController'
        })
        .state('landing.journal', {
            url: '/journal',
            templateUrl: 'Views/Journal/Journal.html',
            controller: 'journalController'
        })
        .state('landing.createEvent', {
            url: '/createEvent',
            templateUrl: 'Views/Journal/CreateEvent.html',
            controller: 'createEventController'
        })
        .state('landing.utilitiesClauses', {
            url: '/utilitiesClauses',
            templateUrl: 'Views/UtilitiesClause/UtilitiesClauses.html',
            controller: 'utilitiesClausesController'
        })
        .state('landing.editUtilitiesClause', {
            url: '/editUtilitiesClause:utilitiesClauseId',
            templateUrl: 'Views/UtilitiesClause/EditUtilitiesClause.html',
            controller: 'editUtilitiesClauseController'
        })
        .state('landing.editBill', {
            url: '/editBill:billId',
            templateUrl: 'Views/Utilities/EditBill.html',
            controller: 'editBillController'
        })
        .state('landing.billDetails', {
            url: '/billDetails:billId',
            templateUrl: 'Views/Utilities/BillDetails.html',
            controller: 'billDetailsController'
        })
        .state('landing.billsBoard', {
            url: '/billsBoard',
            templateUrl: 'Views/Utilities/BillsBoard.html',
            controller: 'billsBoardController'
        })
        .state('landing.viewPdf', {
            url: '/pdfViewer:billId',
            templateUrl: 'Views/Utilities/BillPdfViewer.html',
            controller: 'billPdfViewerController'
        });
});

//todo: implement for $state
//app.run(['$rootScope', '$location', 'authenticationService', function ($rootScope, $location, authenticationService) {
//        $rootScope.$on('$routeChangeStart', function () {
//            if (!authenticationService.authentication.isAuthenticated) {
//                $location.path('/login');
//            }
//        });
//    }
//]);

//var serviceBase = 'http://wsb-035/ahmapi/';
var serviceBase = 'http://localhost:6888/';
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


