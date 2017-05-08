(function (factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define(['jquery'], factory);
    } else if (typeof exports === 'object') {
        // Node/CommonJS style for Browserify
        module.exports = factory;
    } else {
        // Browser globals
        factory(jQuery);
    }
}(function ($) {

    var toFix = ['wheel', 'mousewheel', 'DOMMouseScroll', 'MozMousePixelScroll'];
    var toBind = 'onwheel' in document || document.documentMode >= 9 ? ['wheel'] : ['mousewheel', 'DomMouseScroll', 'MozMousePixelScroll'];
    var lowestDelta, lowestDeltaXY;

    if ($.event.fixHooks) {
        for (var i = toFix.length; i;) {
            $.event.fixHooks[toFix[--i]] = $.event.mouseHooks;
        }
    }

    $.event.special.mousewheel = {
        setup: function () {
            if (this.addEventListener) {
                for (var i = toBind.length; i;) {
                    this.addEventListener(toBind[--i], handler, false);
                }
            } else {
                this.onmousewheel = handler;
            }
        },

        teardown: function () {
            if (this.removeEventListener) {
                for (var i = toBind.length; i;) {
                    this.removeEventListener(toBind[--i], handler, false);
                }
            } else {
                this.onmousewheel = null;
            }
        }
    };

    $.fn.extend({
        mousewheel: function (fn) {
            return fn ? this.bind("mousewheel", fn) : this.trigger("mousewheel");
        },

        unmousewheel: function (fn) {
            return this.unbind("mousewheel", fn);
        }
    });


    function handler(event) {
        var orgEvent = event || window.event,
            args = [].slice.call(arguments, 1),
            delta = 0,
            deltaX = 0,
            deltaY = 0,
            absDelta = 0,
            absDeltaXY = 0,
            fn;
        event = $.event.fix(orgEvent);
        event.type = "mousewheel";

        // Old school scrollwheel delta
        if (orgEvent.wheelDelta) { delta = orgEvent.wheelDelta; }
        if (orgEvent.detail) { delta = orgEvent.detail * -1; }

        // New school wheel delta (wheel event)
        if (orgEvent.deltaY) {
            deltaY = orgEvent.deltaY * -1;
            delta = deltaY;
        }
        if (orgEvent.deltaX) {
            deltaX = orgEvent.deltaX;
            delta = deltaX * -1;
        }

        // Webkit
        if (orgEvent.wheelDeltaY !== undefined) { deltaY = orgEvent.wheelDeltaY; }
        if (orgEvent.wheelDeltaX !== undefined) { deltaX = orgEvent.wheelDeltaX * -1; }

        // Look for lowest delta to normalize the delta values
        absDelta = Math.abs(delta);
        if (!lowestDelta || absDelta < lowestDelta) { lowestDelta = absDelta; }
        absDeltaXY = Math.max(Math.abs(deltaY), Math.abs(deltaX));
        if (!lowestDeltaXY || absDeltaXY < lowestDeltaXY) { lowestDeltaXY = absDeltaXY; }

        // Get a whole value for the deltas
        fn = delta > 0 ? 'floor' : 'ceil';
        delta = Math[fn](delta / lowestDelta);
        deltaX = Math[fn](deltaX / lowestDeltaXY);
        deltaY = Math[fn](deltaY / lowestDeltaXY);

        // Add event and delta to the front of the arguments
        args.unshift(event, delta, deltaX, deltaY);

        return ($.event.dispatch || $.event.handle).apply(this, args);
    }

}));

