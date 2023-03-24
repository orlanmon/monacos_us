(function () {

    'use strict'

    AngularApplication.component("menuSidebarComponent",
        {

            templateUrl: function (rootUrl) { return rootUrl + "/angular_app/components/views/menu_sidebar_component.html" },
            bindings: {},
            controller: ["$scope", "$log", "security_service", "navigation_service", menu_sidebar_component_ctrl],
            controllerAs: "ctr"
        }
    );


    function menu_sidebar_component_ctrl($scope, $log, security_service, navigation_service) {



        function RenderSideBarMenu()
        {
            var KendoMenuJSON = null;
            var Result = null;


            try {


                Result = navigation_service.BuildSideBarMenu();


                if (Result.status == "success") {


                    KendoMenuJSON = Result.data;


                    $("#sidebar_treeview").kendoTreeView({
                        dataSource: KendoMenuJSON,
                        dataBound: function (e) {



                            /*
                            
                            var URL = $(e.node).find('a:first').attr('href');
                            var anchor_tag = $(e.node).find('a:first');
    
                            if (URL.indexOf("#") != -1) {
    
                                anchor_tag[0].target = "_parent";
                            }
                            else {
    
                                anchor_tag[0].target = "_blank";
    
                            }
                        */


                        },


                        select: function (e) {



                            // For Now Everything is Opened In New Window
                            // Revist when local content is added

                            var anchor_tag = $(e.node).find('a:first');

                            anchor_tag[0].target = "_blank";



                            /*
                            var URL = $(e.node).find('a:first').attr('href');
                            var anchor_tag = $(e.node).find('a:first');
    
                            if (URL.indexOf("") != -1) {
    
                                anchor_tag[0].target = "_parent";
                            }
                            else {
    
                                anchor_tag[0].target = "_blank";
    
                            }
                            */


                        }

                    });
                }
                else
                {
                    utility_service.ExceptionPopUpDialog("RenderSideBarMenu", "An error occured: " + Result.description, 5);

                }

            }
            catch (e) {

                throw {
                    source: "RenderSideBarMenu",
                    message: "Error: " + e
                }

            }

        }
          
        this.$onInit = function()
        {

         
            RenderSideBarMenu(false);

        }
            
    }


})();
