(function () {

    'use strict'

    AngularApplication.controller("contenteditor_page_controller", ["$scope", "$log", "utility_service", "security_service", "content_service", contenteditor_page_controller])

    function contenteditor_page_controller($scope, $log, utility_service, security_service, content_service) {

      
        $scope.content_type_description = null;
        $scope.ContentEdit_Mode = null;
        $scope.ContentArea_ID = null;
        $scope.Content_ID = null;

        $scope.Content_Description = null;
        $scope.Content_PublishDate = null;
        $scope.Content_ExpirationDate = null;
        $scope.Content_Area = null;
        $scope.Content_ActiveStatus = true;

        $scope.Initialize = function () {


            var LoggedInStatus = false;
            var Result = null;


            try {
                Result = security_service.GetLoggedInStatusRole("Administration Access");


                if (Result.status == "success")
                {

                    LoggedInStatus = Result.data;

                    if (LoggedInStatus == false) {

                        utility_service.NotificationPopupDialog("Invalid Access", "Invalid Access Please Login", 3);

                        utility_service.DelayRun(utility_service.NavigateHome, 3);

                    }
                    else {

                        $scope.BuildContentEditor();

                        // caid=2&m=u&cid=

                        $scope.ContentEdit_Mode = utility_service.GetParam("m");
                        $scope.ContentArea_ID = utility_service.GetParam("caid");
                        $scope.Content_ID = utility_service.GetParam("cid");


                        //$("#input_content_expirationdate").kendoDatePicker();
                        //$("#input_content_publishdate").kendoDatePicker();

                        $("#input_content_expirationdate").kendoDatePicker();



                        $("#input_content_publishdate").kendoDatePicker();



                        if ($scope.ContentEdit_Mode == "a") {

                            if ($scope.ContentArea_ID == "1") {
                                $scope.content_type_description = "Add Home Page Content";
                            }
                            else {
                                $scope.content_type_description = "Add News Page Content";
                            }

                            // Initialize Kendo Button Styles
                            $("#button_content_add_submit").kendoButton();

                            $("#button_content_update_submit").hide();

                            $("#button_content_delete_submit").hide();


                        }
                        else {

                            if ($scope.ContentArea_ID == "1") {
                                $scope.content_type_description = "Update Home Page Content";
                            }
                            else {
                                $scope.content_type_description = "Update News Page Content";
                            }


                            // Populate Editor Form
                            $scope.SelectContent();


                            // Initialize Kendo Button Styles
                            $("#button_content_update_submit").kendoButton();

                            $("#button_content_delete_submit").kendoButton();


                            $("#button_content_add_submit").hide();



                        }
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

        $scope.SelectContent = function () {

            var DataContentItem = null;
            var Result = null;

        
            try {
               

                Result   = content_service.GetContentItem($scope.Content_ID)

                if (Result.status == "success") {


                    DataContentItem = Result.data;


                    // Populate Angular Model

                    $scope.Content_Description = DataContentItem.Description;


                    var NewDate = null;

                    NewDate = kendo.toString(DataContentItem.Publish_Date, 'MM/dd/yyyy');
                    $("#input_content_publishdate").data('kendoDatePicker').value(NewDate);

                    $scope.Content_PublishDate = DataContentItem.Publish_Date;


                    NewDate = kendo.toString(DataContentItem.Expiration_Date, 'MM/dd/yyyy');
                    $("#input_content_expirationdate").data('kendoDatePicker').value(NewDate);

                    $scope.Content_ExpirationDate = DataContentItem.Expiration_Date;

                    var editor = $("#content_editor").data("kendoEditor");

                    editor.value(DataContentItem.ContentValue);


                    $scope.Content_Area = DataContentItem.ContentValue;

                }
                else
                {
                    utility_service.ExceptionPopUpDialog("SelectContent: ", "An error occured: " + Result.description, 5);

                }
            }
            catch (ex) {

                $exceptionHandler(ex.message, "SelectContent")
                utility_service.ExceptionPopUpDialog("SelectContent : ", "An error occured: " + ex.message, 5);

            }

        }


        $scope.UpdateContent = function () {

            try {


              


                var ContentItem = null;


                var HTMLContent = null;
                var Publish_Date = null;
                var Expiration_Date = null;
                var Result = null;



                var ContentServiceEndpoint = utility_service.GetBaseURL() + "/ContentService/Content/Update";

                // Build JSON Object For Request


                var editor = $("#content_editor").data("kendoEditor");


                HTMLContent = editor.value();

                // Update Scope
                $scope.Content_Area = HTMLContent;

                Expiration_Date = kendo.toString($("#input_content_expirationdate").data('kendoDatePicker').value(), 'MM/dd/yyyy');

                Publish_Date = kendo.toString($("#input_content_publishdate").data('kendoDatePicker').value(), 'MM/dd/yyyy');

                ContentItem =
                       {
                           "Content_ID": $scope.Content_ID,
                           "ContentArea_ID": $scope.ContentArea_ID,
                           "ContentValue": HTMLContent,
                           "Create_Date": "",
                           "Publish_Date": Publish_Date,
                           "Expiration_Date": Expiration_Date,
                           "Description": $scope.Content_Description,
                           "Active": $scope.Content_ActiveStatus
                       };


                Result = content_service.UpdateContentItem(ContentItem);

                if (Result.status == "success" ) {

                    utility_service.NotificationPopupDialog("Content Update", "Content Update Success.", 3);

                    utility_service.DelayRun($scope.RedirectAfterEdit, 3);
                }
                else
                {
                    utility_service.ExceptionPopUpDialog("UpdateContent", "An error occured: " + Result.description, 5);

                }

            }
            catch (ex) {

                $exceptionHandler(ex.message, "UpdateContent")
                utility_service.ExceptionPopUpDialog("UpdateContent : ", "An error occured: " + ex.message, 5);

            }

        }

        $scope.RedirectAfterEdit = function () {

            if ($scope.ContentArea_ID == "1") {
                utility_service.PageRedirect(utility_service.GetBaseURL() + "/HomeContent");
            }
            else {
                utility_service.PageRedirect(utility_service.GetBaseURL() + + "/NewsContent");
            }
        }


        $scope.AddContent = function () {

            try {

                var ContentItem = null;
                var JSONRequestStringData = null;

                var HTMLContent = null;
                var Publish_Date = null;
                var Expiration_Date = null;
                var Creation_Date = null;
                var Result = null;



                var ContentServiceEndpoint = utility_service.GetBaseURL() + "/ContentService/Content/Add";

                // Build JSON Object For Request

                var editor = $("#content_editor").data("kendoEditor");


                HTMLContent = editor.value();

                // Update Scope
                $scope.Content_Area = HTMLContent;

                Expiration_Date = kendo.toString($("#input_content_expirationdate").data('kendoDatePicker').value(), 'MM/dd/yyyy');

                Publish_Date = kendo.toString($("#input_content_publishdate").data('kendoDatePicker').value(), 'MM/dd/yyyy');

                Creation_Date = kendo.toString(new Date(), 'MM/dd/yyyy');


                ContentItem =
                       {
                           "Content_ID": $scope.Content_ID,
                           "ContentArea_ID": $scope.ContentArea_ID,
                           "ContentValue": HTMLContent,
                           "Create_Date": Creation_Date,
                           "Publish_Date": Publish_Date,
                           "Expiration_Date": Expiration_Date,
                           "Description": $scope.Content_Description,
                           "Active": $scope.Content_ActiveStatus
                       };


                Result = content_service.AddContentItem(ContentItem);

                if (Result.status == "success")
                {
                    utility_service.NotificationPopupDialog("Content Add", "Content Add Success.", 3);

                    utility_service.DelayRun($scope.RedirectAfterEdit, 3);
                }
                else
                {
                    utility_service.ExceptionPopUpDialog("AddContent", "An error occured: " + Result.description, 5);
                }

            }
            catch (ex) {

                $exceptionHandler(ex.message, "AddContent")
                utility_service.ExceptionPopUpDialog("AddContent : ", "An error occured: " + ex.message, 5);

            }

        }


        $scope.DeleteContent = function () {

            try {

                var ContentItem = null;
                var HTMLContent = null;
                var Publish_Date = null;
                var Expiration_Date = null;
                var Result = false;

     


              

                // Build JSON Object For Request


                var editor = $("#content_editor").data("kendoEditor");


                HTMLContent = editor.value();

                // Update Scope
                $scope.Content_Area = HTMLContent;

                Expiration_Date = kendo.toString($("#input_content_expirationdate").data('kendoDatePicker').value(), 'MM/dd/yyyy');

                Publish_Date = kendo.toString($("#input_content_publishdate").data('kendoDatePicker').value(), 'MM/dd/yyyy');

                $scope.Content_ActiveStatus = false;


                ContentItem =
                       {
                           "Content_ID": $scope.Content_ID,
                           "ContentArea_ID": $scope.ContentArea_ID,
                           "ContentValue": HTMLContent,
                           "Create_Date": "",
                           "Publish_Date": Publish_Date,
                           "Expiration_Date": Expiration_Date,
                           "Description": $scope.Content_Description,
                           "Active": $scope.Content_ActiveStatus
                       };

                Result = content_service.DeleteContentItem(ContentItem);

                if (Result.status == "success") {

                    utility_service.NotificationPopupDialog("Content Delete", "Content Delete Success.", 3);

                    utility_service.DelayRun($scope.RedirectAfterEdit, 3);

                }
                else {
                    utility_service.ExceptionPopUpDialog("DeleteContent", "An error occured: " + Result.description, 5);

                }
               

            }
            catch (e) {

                $exceptionHandler(ex.message, "DeleteContent")
                utility_service.ExceptionPopUpDialog("DeleteContent : ", "An error occured: " + ex.message, 5);

            }

        }






        $scope.BuildContentEditor = function () {

            try {


                /*
                $("#content_editor").kendoEditor({
                    resizable: {
                        content: true,
                        toolbar: true
                    }
                });
                */

                
                $("#content_editor").kendoEditor({
                    serialization: {
                        scripts: true
                    },
                    tools: [
                        "bold",
                        "italic",
                        "underline",
                        "strikethrough",
                        "justifyLeft",
                        "justifyCenter",
                        "justifyRight",
                        "justifyFull",
                        "insertUnorderedList",
                        "insertOrderedList",
                        "indent",
                        "outdent",
                        "createLink",
                        "unlink",
                        "insertImage",
                        "subscript",
                        "superscript",
                        "createTable",
                        "addRowAbove",
                        "addRowBelow",
                        "addColumnLeft",
                        "addColumnRight",
                        "deleteRow",
                        "deleteColumn",
                        "viewHtml",
                        "formatting",
                        "fontName",
                        "fontSize",
                        "foreColor",
                        "backColor"
                    ]
                });
                


            }
            catch (e) {

                $exceptionHandler(ex.message, "BuildContentEditor")
                utility_service.ExceptionPopUpDialog("BuildContentEditor : ", "An error occured: " + ex.message, 5);

            }

        }

        $scope.Initialize();

    }

})();
