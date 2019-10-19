$(document).ready(function() {

    function d(e) { return e.replace(/^\//, "").replace(/(index|default).[a-zA-Z]{3,4}$/, "").replace(/\/$/, ""); }

    var c = d(location.pathname);
    var b = a("html", "body");
    $("a[href*=#]").each(function() {
        var f = d(this.pathname) || c;
        if (c == f && (location.hostname == this.hostname || !this.hostname) && this.hash.replace(/#/, "")) {
            var e = $(this.hash), g = this.hash;
            if (g) {
                var h = e.offset().top;
                $(this).click(function(i) {
                    i.preventDefault();
                    $(b).animate({ scrollTop: h }, 400, function() { location.hash = g; });
                });
            }
        }
    });

    function a(h) {
        for (var g = 0, k = arguments.length; g < k; g++) {
            var j = arguments[g], e = $(j);
            if (e.scrollTop() > 0) {
                return j;
            } else {
                e.scrollTop(1);
                var f = e.scrollTop() > 0;
                e.scrollTop(0);
                if (f) {
                    return j;
                }
            }
        }
        return [];
    }
});