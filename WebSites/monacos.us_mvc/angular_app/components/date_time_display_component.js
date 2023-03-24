(function () {

    'use strict'

    AngularApplication.component("dateTimeDisplayComponent",
        {
            templateUrl: function (rootUrl) { return rootUrl + '/angular_app/components/views/date_time_display_component.html' },
            bindings: {},
            controller: ["$scope", "$log", "timer_service", date_time_display_component_ctrl],
            controllerAs: "ctr"
        }
    );


    
    function date_time_display_component_ctrl($scope, $log, timer_service) {


            $scope.Date_Display_Value = "";
            $scope.Time_Display_Value = "";

            $scope.timer_time_obj = null;
            $scope.timer_date_obj = null;




            var dayarray = new Array("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");
            var montharray = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");

         

            this.gettheTime = function () {

                var mydate = new Date();
                var hours = mydate.getHours();
                var minutes = mydate.getMinutes();
                var seconds = mydate.getSeconds();
                var display_time = "";
                var dn = "AM";

                if (hours >= 12) {
                    dn = "PM";
                }
                if (hours > 12) {
                    hours = hours - 12;
                }
                if (hours == 0) {
                    hours = 12;
                }
                if (minutes <= 9) {

                    minutes = "0" + minutes;
                }
                if (seconds <= 9) {
                    seconds = "0" + seconds;
                }
                

                display_time = hours + ":" + minutes + ":" + seconds + " " + dn;

                $scope.Time_Display_Value = display_time;

               
                //$log.debug("1");
               
                
            }

            this.gettheDate = function () {

                var mydate = new Date();
                var year = mydate.getYear();
                var day = mydate.getDay();
                var month = mydate.getMonth();
                var daym = mydate.getDate();
                var display_date = "";



                if (year < 1000) {
                    year += 1900;
                }
                if (daym < 10) {
                    daym = "0" + daym;
                }

                display_date = dayarray[day] + ", " + montharray[month] + " " + daym + ", " + year;

                $scope.Date_Display_Value = display_date;

                //$log.debug("2");

            }

           
            this.$onInit = function()
            {

                this.gettheDate();


                $scope.timer_time_obj = timer_service(this.gettheTime, 1000);

                $scope.timer_time_obj.start();






                $scope.timer_date_obj = timer_service(this.gettheDate, 60000);

                $scope.timer_date_obj.start();


            }
            
    }


})();
