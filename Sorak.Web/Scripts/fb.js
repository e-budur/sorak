﻿define(['facebook'], function(){
    alert('test1');
var userInfo;
alert('test');
window.fbAsyncInit = function () {
    FB.init({
        appId: '1418633691748178', //change the appId to your appId
        status: true,
        cookie: true,
        xfbml: true,
        oauth: true
    });

    showLoader(true);

    function updateStatus(response) {
        userInfo = document.getElementById('user-info');

        if (response.authResponse) {
            //user is already logged in and connected
            FB.api('/me', function (info) {
                login(response, info);
            });

        } else {
            logout(response);
        }
    }

    // run once with current status and whenever the status changes
    FB.getLoginStatus(updateStatus);
    FB.Event.subscribe('auth.statusChange', updateStatus);
};
(function () {
    var e = document.createElement('script'); e.async = true;
    e.src = document.location.protocol
        + '//connect.facebook.net/en_US/all.js';
    document.getElementById('fb-root').appendChild(e);
}());


function login(response, info) {
    if (response.authResponse) {
        var accessToken = response.authResponse.accessToken;

        userInfo.innerHTML = '<img src="https://graph.facebook.com/' + info.id + '/picture">' + info.name
                                                         + "<br />"

        showLoader(false);
    }
}

function logout(response) {
    userInfo.innerHTML = "";
    document.getElementById('debug').innerHTML = "";
    showLoader(false);
}

//stream publish method
function streamPublish(name, description, hrefTitle, hrefLink, userPrompt) {
    showLoader(true);
    FB.ui(
    {
        method: 'stream.publish',
        message: '',
        attachment: {
            name: name,
            caption: '',
            description: (description),
            href: hrefLink
        },
        action_links: [
            { text: hrefTitle, href: hrefLink }
        ],
        user_prompt_message: userPrompt
    },
    function (response) {
        showLoader(false);
    });

}
function showStream() {
    FB.api('/me', function (response) {
        //console.log(response.id);
        streamPublish(response.name, 'I like the articles of Thinkdiff.net', 'hrefTitle', 'http://thinkdiff.net', "Share thinkdiff.net");
    });
}

function share() {
    showLoader(true);
    var share = {
        method: 'stream.share',
        u: 'http://thinkdiff.net/'
    };

    FB.ui(share, function (response) {
        showLoader(false);
        console.log(response);
    });
}

function graphStreamPublish() {
    showLoader(true);

    FB.api('/me/feed', 'post',
        {
            message: "I love thinkdiff.net for facebook app development tutorials",
            link: 'http://ithinkdiff.net',
            picture: 'http://thinkdiff.net/iphone/lucky7_ios.jpg',
            name: 'iOS Apps & Games',
            description: 'Checkout iOS apps and games from iThinkdiff.net. I found some of them are just awesome!'

        },
    function (response) {
        showLoader(false);

        if (!response || response.error) {
            alert('Error occured');
        } else {
            alert('Post ID: ' + response.id);
        }
    });
}

function fqlQuery() {
    showLoader(true);

    FB.api('/me', function (response) {
        showLoader(false);

        //http://developers.facebook.com/docs/reference/fql/user/
        var query = FB.Data.query('select name, profile_url, sex, pic_small from user where uid={0}', response.id);
        query.wait(function (rows) {
            document.getElementById('debug').innerHTML =
              'FQL Information: ' + "<br />" +
              'Your name: ' + rows[0].name + "<br />" +
              'Your Sex: ' + (rows[0].sex != undefined ? rows[0].sex : "") + "<br />" +
              'Your Profile: ' + "<a href='" + rows[0].profile_url + "'>" + rows[0].profile_url + "</a>" + "<br />" +
              '<img src="' + rows[0].pic_small + '" alt="" />' + "<br />";
        });
    });
}

function setStatus() {
    showLoader(true);

    status1 = document.getElementById('status').value;
    FB.api(
      {
          method: 'status.set',
          status: status1
      },
      function (response) {
          if (response == 0) {
              alert('Your facebook status not updated. Give Status Update Permission.');
          }
          else {
              alert('Your facebook status updated');
          }
          showLoader(false);
      }
    );
}

function showLoader(status) {
    if (status)
        document.getElementById('loader').style.display = 'block';
    else
        document.getElementById('loader').style.display = 'none';
}


});