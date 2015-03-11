/**
 * Created by sevil_000 on 2015-02-19.
 */

( function( $ ) {
    $( document ).ready(function() {

        $('span.nav-btn').click(function(){
            $('#nav ul').slideToggle(500);
        });

        $(window).resize(function (){
            if($(window).width() > 600 ){
                $('#nav ul').removeAttr('style');
            }
        })

        //$(function(){
        //    $("#nav ul li").click(function(){
        //        $.scrollTo($("#main"), { duration:0});
        //    });
        //});

    });
} )( jQuery );