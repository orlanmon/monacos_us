(function () {

    'use strict'

    AngularApplication.controller("login_page_controller", ["$scope", "$log", "utility_service",  "security_service", login_page_controller])


    function login_page_controller($scope, $log, utility_service, security_service) {


        $scope.Initialize = function () {

            // Initialize Kendo Button Styles
            //$("#button_login_submit").kendoButton();

        }


        $scope.LoginUser = function () {


            var LoginResponse = false;
            var requestUrl = "";
            var PostProxyURL = "";
            var reCaptchaResponseArray = false;
            var Result = null;


            
            try {

                /*
                // Call Recaptcha

                
                LoginResponse = true;   // Recaptcha


                $scope.recaptcha_challenge_field = Recaptcha.get_challenge();

                $scope.recaptcha_response_field = Recaptcha.get_response();

                $scope.remoteIp = $("#remote_ip").val();

                $scope.privateKey = "6Lcarf8SAAAAAEXGEqSWRo1zN1pURA6A3d1Ayevm";

                requestUrl = "http://www.google.com/recaptcha/api/verify?privatekey=" + $scope.privateKey + "&remoteip=" + $scope.remoteIp + "&challenge=" + $scope.recaptcha_challenge_field + "&response=" + $scope.recaptcha_response_field;

                PostProxyURL = utility_service.GetBaseURL() + "/PostProxy.aspx?url=" + encodeURIComponent(requestUrl);

                $.ajax({
                    type: "POST",
                    url: PostProxyURL,
                    dataType: "json",
                    crossDomain: true,
                    success: function (data) {


                    },
                    error: function (xhr, textStatus, errorThrown) {

                       
                        if (xhr.status == 200) {

                            reCaptchaResponseArray = xhr.responseText.split("\n");

                            if (reCaptchaResponseArray[0] == "true") {


                                // Recaptcha Worked Now Try Login Credentials 

                                 Result = security_service.LoginUser($scope.UserName, $scope.Password);


                                if (Result.status == "success") {

                                    LoginResponse = Result.data;

                                    if (LoginResponse) {


                                       


                                        utility_service.NotificationPopupDialogWithHandler("Login", "Login Successful", 5, utility_service.NavigateHome());


                                        $scope.UserName = null;
                                        $scope.Password = null;

                                       

                                    }
                                    else {

                                        utility_service.NotificationPopupDialog("Login", "Login Unsuccessful", 3);

                                        Recaptcha.reload();

                                    }
                                }
                                 else
                                {
                                    utility_service.ExceptionPopUpDialog("LoginUser", "An error occured: " + Result.Description, 5);

                                }
   
                            }
                            else {

                                utility_service.NotificationPopupDialog("Login", "Login Unsuccessful", 3);

                                Recaptcha.reload();


                            }
                        }
                        else {

                            utility_service.ExceptionPopUpDialog("LoginUser", "An error occured: " + xhr.status + " " + xhr.statusText, 5);

                           
                        }
                    }
                });
                */


                // Recaptcha Worked Now Try Login Credentials 

                Result = security_service.LoginUser($scope.UserName, $scope.Password);


                if (Result.status == "success") {

                    LoginResponse = Result.data;

                    if (LoginResponse) {


                        utility_service.NotificationPopupDialogWithHandler("Login", "Login Successful", 5, utility_service.NavigateHome());


                        $scope.UserName = null;
                        $scope.Password = null;



                    }
                    else {

                        utility_service.NotificationPopupDialog("Login", "Login Unsuccessful", 3);

                        Recaptcha.reload();

                    }
                }
                else {
                    utility_service.ExceptionPopUpDialog("LoginUser", "An error occured: " + Result.Description, 5);

                }




            }
            catch (e) {

                $exceptionHandler(ex.message, "LoginUser")
                utility_service.ExceptionPopUpDialog("LoginUser : ", "An error occured: " + ex.message, 5);
            }
           







            return true;

        }


        $scope.Initialize();

       
    }

})();
