(function () {

    'use strict'

    AngularApplication.controller("homecontent_page_controller", ["$scope", "$log", "utility_service", "security_service", "content_service", "$exceptionHandler", homecontent_page_controller])


    function homecontent_page_controller($scope, $log, utility_service, security_service, content_service, $exceptionHandler) {


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

                         $scope.LoadContentItemGrid();

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

        $scope.NavigateContentAdd = function () {

            utility_service.PageRedirect(utility_service.GetBaseURL() + "/ContentEditor?caid=1&m=a");

        }

      
        $scope.LoadContentItemGrid = function () {


            // Do Not Include Only Currently Published = Everything Except InActive

            var ContentServiceEndpoint = utility_service.GetBaseURL() + "/ContentService/Content/GetActive?caid=1&icp=false";
            var ContentItemsJSON = null;
            var Result = null;



            try {

                // Initialize Kendo Button Styles
                $("#button_add_contentarea").kendoButton();

                Result = content_service.GetContentItems(1);


                if (Result.status = "success") {


                    ContentItemsJSON = Result.data;

                    //$scope.DataSource = [{  "Content_ID" : "1", ContentArea_ID : "1", Description : "TEST" }];

                    $("#contentarea_grid").kendoGrid({
                        dataSource: ContentItemsJSON,
                        navigatable: true,
                        pageable: true,
                        sortable: true,
                        height: 430,
                        columns: [
                            { field: "Content_ID", title: "ID", width: 70 },
                            { field: "Description", title: "Description", width: 100 },
                            { field: "Create_Date", title: "Create Date", width: 120 },
                            { field: "Publish_Date", title: "Publish Date", width: 120 },
                            { field: "Expiration_Date", title: "Expiration Date", width: 150 },
                            { field: "Edit", title: "Edit", width: 50, template: '<a href="' + utility_service.GetBaseURL() + '/ContentEditor?caid=1&m=u&cid=#=Content_ID#" title="Click Here to Edit">Edit</a>' }],

                        editable: false
                    });

                }
                else
                {
                    // fail
                    utility_service.ExceptionPopUpDialog("LoadContentItemGrid", "An error occured: " + Result.description, 5);

                }

            }
            catch (ex) {

                $exceptionHandler(ex.message, "LoadContentItemGrid")
                utility_service.ExceptionPopUpDialog("LoadContentItemGrid : ", "An error occured: " + ex.message, 5);

            }

        }


        $scope.Initialize();



    }

})();
