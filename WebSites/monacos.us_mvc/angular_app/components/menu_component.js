(function () {

    'use strict'

    AngularApplication.component("menuComponent",
        {

            templateUrl: function (rootUrl) { return rootUrl + "/angular_app/components/views/menu_component.html" },
            bindings: {},
            controller: ["$scope", "$log", "security_service", "navigation_service", "utility_service", menu_component_ctrl],
            controllerAs: "ctr"
        }
    );


    function menu_component_ctrl($scope, $log, security_service, navigation_service, utility_service) {


        this.LoginEvent = function () {

            var Result = null;


            // Login Occured Rebuild Menu On 
            // On Page Render after Login Redirect
            // Render Menu with no Refresh

            Result = navigation_service.BuildMenu(true);

            if ( Result.status != "success")
            {
                utility_service.ExceptionPopUpDialog("LoginEvent", "An error occured: " + Result.description, 5);
            }

        }

        this.LogoutEvent = function () {


            RenderMenu(true);


        }

        function RenderMenu(boolRefresh)
        {

            var Result = null;

            var KendoMenuJSON = null;

            Result = navigation_service.BuildMenu(boolRefresh);

            if (Result.status == "success") {

                KendoMenuJSON = Result.data;

                $("#header_menu").kendoMenu({
                    dataSource: KendoMenuJSON,
                    select: function (e) {

                        var URL = $(e.item).find('a:first').attr('href');
                        var anchor_tag = $(e.item).find('a:first');

                        if (URL.indexOf("target=_blank") != -1) {

                            anchor_tag[0].target = "_blank";
                        }
                        else {

                            anchor_tag[0].target = "_parent";

                        }

                    }

                });
            }
            else
            {
                
                utility_service.ExceptionPopUpDialog("RenderMenu", "An error occured: " + Result.description, 5);
            
            }


        }
          
        this.$onInit = function()
        {

          

            security_service.Subscribe_logout_event($scope, this.LogoutEvent);


            security_service.Subscribe_login_event($scope, this.LoginEvent);


            // Always Render From Current/Last Menu JSON Generated.

            RenderMenu(false);




        }
            
    }


})();
