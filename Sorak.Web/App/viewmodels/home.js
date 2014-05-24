define(['services/logger',
        'services/datacontext',
        'plugins/router',], function (logger, datacontext, router) {
            var title = 'Hüsnü bey ile sohbet toplantıları';
            var topics = ko.observable();
            var vm = {
                activate: activate,
                title: title,
                topics: topics,
                goQuestions: goQuestions
            };

            return vm;

            //#region Internal Methods
            function activate() {
               // logger.log(title + ' View Activated', null, title, true);
                return datacontext.getTopics(topics).then(function () {
                    var a = 1;
                });
            }

            function goQuestions(e) {
                var url = "#/questions/" + e.id;
                router.navigate(url);
            }

            //#endregion
        });