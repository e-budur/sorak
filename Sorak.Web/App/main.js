// Maps the files so Durandal knows where to find these.
require.config({
    paths: {
        'text': '../Scripts/text',
        'durandal': '../Scripts/durandal',
        'plugins': '../Scripts/durandal/plugins',
        'transitions': '../Scripts/durandal/transitions'
    }
});

 
define('jquery', function () { return jQuery; });
define('knockout', ko);

define(['durandal/app', 'durandal/viewLocator', 'durandal/system', 'plugins/router', 'services/logger'], boot);

function boot (app, viewLocator, system, router, logger) {

    // Enable debug message to show in the console 
    system.debug(true);

    app.title = 'Sorak';

    app.configurePlugins({
        router: true
    });
    
    app.start().then(function () {
        toastr.options.positionClass = 'toast-bottom-right';
        toastr.options.backgroundpositionClass = 'toast-bottom-right';


        viewLocator.useConvention();
        
    
        app.setRoot('viewmodels/shell', 'entrance');
    });
};