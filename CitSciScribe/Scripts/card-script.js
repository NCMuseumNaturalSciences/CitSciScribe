
$(document).ready(function() {
    $(".form-row").scrollToFixed();
    $("input[type=text]").blur(function() {
        if (!$(this).val()) {
            $(this).css("background-color", "rgb(255,255,255)");
            $(this).siblings(".float-label").css("display", "inline-block");
        } else {
            $(this).css("background-color", "rgb(200,200,200)");
            $(this).siblings(".float-label").css("display", "none");
        }
    });
    $("textarea").blur(function() {
        if (!$(this).val()) {
            $(this).css("background-color", "rgb(255,255,255)");
        } else {
            $(this).css("background-color", "rgb(200,200,200)");
        }
    });
    $("a.help").click(function(e) {
        $("#transcription-help-dialog").dialog({
            show: "fade",
            hide: "fade",
            width: "720",
            height: "auto",
            draggable: true,
            resizable: false,
            modal: true,
            position: { my: "top", at: "top" },
            dialogClass: "ui-dialog-help"
        });
    });
    $(".form-control").on("focus blur",
        function(e) {
            $(this).parents(".field-wrapper").toggleClass("focused", (e.type === "focus" || this.value.length > 0));
        }).trigger("blur");
    // Disable submit on enter key press
    $("#transcription-form").on("keyup keypress",
        function(e) {
            var keyCode = e.keyCode || e.which;
            if (keyCode === 13) {
                e.preventDefault();
                return false;
            }
        });
});