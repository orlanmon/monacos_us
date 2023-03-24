(function () {

    'use strict'


    AngularApplication.service("utility_service", ["$log", "$http", "rootUrl", utility_service]);

    function utility_service($log, $http, rootUrl) {


        var KendoWindowRef = null;


        function GetBaseURL() {
                     
            // Note rootURL is a application constant that is depedency injected into this service
            return rootUrl;

        }

        function GetParam(name) {
            if (name = (new RegExp('[?&]' + encodeURIComponent(name) + '=([^&]*)')).exec(location.search))
                return decodeURIComponent(name[1]);
        }

        function NotificationDialog(Notification_dlg_title, Notification_msg) {

            var kendoWindow = $("<div />").kendoWindow({
                title: Notification_dlg_title,
                resizable: false,
                modal: true
            });

            kendoWindow.data("kendoWindow")
                .content($("#notification_dialog").html())
                .center().open();

            KendoWindowRef = kendoWindow;

            var objconfirmation_msg = kendoWindow.find("#notification_msg")

            objconfirmation_msg[0].innerHTML = Notification_msg;

            kendoWindow
                .find(".ok-button")
                    .click(function () {


                        kendoWindow.data("kendoWindow").close();


                    })
                    .end()

        }


        function NotificationPopupDialog(Notification_dlg_title, Notification_msg, opentime) {

            var kendoWindow = $("<div />").kendoWindow({
                title: Notification_dlg_title,
                resizable: false,
                modal: true
            });


            kendoWindow.data("kendoWindow")
                .content($("#notification_popup_dialog").html())
                .center().open();

            KendoWindowRef = kendoWindow;


            var objconfirmation_msg = kendoWindow.find("#notification_msg")

            objconfirmation_msg[0].innerHTML = Notification_msg;

            setTimeout(function () {

                KendoWindowRef.data("kendoWindow").close();

            }, opentime * 1000);

        }


        function NotificationPopupDialogWithHandler(Notification_dlg_title, Notification_msg, opentime, handler ) {

            var kendoWindow = $("<div />").kendoWindow({
                title: Notification_dlg_title,
                resizable: false,
                modal: true
            });


            kendoWindow.data("kendoWindow")
                .content($("#notification_popup_dialog").html())
                .center().open();

            KendoWindowRef = kendoWindow;


            var objconfirmation_msg = kendoWindow.find("#notification_msg")

            objconfirmation_msg[0].innerHTML = Notification_msg;

            setTimeout(function () {

                KendoWindowRef.data("kendoWindow").close();

            }, opentime * 1000);


            setTimeout(  handler, (opentime+2) * 1000);

        }



        function ExceptionPopUpDialog(exception_src, exception_msg, opentime) {

            var confirmation_dlg_title = "Site Exception Occurred";

            var kendoWindow = $("<div />").kendoWindow({
                title: confirmation_dlg_title,
                resizable: false,
                modal: true
            });

            kendoWindow.data("kendoWindow")
               .content($("#exception_dialog").html())
               .center().open();


            var objexception_msg = kendoWindow.find("#exception_msg")

            objexception_msg[0].innerHTML = exception_msg;

            var objexception_src = kendoWindow.find("#exception_src")

            objexception_src[0].innerHTML = exception_src;


            KendoWindowRef = kendoWindow;


            setTimeout(function () {

                KendoWindowRef.data("kendoWindow").close();

            }, opentime * 1000);


        }


        function DelayRun(FuncPtr, DelayTime) {

            setTimeout(FuncPtr, DelayTime * 1000);

        }


     
        function GetSessionID() {


            var RestServiceEndpoint = site.GetBaseURL() + "/SecurityService/GetSessionID";
            var SessionID = null;

            try {

                $.ajax({
                    dataType: "json",
                    async: false,
                    url: RestServiceEndpoint, error: function (xhr) {

                        this.ExceptionPopUpDialog("GetSessionID ", "An error occured: " + xhr.status + " " + xhr.statusText, 5);
                    },
                    success: function (result) {

                        SessionID = result
                        //alert("SessionID: " + $scope.SessionID );

                        return SessionID;
                    }


                });
            }
            catch (e) {

                throw {
                    source: "GetSessionID",
                    message: "Error: " + e
                }

            }
        }

        function NavigateHome() {

            PageRedirect(GetBaseURL() + "/Home");

        }

        function PageRedirect(RedirectPage) {

            window.location.replace(RedirectPage);

        }

        function PopUpWindow(url, title, width, height) {
            var leftPosition, topPosition;
            //Allow for borders.
            leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
            //Allow for title and status bars.
            topPosition = (window.screen.height / 2) - ((height / 2) + 50);
            //Open the window.
            window.open(url, title,
            "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
            + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
            + topPosition + ",toolbar=no,menubar=no,scrollbars=yes,location=no,directories=no");
        }



        return{

            GetBaseURL: GetBaseURL,
            NotificationDialog: NotificationDialog,
            ExceptionPopUpDialog: ExceptionPopUpDialog,
            NotificationPopupDialog: NotificationPopupDialog,
            NotificationPopupDialogWithHandler: NotificationPopupDialogWithHandler,
            GetParam: GetParam,
            DelayRun: DelayRun,
            GetSessionID: GetSessionID,
            NavigateHome: NavigateHome,
            PopUpWindow: PopUpWindow,
            PageRedirect: PageRedirect


        }


    }

   





})();