// Custom combobox jqueryui
(function ($) {
    $.widget("ui.customcombobox", {
        _create: function () {
            var self = this,
                select = this.element.hide(),
                selected = select.children(":selected"),
                value = selected.val() ? selected.text() : "";
            var input = this.input = $("<input readonly='readonly'>")
                .insertAfter(select)
                .val(value)
                .autocomplete({
                    delay: 0,
                    minLength: 0,
                    source: function (request, response) {
                        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                        response(select.children("option").map(function () {
                            var text = $(this).text();
                            if (this.value && (!request.term || matcher.test(text)))
                                return {
                                    label: text.replace(
                                        new RegExp(
                                            "(?![^&;]+;)(?!<[^<>]*)(" +
                                            $.ui.autocomplete.escapeRegex(request.term) +
                                            ")(?![^<>]*>)(?![^&;]+;)", "gi"
                                        ), "<strong>$1</strong>"),
                                    value: text,
                                    option: this
                                };
                        }));
                    },
                    select: function (event, ui) {
                       
                        if ($(ui.item.option).prop('disabled') === true) {
                            return false;
                        }
                        ui.item.option.selected = true;
                        self._trigger("selected", event, {
                            item: ui.item.option
                        });
                        
                        select.trigger("change");
                    },
                    change: function (event, ui) {
                        
                        if (!ui.item) {
                            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i"),
                                valid = false;
                            select.children("option").each(function () {
                                if ($(this).text().match(matcher)) {
                                    this.selected = valid = true;
                                    return false;
                                }
                            });
                            if (!valid) {
                                // remove invalid value, as it didn't match anything
                                $(this).val("");
                                select.val("");
                                input.data("autocomplete").term = "";
                                return false;
                            }
                        }
                    }
                })
                .addClass("custom-combobox-input ui-widget ui-widget-content ui-corner-left");

            input.data("ui-autocomplete")._renderItem = function (ul, item) {
    
                ul.addClass('custom-combobox-content');
                return $("<li></li>")
                    .data("item.autocomplete", item)
                   .append(item.option.disabled ? "<span class='disabled'>" + item.label + "</span>" : "<a>" + item.label + "</a>")
                        .appendTo(ul);

            };

            this.button = $("<button type='button'>&nbsp;</button>")
                .addClass("custom-combobox-button")
                .attr("tabIndex", -1)
                .attr("title", "Show All Items")
                .insertAfter(input)
                .button({
                    icons: {
                        primary: "ui-icon-triangle-1-s"
                    },
                    text: false
                })
                .removeClass("ui-corner-all")
                .addClass("ui-corner-right ui-button-icon")
                .click(function () {
                    // close if already visible
                    if (input.autocomplete("widget").is(":visible")) {
                        input.autocomplete("close");
                        return;
                    }

                    // pass empty string as value to search for, displaying all results
                    input.autocomplete("search", "");
                    input.focus();
                });

            if (select.width() > 0) input.css('width', select.width());
        },

        destroy: function () {
            this.input.remove();
            this.button.remove();
            this.element.show();
            $.Widget.prototype.destroy.call(this);
        },
        setValue: function (val) {
            var $element = $(this.element);
            $element.val(val);
            this.input.val($element.find("option:selected").text());
        },
        setIndex: function (index) {
            var $element = $(this.element);
            $element.val($('option', $element).eq(index).val());
            this.input.val($element.find("option:selected").text());
        },
        addOption: function (val, text) {
            var $element = $(this.element);
            $element.append('<option value="' + val + '">' + text + '</option>');
        },
        disable: function () {
            this.input.prop('disabled', true);
            this.input.autocomplete("disable");
            this.button.prop('disabled', true);
        },
        enable: function () {
            this.input.prop('disabled', false);
            this.input.autocomplete("enable");
            this.button.prop('disabled', false);
        }
    });
})(jQuery);

// extend method jqgrid to set column width
$.jgrid.extend({
    setColWidth: function (iCol, newWidth, adjustGridWidth) {
        "use strict";
        return this.each(function () {
            var $self = $(this), grid = this.grid, colName, colModel, i, nCol;
            if (typeof iCol === "string") {
                // the first parametrer is column name instead of index
                colName = iCol;
                colModel = $self.jqGrid("getGridParam", "colModel");
                for (i = 0, nCol = colModel.length; i < nCol; i++) {
                    if (colModel[i].name === colName) {
                        iCol = i;
                        break;
                    }
                }
                if (i >= nCol) {
                    // error: non-existing column name specified as the first parameter
                    return;
                }
            } else if (typeof iCol !== "number") {
                return; // error: wrong parameters
            }
            grid.resizing = { idx: iCol };
            grid.headers[iCol].newWidth = newWidth;
            if (adjustGridWidth !== false) {
                grid.newWidth = grid.width + newWidth - grid.headers[iCol].width;
            }
            grid.dragEnd();   // adjust column width
            if (adjustGridWidth !== false) {
                // adjust grid width too
                $self.jqGrid("setGridWidth", grid.newWidth, false);
            }
        });
    },
    
    info_dialog: function(caption, content, c_b, modalopt) {
        jAlert(content, caption);
    }
});

var useCustomDialog = true, oldInfoDialog = $.jgrid.info_dialog;
$.extend($.jgrid, {
    info_dialog: function(caption, content, c_b, modalopt) {
        if (useCustomDialog) {
            jAlert(content, caption);
        } else {
            return oldInfoDialog.apply(this, arguments);
        }
    }
});