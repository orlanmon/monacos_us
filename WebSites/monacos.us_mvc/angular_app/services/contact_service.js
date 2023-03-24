(function () {

    'use strict'

    AngularApplication.service("contact_service", ["$log", "$http", "utility_service", contact_service]);

    function contact_service( $log, $http,  utility_service) {

    
        function AddContactRequest(ContactRequest) {

            var ContentServiceEndpoint = utility_service.GetBaseURL() + "/Contact/ContactSubmit";
            var JSONContentItem = null;
            var Result =  { status : "success", description : "success" };


            try {

            // Notice Here We are Converting to JSON String For AJAX POST
            JSONContentItem = JSON.stringify(ContactRequest)

            $.ajax({
                type: "POST",
                url: ContentServiceEndpoint,
                data: JSONContentItem,
                contentType: "application/json; charset=utf-8",  // content type sent to server
                dataType: "json",         //Expected data format from server
                processData: true,
                async: false,
                error: function (xhr) {

                    if (xhr.status == "200") {


                        Result = { status: "success", description: "success", data: true };

                    }
                    else {

                        Result = { status: "fail", description: "An error occured: " + xhr.status + " " + xhr.statusText, data: false };

                        
                    }
                },
                success: function (result) {


                    Result = { status: "success", description: "success", data: true };


                }

            });

    }
   catch (e) {

       throw {
           source: "AddContactRequest",
           message: "Error: " + e
       }
    }
       
    return Result;


  }

        return {
        
            AddContactRequest: AddContactRequest

        }
        
    }


})();