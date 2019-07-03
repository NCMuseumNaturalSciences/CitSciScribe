/* Vertical Transcription Layout Script */

/* Option 1 - Sliding Panel
// https://stackoverflow.com/questions/21426056/horizontal-sliding-panel-with-relative-postion-handle-show-hide-jquery
$(function () {
    $('#slider-btn').click(function () {
        console.log("slider btn clicked");
        if ($(this).hasClass('show')) {
            $("#slider-btn, #slide-panel").animate({                
                right: "+=290"
            }, 700, function () {
                console.log("show slider panel complete");
            });
            $(this).html('&laquo;').removeClass('show').addClass('hide');
        } else {
            $("#slider-btn, #slide-panel").animate({
                right: "-=290"
            }, 700, function () {
                console.log("hide slider panel complete");
            });
            $(this).html('&raquo;').removeClass('hide').addClass('show');
        }
    });
});
*/
/* Resizable form container for horizontal layouts */
$(document).ready(function() {
    $(".v-draggable").resizable({
        handles: {
            'n': "#handle"
        },
        resize: function(event, ui) {
            ui.size.width = ui.originalSize.width;
        }
    });
    $(".v-draggable").draggable({
        axis: "y"
    });
});

$("#slide-button").on("click",
    function(e) {
        console.log("slide button clicke - toggle class");
        e.preventDefault();
        $("body").toggleClass("slide-open slide-close");
        $("#img-wrapper").toggleClass("slide-open slide-close");
        $("#penn-mussels-card").css("margin", "0 auto");
    });
$("#slide-button").on("click",
    function(e) {
        console.log("slide button clicke - remove class");
        e.preventDefault();
//    $('body').removeClass('slide-open');
    });
/* Slidereveal */
var sp = $("#slide-panel").slideReveal({
    trigger: $("#slide-button"),
    push: false,
    overlay: false,
    position: "right",
    width: "40%",
});
sp.slideReveal("show");

/* Form Field Focus */

$(".form-control").focus(function() {
    var dl = $(this).data("label");
    console.log("dl " + dl);
    $(".active-field").html(dl);
});

/*
/* Slide Button
jQuery(document).ready(function ($) {
    $('.cd-btn').on('click', function (event) {
        event.preventDefault();
        $('.cd-panel').addClass('is-visible');
    });
    $('.cd-panel').on('click', function (event) {
        if ($(event.target).is('.cd-panel') || $(event.target).is('.cd-panel-close')) {
            $('.cd-panel').removeClass('is-visible');
            event.preventDefault();
        }
    });
});
/*
$('#slide-btn-wrapper button').toggle(
function () {
    $('#slide-btn-wrapper').css('right', '20px')
}, function () {
    $('#slide-btn-wrapper').css('right', '800px')
})

/* Slimscroll 
$("#slide-panel").slimScroll({
    position: 'left',
    height: '2410px',
    railVisible: true,
    railOpacity: 0.8,
    railColor: '#FFF',
    alwaysVisible: true,
    wheelstep: 5,
    size: '8px',
});
*/