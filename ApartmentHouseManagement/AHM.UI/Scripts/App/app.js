var app = angular.module('AhmApp', ['ui.router', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap', 'ngPDFViewer']);

app.config(function ($stateProvider, $urlRouterProvider, roles) {

    $urlRouterProvider.otherwise('/landing');

    $urlRouterProvider.when('/building', 'landing/apartments');

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
            controller: 'locationsController',
            roles: [roles.concierge]
        })
        .state('landing.home', {
            url: '/home',
            templateUrl: 'Views/Home.html',
            controller: 'homeController'
        })
        .state('landing.packageTypes', {
            url: '/packageTypes',
            templateUrl: 'Views/BuildingInfo/PackageTypes.html',
            controller: 'packageTypesController',
            roles: [roles.concierge]
        })
        .state('landing.apartments', {
            url: '/apartments',
            templateUrl: 'Views/BuildingInfo/Apartments.html',
            controller: 'apartmentsController',
            roles: [roles.manager]
        })
        .state('landing.editApartment', {
            url: '/editApartment:apartmentId',
            templateUrl: 'Views/BuildingInfo/EditApartment.html',
            controller: 'editApartmentController',
            roles: [roles.manager]
        })
        .state('landing.occupants', {
            url: '/occupants',
            templateUrl: 'Views/BuildingInfo/Occupants.html',
            controller: 'occupantsController',
            roles: [roles.manager]
        })
        .state('landing.createOccupant', {
            url: '/createOccupant:occupantId',
            templateUrl: 'Views/BuildingInfo/CreateOccupant.html',
            controller: 'createOccupantController',
            roles: [roles.manager]
        })
        .state('landing.createPackage', {
            url: '/createPackage',
            templateUrl: 'Views/Post/CreatePackage.html',
            controller: 'createPackageController',
            roles: [roles.concierge]
        })
        .state('landing.editPackage', {
            url: '/editPackage:packageId',
            templateUrl: 'Views/Post/EditPackage.html',
            controller: 'editPackageController',
            roles: [roles.concierge]
        })
        .state('landing.packagesBoard', {
            url: '/packagesBoard',
            templateUrl: 'Views/Post/PackagesBoard.html',
            controller: 'packagesBoardController',
            roles: [roles.concierge]
        })
        .state('landing.instructions', {
            url: '/instructions',
            templateUrl: 'Views/Instructions/Instructions.html',
            controller: 'instructionsController',
            roles: [roles.concierge, roles.worker]
        })
        .state('landing.createInstruction', {
            url: '/createInstruction',
            templateUrl: 'Views/Instructions/CreateInstruction.html',
            controller: 'createInstructionController',
            roles: [roles.concierge, roles.worker]
        })
        .state('landing.editInstruction', {
            url: '/editInstruction:instructionId',
            templateUrl: 'Views/Instructions/EditInstruction.html',
            controller: 'editInstructionController',
            roles: [roles.concierge, roles.worker]
        })
        .state('landing.journal', {
            url: '/journal',
            templateUrl: 'Views/Journal/Journal.html',
            controller: 'journalController',
            roles: [roles.concierge, roles.manager]
        })
        .state('landing.createEvent', {
            url: '/createEvent',
            templateUrl: 'Views/Journal/CreateEvent.html',
            controller: 'createEventController',
            roles: [roles.concierge]
        })
        .state('landing.utilitiesClauses', {
            url: '/utilitiesClauses',
            templateUrl: 'Views/UtilitiesClause/UtilitiesClauses.html',
            controller: 'utilitiesClausesController',
            roles: [roles.accountant]
        })
        .state('landing.editUtilitiesClause', {
            url: '/editUtilitiesClause:utilitiesClauseId',
            templateUrl: 'Views/UtilitiesClause/EditUtilitiesClause.html',
            controller: 'editUtilitiesClauseController',
            roles: [roles.accountant]
        })
        .state('landing.editBill', {
            url: '/editBill:billId',
            templateUrl: 'Views/Utilities/EditBill.html',
            controller: 'editBillController',
            roles: [roles.accountant]
        })
        .state('landing.payBill', {
            url: '/payBill:billId',
            templateUrl: 'Views/Utilities/PayBill.html',
            controller: 'payBillController',
            roles: [roles.accountant]
        })
        .state('landing.billDetails', {
            url: '/billDetails:billId',
            templateUrl: 'Views/Utilities/BillDetails.html',
            controller: 'billDetailsController',
            roles: [roles.accountant]
        })
        .state('landing.billsBoard', {
            url: '/billsBoard',
            templateUrl: 'Views/Utilities/BillsBoard.html',
            controller: 'billsBoardController',
            roles: [roles.accountant]
        })
        .state('landing.viewPdf', {
            url: '/pdfViewer:billId',
            templateUrl: 'Views/Utilities/BillPdfViewer.html',
            controller: 'billPdfViewerController',
            roles: [roles.accountant]
        })
        .state('landing.buildings', {
            url: '/buildings',
            templateUrl: 'Views/Admin/Buildings.html',
            controller: 'buildingsController',
            roles: [roles.admin]
        })
        .state('landing.editBuilding', {
            url: '/editBuilding:id',
            templateUrl: 'Views/Admin/EditBuilding.html',
            controller: 'editBuildingController',
            roles: [roles.admin]
        })
        .state('landing.users', {
            url: '/users',
            templateUrl: 'Views/Admin/Users.html',
            controller: 'usersController',
            roles: [roles.admin]
        })
        .state('landing.editUser', {
            url: '/editUser',
            templateUrl: 'Views/Admin/EditUser.html',
            controller: 'editUserController',
            roles: [roles.admin]
        })
    ;
});

app.run(['$rootScope', '$state', 'authenticationService', function ($rootScope, $state, authenticationService) {
    $rootScope.$on('$stateChangeStart', function (event, to) {
            if (!authenticationService.authentication.isAuthenticated && to.roles) {
                $state.go('login');
                event.preventDefault();
            } else if (!authenticationService.hasPermissions(to.roles)) {
                $state.go('landing.home');
                event.preventDefault();
            }
        });
    }
]);

//var serviceBase = 'http://wsb-035/ahmapi/';
var serviceBase = 'http://localhost/ahmapi/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.constant('roles', {
    manager: 'Manager',
    concierge: 'Concierge',
    admin: 'Admin',
    worker: 'Worker',
    accountant: 'Accountant'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authenticationService', function (authenticationService) {
    authenticationService.fillAuthenticationData();
}]);


