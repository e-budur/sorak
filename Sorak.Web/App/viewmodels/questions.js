define(['services/logger',
 'services/datacontext',
 'plugins/dialog'], function (logger, datacontext, dialog) {
     var title = 'Details';

     var imgLikeSelected = ko.observable(false);
     var imgLike = ko.observable(true);
     var like = ko.observable("0");
     var questions = ko.observableArray();
     var result = ko.observable();
     var count = ko.observable(160);

     var id;

     var vm = {
         activate: activate,
         title: title,
         like: like,
         questions: questions,
         insertQuestion: insertQuestion,
         count: count,
         afterkeydown: afterkeydown,
         unLike: unLike,
         result: result,
         insertQuestion: insertQuestion,
         qlike: qlike
     };

     return vm;

     //#region Internal Methods
     function activate(routeData) {
         id = parseInt(routeData);
         //  logger.log(title + ' View Activated', null, title, true);
         return datacontext.getQuestions(id, questions).then(function () {
             var a = 1;
         });
     }

     function afterkeydown() {
         if ($("#question").val().length >= 160) {
             $("#question").val($("#question").val().substr(0, 160));

         }
         count(160 - $("#question").val().length);
     }

     function qlike(e) {
         datacontext.like(e.id, result).then(function () {
             datacontext.getQuestions(id, questions);
             logger.log('Oyunuz alınmıştır.', null, title, true);
         });
     }

     function unLike(e) {

         datacontext.unLike(e.id, result).then(function () {
             datacontext.getQuestions(id, questions).then(function () {
                 var a = 1;
                 logger.log('Oyunuz iptal edilmiştir.', null, title, true);
             });
         });
     }

     function insertQuestion() {
         if ($.trim($("#question").val()).length > 5) {
             datacontext.ask(id, $("#question").val()).then(function () {
                 datacontext.getQuestions(id, questions);
                 $("#question").val("");
                 logger.log('Sorunuz kaydedilmiştir.', null, title, true);
             });
         }
         else {
             alert("Lütfen en az 5 karakterden oluşan bir soru giriniz.");
         }
     }


     //#endregion
 });