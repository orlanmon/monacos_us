(function () {

    'use strict'

    AngularApplication.service("azuredevice_service", ["$log", "$http", "utility_service", azuredevice_service]);

    function azuredevice_service($log, $http, utility_service) {



        function GetDeviceTelemetryItems(Device_ID, TelemetryItemResultCount) {

            var ServiceEndpoint = "http://voltagemonitorservice-developmentenv.azurewebsites.net/api/DeviceData/api/DeviceData/GetDeviceTelemetry";
            var JSONTelemetryRequest = null;
            var TelemetryRequest = null;

            var Result = null;

            try {


                var PostProxyURL = utility_service.GetBaseURL() + "/PostProxy.aspx?url=" + encodeURIComponent(ServiceEndpoint);


                TelemetryRequest = {
                    'device_ID': Device_ID,
                    'topNTelemetry': TelemetryItemResultCount,
                    'start_Time': null,
                    'end_Time': null
                }



                JSONTelemetryRequest = JSON.stringify(TelemetryRequest)

                $.ajax({
                    type: "POST",
                    url: PostProxyURL,
                    data: JSONTelemetryRequest,
                    contentType: "application/json; charset=utf-8",  // content type sent to server
                    dataType: "json",         //Expected data format from server
                    processData: true,
                    async: false,
                    error: function (xhr) {

                        if (xhr.status == "200") {

                            Result = { status: "success", description: "success", data: null };

                        }
                        else {

                            Result = { status: "fail", description: "An error occured: " + xhr.status + " " + xhr.statusText, data: null };

                        }
                    },
                    success: function (result) {


                        Result = { status: "success", description: "success", data: result };


                    }

                });

            }
            catch (e) {

                throw {
                    source: "GetDeviceTelemeryItems",
                    message: "Error: " + e
                }

            }


            return Result;

        }

        function EnableDisableTelemtry(Device_ID, bolEnableTelemetry) {

            var ServiceEndpoint = "";
            var Result = null;

            try {

                if (bolEnableTelemetry == true) {

                    ServiceEndpoint = "http://voltagemonitorservice-developmentenv.azurewebsites.net/api/DeviceControl/EnableTelemetry/" + Device_ID + "/true";

                }
                else {

                    ServiceEndpoint = "http://voltagemonitorservice-developmentenv.azurewebsites.net/api/DeviceControl/EnableTelemetry/" + Device_ID + "/false";

                }

                var PostProxyURL = utility_service.GetBaseURL() + "/PostProxy.aspx?url=" + encodeURIComponent(ServiceEndpoint);



                $.ajax({
                    type: "POST",
                    url: PostProxyURL,
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
                    source: "EnableDisableTelemtry",
                    message: "Error: " + e
                }
            }

            return Result;


        }

        function UpdateTelemetryInterval(Device_ID, TelemetryInterval) {

         
            var ServiceEndpoint = 'http://voltagemonitorservice-developmentenv.azurewebsites.net/api/DeviceControl/SetTelemetryPeriod/' + Device_ID + '/' + TelemetryInterval.toString();  
            var Result = null;

            try {


                var PostProxyURL = utility_service.GetBaseURL() + "/PostProxy.aspx?url=" + encodeURIComponent(ServiceEndpoint);


            $.ajax({
                type: "POST",
                url: PostProxyURL,
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
                    source: "UpdateTelemetryInterval",
                    message: "Error: " + e
                }
         }
       
        return Result;

        }

        
        function GetDeviceStatus(Device_ID) {

            var ServiceEndpoint = "http://voltagemonitorservice-developmentenv.azurewebsites.net/api/DeviceControl/GetDeviceStatus/" + Device_ID;

           
            var ContentItemsJSON = null;
            var Result = null;


            try {

                var PostProxyURL = utility_service.GetBaseURL() + "/PostProxy.aspx?url=" + encodeURIComponent(ServiceEndpoint);


                $.ajax({
                    type: "GET",
                    dataType: "json",         //Expected data format from server
                    url: PostProxyURL,
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
                    source: "GetDeviceStatus",
                    message: "Error: " + e
                }

            }


            return Result;

        }

        function GetHubDevices() {


           
            var ServiceEndpoint = "http://voltagemonitorservice-developmentenv.azurewebsites.net/api/DeviceControl/GetHubDevices";



            var ContentItemsJSON = null;
            var Result = null;


            try {

                var PostProxyURL = utility_service.GetBaseURL() + "/PostProxy.aspx?url=" + encodeURIComponent(ServiceEndpoint);


                $.ajax({
                    type: "GET",
                    dataType: "json",         //Expected data format from server
                    url: PostProxyURL,
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
                    source: "GetDeviceStatus",
                    message: "Error: " + e
                }

            }


            return Result;

        }

        return {
        
            GetDeviceTelemetryItems: GetDeviceTelemetryItems,
            EnableDisableTelemtry: EnableDisableTelemtry,
            UpdateTelemetryInterval: UpdateTelemetryInterval,
            GetDeviceStatus: GetDeviceStatus,
            GetHubDevices: GetHubDevices
        }
        
    }

})();