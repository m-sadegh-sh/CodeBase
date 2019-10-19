(function(b) {
    var a = function(m, s) {
        var j = b.extend({ }, b.fn.nivoSlider.defaults, s);
        var p = { currentSlide: 0, currentImage: "", totalSlides: 0, randAnim: "", running: false, paused: false, stop: false };
        var d = b(m);
        d.data("nivo:vars", p);
        d.css("position", "relative");
        d.addClass("nivoSlider");
        var e = d.children();
        e.each(function() {
            var v = b(this);
            var u = "";
            if (!v.is("img")) {
                if (v.is("a")) {
                    v.addClass("nivo-imageLink");
                    u = v;
                }
                v = v.find("img:first");
            }
            var t = v.width();
            if (t == 0) {
                t = v.attr("width");
            }
            var i = v.height();
            if (i == 0) {
                i = v.attr("height");
            }
            if (t > d.width()) {
                d.width(t);
            }
            if (i > d.height()) {
                d.height(i);
            }
            if (u != "") {
                u.css("display", "none");
            }
            v.css("display", "none");
            p.totalSlides++;
        });
        if (j.startSlide > 0) {
            if (j.startSlide >= p.totalSlides) {
                j.startSlide = p.totalSlides - 1;
            }
            p.currentSlide = j.startSlide;
        }
        if (b(e[p.currentSlide]).is("img")) {
            p.currentImage = b(e[p.currentSlide]);
        } else {
            p.currentImage = b(e[p.currentSlide]).find("img:first");
        }
        if (b(e[p.currentSlide]).is("a")) {
            b(e[p.currentSlide]).css("display", "block");
        }
        d.css("background", 'url("' + p.currentImage.attr("src") + '") no-repeat');
        d.append(b('<div class="nivo-caption"><p></p></div>').css({ display: "none", opacity: j.captionOpacity }));
        var q = function(i) {
            var u = b(".nivo-caption", d);
            if (p.currentImage.attr("title") != "" && p.currentImage.attr("title") != undefined) {
                var t = p.currentImage.attr("title");
                if (t.substr(0, 1) == "#") {
                    t = b(t).html();
                }
                if (u.css("display") == "block") {
                    u.find("p").fadeOut(i.animSpeed, function() {
                        b(this).html(t);
                        b(this).fadeIn(i.animSpeed);
                    });
                } else {
                    u.find("p").html(t);
                }
                u.fadeIn(i.animSpeed);
            } else {
                u.fadeOut(i.animSpeed);
            }
        };
        q(j);
        var c = 0;
        if (!j.manualAdvance && e.length > 1) {
            c = setInterval(function() { r(d, e, j, false); }, j.pauseTime);
        }
        if (j.directionNav) {
            d.append('<div class="nivo-directionNav"><a class="nivo-prevNav">' + j.prevText + '</a><a class="nivo-nextNav">' + j.nextText + "</a></div>");
            if (j.directionNavHide) {
                b(".nivo-directionNav", d).hide();
                d.hover(function() { b(".nivo-directionNav", d).show(); }, function() { b(".nivo-directionNav", d).hide(); });
            }
            b("a.nivo-prevNav", d).live("click", function() {
                if (p.running) {
                    return false;
                }
                clearInterval(c);
                c = "";
                p.currentSlide -= 2;
                r(d, e, j, "prev");
            });
            b("a.nivo-nextNav", d).live("click", function() {
                if (p.running) {
                    return false;
                }
                clearInterval(c);
                c = "";
                r(d, e, j, "next");
            });
        }
        if (j.controlNav) {
            var n = b('<div class="nivo-controlNav"></div>');
            d.append(n);
            for (var l = 0; l < e.length; l++) {
                if (j.controlNavThumbs) {
                    var f = e.eq(l);
                    if (!f.is("img")) {
                        f = f.find("img:first");
                    }
                    if (j.controlNavThumbsFromRel) {
                        n.append('<a class="nivo-control" rel="' + l + '"><img src="' + f.attr("rel") + '" alt="" /></a>');
                    } else {
                        n.append('<a class="nivo-control" rel="' + l + '"><img src="' + f.attr("src").replace(j.controlNavThumbsSearch, j.controlNavThumbsReplace) + '" alt="" /></a>');
                    }
                } else {
                    n.append('<a class="nivo-control" rel="' + l + '">' + (l + 1) + "</a>");
                }
            }
            b(".nivo-controlNav a:eq(" + p.currentSlide + ")", d).addClass("active");
            b(".nivo-controlNav a", d).live("click", function() {
                if (p.running) {
                    return false;
                }
                if (b(this).hasClass("active")) {
                    return false;
                }
                clearInterval(c);
                c = "";
                d.css("background", 'url("' + p.currentImage.attr("src") + '") no-repeat');
                p.currentSlide = b(this).attr("rel") - 1;
                r(d, e, j, "control");
            });
        }
        if (j.keyboardNav) {
            b(window).keypress(function(i) {
                if (i.keyCode == "37") {
                    if (p.running) {
                        return false;
                    }
                    clearInterval(c);
                    c = "";
                    p.currentSlide -= 2;
                    r(d, e, j, "prev");
                }
                if (i.keyCode == "39") {
                    if (p.running) {
                        return false;
                    }
                    clearInterval(c);
                    c = "";
                    r(d, e, j, "next");
                }
            });
        }
        if (j.pauseOnHover) {
            d.hover(function() {
                p.paused = true;
                clearInterval(c);
                c = "";
            }, function() {
                p.paused = false;
                if (c == "" && !j.manualAdvance) {
                    c = setInterval(function() { r(d, e, j, false); }, j.pauseTime);
                }
            });
        }
        d.bind("nivo:animFinished", function() {
            p.running = false;
            b(e).each(function() {
                if (b(this).is("a")) {
                    b(this).css("display", "none");
                }
            });
            if (b(e[p.currentSlide]).is("a")) {
                b(e[p.currentSlide]).css("display", "block");
            }
            if (c == "" && !p.paused && !j.manualAdvance) {
                c = setInterval(function() { r(d, e, j, false); }, j.pauseTime);
            }
            j.afterChange.call(this);
        });
        var g = function(v, u, x) {
            for (var t = 0; t < u.slices; t++) {
                var w = Math.round(v.width() / u.slices);
                if (t == u.slices - 1) {
                    v.append(b('<div class="nivo-slice"></div>').css({ left: (w * t) + "px", width: (v.width() - (w * t)) + "px", height: "0px", opacity: "0", background: 'url("' + x.currentImage.attr("src") + '") no-repeat -' + ((w + (t * w)) - w) + "px 0%" }));
                } else {
                    v.append(b('<div class="nivo-slice"></div>').css({ left: (w * t) + "px", width: w + "px", height: "0px", opacity: "0", background: 'url("' + x.currentImage.attr("src") + '") no-repeat -' + ((w + (t * w)) - w) + "px 0%" }));
                }
            }
        };
        var h = function(u, i, x) {
            var t = Math.round(u.width() / i.boxCols);
            var y = Math.round(u.height() / i.boxRows);
            for (var v = 0; v < i.boxRows; v++) {
                for (var w = 0; w < i.boxCols; w++) {
                    if (w == i.boxCols - 1) {
                        u.append(b('<div class="nivo-box"></div>').css({ opacity: 0, left: (t * w) + "px", top: (y * v) + "px", width: (u.width() - (t * w)) + "px", height: y + "px", background: 'url("' + x.currentImage.attr("src") + '") no-repeat -' + ((t + (w * t)) - t) + "px -" + ((y + (v * y)) - y) + "px" }));
                    } else {
                        u.append(b('<div class="nivo-box"></div>').css({ opacity: 0, left: (t * w) + "px", top: (y * v) + "px", width: t + "px", height: y + "px", background: 'url("' + x.currentImage.attr("src") + '") no-repeat -' + ((t + (w * t)) - t) + "px -" + ((y + (v * y)) - y) + "px" }));
                    }
                }
            }
        };
        var r = function(H, G, K, D) {
            var F = H.data("nivo:vars");
            if (F && (F.currentSlide == F.totalSlides - 1)) {
                K.lastSlide.call(this);
            }
            if ((!F || F.stop) && !D) {
                return false;
            }
            K.beforeChange.call(this);
            if (!D) {
                H.css("background", 'url("' + F.currentImage.attr("src") + '") no-repeat');
            } else {
                if (D == "prev") {
                    H.css("background", 'url("' + F.currentImage.attr("src") + '") no-repeat');
                }
                if (D == "next") {
                    H.css("background", 'url("' + F.currentImage.attr("src") + '") no-repeat');
                }
            }
            F.currentSlide++;
            if (F.currentSlide == F.totalSlides) {
                F.currentSlide = 0;
                K.slideshowEnd.call(this);
            }
            if (F.currentSlide < 0) {
                F.currentSlide = (F.totalSlides - 1);
            }
            if (b(G[F.currentSlide]).is("img")) {
                F.currentImage = b(G[F.currentSlide]);
            } else {
                F.currentImage = b(G[F.currentSlide]).find("img:first");
            }
            if (K.controlNav) {
                b(".nivo-controlNav a", H).removeClass("active");
                b(".nivo-controlNav a:eq(" + F.currentSlide + ")", H).addClass("active");
            }
            q(K);
            b(".nivo-slice", H).remove();
            b(".nivo-box", H).remove();
            if (K.effect == "random") {
                var M = new Array("sliceDownRight", "sliceDownLeft", "sliceUpRight", "sliceUpLeft", "sliceUpDown", "sliceUpDownLeft", "fold", "fade", "boxRandom", "boxRain", "boxRainReverse", "boxRainGrow", "boxRainGrowReverse");
                F.randAnim = M[Math.floor(Math.random() * (M.length + 1))];
                if (F.randAnim == undefined) {
                    F.randAnim = "fade";
                }
            }
            if (K.effect.indexOf(",") != -1) {
                var M = K.effect.split(",");
                F.randAnim = M[Math.floor(Math.random() * (M.length))];
                if (F.randAnim == undefined) {
                    F.randAnim = "fade";
                }
            }
            F.running = true;
            if (K.effect == "sliceDown" || K.effect == "sliceDownRight" || F.randAnim == "sliceDownRight" || K.effect == "sliceDownLeft" || F.randAnim == "sliceDownLeft") {
                g(H, K, F);
                var L = 0;
                var I = 0;
                var t = b(".nivo-slice", H);
                if (K.effect == "sliceDownLeft" || F.randAnim == "sliceDownLeft") {
                    t = b(".nivo-slice", H)._reverse();
                }
                t.each(function() {
                    var i = b(this);
                    i.css({ top: "0px" });
                    if (I == K.slices - 1) {
                        setTimeout(function() { i.animate({ height: "100%", opacity: "1.0" }, K.animSpeed, "", function() { H.trigger("nivo:animFinished"); }); }, (100 + L));
                    } else {
                        setTimeout(function() { i.animate({ height: "100%", opacity: "1.0" }, K.animSpeed); }, (100 + L));
                    }
                    L += 50;
                    I++;
                });
            } else {
                if (K.effect == "sliceUp" || K.effect == "sliceUpRight" || F.randAnim == "sliceUpRight" || K.effect == "sliceUpLeft" || F.randAnim == "sliceUpLeft") {
                    g(H, K, F);
                    var L = 0;
                    var I = 0;
                    var t = b(".nivo-slice", H);
                    if (K.effect == "sliceUpLeft" || F.randAnim == "sliceUpLeft") {
                        t = b(".nivo-slice", H)._reverse();
                    }
                    t.each(function() {
                        var i = b(this);
                        i.css({ bottom: "0px" });
                        if (I == K.slices - 1) {
                            setTimeout(function() { i.animate({ height: "100%", opacity: "1.0" }, K.animSpeed, "", function() { H.trigger("nivo:animFinished"); }); }, (100 + L));
                        } else {
                            setTimeout(function() { i.animate({ height: "100%", opacity: "1.0" }, K.animSpeed); }, (100 + L));
                        }
                        L += 50;
                        I++;
                    });
                } else {
                    if (K.effect == "sliceUpDown" || K.effect == "sliceUpDownRight" || F.randAnim == "sliceUpDown" || K.effect == "sliceUpDownLeft" || F.randAnim == "sliceUpDownLeft") {
                        g(H, K, F);
                        var L = 0;
                        var I = 0;
                        var B = 0;
                        var t = b(".nivo-slice", H);
                        if (K.effect == "sliceUpDownLeft" || F.randAnim == "sliceUpDownLeft") {
                            t = b(".nivo-slice", H)._reverse();
                        }
                        t.each(function() {
                            var i = b(this);
                            if (I == 0) {
                                i.css("top", "0px");
                                I++;
                            } else {
                                i.css("bottom", "0px");
                                I = 0;
                            }
                            if (B == K.slices - 1) {
                                setTimeout(function() { i.animate({ height: "100%", opacity: "1.0" }, K.animSpeed, "", function() { H.trigger("nivo:animFinished"); }); }, (100 + L));
                            } else {
                                setTimeout(function() { i.animate({ height: "100%", opacity: "1.0" }, K.animSpeed); }, (100 + L));
                            }
                            L += 50;
                            B++;
                        });
                    } else {
                        if (K.effect == "fold" || F.randAnim == "fold") {
                            g(H, K, F);
                            var L = 0;
                            var I = 0;
                            b(".nivo-slice", H).each(function() {
                                var i = b(this);
                                var v = i.width();
                                i.css({ top: "0px", height: "100%", width: "0px" });
                                if (I == K.slices - 1) {
                                    setTimeout(function() { i.animate({ width: v, opacity: "1.0" }, K.animSpeed, "", function() { H.trigger("nivo:animFinished"); }); }, (100 + L));
                                } else {
                                    setTimeout(function() { i.animate({ width: v, opacity: "1.0" }, K.animSpeed); }, (100 + L));
                                }
                                L += 50;
                                I++;
                            });
                        } else {
                            if (K.effect == "fade" || F.randAnim == "fade") {
                                g(H, K, F);
                                var z = b(".nivo-slice:first", H);
                                z.css({ height: "100%", width: H.width() + "px" });
                                z.animate({ opacity: "1.0" }, (K.animSpeed * 2), "", function() { H.trigger("nivo:animFinished"); });
                            } else {
                                if (K.effect == "slideInRight" || F.randAnim == "slideInRight") {
                                    g(H, K, F);
                                    var z = b(".nivo-slice:first", H);
                                    z.css({ height: "100%", width: "0px", opacity: "1" });
                                    z.animate({ width: H.width() + "px" }, (K.animSpeed * 2), "", function() { H.trigger("nivo:animFinished"); });
                                } else {
                                    if (K.effect == "slideInLeft" || F.randAnim == "slideInLeft") {
                                        g(H, K, F);
                                        var z = b(".nivo-slice:first", H);
                                        z.css({ height: "100%", width: "0px", opacity: "1", left: "", right: "0px" });
                                        z.animate({ width: H.width() + "px" }, (K.animSpeed * 2), "", function() {
                                            z.css({ left: "0px", right: "" });
                                            H.trigger("nivo:animFinished");
                                        });
                                    } else {
                                        if (K.effect == "boxRandom" || F.randAnim == "boxRandom") {
                                            h(H, K, F);
                                            var J = K.boxCols * K.boxRows;
                                            var I = 0;
                                            var L = 0;
                                            var w = o(b(".nivo-box", H));
                                            w.each(function() {
                                                var i = b(this);
                                                if (I == J - 1) {
                                                    setTimeout(function() { i.animate({ opacity: "1" }, K.animSpeed, "", function() { H.trigger("nivo:animFinished"); }); }, (100 + L));
                                                } else {
                                                    setTimeout(function() { i.animate({ opacity: "1" }, K.animSpeed); }, (100 + L));
                                                }
                                                L += 20;
                                                I++;
                                            });
                                        } else {
                                            if (K.effect == "boxRain" || F.randAnim == "boxRain" || K.effect == "boxRainReverse" || F.randAnim == "boxRainReverse" || K.effect == "boxRainGrow" || F.randAnim == "boxRainGrow" || K.effect == "boxRainGrowReverse" || F.randAnim == "boxRainGrowReverse") {
                                                h(H, K, F);
                                                var J = K.boxCols * K.boxRows;
                                                var I = 0;
                                                var L = 0;
                                                var y = 0;
                                                var E = 0;
                                                var C = new Array();
                                                C[y] = new Array();
                                                var w = b(".nivo-box", H);
                                                if (K.effect == "boxRainReverse" || F.randAnim == "boxRainReverse" || K.effect == "boxRainGrowReverse" || F.randAnim == "boxRainGrowReverse") {
                                                    w = b(".nivo-box", H)._reverse();
                                                }
                                                w.each(function() {
                                                    C[y][E] = b(this);
                                                    E++;
                                                    if (E == K.boxCols) {
                                                        y++;
                                                        E = 0;
                                                        C[y] = new Array();
                                                    }
                                                });
                                                for (var A = 0; A < (K.boxCols * 2); A++) {
                                                    var u = A;
                                                    for (var x = 0; x < K.boxRows; x++) {
                                                        if (u >= 0 && u < K.boxCols) {
                                                            (function(S, N, R, O, T) {
                                                                var Q = b(C[S][N]);
                                                                var v = Q.width();
                                                                var P = Q.height();
                                                                if (K.effect == "boxRainGrow" || F.randAnim == "boxRainGrow" || K.effect == "boxRainGrowReverse" || F.randAnim == "boxRainGrowReverse") {
                                                                    Q.width(0).height(0);
                                                                }
                                                                if (O == T - 1) {
                                                                    setTimeout(function() { Q.animate({ opacity: "1", width: v, height: P }, K.animSpeed / 1.3, "", function() { H.trigger("nivo:animFinished"); }); }, (100 + R));
                                                                } else {
                                                                    setTimeout(function() { Q.animate({ opacity: "1", width: v, height: P }, K.animSpeed / 1.3); }, (100 + R));
                                                                }
                                                            })(x, u, L, I, J);
                                                            I++;
                                                        }
                                                        u--;
                                                    }
                                                    L += 100;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
        var o = function(u) {
            for (var v, t, w = u.length; w; v = parseInt(Math.random() * w), t = u[--w], u[w] = u[v], u[v] = t) {
            }
            return u;
        };
        var k = function(i) {
            if (this.console && typeof console.log != "undefined") {
                console.log(i);
            }
        };
        this.stop = function() {
            if (!b(m).data("nivo:vars").stop) {
                b(m).data("nivo:vars").stop = true;
                k("Stop Slider");
            }
        };
        this.start = function() {
            if (b(m).data("nivo:vars").stop) {
                b(m).data("nivo:vars").stop = false;
                k("Start Slider");
            }
        };
        j.afterLoad.call(this);
        return this;
    };
    b.fn.nivoSlider = function(c) {
        return this.each(function(e, g) {
            var d = b(this);
            if (d.data("nivoslider")) {
                return d.data("nivoslider");
            }
            var f = new a(this, c);
            d.data("nivoslider", f);
        });
    };
    b.fn.nivoSlider.defaults = {
        effect: "random",
        slices: 15,
        boxCols: 8,
        boxRows: 4,
        animSpeed: 500,
        pauseTime: 3000,
        startSlide: 0,
        directionNav: true,
        directionNavHide: true,
        controlNav: true,
        controlNavThumbs: false,
        controlNavThumbsFromRel: false,
        controlNavThumbsSearch: ".jpg",
        controlNavThumbsReplace: "_thumb.jpg",
        keyboardNav: true,
        pauseOnHover: true,
        manualAdvance: false,
        captionOpacity: 0,
        prevText: "Prev",
        nextText: "Next",
        beforeChange: function() {
        },
        afterChange: function() {
        },
        slideshowEnd: function() {
        },
        lastSlide: function() {
        },
        afterLoad: function() {
        }
    };
    b.fn._reverse = [].reverse;
})(jQuery);