(function () {

    'use strict'

    AngularApplication.service("security_service", ["$rootScope", "$log", "$http", "utility_service", security_service]);

    function security_service($rootScope, $log, $http, utility_service) {

        // Events For Page or Component Controllers To Subscribe To

        function Subscribe_logout_event(scope, callback) {
            var handler = $rootScope.$on('security-service-event-logout', callback);
            scope.$on('$destroy', handler);
        }

        function Subscribe_login_event (scope, callback) {
            var handler = $rootScope.$on('security-service-event-login', callback);
            scope.$on('$destroy', handler);
        }


        function LoginUser(strUserName, strPassword) {

            var RestServiceEndpoint = utility_service.GetBaseURL() + "/SecurityService/Login?un=" + strUserName + "&pw=" + strPassword;
            var Result = null;
            var LoginResponse = null;

            try {

                $.ajax({
                    dataType: "json",
                    url: RestServiceEndpoint,
                    async: false,
                    error: function (xhr) {

                        Result = { status: "fail", description: "An error occured: " + xhr.status + " " + xhr.statusText, data: null };

                    },
                    success: function (result) {

                        LoginResponse = eval(result);

                        if (LoginResponse == true) {

                            
                            // Authentication May Have Changed Rebuild Menu
                            // Will Cause Menu Component To Rebuild It's Menu Only
                            // But Not Displat Until Home Page Redirect after Login
                            $rootScope.$emit('security-service-event-login');

                            Result = { status: "success", description: "success", data: true };


                        }
                        else {

                            Result = { status: "success", description: "success", data: false };

                        }
                    }


                });
            }
            catch (e) {

                throw {
                    source: "LoginUser",
                    message: "Error: " + e
                }

            }

            return Result;


        }



        function LogoutUser () {


            var RestServiceEndpoint = utility_service.GetBaseURL() + "/SecurityService/Logout";
            var Result = null;


            var LogoutResponse = null;

            try {
                $.ajax({
                    dataType: "json",
                    url: RestServiceEndpoint,
                    async: false,
                    error: function (xhr) {


                        Result = { status: "fail", description: "An error occured: " + xhr.status + " " + xhr.statusText, data: null };
                        
                    },
                    success: function (result) {

                        LogoutResponse = eval(result);

                        if (LogoutResponse == true) {


                            Result = { status: "success", description: "success", data: true };

                         

                            // Authentication May Have Changed Rebuild Menu
                            // navigation_service.BuildMenu(true);
                            utility_service.DelayRun(utility_service.NavigateHome, 3);


                            // Service Event
                            $rootScope.$emit('security-service-event-logout');


                        }
                        else {


                            Result = { status: "success", description: "success", data: false };

                           
                            // Authentication Not Changed Get Latest Menu
                            // navigation_service.BuildMenu(false);



                        }
                    }


                });
            }
            catch (e) {

                throw {
                    source: "LogoutUser",
                    message: "Error: " + e
                }

            }

            return Result;

        }

        function GetLoggedInStatus() {


            try {
                var RestServiceEndpoint = utility_service.GetBaseURL() + "/SecurityService/GetLogInStatus";
                var bolLoggedInStatus = false;
                var Result = null;


                $.ajax({
                    dataType: "json",
                    url: RestServiceEndpoint,
                    async: false,
                    error: function (xhr) {

                        Result = { status: "fail", description: "An error occured: " + xhr.status + " " + xhr.statusText, data: false };

                    },
                    success: function (result) {

                        bolLoggedInStatus = result;

                        Result = { status: "success", description: "success", data: bolLoggedInStatus };

                    }

                });
            }
            catch (e) {

                throw {
                    source: "GetLoggedInStatus",
                    message: "Error: " + e
                }

            }
            return Result;

        }


        function GetLoggedInStatusRole(RoleName) {


            try {
                // GetLogInStatusRole?rn={RoleName}
                var RestServiceEndpoint = utility_service.GetBaseURL() + "/SecurityService/GetLogInStatusRole?rn=" + RoleName;
                var bolLoggedInStatus = false;
                var Result = null;


                $.ajax({
                    dataType: "json",
                    url: RestServiceEndpoint,
                    async: false,
                    error: function (xhr) {

                        Result = { status: "fail", description: "An error occured: " + xhr.status + " " + xhr.statusText, data: false };

                    },
                    success: function (result) {

                        bolLoggedInStatus = result;


                        Result = { status: "success", description: "success", data: bolLoggedInStatus };



                    }

                });
            }
            catch (e) {

                throw {
                    source: "GetLoggedInStatusRole",
                    message: "Error: " + e
                }

            }
            return Result;

        }



       

        return {

            LogoutUser: LogoutUser,
            LoginUser: LoginUser,
            GetLoggedInStatusRole: GetLoggedInStatusRole,
            GetLoggedInStatus: GetLoggedInStatus,
            Subscribe_login_event : Subscribe_login_event,
            Subscribe_logout_event : Subscribe_logout_event


        }

    }

   





})();