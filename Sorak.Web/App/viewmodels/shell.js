define(['durandal/system', 'plugins/router', 'services/logger', 'services/datacontext'],
    function (system, router, logger, datacontext) {
        var userInfo = ko.observable();
        var shell = {
            activate: activate,
            router: router,
            userInfo: userInfo,
            gotoRoot: gotoRoot
        };

        return shell;

        //#region Internal Methods
        function activate() {
            datacontext.getCurrenUser(userInfo).then(function () {
                var a = 1;
            });
            return boot();
        }

        function boot() {


            router.on('router:route:not-found', function (fragment) {
                logError('No Route Found', fragment, true);
            });

            var routes = [
                { route: '', moduleId: 'home', title: 'Toplantılar', nav: 1 },
                { route: 'questions/:id', moduleId: 'questions', title: 'Sorular', nav: 2}];

            return router.makeRelative({ moduleId: 'viewmodels' }) // router will look here for viewmodels by convention
                .map(routes)            // Map the routes
                .buildNavigationModel() // Finds all nav routes and readies them
                .activate();            // Activate the router
        }

        function log(msg, data, showToast) {
            logger.log(msg, data, system.getModuleId(shell), showToast);
        }

        function logError(msg, data, showToast) {
            logger.logError(msg, data, system.getModuleId(shell), showToast);
        }

        function gotoRoot() {
            router.navigate('');
        }
        //#endregion
    });