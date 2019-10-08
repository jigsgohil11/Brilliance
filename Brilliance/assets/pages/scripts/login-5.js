var Login = function() {

    var handleLogin = function() {


        $('#forget-password').click(function(){
            $('.login-form').hide();
            $('.forget-form').show();
            $('.otp-form').hide();
        });

        $('#back-btn').click(function(){
            $('.login-form').show();
            $('.forget-form').hide();
            $('.otp-form').hide();
        });
        $('#otpback-btn').click(function () {
            window.location.href = "/Login/Login";
            //$('.login-form').show();
            //$('.forget-form').hide();
            //$('.otp-form').hide();
        });

        //$('#login_btn').click(function () {
        //    $('.login-form').hide();
        //    $('.forget-form').hide();
        //    $('.otp-form').show();
        //});
    }

 
  

    return {
        //main function to initiate the module
        init: function() {

            handleLogin();

            // init background slide images
            //$('.login-bg').backstretch([
            //    "../assets/pages/img/login/bg1.jpg",
            //    "../assets/pages/img/login/bg2.jpg",
            //    "../assets/pages/img/login/bg3.jpg"
            //    ], {
            //      fade: 1000,
            //      duration: 8000
            //    }
            //);

            $('.forget-form').hide();
            $('.otp-form').hide();

        }

    };

}();

jQuery(document).ready(function() {
    Login.init();
});