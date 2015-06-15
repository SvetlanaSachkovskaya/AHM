var app = angular.module('AhmApp', ['ui.router', 'LocalStorageModule', 'ui.bootstrap', 'ngPDFViewer', 'angularSpinner']);

app.config(function ($stateProvider, $urlRouterProvider, roles, pages) {

    $urlRouterProvider.otherwise('/404');

    $stateProvider
        .state(pages.home, {
            url: '/'
        })
        .state(pages.login, {
            url: '/login',
            templateUrl: 'Views/Authorization/Login.html',
            controller: 'loginController'
        })
        .state('landing', {
            url: '/landing',
            templateUrl: 'Views/Landing.html'
        })
        .state(pages.notFound, {
            url: '/404',
            templateUrl: 'Views/404.html'
        })
        .state(pages.accessDenied, {
            url: '/accessDenied',
            templateUrl: 'Views/AccessDenied.html'
        })
        .state(pages.locations, {
            url: '/locations',
            templateUrl: 'Views/BuildingInfo/Locations.html',
            controller: 'locationsController',
            roles: [roles.concierge]
        })
        .state(pages.packageTypes, {
            url: '/packageTypes',
            templateUrl: 'Views/BuildingInfo/PackageTypes.html',
            controller: 'packageTypesController',
            roles: [roles.concierge]
        })
        .state(pages.apartments, {
            url: '/apartments',
            templateUrl: 'Views/BuildingInfo/Apartments.html',
            controller: 'apartmentsController',
            roles: [roles.manager]
        })
        .state(pages.createApartment, {
            url: '/createApartment',
            templateUrl: 'Views/BuildingInfo/EditApartment.html',
            controller: 'editApartmentController',
            roles: [roles.manager]
        })
        .state(pages.editApartment, {
            url: '/editApartment:apartmentId',
            templateUrl: 'Views/BuildingInfo/EditApartment.html',
            controller: 'editApartmentController',
            roles: [roles.manager]
        })
        .state(pages.occupants, {
            url: '/occupants',
            templateUrl: 'Views/BuildingInfo/Occupants.html',
            controller: 'occupantsController',
            roles: [roles.manager]
        })
        .state(pages.editOccupant, {
            url: '/editOccupant:occupantId',
            templateUrl: 'Views/BuildingInfo/EditOccupant.html',
            controller: 'editOccupantController',
            roles: [roles.manager]
        })
        .state(pages.createOccupant, {
            url: '/createOccupant',
            templateUrl: 'Views/BuildingInfo/EditOccupant.html',
            controller: 'editOccupantController',
            roles: [roles.manager]
        })
        .state(pages.createPackage, {
            url: '/createPackage',
            templateUrl: 'Views/Post/CreatePackage.html',
            controller: 'createPackageController',
            roles: [roles.concierge]
        })
        .state(pages.editPackage, {
            url: '/editPackage:packageId',
            templateUrl: 'Views/Post/EditPackage.html',
            controller: 'editPackageController',
            roles: [roles.concierge]
        })
        .state(pages.packagesBoard, {
            url: '/packagesBoard',
            templateUrl: 'Views/Post/PackagesBoard.html',
            controller: 'packagesBoardController',
            roles: [roles.concierge]
        })
        .state(pages.instructions, {
            url: '/instructions',
            templateUrl: 'Views/Instructions/Instructions.html',
            controller: 'instructionsController',
            roles: [roles.concierge, roles.worker]
        })
        .state(pages.createInstruction, {
            url: '/createInstruction',
            templateUrl: 'Views/Instructions/CreateInstruction.html',
            controller: 'createInstructionController',
            roles: [roles.concierge, roles.worker]
        })
        .state(pages.editInstruction, {
            url: '/editInstruction:instructionId',
            templateUrl: 'Views/Instructions/EditInstruction.html',
            controller: 'editInstructionController',
            roles: [roles.concierge, roles.worker]
        })
        .state(pages.journal, {
            url: '/journal',
            templateUrl: 'Views/Journal/Journal.html',
            controller: 'journalController',
            roles: [roles.concierge, roles.manager]
        })
        .state(pages.createEvent, {
            url: '/createEvent',
            templateUrl: 'Views/Journal/CreateEvent.html',
            controller: 'createEventController',
            roles: [roles.concierge]
        })
        .state(pages.utilitiesClauses, {
            url: '/utilitiesClauses',
            templateUrl: 'Views/UtilitiesClause/UtilitiesClauses.html',
            controller: 'utilitiesClausesController',
            roles: [roles.accountant]
        })
        .state(pages.createUtilitiesClause, {
            url: '/createUtilitiesClause',
            templateUrl: 'Views/UtilitiesClause/EditUtilitiesClause.html',
            controller: 'editUtilitiesClauseController',
            roles: [roles.accountant]
        })
        .state(pages.editUtilitiesClause, {
            url: '/editUtilitiesClause:utilitiesClauseId',
            templateUrl: 'Views/UtilitiesClause/EditUtilitiesClause.html',
            controller: 'editUtilitiesClauseController',
            roles: [roles.accountant]
        })
        .state(pages.utilitiesSettings, {
            url: '/utilitiesSettings',
            templateUrl: 'Views/Utilities/UtilitiesSettings.html',
            controller: 'utilitiesSettingsController',
            roles: [roles.accountant]
        })
        .state(pages.editBill, {
            url: '/editBill:billId',
            templateUrl: 'Views/Utilities/EditBill.html',
            controller: 'editBillController',
            roles: [roles.accountant]
        })
        .state(pages.createBill, {
            url: '/createtBill',
            templateUrl: 'Views/Utilities/EditBill.html',
            controller: 'editBillController',
            roles: [roles.accountant]
        })
        .state(pages.payBill, {
            url: '/payBill:billId',
            templateUrl: 'Views/Utilities/PayBill.html',
            controller: 'payBillController',
            roles: [roles.accountant]
        })
        .state(pages.billDetails, {
            url: '/billDetails:billId',
            templateUrl: 'Views/Utilities/BillDetails.html',
            controller: 'billDetailsController',
            roles: [roles.accountant]
        })
        .state(pages.billsBoard, {
            url: '/billsBoard',
            templateUrl: 'Views/Utilities/BillsBoard.html',
            controller: 'billsBoardController',
            roles: [roles.accountant]
        })
        .state(pages.viewPdf, {
            url: '/pdfViewer:billId',
            templateUrl: 'Views/Utilities/BillPdfViewer.html',
            controller: 'billPdfViewerController',
            roles: [roles.accountant]
        })
        .state(pages.buildings, {
            url: '/buildings',
            templateUrl: 'Views/Admin/Buildings.html',
            controller: 'buildingsController',
            roles: [roles.admin]
        })
        .state(pages.editBuilding, {
            url: '/editBuilding:id',
            templateUrl: 'Views/Admin/EditBuilding.html',
            controller: 'editBuildingController',
            roles: [roles.admin]
        })
        .state(pages.createBuilding, {
            url: '/createBuilding',
            templateUrl: 'Views/Admin/EditBuilding.html',
            controller: 'editBuildingController',
            roles: [roles.admin]
        })
        .state(pages.users, {
            url: '/users:buildingId',
            templateUrl: 'Views/Admin/Users.html',
            controller: 'usersController',
            roles: [roles.admin]
        })
        .state(pages.createUser, {
            url: '/createUser',
            templateUrl: 'Views/Admin/EditUser.html',
            controller: 'editUserController',
            roles: [roles.admin]
        })
        .state(pages.editUser, {
            url: '/editUser:id',
            templateUrl: 'Views/Admin/EditUser.html',
            controller: 'editUserController',
            roles: [roles.admin]
        })
    ;
});

