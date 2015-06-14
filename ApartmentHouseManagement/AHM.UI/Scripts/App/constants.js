//'http://wsb-035/ahmapi/'
app.constant('ngAuthSettings', {
    apiServiceBaseUri: 'http://localhost/ahmapi/',
    clientId: 'ngAuthApp'
});

app.constant('roles', {
    manager: 'Manager',
    concierge: 'Concierge',
    admin: 'Admin',
    worker: 'Worker',
    accountant: 'Accountant'
});

app.constant('pages', {
    notFound: '404',
    accessDenied: 'landing.accessDenied',
    login: 'login',
    home: 'home',
    locations: 'landing.locations',
    packageTypes: 'landing.packageTypes',
    apartments: 'landing.apartments',
    editApartment: 'landing.editApartment',
    createApartment: 'landing.createApartment',
    occupants: 'landing.occupants',
    editOccupant: 'landing.editOccupant',
    createOccupant: 'landing.createOccupant',
    createPackage: 'landing.createPackage',
    editPackage: 'landing.editPackage',
    packagesBoard: 'landing.packagesBoard',
    instructions: 'landing.instructions',
    createInstruction: 'landing.createInstruction',
    editInstruction: 'landing.editInstruction',
    journal: 'landing.journal',
    createEvent: 'landing.createEvent',
    utilitiesClauses: 'landing.utilitiesClauses',
    editUtilitiesClause: 'landing.editUtilitiesClause',
    createUtilitiesClause: 'landing.createUtilitiesClause',
    editBill: 'landing.editBill',
    createBill: 'landing.createBill',
    payBill: 'landing.payBill',
    billDetails: 'landing.billDetails',
    billsBoard: 'landing.billsBoard',
    viewPdf: 'landing.viewPdf',
    buildings: 'landing.buildings',
    editBuilding: 'landing.editBuilding',
    createBuilding: 'landing.createBuilding',
    users: 'landing.users',
    editUser: 'landing.editUser',
    createUser: 'landing.createUser',
    utilitiesSettings: 'landing.utilitiesSettings'
});