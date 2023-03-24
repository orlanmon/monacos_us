(function () {

    'use strict'

    AngularApplication.controller("homeautomation_page_controller", ["$scope", "$log", "utility_service", "security_service", "content_service", "$exceptionHandler", homeautomation_page_controller])


    function homeautomation_page_controller($scope, $log, utility_service, security_service, content_service, $exceptionHandler) {


        $scope.Initialize = function () {


            try
            {

                var LoggedInStatus = false;
                var Result = null;

                Result = security_service.GetLoggedInStatusRole("Administration Access")

                if (Result.status == "success") {


                     LoggedInStatus = Result.data;

                   


                     if (LoggedInStatus == false) {


                         utility_service.NotificationPopupDialog("Invalid Access", "Invalid Access Please Login", 3);

                         utility_service.DelayRun(utility_service.NavigateHome, 3);


                     }
                     else {

                        

                     }
                 }
                 else
                 {

                     utility_service.ExceptionPopUpDialog("Initialize", "An error occured: " + Result.description, 5);


                 }
            }
            catch (ex) {

                $exceptionHandler(ex.message, "Initialize")
                utility_service.ExceptionPopUpDialog("Initialize : ", "An error occured: " + ex.message, 5);
            }
        

        }

      
     
        $scope.Initialize();



    }

})();
