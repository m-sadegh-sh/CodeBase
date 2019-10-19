
(function($) {
    var counter = 0;
    $.fn.wmd = function(_options) {
        this.each(function() {
            var defaults = { "preview": true };
            var options = $.extend({ }, _options || { }, defaults);
            if (!options.button_bar) {
                options.button_bar = "wmd-button-bar-" + counter;
                $("<div/>").attr("class", "wmd-button-bar").attr("id", options.button_bar).insertBefore(this);
            }
            if (typeof(options.preview) == "boolean" && options.preview) {
                options.preview = "wmd-preview-" + counter;
                $("<div/>").attr("class", "wmd-preview").attr("id", options.preview).insertAfter(this);
            }
            if (typeof(options.output) == "boolean" && options.output) {
                options.output = "wmd-output-" + counter;
                $("<div/>").attr("class", "wmd-output").attr("id", options.output).insertAfter(this);
            }
            this.id = this.id || "wmd-input-" + counter;
            options.input = this.id;
            setup_wmd(options);
            counter++;
        });
    };
})(jQuery);

function setup_wmd(wmd_options) {
    var Attacklab = Attacklab || { };
    wmd_options = wmd_options || top.wmd_options || { };
    Attacklab.wmdBase = function() {
        var wmd = Attacklab;
        var doc = top.document;
        var re = top.RegExp;
        var nav = top.navigator;
        wmd.Util = { };
        wmd.Position = { };
        wmd.Command = { };
        wmd.Global = { };
        wmd.buttons = { };
        wmd.showdown = top.Attacklab && top.Attacklab.showdown;
        var util = wmd.Util;
        var position = wmd.Position;
        var command = wmd.Command;
        var global = wmd.Global;
        global.isIE = /msie/.test(nav.userAgent.toLowerCase());
        global.isIE_5or6 = /msie 6/.test(nav.userAgent.toLowerCase()) || /msie 5/.test(nav.userAgent.toLowerCase());
        global.isIE_7plus = global.isIE && !global.isIE_5or6;
        global.isOpera = /opera/.test(nav.userAgent.toLowerCase());
        global.isKonqueror = /konqueror/.test(nav.userAgent.toLowerCase());
        var imageDialogText = wmd_options.imageDialogText || "<p style='margin-top: 0px'><b>Enter the image URL.</b></p><p>You can also add a title, which will be displayed as a tool tip.</p><p>Example:<br />http://wmd-editor.com/images/cloud1.jpg   \"Optional title\"</p>";
        var linkDialogText = wmd_options.linkDialogText || "<p style='margin-top: 0px'><b>Enter the web address.</b></p><p>You can also add a title, which will be displayed as a tool tip.</p><p>Example:<br />http://wmd-editor.com/   \"Optional title\"</p>";
        var imageDefaultText = "http://";
        var linkDefaultText = "http://";
        var imageDirectory = "images/";
        var previewPollInterval = 500;
        var pastePollInterval = 100;
        var helpLink = wmd_options.helpLink || "http://wmd-editor.com/";
        var helpHoverTitle = wmd_options.helpHoverTitle || "WMD website";
        var helpTarget = wmd_options.helpTarget || "_blank";
        wmd.PanelCollection = function() {
            this.buttonBar = doc.getElementById(wmd_options.button_bar || "wmd-button-bar");
            this.preview = doc.getElementById(wmd_options.preview || "wmd-preview");
            this.output = doc.getElementById(wmd_options.output || "wmd-output");
            this.input = doc.getElementById(wmd_options.input || "wmd-input");
        };
        wmd.panels = undefined;
        wmd.ieCachedRange = null;
        wmd.ieRetardedClick = false;
        util.isVisible = function(elem) {
            if (window.getComputedStyle) {
                return window.getComputedStyle(elem, null).getPropertyValue("display") !== "none";
            } else if (elem.currentStyle) {
                return elem.currentStyle["display"] !== "none";
            }
        };
        util.addEvent = function(elem, event, listener) {
            if (elem.attachEvent) {
                elem.attachEvent("on" + event, listener);
            } else {
                elem.addEventListener(event, listener, false);
            }
        };
        util.removeEvent = function(elem, event, listener) {
            if (elem.detachEvent) {
                elem.detachEvent("on" + event, listener);
            } else {
                elem.removeEventListener(event, listener, false);
            }
        };
        util.fixEolChars = function(text) {
            text = text.replace(/\r\n/g, "\n");
            text = text.replace(/\r/g, "\n");
            return text;
        };
        util.extendRegExp = function(regex, pre, post) {
            if (pre === null || pre === undefined) {
                pre = "";
            }
            if (post === null || post === undefined) {
                post = "";
            }
            var pattern = regex.toString();
            var flags = "";
            var result = pattern.match(/\/([gim]*)$/);
            if (result === null) {
                flags = result[0];
            } else {
                flags = "";
            }
            pattern = pattern.replace(/(^\/|\/[gim]*$)/g, "");
            pattern = pre + pattern + post;
            return new RegExp(pattern, flags);
        };
        util.createImage = function(img) {
            var imgPath = imageDirectory + img;
            var elem = doc.createElement("img");
            elem.className = "wmd-button";
            elem.src = imgPath;
            return elem;
        };
        util.prompt = function(text, defaultInputText, makeLinkMarkdown) {
            var dialog;
            var background;
            var input;
            if (defaultInputText === undefined) {
                defaultInputText = "";
            }
            var checkEscape = function(key) {
                var code = (key.charCode || key.keyCode);
                if (code === 27) {
                    close(true);
                }
            };
            var close = function(isCancel) {
                util.removeEvent(doc.body, "keydown", checkEscape);
                var text = input.value;
                if (isCancel) {
                    text = null;
                } else {
                    text = text.replace('http://http://', 'http://');
                    text = text.replace('http://https://', 'https://');
                    text = text.replace('http://ftp://', 'ftp://');
                    if (text.indexOf('http://') === -1 && text.indexOf('ftp://') === -1 && text.indexOf('https://') === -1) {
                        text = 'http://' + text;
                    }
                }
                dialog.parentNode.removeChild(dialog);
                background.parentNode.removeChild(background);
                makeLinkMarkdown(text);
                return false;
            };
            var createBackground = function() {
                background = doc.createElement("div");
                background.className = "wmd-prompt-background";
                style = background.style;
                style.position = "absolute";
                style.top = "0";
                style.zIndex = "1000";
                if (global.isKonqueror) {
                    style.backgroundColor = "transparent";
                } else if (global.isIE) {
                    style.filter = "alpha(opacity=50)";
                } else {
                    style.opacity = "0.5";
                }
                var pageSize = position.getPageSize();
                style.height = pageSize[1] + "px";
                if (global.isIE) {
                    style.left = doc.documentElement.scrollLeft;
                    style.width = doc.documentElement.clientWidth;
                } else {
                    style.left = "0";
                    style.width = "100%";
                }
                doc.body.appendChild(background);
            };
            var createDialog = function() {
                dialog = doc.createElement("div");
                dialog.className = "wmd-prompt-dialog";
                dialog.style.padding = "10px;";
                dialog.style.position = "fixed";
                dialog.style.width = "400px";
                dialog.style.zIndex = "1001";
                var question = doc.createElement("div");
                question.innerHTML = text;
                question.style.padding = "5px";
                dialog.appendChild(question);
                var form = doc.createElement("form");
                form.onsubmit = function() { return close(false); };
                style = form.style;
                style.padding = "0";
                style.margin = "0";
                style.cssFloat = "left";
                style.width = "100%";
                style.textAlign = "center";
                style.position = "relative";
                dialog.appendChild(form);
                input = doc.createElement("input");
                input.type = "text";
                input.value = defaultInputText;
                style = input.style;
                style.display = "block";
                style.width = "80%";
                style.marginLeft = style.marginRight = "auto";
                form.appendChild(input);
                var okButton = doc.createElement("input");
                okButton.type = "button";
                okButton.onclick = function() { return close(false); };
                okButton.value = "OK";
                style = okButton.style;
                style.margin = "10px";
                style.display = "inline";
                style.width = "7em";
                var cancelButton = doc.createElement("input");
                cancelButton.type = "button";
                cancelButton.onclick = function() { return close(true); };
                cancelButton.value = "Cancel";
                style = cancelButton.style;
                style.margin = "10px";
                style.display = "inline";
                style.width = "7em";
                if (/mac/.test(nav.platform.toLowerCase())) {
                    form.appendChild(cancelButton);
                    form.appendChild(okButton);
                } else {
                    form.appendChild(okButton);
                    form.appendChild(cancelButton);
                }
                util.addEvent(doc.body, "keydown", checkEscape);
                dialog.style.top = "50%";
                dialog.style.left = "50%";
                dialog.style.display = "block";
                if (global.isIE_5or6) {
                    dialog.style.position = "absolute";
                    dialog.style.top = doc.documentElement.scrollTop + 200 + "px";
                    dialog.style.left = "50%";
                }
                doc.body.appendChild(dialog);
                dialog.style.marginTop = -(position.getHeight(dialog) / 2) + "px";
                dialog.style.marginLeft = -(position.getWidth(dialog) / 2) + "px";
            };
            createBackground();
            top.setTimeout(function() {
                createDialog();
                var defTextLen = defaultInputText.length;
                if (input.selectionStart !== undefined) {
                    input.selectionStart = 0;
                    input.selectionEnd = defTextLen;
                } else if (input.createTextRange) {
                    var range = input.createTextRange();
                    range.collapse(false);
                    range.moveStart("character", -defTextLen);
                    range.moveEnd("character", defTextLen);
                    range.select();
                }
                input.focus();
            }, 0);
        };
        position.getTop = function(elem, isInner) {
            var result = elem.offsetTop;
            if (!isInner) {
                while (elem = elem.offsetParent) {
                    result += elem.offsetTop;
                }
            }
            return result;
        };
        position.getHeight = function(elem) { return elem.offsetHeight || elem.scrollHeight; };
        position.getWidth = function(elem) { return elem.offsetWidth || elem.scrollWidth; };
        position.getPageSize = function() {
            var scrollWidth, scrollHeight;
            var innerWidth, innerHeight;
            if (self.innerHeight && self.scrollMaxY) {
                scrollWidth = doc.body.scrollWidth;
                scrollHeight = self.innerHeight + self.scrollMaxY;
            } else if (doc.body.scrollHeight > doc.body.offsetHeight) {
                scrollWidth = doc.body.scrollWidth;
                scrollHeight = doc.body.scrollHeight;
            } else {
                scrollWidth = doc.body.offsetWidth;
                scrollHeight = doc.body.offsetHeight;
            }
            if (self.innerHeight) {
                innerWidth = self.innerWidth;
                innerHeight = self.innerHeight;
            } else if (doc.documentElement && doc.documentElement.clientHeight) {
                innerWidth = doc.documentElement.clientWidth;
                innerHeight = doc.documentElement.clientHeight;
            } else if (doc.body) {
                innerWidth = doc.body.clientWidth;
                innerHeight = doc.body.clientHeight;
            }
            var maxWidth = Math.max(scrollWidth, innerWidth);
            var maxHeight = Math.max(scrollHeight, innerHeight);
            return [maxWidth, maxHeight, innerWidth, innerHeight];
        };
        wmd.inputPoller = function(callback, interval) {
            var pollerObj = this;
            var inputArea = wmd.panels.input;
            var lastStart;
            var lastEnd;
            var markdown;
            var killHandle;
            this.tick = function() {
                if (!util.isVisible(inputArea)) {
                    return;
                }
                if (inputArea.selectionStart || inputArea.selectionStart === 0) {
                    var start = inputArea.selectionStart;
                    var end = inputArea.selectionEnd;
                    if (start != lastStart || end != lastEnd) {
                        lastStart = start;
                        lastEnd = end;
                        if (markdown != inputArea.value) {
                            markdown = inputArea.value;
                            return true;
                        }
                    }
                }
                return false;
            };
            var doTickCallback = function() {
                if (!util.isVisible(inputArea)) {
                    return;
                }
                if (pollerObj.tick()) {
                    callback();
                }
            };
            var assignInterval = function() { killHandle = top.setInterval(doTickCallback, interval); };
            this.destroy = function() { top.clearInterval(killHandle); };
            assignInterval();
        };
        wmd.undoManager = function(callback) {
            var undoObj = this;
            var undoStack = [];
            var stackPtr = 0;
            var mode = "none";
            var lastState;
            var poller;
            var timer;
            var inputStateObj;
            var setMode = function(newMode, noSave) {
                if (mode != newMode) {
                    mode = newMode;
                    if (!noSave) {
                        saveState();
                    }
                }
                if (!global.isIE || mode != "moving") {
                    timer = top.setTimeout(refreshState, 1);
                } else {
                    inputStateObj = null;
                }
            };
            var refreshState = function() {
                inputStateObj = new wmd.TextareaState();
                poller.tick();
                timer = undefined;
            };
            this.setCommandMode = function() {
                mode = "command";
                saveState();
                timer = top.setTimeout(refreshState, 0);
            };
            this.canUndo = function() { return stackPtr > 1; };
            this.canRedo = function() {
                if (undoStack[stackPtr + 1]) {
                    return true;
                }
                return false;
            };
            this.undo = function() {
                if (undoObj.canUndo()) {
                    if (lastState) {
                        lastState.restore();
                        lastState = null;
                    } else {
                        undoStack[stackPtr] = new wmd.TextareaState();
                        undoStack[--stackPtr].restore();
                        if (callback) {
                            callback();
                        }
                    }
                }
                mode = "none";
                wmd.panels.input.focus();
                refreshState();
            };
            this.redo = function() {
                if (undoObj.canRedo()) {
                    undoStack[++stackPtr].restore();
                    if (callback) {
                        callback();
                    }
                }
                mode = "none";
                wmd.panels.input.focus();
                refreshState();
            };
            var saveState = function() {
                var currState = inputStateObj || new wmd.TextareaState();
                if (!currState) {
                    return false;
                }
                if (mode == "moving") {
                    if (!lastState) {
                        lastState = currState;
                    }
                    return;
                }
                if (lastState) {
                    if (undoStack[stackPtr - 1].text != lastState.text) {
                        undoStack[stackPtr++] = lastState;
                    }
                    lastState = null;
                }
                undoStack[stackPtr++] = currState;
                undoStack[stackPtr + 1] = null;
                if (callback) {
                    callback();
                }
            };
            var handleCtrlYZ = function(event) {
                var handled = false;
                if (event.ctrlKey || event.metaKey) {
                    var keyCode = event.charCode || event.keyCode;
                    var keyCodeChar = String.fromCharCode(keyCode);
                    switch (keyCodeChar) {
                    case "y":
                        undoObj.redo();
                        handled = true;
                        break;
                    case "z":
                        if (!event.shiftKey) {
                            undoObj.undo();
                        } else {
                            undoObj.redo();
                        }
                        handled = true;
                        break;
                    }
                }
                if (handled) {
                    if (event.preventDefault) {
                        event.preventDefault();
                    }
                    if (top.event) {
                        top.event.returnValue = false;
                    }
                    return;
                }
            };
            var handleModeChange = function(event) {
                if (!event.ctrlKey && !event.metaKey) {
                    var keyCode = event.keyCode;
                    if ((keyCode >= 33 && keyCode <= 40) || (keyCode >= 63232 && keyCode <= 63235)) {
                        setMode("moving");
                    } else if (keyCode == 8 || keyCode == 46 || keyCode == 127) {
                        setMode("deleting");
                    } else if (keyCode == 13) {
                        setMode("newlines");
                    } else if (keyCode == 27) {
                        setMode("escape");
                    } else if ((keyCode < 16 || keyCode > 20) && keyCode != 91) {
                        setMode("typing");
                    }
                }
            };
            var setEventHandlers = function() {
                util.addEvent(wmd.panels.input, "keypress", function(event) {
                    if ((event.ctrlKey || event.metaKey) && (event.keyCode == 89 || event.keyCode == 90)) {
                        event.preventDefault();
                    }
                });
                var handlePaste = function() {
                    if (global.isIE || (inputStateObj && inputStateObj.text != wmd.panels.input.value)) {
                        if (timer == undefined) {
                            mode = "paste";
                            saveState();
                            refreshState();
                        }
                    }
                };
                poller = new wmd.inputPoller(handlePaste, pastePollInterval);
                util.addEvent(wmd.panels.input, "keydown", handleCtrlYZ);
                util.addEvent(wmd.panels.input, "keydown", handleModeChange);
                util.addEvent(wmd.panels.input, "mousedown", function() { setMode("moving"); });
                wmd.panels.input.onpaste = handlePaste;
                wmd.panels.input.ondrop = handlePaste;
            };
            var init = function() {
                setEventHandlers();
                refreshState();
                saveState();
            };
            this.destroy = function() {
                if (poller) {
                    poller.destroy();
                }
            };
            init();
        };
        wmd.editor = function(previewRefreshCallback) {
            if (!previewRefreshCallback) {
                previewRefreshCallback = function() {
                };
            }
            var inputBox = wmd.panels.input;
            var offsetHeight = 0;
            var editObj = this;
            var mainDiv;
            var mainSpan;
            var div;
            var creationHandle;
            var undoMgr;
            var doClick = function(button) {
                inputBox.focus();
                if (button.textOp) {
                    if (undoMgr) {
                        undoMgr.setCommandMode();
                    }
                    var state = new wmd.TextareaState();
                    if (!state) {
                        return;
                    }
                    var chunks = state.getChunks();
                    var fixupInputArea = function() {
                        inputBox.focus();
                        if (chunks) {
                            state.setChunks(chunks);
                        }
                        state.restore();
                        previewRefreshCallback();
                    };
                    var useDefaultText = true;
                    var noCleanup = button.textOp(chunks, fixupInputArea, useDefaultText);
                    if (!noCleanup) {
                        fixupInputArea();
                    }
                }
                if (button.execute) {
                    button.execute(editObj);
                }
            };
            var setUndoRedoButtonStates = function() {
                if (undoMgr) {
                    setupButton(wmd.buttons["wmd-undo-button"], undoMgr.canUndo());
                    setupButton(wmd.buttons["wmd-redo-button"], undoMgr.canRedo());
                }
            };
            var setupButton = function(button, isEnabled) {
                var normalYShift = "0px";
                var disabledYShift = "-20px";
                var highlightYShift = "-40px";
                if (isEnabled) {
                    button.style.backgroundPosition = button.XShift + " " + normalYShift;
                    button.onmouseover = function() { this.style.backgroundPosition = this.XShift + " " + highlightYShift; };
                    button.onmouseout = function() { this.style.backgroundPosition = this.XShift + " " + normalYShift; };
                    if (global.isIE) {
                        button.onmousedown = function() {
                            wmd.ieRetardedClick = true;
                            wmd.ieCachedRange = document.selection.createRange();
                        };
                    }
                    if (!button.isHelp) {
                        button.onclick = function() {
                            if (this.onmouseout) {
                                this.onmouseout();
                            }
                            doClick(this);
                            return false;
                        };
                    }
                } else {
                    button.style.backgroundPosition = button.XShift + " " + disabledYShift;
                    button.onmouseover = button.onmouseout = button.onclick = function() {
                    };
                }
            };
            var makeSpritedButtonRow = function() {
                var buttonBar = document.getElementById(wmd_options.button_bar || "wmd-button-bar");
                var normalYShift = "0px";
                var disabledYShift = "-20px";
                var highlightYShift = "-40px";
                var buttonRow = document.createElement("ul");
                buttonRow.className = "wmd-button-row";
                buttonRow = buttonBar.appendChild(buttonRow);
                var xoffset = 0;

                function createButton(name, title, textOp) {
                    var button = document.createElement("li");
                    wmd.buttons[name] = button;
                    button.className = "wmd-button " + name;
                    button.XShift = xoffset + "px";
                    xoffset -= 20;
                    if (title)
                        button.title = title;
                    if (textOp)
                        button.textOp = textOp;
                    return button;
                }

                function addButton(name, title, textOp) {
                    var button = createButton(name, title, textOp);
                    setupButton(button, true);
                    buttonRow.appendChild(button);
                    return button;
                }

                function addSpacer() {
                    var spacer = document.createElement("li");
                    spacer.className = "wmd-spacer";
                    buttonRow.appendChild(spacer);
                    return spacer;
                }

                var boldButton = addButton("wmd-bold-button", "Strong <strong> Ctrl+B", command.doBold);
                var italicButton = addButton("wmd-italic-button", "Emphasis <em> Ctrl+I", command.doItalic);
                var spacer1 = addSpacer();
                var linkButton = addButton("wmd-link-button", "Hyperlink <a> Ctrl+L", function(chunk, postProcessing, useDefaultText) { return command.doLinkOrImage(chunk, postProcessing, false); });
                var quoteButton = addButton("wmd-quote-button", "Blockquote <blockquote> Ctrl+Q", command.doBlockquote);
                var codeButton = addButton("wmd-code-button", "Code Sample <pre><code> Ctrl+K", command.doCode);
                var imageButton = addButton("wmd-image-button", "Image <img> Ctrl+G", function(chunk, postProcessing, useDefaultText) { return command.doLinkOrImage(chunk, postProcessing, true); });
                var spacer2 = addSpacer();
                var olistButton = addButton("wmd-olist-button", "Numbered List <ol> Ctrl+O", function(chunk, postProcessing, useDefaultText) { command.doList(chunk, postProcessing, true, useDefaultText); });
                var ulistButton = addButton("wmd-ulist-button", "Bulleted List <ul> Ctrl+U", function(chunk, postProcessing, useDefaultText) { command.doList(chunk, postProcessing, false, useDefaultText); });
                var headingButton = addButton("wmd-heading-button", "Heading <h1>/<h2> Ctrl+H", command.doHeading);
                var hrButton = addButton("wmd-hr-button", "Horizontal Rule <hr> Ctrl+R", command.doHorizontalRule);
                var spacer3 = addSpacer();
                var undoButton = addButton("wmd-undo-button", "Undo - Ctrl+Z");
                undoButton.execute = function(manager) { manager.undo(); };
                var redo_title = null;
                var redoButton = addButton("wmd-redo-button", "Redo - Ctrl+Y");
                if (/win/.test(nav.platform.toLowerCase())) {
                    redoButton.title = "Redo - Ctrl+Y";
                } else {
                    redoButton.title = "Redo - Ctrl+Shift+Z";
                }
                redoButton.execute = function(manager) { manager.redo(); };
                var helpButton = createButton("wmd-help-button");
                helpButton.isHelp = true;
                setupButton(helpButton, true);
                buttonRow.appendChild(helpButton);
                var helpAnchor = document.createElement("a");
                helpAnchor.href = helpLink;
                helpAnchor.target = helpTarget;
                helpAnchor.title = helpHoverTitle;
                helpButton.appendChild(helpAnchor);
                setUndoRedoButtonStates();
            };
            var setupEditor = function() {
                if (/\?noundo/.test(doc.location.href)) {
                    wmd.nativeUndo = true;
                }
                if (!wmd.nativeUndo) {
                    undoMgr = new wmd.undoManager(function() {
                        previewRefreshCallback();
                        setUndoRedoButtonStates();
                    });
                }
                makeSpritedButtonRow();
                var keyEvent = "keydown";
                if (global.isOpera) {
                    keyEvent = "keypress";
                }
                util.addEvent(inputBox, keyEvent, function(key) {
                    if (key.ctrlKey || key.metaKey) {
                        var keyCode = key.charCode || key.keyCode;
                        var keyCodeStr = String.fromCharCode(keyCode).toLowerCase();
                        switch (keyCodeStr) {
                        case "b":
                            doClick(wmd.buttons["wmd-bold-button"]);
                            break;
                        case "i":
                            doClick(wmd.buttons["wmd-italic-button"]);
                            break;
                        case "l":
                            doClick(wmd.buttons["wmd-link-button"]);
                            break;
                        case "q":
                            doClick(wmd.buttons["wmd-quote-button"]);
                            break;
                        case "k":
                            doClick(wmd.buttons["wmd-code-button"]);
                            break;
                        case "g":
                            doClick(wmd.buttons["wmd-image-button"]);
                            break;
                        case "o":
                            doClick(wmd.buttons["wmd-olist-button"]);
                            break;
                        case "u":
                            doClick(wmd.buttons["wmd-ulist-button"]);
                            break;
                        case "h":
                            doClick(wmd.buttons["wmd-heading-button"]);
                            break;
                        case "r":
                            doClick(wmd.buttons["wmd-hr-button"]);
                            break;
                        case "y":
                            doClick(wmd.buttons["wmd-redo-button"]);
                            break;
                        case "z":
                            if (key.shiftKey) {
                                doClick(wmd.buttons["wmd-redo-button"]);
                            } else {
                                doClick(wmd.buttons["wmd-undo-button"]);
                            }
                            break;
                        default:
                            return;
                        }
                        if (key.preventDefault) {
                            key.preventDefault();
                        }
                        if (top.event) {
                            top.event.returnValue = false;
                        }
                    }
                });
                util.addEvent(inputBox, "keyup", function(key) {
                    if (!key.shiftKey && !key.ctrlKey && !key.metaKey) {
                        var keyCode = key.charCode || key.keyCode;
                        if (keyCode === 13) {
                            fakeButton = { };
                            fakeButton.textOp = command.doAutoindent;
                            doClick(fakeButton);
                        }
                    }
                });
                if (global.isIE) {
                    util.addEvent(inputBox, "keydown", function(key) {
                        var code = key.keyCode;
                        if (code === 27) {
                            return false;
                        }
                    });
                }
                if (inputBox.form) {
                    var submitCallback = inputBox.form.onsubmit;
                    inputBox.form.onsubmit = function() {
                        convertToHtml();
                        if (submitCallback) {
                            return submitCallback.apply(this, arguments);
                        }
                    };
                }
            };
            var convertToHtml = function() {
                if (wmd.showdown) {
                    var markdownConverter = new wmd.showdown.converter();
                }
                var text = inputBox.value;
                var callback = function() { inputBox.value = text; };
                if (!/markdown/.test(wmd.wmd_env.output_format.toLowerCase())) {
                    if (markdownConverter) {
                        inputBox.value = markdownConverter.makeHtml(text);
                        top.setTimeout(callback, 0);
                    }
                }
                return true;
            };
            this.undo = function() {
                if (undoMgr) {
                    undoMgr.undo();
                }
            };
            this.redo = function() {
                if (undoMgr) {
                    undoMgr.redo();
                }
            };
            var init = function() { setupEditor(); };
            this.destroy = function() {
                if (undoMgr) {
                    undoMgr.destroy();
                }
                if (div.parentNode) {
                    div.parentNode.removeChild(div);
                }
                if (inputBox) {
                    inputBox.style.marginTop = "";
                }
                top.clearInterval(creationHandle);
            };
            init();
        };
        wmd.TextareaState = function() {
            var stateObj = this;
            var inputArea = wmd.panels.input;
            this.init = function() {
                if (!util.isVisible(inputArea)) {
                    return;
                }
                this.setInputAreaSelectionStartEnd();
                this.scrollTop = inputArea.scrollTop;
                if (!this.text && inputArea.selectionStart || inputArea.selectionStart === 0) {
                    this.text = inputArea.value;
                }
            };
            this.setInputAreaSelection = function() {
                if (!util.isVisible(inputArea)) {
                    return;
                }
                if (inputArea.selectionStart !== undefined && !global.isOpera) {
                    inputArea.focus();
                    inputArea.selectionStart = stateObj.start;
                    inputArea.selectionEnd = stateObj.end;
                    inputArea.scrollTop = stateObj.scrollTop;
                } else if (doc.selection) {
                    if (doc.activeElement && doc.activeElement !== inputArea) {
                        return;
                    }
                    inputArea.focus();
                    var range = inputArea.createTextRange();
                    range.moveStart("character", -inputArea.value.length);
                    range.moveEnd("character", -inputArea.value.length);
                    range.moveEnd("character", stateObj.end);
                    range.moveStart("character", stateObj.start);
                    range.select();
                }
            };
            this.setInputAreaSelectionStartEnd = function() {
                if (inputArea.selectionStart || inputArea.selectionStart === 0) {
                    stateObj.start = inputArea.selectionStart;
                    stateObj.end = inputArea.selectionEnd;
                } else if (doc.selection) {
                    stateObj.text = util.fixEolChars(inputArea.value);
                    var range;
                    if (wmd.ieRetardedClick && wmd.ieCachedRange) {
                        range = wmd.ieCachedRange;
                        wmd.ieRetardedClick = false;
                    } else {
                        range = doc.selection.createRange();
                    }
                    var fixedRange = util.fixEolChars(range.text);
                    var marker = "\x07";
                    var markedRange = marker + fixedRange + marker;
                    range.text = markedRange;
                    var inputText = util.fixEolChars(inputArea.value);
                    range.moveStart("character", -markedRange.length);
                    range.text = fixedRange;
                    stateObj.start = inputText.indexOf(marker);
                    stateObj.end = inputText.lastIndexOf(marker) - marker.length;
                    var len = stateObj.text.length - util.fixEolChars(inputArea.value).length;
                    if (len) {
                        range.moveStart("character", -fixedRange.length);
                        while (len--) {
                            fixedRange += "\n";
                            stateObj.end += 1;
                        }
                        range.text = fixedRange;
                    }
                    this.setInputAreaSelection();
                }
            };
            this.restore = function() {
                if (stateObj.text != undefined && stateObj.text != inputArea.value) {
                    inputArea.value = stateObj.text;
                }
                this.setInputAreaSelection();
                inputArea.scrollTop = stateObj.scrollTop;
            };
            this.getChunks = function() {
                var chunk = new wmd.Chunks();
                chunk.before = util.fixEolChars(stateObj.text.substring(0, stateObj.start));
                chunk.startTag = "";
                chunk.selection = util.fixEolChars(stateObj.text.substring(stateObj.start, stateObj.end));
                chunk.endTag = "";
                chunk.after = util.fixEolChars(stateObj.text.substring(stateObj.end));
                chunk.scrollTop = stateObj.scrollTop;
                return chunk;
            };
            this.setChunks = function(chunk) {
                chunk.before = chunk.before + chunk.startTag;
                chunk.after = chunk.endTag + chunk.after;
                if (global.isOpera) {
                    chunk.before = chunk.before.replace(/\n/g, "\r\n");
                    chunk.selection = chunk.selection.replace(/\n/g, "\r\n");
                    chunk.after = chunk.after.replace(/\n/g, "\r\n");
                }
                this.start = chunk.before.length;
                this.end = chunk.before.length + chunk.selection.length;
                this.text = chunk.before + chunk.selection + chunk.after;
                this.scrollTop = chunk.scrollTop;
            };
            this.init();
        };
        wmd.Chunks = function() {
        };
        wmd.Chunks.prototype.findTags = function(startRegex, endRegex) {
            var chunkObj = this;
            var regex;
            if (startRegex) {
                regex = util.extendRegExp(startRegex, "", "$");
                this.before = this.before.replace(regex, function(match) {
                    chunkObj.startTag = chunkObj.startTag + match;
                    return "";
                });
                regex = util.extendRegExp(startRegex, "^", "");
                this.selection = this.selection.replace(regex, function(match) {
                    chunkObj.startTag = chunkObj.startTag + match;
                    return "";
                });
            }
            if (endRegex) {
                regex = util.extendRegExp(endRegex, "", "$");
                this.selection = this.selection.replace(regex, function(match) {
                    chunkObj.endTag = match + chunkObj.endTag;
                    return "";
                });
                regex = util.extendRegExp(endRegex, "^", "");
                this.after = this.after.replace(regex, function(match) {
                    chunkObj.endTag = match + chunkObj.endTag;
                    return "";
                });
            }
        };
        wmd.Chunks.prototype.trimWhitespace = function(remove) {
            this.selection = this.selection.replace(/^(\s*)/, "");
            if (!remove) {
                this.before += re.$1;
            }
            this.selection = this.selection.replace(/(\s*)$/, "");
            if (!remove) {
                this.after = re.$1 + this.after;
            }
        };
        wmd.Chunks.prototype.addBlankLines = function(nLinesBefore, nLinesAfter, findExtraNewlines) {
            if (nLinesBefore === undefined) {
                nLinesBefore = 1;
            }
            if (nLinesAfter === undefined) {
                nLinesAfter = 1;
            }
            nLinesBefore++;
            nLinesAfter++;
            var regexText;
            var replacementText;
            this.selection = this.selection.replace(/(^\n*)/, "");
            this.startTag = this.startTag + re.$1;
            this.selection = this.selection.replace(/(\n*$)/, "");
            this.endTag = this.endTag + re.$1;
            this.startTag = this.startTag.replace(/(^\n*)/, "");
            this.before = this.before + re.$1;
            this.endTag = this.endTag.replace(/(\n*$)/, "");
            this.after = this.after + re.$1;
            if (this.before) {
                regexText = replacementText = "";
                while (nLinesBefore--) {
                    regexText += "\\n?";
                    replacementText += "\n";
                }
                if (findExtraNewlines) {
                    regexText = "\\n*";
                }
                this.before = this.before.replace(new re(regexText + "$", ""), replacementText);
            }
            if (this.after) {
                regexText = replacementText = "";
                while (nLinesAfter--) {
                    regexText += "\\n?";
                    replacementText += "\n";
                }
                if (findExtraNewlines) {
                    regexText = "\\n*";
                }
                this.after = this.after.replace(new re(regexText, ""), replacementText);
            }
        };
        command.prefixes = "(?:\\s{4,}|\\s*>|\\s*-\\s+|\\s*\\d+\\.|=|\\+|-|_|\\*|#|\\s*\\[[^\n]]+\\]:)";
        command.unwrap = function(chunk) {
            var txt = new re("([^\\n])\\n(?!(\\n|" + command.prefixes + "))", "g");
            chunk.selection = chunk.selection.replace(txt, "$1 $2");
        };
        command.wrap = function(chunk, len) {
            command.unwrap(chunk);
            var regex = new re("(.{1," + len + "})( +|$\\n?)", "gm");
            chunk.selection = chunk.selection.replace(regex, function(line, marked) {
                if (new re("^" + command.prefixes, "").test(line)) {
                    return line;
                }
                return marked + "\n";
            });
            chunk.selection = chunk.selection.replace(/\s+$/, "");
        };
        command.doBold = function(chunk, postProcessing, useDefaultText) { return command.doBorI(chunk, 2, "strong text"); };
        command.doItalic = function(chunk, postProcessing, useDefaultText) { return command.doBorI(chunk, 1, "emphasized text"); };
        command.doBorI = function(chunk, nStars, insertText) {
            chunk.trimWhitespace();
            chunk.selection = chunk.selection.replace(/\n{2,}/g, "\n");
            chunk.before.search(/(\**$)/);
            var starsBefore = re.$1;
            chunk.after.search(/(^\**)/);
            var starsAfter = re.$1;
            var prevStars = Math.min(starsBefore.length, starsAfter.length);
            if ((prevStars >= nStars) && (prevStars != 2 || nStars != 1)) {
                chunk.before = chunk.before.replace(re("[*]{" + nStars + "}$", ""), "");
                chunk.after = chunk.after.replace(re("^[*]{" + nStars + "}", ""), "");
            } else if (!chunk.selection && starsAfter) {
                chunk.after = chunk.after.replace(/^([*_]*)/, "");
                chunk.before = chunk.before.replace(/(\s?)$/, "");
                var whitespace = re.$1;
                chunk.before = chunk.before + starsAfter + whitespace;
            } else {
                if (!chunk.selection && !starsAfter) {
                    chunk.selection = insertText;
                }
                var markup = nStars <= 1 ? "*" : "**";
                chunk.before = chunk.before + markup;
                chunk.after = markup + chunk.after;
            }
            return;
        };
        command.stripLinkDefs = function(text, defsToAdd) {
            text = text.replace(/^[ ]{0,3}\[(\d+)\]:[ \t]*\n?[ \t]*<?(\S+?)>?[ \t]*\n?[ \t]*(?:(\n*)["(](.+?)[")][ \t]*)?(?:\n+|$)/gm, function(totalMatch, id, link, newlines, title) {
                defsToAdd[id] = totalMatch.replace(/\s*$/, "");
                if (newlines) {
                    defsToAdd[id] = totalMatch.replace(/["(](.+?)[")]$/, "");
                    return newlines + title;
                }
                return "";
            });
            return text;
        };
        command.addLinkDef = function(chunk, linkDef) {
            var refNumber = 0;
            var defsToAdd = { };
            chunk.before = command.stripLinkDefs(chunk.before, defsToAdd);
            chunk.selection = command.stripLinkDefs(chunk.selection, defsToAdd);
            chunk.after = command.stripLinkDefs(chunk.after, defsToAdd);
            var defs = "";
            var regex = /(\[(?:\[[^\]]*\]|[^\[\]])*\][ ]?(?:\n[ ]*)?\[)(\d+)(\])/g;
            var addDefNumber = function(def) {
                refNumber++;
                def = def.replace(/^[ ]{0,3}\[(\d+)\]:/, "  [" + refNumber + "]:");
                defs += "\n" + def;
            };
            var getLink = function(wholeMatch, link, id, end) {
                if (defsToAdd[id]) {
                    addDefNumber(defsToAdd[id]);
                    return link + refNumber + end;
                }
                return wholeMatch;
            };
            chunk.before = chunk.before.replace(regex, getLink);
            if (linkDef) {
                addDefNumber(linkDef);
            } else {
                chunk.selection = chunk.selection.replace(regex, getLink);
            }
            var refOut = refNumber;
            chunk.after = chunk.after.replace(regex, getLink);
            if (chunk.after) {
                chunk.after = chunk.after.replace(/\n*$/, "");
            }
            if (!chunk.after) {
                chunk.selection = chunk.selection.replace(/\n*$/, "");
            }
            chunk.after += "\n\n" + defs;
            return refOut;
        };
        command.doLinkOrImage = function(chunk, postProcessing, isImage) {
            chunk.trimWhitespace();
            chunk.findTags(/\s*!?\[/, /\][ ]?(?:\n[ ]*)?(\[.*?\])?/);
            if (chunk.endTag.length > 1) {
                chunk.startTag = chunk.startTag.replace(/!?\[/, "");
                chunk.endTag = "";
                command.addLinkDef(chunk, null);
            } else {
                if (/\n\n/.test(chunk.selection)) {
                    command.addLinkDef(chunk, null);
                    return;
                }
                var makeLinkMarkdown = function(link) {
                    if (link !== null) {
                        chunk.startTag = chunk.endTag = "";
                        var linkDef = " [999]: " + link;
                        var num = command.addLinkDef(chunk, linkDef);
                        chunk.startTag = isImage ? "![" : "[";
                        chunk.endTag = "][" + num + "]";
                        if (!chunk.selection) {
                            if (isImage) {
                                chunk.selection = "alt text";
                            } else {
                                chunk.selection = "link text";
                            }
                        }
                    }
                    postProcessing();
                };
                if (isImage) {
                    util.prompt(imageDialogText, imageDefaultText, makeLinkMarkdown);
                } else {
                    util.prompt(linkDialogText, linkDefaultText, makeLinkMarkdown);
                }
                return true;
            }
        };
        util.makeAPI = function() {
            wmd.wmd = { };
            wmd.wmd.editor = wmd.editor;
            wmd.wmd.previewManager = wmd.previewManager;
        };
        util.startEditor = function() {
            if (wmd.wmd_env.autostart === false) {
                util.makeAPI();
                return;
            }
            var edit;
            var previewMgr;
            var loadListener = function() {
                wmd.panels = new wmd.PanelCollection();
                previewMgr = new wmd.previewManager();
                var previewRefreshCallback = previewMgr.refresh;
                edit = new wmd.editor(previewRefreshCallback);
                previewMgr.refresh(true);
            };
            util.addEvent(top, "load", loadListener);
        };
        wmd.previewManager = function() {
            var managerObj = this;
            var converter;
            var poller;
            var timeout;
            var elapsedTime;
            var oldInputText;
            var htmlOut;
            var maxDelay = 3000;
            var startType = "delayed";
            var setupEvents = function(inputElem, listener) {
                util.addEvent(inputElem, "input", listener);
                inputElem.onpaste = listener;
                inputElem.ondrop = listener;
                util.addEvent(inputElem, "keypress", listener);
                util.addEvent(inputElem, "keydown", listener);
                poller = new wmd.inputPoller(listener, previewPollInterval);
            };
            var getDocScrollTop = function() {
                var result = 0;
                if (top.innerHeight) {
                    result = top.pageYOffset;
                } else if (doc.documentElement && doc.documentElement.scrollTop) {
                    result = doc.documentElement.scrollTop;
                } else if (doc.body) {
                    result = doc.body.scrollTop;
                }
                return result;
            };
            var makePreviewHtml = function() {
                if (!wmd.panels.preview && !wmd.panels.output) {
                    return;
                }
                var text = wmd.panels.input.value;
                if (text && text == oldInputText) {
                    return;
                } else {
                    oldInputText = text;
                }
                var prevTime = new Date().getTime();
                if (!converter && wmd.showdown) {
                    converter = new wmd.showdown.converter();
                }
                if (converter) {
                    text = converter.makeHtml(text);
                }
                var currTime = new Date().getTime();
                elapsedTime = currTime - prevTime;
                pushPreviewHtml(text);
                htmlOut = text;
            };
            var applyTimeout = function() {
                if (timeout) {
                    top.clearTimeout(timeout);
                    timeout = undefined;
                }
                if (startType !== "manual") {
                    var delay = 0;
                    if (startType === "delayed") {
                        delay = elapsedTime;
                    }
                    if (delay > maxDelay) {
                        delay = maxDelay;
                    }
                    timeout = top.setTimeout(makePreviewHtml, delay);
                }
            };
            var getScaleFactor = function(panel) {
                if (panel.scrollHeight <= panel.clientHeight) {
                    return 1;
                }
                return panel.scrollTop / (panel.scrollHeight - panel.clientHeight);
            };
            var setPanelScrollTops = function() {
                if (wmd.panels.preview) {
                    wmd.panels.preview.scrollTop = (wmd.panels.preview.scrollHeight - wmd.panels.preview.clientHeight) * getScaleFactor(wmd.panels.preview);
                    ;
                }
                if (wmd.panels.output) {
                    wmd.panels.output.scrollTop = (wmd.panels.output.scrollHeight - wmd.panels.output.clientHeight) * getScaleFactor(wmd.panels.output);
                    ;
                }
            };
            this.refresh = function(requiresRefresh) {
                if (requiresRefresh) {
                    oldInputText = "";
                    makePreviewHtml();
                } else {
                    applyTimeout();
                }
            };
            this.processingTime = function() { return elapsedTime; };
            this.output = function() { return htmlOut; };
            this.setUpdateMode = function(mode) {
                startType = mode;
                managerObj.refresh();
            };
            var isFirstTimeFilled = true;
            var pushPreviewHtml = function(text) {
                var emptyTop = position.getTop(wmd.panels.input) - getDocScrollTop();
                if (wmd.panels.output) {
                    if (wmd.panels.output.value !== undefined) {
                        wmd.panels.output.value = text;
                        wmd.panels.output.readOnly = true;
                    } else {
                        var newText = text.replace(/&/g, "&amp;");
                        newText = newText.replace(/</g, "&lt;");
                        wmd.panels.output.innerHTML = "<pre><code>" + newText + "</code></pre>";
                    }
                }
                if (wmd.panels.preview) {
                    wmd.panels.preview.innerHTML = text;
                }
                setPanelScrollTops();
                if (isFirstTimeFilled) {
                    isFirstTimeFilled = false;
                    return;
                }
                var fullTop = position.getTop(wmd.panels.input) - getDocScrollTop();
                if (global.isIE) {
                    top.setTimeout(function() { top.scrollBy(0, fullTop - emptyTop); }, 0);
                } else {
                    top.scrollBy(0, fullTop - emptyTop);
                }
            };
            var init = function() {
                setupEvents(wmd.panels.input, applyTimeout);
                makePreviewHtml();
                if (wmd.panels.preview) {
                    wmd.panels.preview.scrollTop = 0;
                }
                if (wmd.panels.output) {
                    wmd.panels.output.scrollTop = 0;
                }
            };
            this.destroy = function() {
                if (poller) {
                    poller.destroy();
                }
            };
            init();
        };
        command.doAutoindent = function(chunk, postProcessing, useDefaultText) {
            chunk.before = chunk.before.replace(/(\n|^)[ ]{0,3}([*+-]|\d+[.])[ \t]*\n$/, "\n\n");
            chunk.before = chunk.before.replace(/(\n|^)[ ]{0,3}>[ \t]*\n$/, "\n\n");
            chunk.before = chunk.before.replace(/(\n|^)[ \t]+\n$/, "\n\n");
            useDefaultText = false;
            if (/(\n|^)[ ]{0,3}([*+-])[ \t]+.*\n$/.test(chunk.before)) {
                if (command.doList) {
                    command.doList(chunk, postProcessing, false, true);
                }
            }
            if (/(\n|^)[ ]{0,3}(\d+[.])[ \t]+.*\n$/.test(chunk.before)) {
                if (command.doList) {
                    command.doList(chunk, postProcessing, true, true);
                }
            }
            if (/(\n|^)[ ]{0,3}>[ \t]+.*\n$/.test(chunk.before)) {
                if (command.doBlockquote) {
                    command.doBlockquote(chunk, postProcessing, useDefaultText);
                }
            }
            if (/(\n|^)(\t|[ ]{4,}).*\n$/.test(chunk.before)) {
                if (command.doCode) {
                    command.doCode(chunk, postProcessing, useDefaultText);
                }
            }
        };
        command.doBlockquote = function(chunk, postProcessing, useDefaultText) {
            chunk.selection = chunk.selection.replace(/^(\n*)([^\r]+?)(\n*)$/, function(totalMatch, newlinesBefore, text, newlinesAfter) {
                chunk.before += newlinesBefore;
                chunk.after = newlinesAfter + chunk.after;
                return text;
            });
            chunk.before = chunk.before.replace(/(>[ \t]*)$/, function(totalMatch, blankLine) {
                chunk.selection = blankLine + chunk.selection;
                return "";
            });
            var defaultText = useDefaultText ? "Blockquote" : "";
            chunk.selection = chunk.selection.replace(/^(\s|>)+$/, "");
            chunk.selection = chunk.selection || defaultText;
            if (chunk.before) {
                chunk.before = chunk.before.replace(/\n?$/, "\n");
            }
            if (chunk.after) {
                chunk.after = chunk.after.replace(/^\n?/, "\n");
            }
            chunk.before = chunk.before.replace(/(((\n|^)(\n[ \t]*)*>(.+\n)*.*)+(\n[ \t]*)*$)/, function(totalMatch) {
                chunk.startTag = totalMatch;
                return "";
            });
            chunk.after = chunk.after.replace(/^(((\n|^)(\n[ \t]*)*>(.+\n)*.*)+(\n[ \t]*)*)/, function(totalMatch) {
                chunk.endTag = totalMatch;
                return "";
            });
            var replaceBlanksInTags = function(useBracket) {
                var replacement = useBracket ? "> " : "";
                if (chunk.startTag) {
                    chunk.startTag = chunk.startTag.replace(/\n((>|\s)*)\n$/, function(totalMatch, markdown) { return "\n" + markdown.replace(/^[ ]{0,3}>?[ \t]*$/gm, replacement) + "\n"; });
                }
                if (chunk.endTag) {
                    chunk.endTag = chunk.endTag.replace(/^\n((>|\s)*)\n/, function(totalMatch, markdown) { return "\n" + markdown.replace(/^[ ]{0,3}>?[ \t]*$/gm, replacement) + "\n"; });
                }
            };
            if (/^(?![ ]{0,3}>)/m.test(chunk.selection)) {
                command.wrap(chunk, wmd.wmd_env.lineLength - 2);
                chunk.selection = chunk.selection.replace(/^/gm, "> ");
                replaceBlanksInTags(true);
                chunk.addBlankLines();
            } else {
                chunk.selection = chunk.selection.replace(/^[ ]{0,3}> ?/gm, "");
                command.unwrap(chunk);
                replaceBlanksInTags(false);
                if (!/^(\n|^)[ ]{0,3}>/.test(chunk.selection) && chunk.startTag) {
                    chunk.startTag = chunk.startTag.replace(/\n{0,2}$/, "\n\n");
                }
                if (!/(\n|^)[ ]{0,3}>.*$/.test(chunk.selection) && chunk.endTag) {
                    chunk.endTag = chunk.endTag.replace(/^\n{0,2}/, "\n\n");
                }
            }
            if (!/\n/.test(chunk.selection)) {
                chunk.selection = chunk.selection.replace(/^(> *)/, function(wholeMatch, blanks) {
                    chunk.startTag += blanks;
                    return "";
                });
            }
        };
        command.doCode = function(chunk, postProcessing, useDefaultText) {
            var hasTextBefore = /\S[ ]*$/.test(chunk.before);
            var hasTextAfter = /^[ ]*\S/.test(chunk.after);
            if ((!hasTextAfter && !hasTextBefore) || /\n/.test(chunk.selection)) {
                chunk.before = chunk.before.replace(/[ ]{4}$/, function(totalMatch) {
                    chunk.selection = totalMatch + chunk.selection;
                    return "";
                });
                var nLinesBefore = 1;
                var nLinesAfter = 1;
                if (/\n(\t|[ ]{4,}).*\n$/.test(chunk.before) || chunk.after === "") {
                    nLinesBefore = 0;
                }
                if (/^\n(\t|[ ]{4,})/.test(chunk.after)) {
                    nLinesAfter = 0;
                }
                chunk.addBlankLines(nLinesBefore, nLinesAfter);
                if (!chunk.selection) {
                    chunk.startTag = "    ";
                    chunk.selection = useDefaultText ? "enter code here" : "";
                } else {
                    if (/^[ ]{0,3}\S/m.test(chunk.selection)) {
                        chunk.selection = chunk.selection.replace(/^/gm, "    ");
                    } else {
                        chunk.selection = chunk.selection.replace(/^[ ]{4}/gm, "");
                    }
                }
            } else {
                chunk.trimWhitespace();
                chunk.findTags(/`/, /`/);
                if (!chunk.startTag && !chunk.endTag) {
                    chunk.startTag = chunk.endTag = "`";
                    if (!chunk.selection) {
                        chunk.selection = useDefaultText ? "enter code here" : "";
                    }
                } else if (chunk.endTag && !chunk.startTag) {
                    chunk.before += chunk.endTag;
                    chunk.endTag = "";
                } else {
                    chunk.startTag = chunk.endTag = "";
                }
            }
        };
        command.doList = function(chunk, postProcessing, isNumberedList, useDefaultText) {
            var previousItemsRegex = /(\n|^)(([ ]{0,3}([*+-]|\d+[.])[ \t]+.*)(\n.+|\n{2,}([*+-].*|\d+[.])[ \t]+.*|\n{2,}[ \t]+\S.*)*)\n*$/;
            var nextItemsRegex = /^\n*(([ ]{0,3}([*+-]|\d+[.])[ \t]+.*)(\n.+|\n{2,}([*+-].*|\d+[.])[ \t]+.*|\n{2,}[ \t]+\S.*)*)\n*/;
            var bullet = "-";
            var num = 1;
            var getItemPrefix = function() {
                var prefix;
                if (isNumberedList) {
                    prefix = " " + num + ". ";
                    num++;
                } else {
                    prefix = " " + bullet + " ";
                }
                return prefix;
            };
            var getPrefixedItem = function(itemText) {
                if (isNumberedList === undefined) {
                    isNumberedList = /^\s*\d/.test(itemText);
                }
                itemText = itemText.replace(/^[ ]{0,3}([*+-]|\d+[.])\s/gm, function(_) { return getItemPrefix(); });
                return itemText;
            };
            chunk.findTags(/(\n|^)*[ ]{0,3}([*+-]|\d+[.])\s+/, null);
            if (chunk.before && !/\n$/.test(chunk.before) && !/^\n/.test(chunk.startTag)) {
                chunk.before += chunk.startTag;
                chunk.startTag = "";
            }
            if (chunk.startTag) {
                var hasDigits = /\d+[.]/.test(chunk.startTag);
                chunk.startTag = "";
                chunk.selection = chunk.selection.replace(/\n[ ]{4}/g, "\n");
                command.unwrap(chunk);
                chunk.addBlankLines();
                if (hasDigits) {
                    chunk.after = chunk.after.replace(nextItemsRegex, getPrefixedItem);
                }
                if (isNumberedList == hasDigits) {
                    return;
                }
            }
            var nLinesBefore = 1;
            chunk.before = chunk.before.replace(previousItemsRegex, function(itemText) {
                if (/^\s*([*+-])/.test(itemText)) {
                    bullet = re.$1;
                }
                nLinesBefore = /[^\n]\n\n[^\n]/.test(itemText) ? 1 : 0;
                return getPrefixedItem(itemText);
            });
            if (!chunk.selection) {
                chunk.selection = useDefaultText ? "List item" : " ";
            }
            var prefix = getItemPrefix();
            var nLinesAfter = 1;
            chunk.after = chunk.after.replace(nextItemsRegex, function(itemText) {
                nLinesAfter = /[^\n]\n\n[^\n]/.test(itemText) ? 1 : 0;
                return getPrefixedItem(itemText);
            });
            chunk.trimWhitespace(true);
            chunk.addBlankLines(nLinesBefore, nLinesAfter, true);
            chunk.startTag = prefix;
            var spaces = prefix.replace(/./g, " ");
            command.wrap(chunk, wmd.wmd_env.lineLength - spaces.length);
            chunk.selection = chunk.selection.replace(/\n/g, "\n" + spaces);
        };
        command.doHeading = function(chunk, postProcessing, useDefaultText) {
            chunk.selection = chunk.selection.replace(/\s+/g, " ");
            chunk.selection = chunk.selection.replace(/(^\s+|\s+$)/g, "");
            if (!chunk.selection) {
                chunk.startTag = "## ";
                chunk.selection = "Heading";
                chunk.endTag = " ##";
                return;
            }
            var headerLevel = 0;
            chunk.findTags(/#+[ ]*/, /[ ]*#+/);
            if (/#+/.test(chunk.startTag)) {
                headerLevel = re.lastMatch.length;
            }
            chunk.startTag = chunk.endTag = "";
            chunk.findTags(null, /\s?(-+|=+)/);
            if (/=+/.test(chunk.endTag)) {
                headerLevel = 1;
            }
            if (/-+/.test(chunk.endTag)) {
                headerLevel = 2;
            }
            chunk.startTag = chunk.endTag = "";
            chunk.addBlankLines(1, 1);
            var headerLevelToCreate = headerLevel == 0 ? 2 : headerLevel - 1;
            if (headerLevelToCreate > 0) {
                var headerChar = headerLevelToCreate >= 2 ? "-" : "=";
                var len = chunk.selection.length;
                if (len > wmd.wmd_env.lineLength) {
                    len = wmd.wmd_env.lineLength;
                }
                chunk.endTag = "\n";
                while (len--) {
                    chunk.endTag += headerChar;
                }
            }
        };
        command.doHorizontalRule = function(chunk, postProcessing, useDefaultText) {
            chunk.startTag = "----------\n";
            chunk.selection = "";
            chunk.addBlankLines(2, 1, true);
        };
    };
    Attacklab.wmd_env = { };
    Attacklab.account_options = { };
    Attacklab.wmd_defaults = { version: 2.0, output_format: "markdown", lineLength: 40, delayLoad: false };
    if (!Attacklab.wmd) {
        Attacklab.wmd = function() {
            Attacklab.loadEnv = function() {
                var mergeEnv = function(env) {
                    if (!env) {
                        return;
                    }
                    for (var key in env) {
                        Attacklab.wmd_env[key] = env[key];
                    }
                };
                mergeEnv(Attacklab.wmd_defaults);
                mergeEnv(Attacklab.account_options);
                mergeEnv(wmd_options);
                Attacklab.full = true;
                var defaultButtons = "bold italic link blockquote code image ol ul heading hr";
                Attacklab.wmd_env.buttons = Attacklab.wmd_env.buttons || defaultButtons;
            };
            Attacklab.loadEnv();
        };
        Attacklab.wmd();
        Attacklab.wmdBase();
        Attacklab.Util.startEditor();
    }
    ;
}

