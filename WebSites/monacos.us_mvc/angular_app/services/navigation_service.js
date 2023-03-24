(function () {

    'use strict'

    AngularApplication.service("navigation_service", ["$log", "$http", "utility_service", navigation_service]);

    function navigation_service( $log, $http,  utility_service) {

        
        function BuildSideBarMenu() {

            
            var KendoParsedJSON = null;
            var Result = null;


            try {

                KendoParsedJSON = [
                        {
                            text: "Monaco Software Solutions", expanded: true, imageUrl: "./images/Folder_Icon_16.png",
                            items: [
                                { text: "Facebook Page", imageUrl: "./images/Sites-icon_16.png", url: "https://www.facebook.com/monacosoftwaresolutions", encoded: "true"  },
                                { text: "Twitter Page", imageUrl: "./images/Sites-icon_16.png", url: "https://twitter.com/monacosoftware", encoded: "true" }
                            ]
                        },
                        {
                            text: "Software Development Resources", expanded: true, imageUrl: "./images/Folder_Icon_16.png",
                            items: [
                                { text: "MSDN", imageUrl: "./images/Sites-icon_16.png", url: "http://msdn.microsoft.com/en-US/", encoded: "true" },
                                { text: "MS.NET", imageUrl: "./images/Sites-icon_16.png", url: "https://www.microsoft.com/net", encoded: "true" },
                                { text: "ASP.NET", imageUrl: "./images/Sites-icon_16.png", url: "http://www.asp.net/", encoded: "true" },
                                { text: "Visual Studio", imageUrl: "./images/Sites-icon_16.png", url: "https://docs.microsoft.com/en-us/visualstudio/#pivot=workloads&panel=windows", encoded: "true" }
                            ]
                        },
                        {
                            text: "Cloud Developer Resources", expanded: true, imageUrl: "./images/Folder_Icon_16.png",
                            items: [
                                { text: "Amazon Cloud Services", imageUrl: "./images/Sites-icon_16.png", url: "https://aws.amazon.com/", encoded: "true" },
                                { text: "Microsoft Azure Developer", imageUrl: "./images/Sites-icon_16.png", url: "https://developer.amazon.com/", encoded: "true" },
                                { text: "Microsoft Azure", imageUrl: "./images/Sites-icon_16.png", url: "https://azure.microsoft.com/en-us/", encoded: "true" },
                                { text: "Microsoft Azure Portal", imageUrl: "./images/Sites-icon_16.png", url: "https://portal.azure.com/", encoded: "true" }

                            ]
                        }
                ];

                


                Result = { status: "success", description: "success", data: KendoParsedJSON };



            }
            catch (e) {

                throw {
                    source: "BuildSideBarMenu",
                    message: "Error: " + e
                }

            }
            
            return  Result;

        }
        


        function BuildMenu(rebuild) {

            // Get Menu and don't Rebuild From Authentication Unless Necessary

            try {



                var RestServiceEndpoint = utility_service.GetBaseURL() + "/NavigationSystemService/GetHeaderMenu?chka=" + rebuild;
                var KendoMenuJSON = null;
                var KendoParsedJSON = null;
                var Result = null;



                $.ajax({
                    dataType: "json",
                    url: RestServiceEndpoint,
                    async: false,
                    timeout: 30000,
                    error: function (xhr, textStatus, errorThrown) {

                      
                        Result = { status: "fail", description: "An error occured: " + xhr.status + " " + xhr.statusText, data: null };

                        KendoMenuJSON = { dataSource: [{ text: 'Home', imageUrl: '/images//globe.png', url: 'Default.aspx' }] };                      

                    },
                    success: function (result) {

                     
                        KendoMenuJSON = result;

                        if (KendoMenuJSON != null) {
                           
                            // This Works!!!
                            //KendoMenuJSON = '{ "text": "Home", "imageUrl" : "/images//globe.png", "url": "Default.aspx" }';

                            KendoParsedJSON = (JSON && JSON.parse(KendoMenuJSON)) || $.parseJSON(KendoMenuJSON);
                            
                            Result = { status: "success", description: "success", data: KendoParsedJSON };
                        }
                        else
                        {

                            Result = { status: "fail", description: "An error occured: No Menu JSON returned",  data: null };

                        }
                      


                    }
                });

            }
            catch (e) {

                throw {
                    source: "BuildMenu",
                    message: "Error: " + e
                }

            }

            return Result;

        }

        
        return {
        
            BuildSideBarMenu : BuildSideBarMenu,
            BuildMenu : BuildMenu
        
        }
        
    }

   





})();