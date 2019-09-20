$.fn.ULSelect = function (options) {
    var defaults = {
        el: '',
        listEl: '',
        fild: 'Value',
        text: 'Text',
        mode: 0,
        arrowClass: '',
        selectedClass: 'xbh',
        callback: function () { }
    };
    defaults = $.extend(defaults, options);
    var open = false;
    var el = $(defaults.el);
    var listEl = $(defaults.listEl);
    function ShowList() {
        open = true;
        el.addClass(defaults.arrowClass);
        listEl.show();
        listEl.find('li[data-id=' + el.data('id') + ']').addClass(defaults.selectedClass);
    }
    function HideList() {
        open = false;
        listEl.hide();
        el.removeClass(defaults.arrowClass);
    }

    el.click(function () {
        if (open)
            HideList();
        else
            ShowList();
    });
    listEl.mouseenter(function () { $(this).find('.xbh').removeClass('xbh'); });
    listEl.on('click', 'li', function () {
        listEl.hide();
        el.text($(this).text());
        el.data('id', $(this).data('id'));
        defaults.callback({ id: $(this).data('id'), name: $(this).text() });
    });
    $("body").bind("click", function (event) {
        var el = $(event.target);
        if (!$(el).hasClass(defaults.el.replace('.', '').replace('#', '')) && !$(el).hasClass(defaults.listEl.replace('.', '').replace('#', ''))) {
            HideList();
        }
    });
};

$.fn.SelectToHtml = function (options) {
    console.log($(this));
    var defaults = {
        el: $(this),
        dt: '',
        url: '',
        defaultText: '--请选择--',
        fild: 'Value',
        text: 'Text',
        bindFilds: ['Value', 'Text'],
        callback: function () { }
    };
    defaults = $.extend(defaults, options);
    $.ajax({
        url: defaults.url,
        type: "Get",
        cache: false,
        data: defaults.dt,
        dataType: "json",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        beforeSend: function () {
            //alert("before:show loading");
        },
        success: function (obj) {

            if (obj.code === 200) {
                var rows = obj.data;//JSON.parse(obj.data);
                var html = (" <option value='0'>" + defaults.defaultText + "</option>");
                if (rows.length > 0) {
                    // json对象的所有属性名称  
                    for (var i = 0; i < rows.length; i++) {
                        var option = "<option ";
                        var selectText = "";
                        var selectValue = "";
                        for (var key in rows[i]) {
                            if (key.toLowerCase() === defaults.text.toLowerCase()) {
                                selectText = rows[i][key];
                            }
                            if (key.toLowerCase() === defaults.fild.toLowerCase()) {
                                selectValue = rows[i][key];
                            }
                            if (defaults.bindFilds.indexOf(key, -1) !== -1) {
                                option += ("  data-" + key + "='" + rows[i][key] + "'" + "  ");
                            }

                        }
                        option += ("  value='" + selectValue + "'" + ">" + selectText + "</option>");
                        html += option;
                    }
                }
                $(defaults.el).html(html);
                defaults.callback(rows);
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        },
        complete: function (XMLHttpRequest, textStatus) {
        }
    });
};
$.fn.SelectToObj = function (options) {

    var defaults = {
        el: $(this),
        url: '',
        dt: '',
        defaultText: '--请选择--',
        callback: function () { }
    };
    defaults = $.extend(defaults, options);
    $.ajax({
        url: defaults.url,
        type: "Get",
        cache: false,
        data: defaults.dt,
        dataType: "json",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        beforeSend: function () {
            //alert("before:show loading");
        },
        success: function (obj) {
            if (obj.code === 200) {
                var rows = obj.data;//JSON.parse(obj);
                defaults.callback(rows);
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        },
        complete: function (XMLHttpRequest, textStatus) {
        }
    });
};