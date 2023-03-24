(function () {

    'use strict'

    AngularApplication.service("content_service", ["$log", "$http", "utility_service", content_service]);

    function content_service( $log, $http,  utility_service) {

     
        function GetContentItems(ContentItemAreaID) {

            var ContentServiceEndpoint = utility_service.GetBaseURL() + "/ContentService/Content/GetActive?caid=" + ContentItemAreaID + "&icp=false";
            var ContentItemsJSON = null;
            var Result = null;


            try {


                $.ajax({
                    type: "GET",
                    dataType: "json",         //Expected data format from server
                    url: ContentServiceEndpoint,
                    async: false,
                    error: function (xhr) {

                        Result = { status: "fail", description: "An error occured: " + xhr.status + " " + xhr.statusText, data: null };

                    },
                    success: function (result) {

                        Result = { status: "success", description: "success", data: result };

                    }


                });

            }
            catch (e) {

                throw {
                    source: "GetContentItems",
                    message: "Error: " + e
                }

            }
            

            return Result;

        }

        function GetContentItem(ContentItemID) {

            var ContentItemJSON = null;
            var ContentServiceEndpoint = utility_service.GetBaseURL() + "/ContentService/Content/Select?cid=" + ContentItemID;
            var Result = null;

            try {

                $.ajax({
                    type: "GET",
                    dataType: "json",         //Expected data format from server
                    url: ContentServiceEndpoint,
                    async: false,
                    error: function (xhr) {

                        Result = { status: "fail", description: "An error occured: " + xhr.status + " " + xhr.statusText, data: null };

                    },
                    success: function (result) {

                        
                        Result = { status: "success", description: "success", data: result };


                    }
                });



            }
            catch (e) {

                  throw {
                      source: "GetContentItem",
                      message: "Error: " + e
                  }

              }
            

            return Result;

        }


        function UpdateContentItem(ContentItem) {

          

          
            var ContentServiceEndpoint = utility_service.GetBaseURL() + "/ContentService/Content/Update";
            var JSONContentItem = null;
            
            var Result = null;



            try {


                JSONContentItem = JSON.stringify(ContentItem)


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
            source: "UpdateContentItem",
            message: "Error: " + e
        }
    }
       
    return Result;


}




        function AddContentItem(ContentItem) {


            
            var ContentServiceEndpoint = utility_service.GetBaseURL() + "/ContentService/Content/Add";
            var JSONContentItem = null;
            var Result = null;





            try {


                JSONContentItem = JSON.stringify(ContentItem)


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
                    source: "AddContentItem",
                    message: "Error: " + e
                }

            }

            return Result;

        }


        function DeleteContentItem(ContentItem) {
            
            var JSONContentItem = null;
            

            var Result = null;
            var ContentServiceEndpoint = utility_service.GetBaseURL() + "/ContentService/Content/Update";

            try {


                JSONContentItem = JSON.stringify(ContentItem)


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
                    source: "DeleteContentItem",
                    message: "Error: " + e
                }

            }

            return Result;

        }



        return {
        
            GetContentItems: GetContentItems,
            GetContentItem: GetContentItem,
            UpdateContentItem: UpdateContentItem,
            AddContentItem: AddContentItem,
            DeleteContentItem: DeleteContentItem

        }
        
    }


   




})();