var Attacklab = Attacklab || { };
Attacklab.showdown = Attacklab.showdown || { };
Attacklab.showdown.converter = function() {
    var g_urls;
    var g_titles;
    var g_html_blocks;
    var g_list_level = 0;
    this.makeHtml = function(text) {
        g_urls = new Array();
        g_titles = new Array();
        g_html_blocks = new Array();
        text = text.replace(/~/g, "~T");
        text = text.replace(/\$/g, "~D");
        text = text.replace(/\r\n/g, "\n");
        text = text.replace(/\r/g, "\n");
        text = "\n\n" + text + "\n\n";
        text = _Detab(text);
        text = text.replace(/^[ \t]+$/mg, "");
        text = _HashHTMLBlocks(text);
        text = _StripLinkDefinitions(text);
        text = _RunBlockGamut(text);
        text = _UnescapeSpecialChars(text);
        text = text.replace(/~D/g, "$$");
        text = text.replace(/~T/g, "~");
        return text;
    };
    var _StripLinkDefinitions = function(text) {
        var text = text.replace(/^[ ]{0,3}\[(.+)\]:[ \t]*\n?[ \t]*<?(\S+?)>?[ \t]*\n?[ \t]*(?:(\n*)["(](.+?)[")][ \t]*)?(?:\n+)/gm, function(wholeMatch, m1, m2, m3, m4) {
            m1 = m1.toLowerCase();
            g_urls[m1] = _EncodeAmpsAndAngles(m2);
            if (m3) {
                return m3 + m4;
            } else if (m4) {
                g_titles[m1] = m4.replace(/"/g, "&quot;");
            }
            return "";
        });
        return text;
    };
    var _HashHTMLBlocks = function(text) {
        text = text.replace(/\n/g, "\n\n");
        var block_tags_a = "p|div|h[1-6]|blockquote|pre|table|dl|ol|ul|script|noscript|form|fieldset|iframe|math|ins|del";
        var block_tags_b = "p|div|h[1-6]|blockquote|pre|table|dl|ol|ul|script|noscript|form|fieldset|iframe|math";
        text = text.replace(/^(<(p|div|h[1-6]|blockquote|pre|table|dl|ol|ul|script|noscript|form|fieldset|iframe|math|ins|del)\b[^\r]*?\n<\/\2>[ \t]*(?=\n+))/gm, hashElement);
        text = text.replace(/^(<(p|div|h[1-6]|blockquote|pre|table|dl|ol|ul|script|noscript|form|fieldset|iframe|math)\b[^\r]*?.*<\/\2>[ \t]*(?=\n+)\n)/gm, hashElement);
        text = text.replace(/(\n[ ]{0,3}(<(hr)\b([^<>])*?\/?>)[ \t]*(?=\n{2,}))/g, hashElement);
        text = text.replace(/(\n\n[ ]{0,3}<!(--[^\r]*?--\s*)+>[ \t]*(?=\n{2,}))/g, hashElement);
        text = text.replace(/(?:\n\n)([ ]{0,3}(?:<([?%])[^\r]*?\2>)[ \t]*(?=\n{2,}))/g, hashElement);
        text = text.replace(/\n\n/g, "\n");
        return text;
    };
    var hashElement = function(wholeMatch, m1) {
        var blockText = m1;
        blockText = blockText.replace(/\n\n/g, "\n");
        blockText = blockText.replace(/^\n/, "");
        blockText = blockText.replace(/\n+$/g, "");
        blockText = "\n\n~K" + (g_html_blocks.push(blockText) - 1) + "K\n\n";
        return blockText;
    };
    var _RunBlockGamut = function(text) {
        text = _DoHeaders(text);
        var key = hashBlock("<hr />");
        text = text.replace(/^[ ]{0,2}([ ]?\*[ ]?){3,}[ \t]*$/gm, key);
        text = text.replace(/^[ ]{0,2}([ ]?-[ ]?){3,}[ \t]*$/gm, key);
        text = text.replace(/^[ ]{0,2}([ ]?_[ ]?){3,}[ \t]*$/gm, key);
        text = _DoLists(text);
        text = _DoCodeBlocks(text);
        text = _DoBlockQuotes(text);
        text = _HashHTMLBlocks(text);
        text = _FormParagraphs(text);
        return text;
    };
    var _RunSpanGamut = function(text) {
        text = _DoCodeSpans(text);
        text = _EscapeSpecialCharsWithinTagAttributes(text);
        text = _EncodeBackslashEscapes(text);
        text = _DoImages(text);
        text = _DoAnchors(text);
        text = _DoAutoLinks(text);
        text = _EncodeAmpsAndAngles(text);
        text = _DoItalicsAndBold(text);
        text = text.replace(/  +\n/g, " <br />\n");
        return text;
    };
    var _EscapeSpecialCharsWithinTagAttributes = function(text) {
        var regex = /(<[a-z\/!$]("[^"]*"|'[^']*'|[^'">])*>|<!(--.*?--\s*)+>)/gi;
        text = text.replace(regex, function(wholeMatch) {
            var tag = wholeMatch.replace(/(.)<\/?code>(?=.)/g, "$1`");
            tag = escapeCharacters(tag, "\\`*_");
            return tag;
        });
        return text;
    };
    var _DoAnchors = function(text) {
        text = text.replace(/(\[((?:\[[^\]]*\]|[^\[\]])*)\][ ]?(?:\n[ ]*)?\[(.*?)\])()()()()/g, writeAnchorTag);
        text = text.replace(/(\[((?:\[[^\]]*\]|[^\[\]])*)\]\([ \t]*()<?(.*?)>?[ \t]*((['"])(.*?)\6[ \t]*)?\))/g, writeAnchorTag);
        text = text.replace(/(\[([^\[\]]+)\])()()()()()/g, writeAnchorTag);
        return text;
    };
    var writeAnchorTag = function(wholeMatch, m1, m2, m3, m4, m5, m6, m7) {
        if (m7 == undefined) m7 = "";
        var whole_match = m1;
        var link_text = m2;
        var link_id = m3.toLowerCase();
        var url = m4;
        var title = m7;
        if (url == "") {
            if (link_id == "") {
                link_id = link_text.toLowerCase().replace(/ ?\n/g, " ");
            }
            url = "#" + link_id;
            if (g_urls[link_id] != undefined) {
                url = g_urls[link_id];
                if (g_titles[link_id] != undefined) {
                    title = g_titles[link_id];
                }
            } else {
                if (whole_match.search(/\(\s*\)$/m) > -1) {
                    url = "";
                } else {
                    return whole_match;
                }
            }
        }
        url = escapeCharacters(url, "*_");
        var result = "<a href=\"" + url + "\"";
        if (title != "") {
            title = title.replace(/"/g, "&quot;");
            title = escapeCharacters(title, "*_");
            result += " title=\"" + title + "\"";
        }
        result += ">" + link_text + "</a>";
        return result;
    };
    var _DoImages = function(text) {
        text = text.replace(/(!\[(.*?)\][ ]?(?:\n[ ]*)?\[(.*?)\])()()()()/g, writeImageTag);
        text = text.replace(/(!\[(.*?)\]\s?\([ \t]*()<?(\S+?)>?[ \t]*((['"])(.*?)\6[ \t]*)?\))/g, writeImageTag);
        return text;
    };
    var writeImageTag = function(wholeMatch, m1, m2, m3, m4, m5, m6, m7) {
        var whole_match = m1;
        var alt_text = m2;
        var link_id = m3.toLowerCase();
        var url = m4;
        var title = m7;
        if (!title) title = "";
        if (url == "") {
            if (link_id == "") {
                link_id = alt_text.toLowerCase().replace(/ ?\n/g, " ");
            }
            url = "#" + link_id;
            if (g_urls[link_id] != undefined) {
                url = g_urls[link_id];
                if (g_titles[link_id] != undefined) {
                    title = g_titles[link_id];
                }
            } else {
                return whole_match;
            }
        }
        alt_text = alt_text.replace(/"/g, "&quot;");
        url = escapeCharacters(url, "*_");
        var result = "<img src=\"" + url + "\" alt=\"" + alt_text + "\"";
        title = title.replace(/"/g, "&quot;");
        title = escapeCharacters(title, "*_");
        result += " title=\"" + title + "\"";
        result += " />";
        return result;
    };
    var _DoHeaders = function(text) {
        text = text.replace(/^(.+)[ \t]*\n=+[ \t]*\n+/gm, function(wholeMatch, m1) { return hashBlock("<h1>" + _RunSpanGamut(m1) + "</h1>"); });
        text = text.replace(/^(.+)[ \t]*\n-+[ \t]*\n+/gm, function(matchFound, m1) { return hashBlock("<h2>" + _RunSpanGamut(m1) + "</h2>"); });
        text = text.replace(/^(\#{1,6})[ \t]*(.+?)[ \t]*\#*\n+/gm, function(wholeMatch, m1, m2) {
            var h_level = m1.length;
            return hashBlock("<h" + h_level + ">" + _RunSpanGamut(m2) + "</h" + h_level + ">");
        });
        return text;
    };
    var _ProcessListItems;
    var _DoLists = function(text) {
        text += "~0";
        var whole_list = /^(([ ]{0,3}([*+-]|\d+[.])[ \t]+)[^\r]+?(~0|\n{2,}(?=\S)(?![ \t]*(?:[*+-]|\d+[.])[ \t]+)))/gm;
        if (g_list_level) {
            text = text.replace(whole_list, function(wholeMatch, m1, m2) {
                var list = m1;
                var list_type = (m2.search(/[*+-]/g) > -1) ? "ul" : "ol";
                list = list.replace(/\n{2,}/g, "\n\n\n");
                ;
                var result = _ProcessListItems(list);
                result = result.replace(/\s+$/, "");
                result = "<" + list_type + ">" + result + "</" + list_type + ">\n";
                return result;
            });
        } else {
            whole_list = /(\n\n|^\n?)(([ ]{0,3}([*+-]|\d+[.])[ \t]+)[^\r]+?(~0|\n{2,}(?=\S)(?![ \t]*(?:[*+-]|\d+[.])[ \t]+)))/g;
            text = text.replace(whole_list, function(wholeMatch, m1, m2, m3) {
                var runup = m1;
                var list = m2;
                var list_type = (m3.search(/[*+-]/g) > -1) ? "ul" : "ol";
                var list = list.replace(/\n{2,}/g, "\n\n\n");
                ;
                var result = _ProcessListItems(list);
                result = runup + "<" + list_type + ">\n" + result + "</" + list_type + ">\n";
                return result;
            });
        }
        text = text.replace(/~0/, "");
        return text;
    };
    _ProcessListItems = function(list_str) {
        g_list_level++;
        list_str = list_str.replace(/\n{2,}$/, "\n");
        list_str += "~0";
        list_str = list_str.replace(/(\n)?(^[ \t]*)([*+-]|\d+[.])[ \t]+([^\r]+?(\n{1,2}))(?=\n*(~0|\2([*+-]|\d+[.])[ \t]+))/gm, function(wholeMatch, m1, m2, m3, m4) {
            var item = m4;
            var leading_line = m1;
            var leading_space = m2;
            if (leading_line || (item.search(/\n{2,}/) > -1)) {
                item = _RunBlockGamut(_Outdent(item));
            } else {
                item = _DoLists(_Outdent(item));
                item = item.replace(/\n$/, "");
                item = _RunSpanGamut(item);
            }
            return "<li>" + item + "</li>\n";
        });
        list_str = list_str.replace(/~0/g, "");
        g_list_level--;
        return list_str;
    };
    var _DoCodeBlocks = function(text) {
        text += "~0";
        text = text.replace(/(?:\n\n|^)((?:(?:[ ]{4}|\t).*\n+)+)(\n*[ ]{0,3}[^ \t\n]|(?=~0))/g, function(wholeMatch, m1, m2) {
            var codeblock = m1;
            var nextChar = m2;
            codeblock = _EncodeCode(_Outdent(codeblock));
            codeblock = _Detab(codeblock);
            codeblock = codeblock.replace(/^\n+/g, "");
            codeblock = codeblock.replace(/\n+$/g, "");
            codeblock = "<pre><code>" + codeblock + "\n</code></pre>";
            return hashBlock(codeblock) + nextChar;
        });
        text = text.replace(/~0/, "");
        return text;
    };
    var hashBlock = function(text) {
        text = text.replace(/(^\n+|\n+$)/g, "");
        return "\n\n~K" + (g_html_blocks.push(text) - 1) + "K\n\n";
    };
    var _DoCodeSpans = function(text) {
        text = text.replace(/(^|[^\\])(`+)([^\r]*?[^`])\2(?!`)/gm, function(wholeMatch, m1, m2, m3, m4) {
            var c = m3;
            c = c.replace(/^([ \t]*)/g, "");
            c = c.replace(/[ \t]*$/g, "");
            c = _EncodeCode(c);
            return m1 + "<code>" + c + "</code>";
        });
        return text;
    };
    var _EncodeCode = function(text) {
        text = text.replace(/&/g, "&amp;");
        text = text.replace(/</g, "&lt;");
        text = text.replace(/>/g, "&gt;");
        text = escapeCharacters(text, "\*_{}[]\\", false);
        return text;
    };
    var _DoItalicsAndBold = function(text) {
        text = text.replace(/(\*\*|__)(?=\S)([^\r]*?\S[\*_]*)\1/g, "<strong>$2</strong>");
        text = text.replace(/(\*|_)(?=\S)([^\r]*?\S)\1/g, "<em>$2</em>");
        return text;
    };
    var _DoBlockQuotes = function(text) {
        text = text.replace(/((^[ \t]*>[ \t]?.+\n(.+\n)*\n*)+)/gm, function(wholeMatch, m1) {
            var bq = m1;
            bq = bq.replace(/^[ \t]*>[ \t]?/gm, "~0");
            bq = bq.replace(/~0/g, "");
            bq = bq.replace(/^[ \t]+$/gm, "");
            bq = _RunBlockGamut(bq);
            bq = bq.replace(/(^|\n)/g, "$1  ");
            bq = bq.replace(/(\s*<pre>[^\r]+?<\/pre>)/gm, function(wholeMatch, m1) {
                var pre = m1;
                pre = pre.replace(/^  /mg, "~0");
                pre = pre.replace(/~0/g, "");
                return pre;
            });
            return hashBlock("<blockquote>\n" + bq + "\n</blockquote>");
        });
        return text;
    };
    var _FormParagraphs = function(text) {
        text = text.replace(/^\n+/g, "");
        text = text.replace(/\n+$/g, "");
        var grafs = text.split(/\n{2,}/g);
        var grafsOut = new Array();
        var end = grafs.length;
        for (var i = 0; i < end; i++) {
            var str = grafs[i];
            if (str.search(/~K(\d+)K/g) >= 0) {
                grafsOut.push(str);
            } else if (str.search(/\S/) >= 0) {
                str = _RunSpanGamut(str);
                str = str.replace(/^([ \t]*)/g, "<p>");
                str += "</p>";
                grafsOut.push(str);
            }
        }
        end = grafsOut.length;
        for (var i = 0; i < end; i++) {
            while (grafsOut[i].search(/~K(\d+)K/) >= 0) {
                var blockText = g_html_blocks[RegExp.$1];
                blockText = blockText.replace(/\$/g, "$$$$");
                grafsOut[i] = grafsOut[i].replace(/~K\d+K/, blockText);
            }
        }
        return grafsOut.join("\n\n");
    };
    var _EncodeAmpsAndAngles = function(text) {
        text = text.replace(/&(?!#?[xX]?(?:[0-9a-fA-F]+|\w+);)/g, "&amp;");
        text = text.replace(/<(?![a-z\/?\$!])/gi, "&lt;");
        return text;
    };
    var _EncodeBackslashEscapes = function(text) {
        text = text.replace(/\\(\\)/g, escapeCharacters_callback);
        text = text.replace(/\\([`*_{}\[\]()>#+-.!])/g, escapeCharacters_callback);
        return text;
    };
    var _DoAutoLinks = function(text) {
        text = text.replace(/<((https?|ftp|dict):[^'">\s]+)>/gi, "<a href=\"$1\">$1</a>");
        text = text.replace(/<(?:mailto:)?([-.\w]+\@[-a-z0-9]+(\.[-a-z0-9]+)*\.[a-z]+)>/gi, function(wholeMatch, m1) { return _EncodeEmailAddress(_UnescapeSpecialChars(m1)); });
        return text;
    };
    var _EncodeEmailAddress = function(addr) {

        function char2hex(ch) {
            var hexDigits = '0123456789ABCDEF';
            var dec = ch.charCodeAt(0);
            return (hexDigits.charAt(dec >> 4) + hexDigits.charAt(dec & 15));
        }

        var encode = [function(ch) { return "&#" + ch.charCodeAt(0) + ";"; }, function(ch) { return "&#x" + char2hex(ch) + ";"; }, function(ch) { return ch; }];
        addr = "mailto:" + addr;
        addr = addr.replace(/./g, function(ch) {
            if (ch == "@") {
                ch = encode[Math.floor(Math.random() * 2)](ch);
            } else if (ch != ":") {
                var r = Math.random();
                ch = (r > .9 ? encode[2](ch) : r > .45 ? encode[1](ch) : encode[0](ch));
            }
            return ch;
        });
        addr = "<a href=\"" + addr + "\">" + addr + "</a>";
        addr = addr.replace(/">.+:/g, "\">");
        return addr;
    };
    var _UnescapeSpecialChars = function(text) {
        text = text.replace(/~E(\d+)E/g, function(wholeMatch, m1) {
            var charCodeToReplace = parseInt(m1);
            return String.fromCharCode(charCodeToReplace);
        });
        return text;
    };
    var _Outdent = function(text) {
        text = text.replace(/^(\t|[ ]{1,4})/gm, "~0");
        text = text.replace(/~0/g, "");
        return text;
    };
    var _Detab = function(text) {
        text = text.replace(/\t(?=\t)/g, "    ");
        text = text.replace(/\t/g, "~A~B");
        text = text.replace(/~B(.+?)~A/g, function(wholeMatch, m1, m2) {
            var leadingText = m1;
            var numSpaces = 4 - leadingText.length % 4;
            for (var i = 0; i < numSpaces; i++) leadingText += " ";
            return leadingText;
        });
        text = text.replace(/~A/g, "    ");
        text = text.replace(/~B/g, "");
        return text;
    };
    var escapeCharacters = function(text, charsToEscape, afterBackslash) {
        var regexString = "([" + charsToEscape.replace(/([\[\]\\])/g, "\\$1") + "])";
        if (afterBackslash) {
            regexString = "\\\\" + regexString;
        }
        var regex = new RegExp(regexString, "g");
        text = text.replace(regex, escapeCharacters_callback);
        return text;
    };
    var escapeCharacters_callback = function(wholeMatch, m1) {
        var charCodeToEscape = m1.charCodeAt(0);
        return "~E" + charCodeToEscape + "E";
    };
};
var Showdown = Attacklab.showdown;
if (Attacklab.fileLoaded) {
    Attacklab.fileLoaded("showdown.js");
}