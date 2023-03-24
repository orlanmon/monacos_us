(function () {

    'use strict'

    AngularApplication.controller("site_controller", ["$scope", "$log", site_controller])


    function site_controller($scope, $log) {

        

        $scope.MyAppName = "monacos.us.mvc";
        $scope.Footer_Date_Value = "";
        $scope.Footer_Time_Value = "";


        //$scope.timer_service_obj = timer_service;
        $scope.timer_one = null;
        $scope.timer_two = null;

        // timer service testfunction
        this.Timer_One_Handler = function ()
        {
            $log.debug("One");

        }

        // timer service test function
        this.Timer_Two_Handler = function () {

            $log.debug("Two");


        }

        this.Initialize = function ()
        {

            // timer service test code
            /*
            $scope.timer_one = timer_service(this.Timer_One_Handler, 1000);

            $scope.timer_one.restart();


            $scope.timer_two = timer_service(this.Timer_Two_Handler, 60000)
            
            $scope.timer_two.restart();
            */



        }

        this.Initialize();



    }



  


    site_controller.$inject = ["$scope", "date_time_display_module"];


})();
