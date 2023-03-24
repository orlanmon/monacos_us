(function () {

    'use strict'

    AngularApplication.controller("contact_page_controller", ["$scope", "$log", "utility_service", "$sce", "$compile", "$exceptionHandler","contact_service", contact_page_controller])


    function contact_page_controller($scope, $log, utility_service, $sce, $compile, $exceptionHandler, contact_service) {

        $scope.ContactData = { first_name: null, last_name: null, email: null, subject: null, msg: null };

        $scope.ContactRequestSubmitMessage = "";

        $scope.Initialize = function () {

            
        }


        $scope.ContactDialogOne_Open = function () {

            try {

                //ng-bind-html="DialogContent" On interface
                $//scope.DialogContent = $sce.trustAsHtml($("#contact_dlg_one").html());
    
                $scope.ContactData = { first_name: null, last_name: null, email: null, msg: null };


                $("#ContactWorkflowDialog").html($("#contact_dlg_one").html());


                $("#ContactWorkflowDialog").modal('show');

                var dialog = $("#ContactWorkflowDialog");

                $compile(dialog.contents())($scope)
                
            
            }
            catch (ex) {

                $exceptionHandler(ex.message, "ContactDialogOne_Open")

                utility_service.ExceptionPopUpDialog("ContactDialogOne_Open: ", "An error occured: " + ex.message, 5);


            }
            
        }


        $scope.ContactDialogOne_Next = function () {

            try {

               
                 // Dont Hide and this Works Out Fine!!
                //$("#ContactWorkflowDialog").modal('hide');

                $("#ContactWorkflowDialog").html($("#contact_dlg_two").html());

                $("#ContactWorkflowDialog").modal('show');

                
                var dialog = $("#ContactWorkflowDialog");
                $compile(dialog.contents())($scope)
                


               
            }
            catch (ex) {

                $exceptionHandler(ex.message, "ContactDialogOne_Next")
                utility_service.ExceptionPopUpDialog("ContactDialogOne_Next : ", "An error occured: " + ex.message, 5);

            }
           

        }


        $scope.ContactDialogTwo_Previous = function () {

            try {


                // Dont Hide and this Works Out Fine!!
                //$("#ContactWorkflowDialog").modal('hide');

                $("#ContactWorkflowDialog").html($("#contact_dlg_one").html());

                $("#ContactWorkflowDialog").modal('show');


                var dialog = $("#ContactWorkflowDialog");
                $compile(dialog.contents())($scope)



                



            }
            catch (ex) {

                $exceptionHandler(ex.message, "ContactDialogTwo_Previous")
                utility_service.ExceptionPopUpDialog("ContactDialogTwo_Previous : ", "An error occured: " + ex.message, 5);

            }


        }

        $scope.ContactDialogTwo_Submit = function () {

            try {


                var Result = null;


                Result = contact_service.AddContactRequest($scope.ContactData);

                if (Result.status  == "success" )
                {
                
                    // Dont Hide and this Works Out Fine!!
                    //$("#ContactWorkflowDialog").modal('hide');

                    $scope.ContactRequestSubmitMessage = "Your Contact Request is on its way.  Thank You"

                    $("#ContactWorkflowDialog").html($("#contact_dlg_three").html());

                    $("#ContactWorkflowDialog").modal('show');


                    var dialog = $("#ContactWorkflowDialog");
                    $compile(dialog.contents())($scope)


                }
                else
                {
                    utility_service.ExceptionPopUpDialog("Contact Request Submission",  Result.description, 5);
               
                }
                

            }
            catch (ex) {

                $exceptionHandler(ex.message, "ContactDialogTwo_Submit")
                utility_service.ExceptionPopUpDialog("ContactDialogTwo_Submit  : ", "An error occured: " + ex.message, 5);

            }


        }


        $scope.ContactDialogThree_Load = function () {

            try {


             
                setTimeout(function () { $("#ContactWorkflowDialog").modal('hide'); }, 4000, null);



            }
            catch (ex) {

                $exceptionHandler(ex.message, "ContactDialogThree_Load")
                utility_service.ExceptionPopUpDialog("ContactDialogThree_Load : ", "An error occured: " + ex.message, 5);

            }

        }

        $scope.Initialize();

       
    }

})();