app.run(['$rootScope', '$state', 'authenticationService', 'roles', 'pages',
    function ($rootScope, $state, authenticationService, roles, pages) {
        $rootScope.$on('$stateChangeStart', function (event, to) {
            if ((!authenticationService.authentication.isAuthenticated) && to.name !== pages.login) {
                $state.go(pages.login);
                event.preventDefault();
                return;
            }

            if (!authenticationService.hasPermissions(to.roles)) {
                $state.go(pages.accessDenied);
                event.preventDefault();
                return;
            }

            if (to.name === pages.home) {
                switch (authenticationService.authentication.role) {
                    case roles.admin:
                        $state.go(pages.buildings);
                        break;
                    case roles.manager:
                        $state.go(pages.apartments);
                        break;
                    case roles.concierge:
                        $state.go(pages.packagesBoard);
                        break;
                    case roles.accountant:
                        $state.go(pages.billsBoard);
                        break;
                    case roles.worker:
                        $state.go(pages.instructions);
                        break;
                    default:
                        $state.go(pages.notFound);
                        break;
                }
                event.preventDefault();
            }
        });
    }
]);

app.config(['usSpinnerConfigProvider', function (usSpinnerConfigProvider) {
    usSpinnerConfigProvider.setDefaults({ color: 'green' });
}]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authenticationService', function (authenticationService) {
    authenticationService.fillAuthenticationData();
}]);
