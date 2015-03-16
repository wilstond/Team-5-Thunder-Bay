$(document).ready(function () {
    
    $(".main_nav ul li").click(function () {
        $(".sec_nav").slideUp("slow");

        if ($(this).find(".sec_nav").hasClass("display-menu")) {
            //$(this).find(".sec_nav").slideUp("slow");
            $(".sec_nav").slideUp("slow");
            $(".sec_nav").removeClass("display-menu");
        }
        else {
            $(".sec_nav").removeClass("display-menu");
            $(this).find(".sec_nav").slideDown("slow");
            $(this).find(".sec_nav").addClass("display-menu");
        }

        //$(this).find(".sec_nav").slideToggle("slow");
    })
})