(function () {

    'use strict'

    AngularApplication.controller("mediaphoto_page_controller", ["$scope", "$log", "utility_service", "security_service", mediaphoto_page_controller])


    function mediaphoto_page_controller($scope, $log, utility_service, security_service) {


        $scope.Initialize = function () {

            try
            {

                hs.graphicsDir = utility_service.GetBaseURL() + '/scripts/lib/highslide/graphics/';

                hs.align = 'center';
                hs.transitions = ['expand', 'crossfade'];
                hs.outlineType = 'glossy-dark';
                hs.wrapperClassName = 'dark';
                hs.fadeInOut = true;
                //hs.dimmingOpacity = 0.75;

                // Add the controlbar
                if (hs.addSlideshow) hs.addSlideshow({
                    //slideshowGroup: 'group1',
                    interval: 5000,
                    repeat: false,
                    useControls: true,
                    fixedControls: 'fit',
                    overlayOptions: {
                        opacity: .6,
                        position: 'bottom center',
                        hideOnMouseOut: true
                    }
                });
            }
            catch(ex)
            {
                $exceptionHandler(ex.message, "Initialize")
                utility_service.ExceptionPopUpDialog("Initialize : ", "An error occured: " + ex.message, 5);
            }

        }


        $scope.Initialize();



    }

})();
