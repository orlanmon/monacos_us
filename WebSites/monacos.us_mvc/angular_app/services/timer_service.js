(function () {

    'use strict'

    AngularApplication.service("timer_service", ["$timeout", "$log", timer_service]);

    function timer_service($timeout, $log) {




        /* Define Function Prototype ( Class Here ) */

        // FYI You are Definining A Time "Class" Here if You Will
        // Then this Service Returns Instance of this Class

        // Constructor
        function Timer(callback, duration, invokeApply) {


            //$log.debug("Timer Constructor");

            // Prototype Properties
            this._callback = callback;
            this._duration = (duration || 0);
            this._invokeApply = (invokeApply !== false);
            this._timer = null;


        }

        // Define a Timer Prototype Pseudo Class

        // Start 

        Timer.prototype.constructor = Timer;

        Timer.prototype.isActive = function ()
        {

            return (!!this._timer);

        }

        Timer.prototype.restart = function () {


            //$log.debug("Timer Restart");

            this.stop();
            this.start();

        }

        Timer.prototype.start = function () {


            //$log.debug("Timer Start");

            var self = this;

            this._timer = $timeout(


                function handleTimeCallBack() {

                    try {


                        if (self._callback != null) {
                            self._callback.call(null);
                        }
                        else {

                            $log.debug("Callback NULL")

                        }

                        

                    }
                    finally {

                        self.start();


                    }

                }

        
        

                , this._duration, this._invokeApply);



        }

        Timer.prototype.stop = function () {

            //$log.debug("Timer Stop");

            $timeout.cancel(this._timer);

            this._timer = null;

        }


        Timer.prototype.close = function () {


            //$log.debug("Timer Close");

            this.stop();
            this._callback = null;
            this._duration = null;
            this._invokeApply = null;
            this._timer = null;

        }

        // End 


        // Factory Method This Returns Instances of a Timer
        // The factory is Dependency Injected and then allows one to initialize 
        // More Then One Timer
        function timerFactory(callback, duration, invokeApply) {
            return (new Timer(callback, duration, invokeApply));
        }


        // TimeBased Constants - Not Needed - Here For Reference
        //timerFactory.ONE_SECOND = (1 * 1000);



        return (timerFactory);



    }

   





})();