$(window).load(function () {
    $('#slider').nivoSlider({ directionNavHide: false });

    $(".nivoSlider .nivo-caption").css({ bottom: "-25px" });
    $(".nivoSlider").hoverIntent(function () {
        $(this).children(".nivo-caption").animate({ bottom: "0", opacity: "1" });
    }, function () {
        $(this).children(".nivo-caption").animate({ bottom: "-25px", opacity: "0" }, "fast");
    });
});

$(document).ready(function () {
    //SyntaxHighlighter.all();

    $("a.back-to-top").click(function (event) {
        event.preventDefault();
        return false;
    });

    $('.portfolio a.lightbox').fancybox({
        openEffect: 'elastic',
        closeEffect: 'elastic',
        helpers: {
            title: {
                type: 'over'
            },
            thumbs: {
                width: 40,
                height: 40
            },
            media: {}
        },
        padding: 16
    });

    $("ul.folio-list li .thumb img").fadeTo("fast", 0.6);
    $("ul.folio-list li .thumb img").hoverIntent(function () {
        $(this).fadeTo("slow", 1.0);
    }, function () {
        $(this).fadeTo("slow", 0.6);
    });

    $("ul.folio-list li h4.title").css({ top: "-25px", opacity: "0" });
    $("ul.folio-list li").hoverIntent(function () {
        $(this).children("h4.title").animate({ top: "-44px", opacity: "1" });
    }, function () {
        $(this).children("h4.title").animate({ top: "-25px", opacity: "0" }, "fast");
    });

    $(".home div section a.more, .services section a.more, .about-us > div > div a.more").css({ opacity: "0" });
    $(".home div section, .services .row section, .about-us > div > div").hoverIntent(function () {
        $(this).children("a.more").animate({ opacity: "1" }, "slow", "easeInBounce");
    }, function () {
        $(this).children("a.more").animate({ opacity: "0" }, "fast", "easeOutBounce");
    });
});