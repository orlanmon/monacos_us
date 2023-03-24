(function () {

    'use strict'

    AngularApplication.component("loginStatusComponent",
        {

            templateUrl: function (rootUrl) { return rootUrl + "/angular_app/components/views/login_status_component.html" },
            bindings: {},
            controller: ["$scope", "$log", "utility_service", "security_service", "navigation_service", login_status_component_ctrl],
            controllerAs: "ctr"
        } 

    );


    function login_status_component_ctrl($scope, $log, utility_service, security_service, navigation_service) {


        function DisplayLoginStatus(bolLoggedInStatus) {


            if (bolLoggedInStatus) {

                $("#a_signin").hide();
                $("#a_signout").show();

            }
            else {

                $("#a_signin").show();
                $("#a_signout").hide();

            }


        }

         
        function LogStatusDisplay() {

            var bolLoggedInStatus = false;
            var Result = null;


            try {


                Result  = security_service.GetLoggedInStatus();

                if (Result.status == "success")
                {

                    bolLoggedInStatus = Result.data;

                    DisplayLoginStatus(bolLoggedInStatus);

                }
                else
                {

                    utility_service.ExceptionPopUpDialog("LogStatusDisplay", "An error occured: " + Request.description, 5);


                }

               
            }
            catch (e) {

                $exceptionHandler(ex.message, "LogStatusDisplay")
                utility_service.ExceptionPopUpDialog("LogStatusDisplay : ", "An error occured: " + ex.message, 5);


            }

        }


       


       
            // Event Handler
            this.LoginEvent = function () {


                DisplayLoginStatus(true);


            }

            // Event Handler
            this.LogoutEvent = function () {


               DisplayLoginStatus(false);



            }
         

            $scope.LogoutUser = function () {
                
                var Result = null;
                var LoggedOut = false;


                try
                {

                    Result = security_service.LogoutUser();

                    if (Result.status == "success") {

                        LoggedOut = Request.data;

                        if (LoggedOut == true) {

                            utility_service.NotificationPopupDialog("Logout", "You are now Logged out.", 3);

                        }
                        else
                        {
                            utility_service.NotificationPopupDialog("Logout", "Logout Unsuccessful", 3);

                        }


                    }
                    else
                    {
                        utility_service.ExceptionPopUpDialog("LogoutUser", "An error occured: " + Result.description, 5);

                    }

                }
                catch(ex)
                {
                    $exceptionHandler(ex.message, "LogoutUser")
                    utility_service.ExceptionPopUpDialog("LogoutUser : ", "An error occured: " + ex.message, 5);
                }

            };


            $scope.LoginURL = function () {
                

                return utility_service.GetBaseURL() + "/LoginPage";


            };

            
            this.$onInit = function()
            {

             
                security_service.Subscribe_logout_event($scope, this.LogoutEvent);


                //security_service.Subscribe_login_event($scope, this.LoginEvent);




                try {

                    LogStatusDisplay()

                }
                catch (e) {

                    throw {
                        source: "LogStatusDisplay",
                        message: "Error: " + e
                    }

                }
            }
            
    }


})();
