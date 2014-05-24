

define(['services/logger'],
    function (config, logger) {
        var EntityQuery = breeze.EntityQuery;
        var remoteServiceName = 'breeze/Shell';
        var manager = configureBreezeManager();

        // var orderBy = model.orderBy;
        //  var entityNames = model.entityNames;

        var getCurrenUser = function (userObservable) {
            var query = EntityQuery.from("UserInformation");

            return manager.executeQuery(query)
                .then(querySucceeded)
                .fail(queryFailed);

            function querySucceeded(data) {

                if (userObservable) {
                    userObservable(data.results[0]);
                }
            }
        };

        var getTopics = function (topicsObservable) {
            var query = EntityQuery.from("GetSubjects");

            return manager.executeQuery(query)
                .then(querySucceeded)
                .fail(queryFailed);

            function querySucceeded(data) {

                if (topicsObservable) {
                    topicsObservable(data.results);
                }
            }
        };

        var getQuestions = function (id,questionsObservable) {
            var query = EntityQuery.from("GetQuestions")
            .withParameters({ subjectId: id });

            return manager.executeQuery(query)
                .then(querySucceeded)
                .fail(queryFailed);

            function querySucceeded(data) {
                if (questionsObservable) {
                    questionsObservable(data.results);
                }
            }
        };

        var like = function (id,result) {
            var query = EntityQuery.from("Like")
            .withParameters({ questionId: id });

            return manager.executeQuery(query)
                .then(querySucceeded)
                .fail(queryFailed);

            function querySucceeded(data) {
                if (result) {
                    result(data.results);
                }
            }
        };

        var unLike = function (id, result) {
            var query = EntityQuery.from("Unlike")
            .withParameters({ questionId: id });

            return manager.executeQuery(query)
                .then(querySucceeded)
                .fail(queryFailed);

            function querySucceeded(data) {
                if (result) {
                    result(data.results);
                }
            }
        };

        var ask = function (id,text, result) {
            var query = EntityQuery.from("Ask")
            .withParameters({ questionText: text,
            subjectId: id});

            return manager.executeQuery(query)
                .then(querySucceeded)
                .fail(queryFailed);

            function querySucceeded(data) {
                if (result) {
                    result(data.results);
                }
            }
        };

        var datacontext = {
            getCurrenUser: getCurrenUser,
            getTopics: getTopics,
            getQuestions: getQuestions,
            like:like,
            unLike: unLike,
            ask: ask
        };

        return datacontext;



        ///********Configurations***********//
        //#region configuration
        function configureBreezeManager() {
            breeze.NamingConvention.camelCase.setAsDefault();
            var mgr = new breeze.EntityManager(remoteServiceName);
            //   model.configureMetadataStore(mgr.metadataStore);
            return mgr;
        }

        function queryFailed(error) {
            var msg = 'Error retreiving data. ' + error.message;
            logError(msg, error);
            throw error;
        }

        function getErrorMessages(error) {
            var msg = error.message;
            if (msg.match(/validation error/i)) {
                return getValidationMessages(error);
            }
            return msg;
        }

        function getValidationMessages(error) {
            try {
                //foreach entity with a validation error
                return error.entitiesWithErrors.map(function (entity) {
                    // get each validation error
                    return entity.entityAspect.getValidationErrors().map(function (valError) {
                        // return the error message from the validation
                        return valError.errorMessage;
                    }).join('; <br/>');
                }).join('; <br/>');
            }
            catch (e) { }
            return 'validation error';
        }
    });
//#endregion