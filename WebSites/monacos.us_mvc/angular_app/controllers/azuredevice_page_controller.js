(function () {

    'use strict'

    AngularApplication.controller("azuredevice_page_controller", ["$scope", "$log", '$interval', "utility_service",   "azuredevice_service", "$exceptionHandler", azuredevice_page_controller])


    function azuredevice_page_controller($scope, $log, $interval, utility_service, azuredevice_service, $exceptionHandler) {


        $scope.bolTelemetryRefresh = false;
        $scope.bolTelemetryRefreshPromise = null;
        $scope.bolEnableTelemetry = false;
        $scope.bolDeviceOnline = false;
        $scope.TelemetryInterval = "0";
        $scope.TelemetryIntervalItemCount = 50;
        $scope.Device_Status = null;
        $scope.Device_Status_Description = "Device Status is not online.";
        $scope.Selected_Device_Refresh_Interval = "None";
        $scope.Selected_Device = "None Selected";
        $scope.Device_Selected = false;
        $scope.RegisteredDevices = null;
        $scope.IoTHubConnected = false;


        $scope.Initialize = function () {


            try
            {

                // Initialize Kendo Button Styles Replaced With Bootstrap Buttons
                /*
                $("#button_device_enable").kendoButton();
                $("#button_device_disable").kendoButton();
                $("#button_device_telemetry_interval").kendoButton();
                $("#button_device_refresh").kendoButton();
                */

                debugger;

                // Connect to IoT Hub and Get Registered Devices

                if ($scope.LoadHubDevices())
                {

                    $scope.LoadHubDevicesDropdown();


                }


              

                $('#select_device_refresh_options li a').on('click', function () {


                    $scope.Selected_Device_Refresh_Interval = $(this).html();

                    $scope.TelemetryItemGridRefresh($scope.Selected_Device_Refresh_Interval);

                });


                /*
                $('#select_device li a').on('click', function () {


                    alert("Select Event" + $(this).html());

                    $scope.Selected_Device = $(this).html();

                    if ($scope.Selected_Device != 'None Selected') {

                        $scope.Device_Selected = true;

                        $scope.GetDeviceStatus();

                        $scope.LoadTelemetryItemGrid();


                    }
                    else {

                        $scope.Device_Selected = false;

                    }

                });
                */

                

            }
            catch (ex) {

                $exceptionHandler(ex.message, "Initialize")

                utility_service.ExceptionPopUpDialog("Initialize : ", "An error occured: " + ex.message, 5);
            }
        

        }


        $scope.OnDeviceSelect = function ($event) {

         


              

                $scope.Selected_Device = $event.target.innerHTML;

                if ($scope.Selected_Device != 'None Selected') {

                    $scope.Device_Selected = true;

                    $scope.GetDeviceStatus();

                    $scope.LoadTelemetryItemGrid();


                    $scope.$apply();


                }
                else {

                    $scope.Device_Selected = false;

                }

        }



        $scope.LoadHubDevices = function () {

            var Result = null; 
           
            try
            {
                Result = azuredevice_service.GetHubDevices();

                if (Result.status = "success") {

                    $scope.RegisteredDevices = Result.data;

                    $scope.IoTHubConnected = true;

                }
                else
                {
                    $scope.IoTHubConnected = false;
                }

                //deviceId


            }
            catch (ex) {

                $exceptionHandler(ex.message, "LoadHubDevices")

                utility_service.ExceptionPopUpDialog("LoadHubDevices : ", "An error occured: " + ex.message, 5);
            }

            return $scope.IoTHubConnected;


        }


        $scope.LoadHubDevicesDropdown = function () {

            

            try {
            

                

                


            }
            catch (ex) {

                $exceptionHandler(ex.message, "LoadHubDevicesDropdown")

                utility_service.ExceptionPopUpDialog("LoadHubDevicesDropdown : ", "An error occured: " + ex.message, 5);
            }

        }



        $scope.EnableTelememtry = function () {

            $scope.bolEnableTelemetry = true;

            azuredevice_service.EnableDisableTelemtry($scope.Selected_Device, $scope.bolEnableTelemetry)

            $scope.GetDeviceStatus();

        }


        $scope.DisableTelememtry = function () {

            $scope.bolEnableTelemetry = false;

            azuredevice_service.EnableDisableTelemtry($scope.Selected_Device, $scope.bolEnableTelemetry);


            $scope.GetDeviceStatus();


        }

        $scope.UpdateTelemetryPeriod = function () {

            azuredevice_service.UpdateTelemetryInterval($scope.Selected_Device, $scope.TelemetryInterval)

            $scope.GetDeviceStatus();

        }

        $scope.GetDeviceStatus = function () {


            var Result = null;

       
            try
            {

                Result = azuredevice_service.GetDeviceStatus($scope.Selected_Device)

            if (Result.status = "success") {


                $scope.Device_Status = Result.data;

                /*
                {
                    "device_ID": "raspberry_voltage_monitor",
                        "deviceOnline": true,
                            "telemetryEnabled": false,
                                "telemetryInterval": 2000,
                                    "deviceStatusDesc": "Device raspberry_voltage_monitor is online and telemetry is currently disabled."
                }
                */


                
                
                
                $scope.TelemetryInterval = $scope.Device_Status.telemetryInterval;

                $scope.bolEnableTelemetry = $scope.Device_Status.telemetryEnabled;

                $scope.bolDeviceOnline = $scope.Device_Status.deviceOnline;

                if ($scope.bolDeviceOnline)
                {
                    $scope.Device_Status_Description = $scope.Device_Status.deviceStatusDesc;
                }
                else
                {
                    $scope.Device_Status_Description = "Device is not online."
                }
            }
            else {

                $scope.Device_Status_Description = "Device is not online.";

            }

        }
              catch (ex) {

                $exceptionHandler(ex.message, "LoadTelemetryItemGrid")
                utility_service.ExceptionPopUpDialog("LoadTelemetryItemGrid : ", "An error occured: " + ex.message, 5);

            }


        }
        
        $scope.LoadTelemetryItemGrid = function () {


            // Do Not Include Only Currently Published = Everything Except InActive

          
            var Result = null;
            var TelemetryItemsJSON = null;



            try {

                Result = azuredevice_service.GetDeviceTelemetryItems($scope.Selected_Device, $scope.TelemetryIntervalItemCount)


                if (Result.status = "success") {

                    
                    TelemetryItemsJSON = [];




                    $("#device_telemetry_grid").kendoGrid({

                        dataSource: {
                            data: TelemetryItemsJSON,
                            pageSize: 10
                        },
                        navigatable: true,
                        pageable: true,
                        sortable: true,
                        height: 430,
                        columns: [
                            { field: "Id", title: "ID", width: 40 },
                            { field: "Sample_Time", title: "Sample Time", width: 100 },
                            { field: "Voltage", title: "Voltage", width: 40 },
                            { field: "Device_ID", title: "Device ID", width: 100 },
                            { field: "Device_Channel", title: "Device Channel", width: 40 }
                        ],
                        editable: false
                    });
                    

                    TelemetryItemsJSON = Result.data;


                    $("#device_telemetry_grid").kendoGrid({
                        
                        dataSource: {
                            data: TelemetryItemsJSON,
                            pageSize: 10               
                        },
                        navigatable: true,
                        pageable: true,
                        sortable: true,
                        height: 430,
                        columns: [
                            { field: "Id", title: "ID", width: 40 },
                            { field: "Sample_Time", title: "Sample Time", width: 100 },
                            { field: "Voltage", title: "Voltage", width: 40 },
                            { field: "Device_ID", title: "Device ID", width: 100 },
                            { field: "Device_Channel", title: "Device Channel", width: 40 }
                            ],
                        editable: false
                    });

                }
                else
                {
                    // fail
                    utility_service.ExceptionPopUpDialog("LoadTelemetryItemGrid", "An error occured: " + Result.description, 5);

                }

            }
            catch (ex) {

                $exceptionHandler(ex.message, "LoadTelemetryItemGrid")
                utility_service.ExceptionPopUpDialog("LoadTelemetryItemGrid : ", "An error occured: " + ex.message, 5);

            }

        }


        $scope.TelemetryItemGridRefresh = function (RefreshInterval) 
        {

            var RefreshIntervalMilliSeconds = 0;


            switch (RefreshInterval)
            {
                case "None":

                    RefreshIntervalMilliSeconds = 0;

                    break;
                case "5 Sec":

                    RefreshIntervalMilliSeconds = 5000;
                    break;
                case "10 Sec":

                    RefreshIntervalMilliSeconds = 10000;
                    break;
                case "30 Sec":
                    RefreshIntervalMilliSeconds = 30000;
                    break;
                case "60 Sec":
                    RefreshIntervalMilliSeconds = 60000;
                    break;
                case "120 Sec":
                    RefreshIntervalMilliSeconds = 1200000;
                    break;
                default:
                    break;
            }

            if (RefreshIntervalMilliSeconds == 0)
            {
                if ($scope.bolTelemetryRefreshPromise != null)
                {
                    
                    $interval.cancel($scope.bolTelemetryRefreshPromise);


                }

                $scope.bolTelemetryRefreshPromise = null;

            }
            else
            {

                $scope.bolTelemetryRefreshPromise = $interval($scope.LoadTelemetryItemGrid, RefreshIntervalMilliSeconds, 0, true);

            }
           
        }


        $scope.Initialize();



    }

})();
