// Alternating Background */
$(document).ready(function() {
    var images = [
        "ledger_4.png", "butterflies_1b.png", "butterflies_2b.png", "crustacean_illustrations2.png", "frogs_1.png"
    ];
    $("body").css({ 'background-image': "url(images/" + images[Math.floor(Math.random() * images.length)] + ")" });
});