/*----------内容浏览-------------
一、 扩展
 1.时间格式 dateFormat    例:str.dateFormat("yyyy-MM-dd")
 2-3.数组max方法和min方法   例:ary.max(),ary.min()
 
二、  判断
 1.空判断：  例:isEmpty(obj)


三、 数据分组
 1.分组    例:var updateArry = groupBy(dirtyItems, function (arryitem) {  return [arryitem.Mark, arryitem.Source];   });

四、 其他通用
 1.生成guid  例：getGuid()
 */
/*==============================扩展 开始============================= */
/*==============================扩展 结束=============================*/
/*==============================扩展 开始=============================  */
function ChineseTimeToString(time) {
    try {
        var d = new Date(time);
        var datetime = d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate() + ' ' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds();
        return datetime.indexOf("NaN") > -1 ? "" : datetime;
    } catch (e) {
        return "";
    }

}

//时间格式 dateFormat
String.prototype.dateFormat = function (mask) {
    var value = this;
    value = value.replace(/\-/g, "/").replace(/T/, " ");
    var d = new Date(value);
    var zeroize = function (value, length) {
        if (!length) length = 2;
        value = String(value);
        for (var i = 0, zeros = ''; i < (length - value.length); i++) {
            zeros += '0';
        }
        return zeros + value;
    };
    return mask.replace(/"[^"]*"|'[^']*'|\b(?:d{1,4}|m{1,4}|yy(?:yy)?|([hHMstT])\1?|[lLZ])\b/g, function ($0) {
        switch ($0) {
            case 'd': return d.getDate();
            case 'dd': return zeroize(d.getDate());
            case 'ddd': return ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'][d.getDay()];
            case 'dddd': return ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][d.getDay()];
            case 'M': return d.getMonth() + 1;
            case 'MM': return zeroize(d.getMonth() + 1);
            case 'MMM': return ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'][d.getMonth()];
            case 'MMMM': return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'][d.getMonth()];
            case 'yy': return String(d.getFullYear()).substr(2);
            case 'yyyy': return d.getFullYear();
            case 'h': return d.getHours() % 12 || 12;
            case 'hh': return zeroize(d.getHours() % 12 || 12);
            case 'H': return d.getHours();
            case 'HH': return zeroize(d.getHours());
            case 'm': return d.getMinutes();
            case 'mm': return zeroize(d.getMinutes());
            case 's': return d.getSeconds();
            case 'ss': return zeroize(d.getSeconds());
            case 'l': return zeroize(d.getMilliseconds(), 3);
            case 'L': var m = d.getMilliseconds();
                if (m > 99) m = Math.round(m / 10);
                return zeroize(m);
            case 'tt': return d.getHours() < 12 ? 'am' : 'pm';
            case 'TT': return d.getHours() < 12 ? 'AM' : 'PM';
            case 'Z': return d.toUTCString().match(/[A-Z]+$/);
            // Return quoted strings with the surrounding quotes removed
            default: return $0.substr(1, $0.length - 2);
        }
    });
}
//转int
String.prototype.toInt = function () {
    var value = this;
    if (value == undefined || value == "" || value == null)
        return 0;
    else {
        try {
            return parseInt(value.toString());
        } catch (e) {
            return 0;
        }
    }
}
//转float
String.prototype.toFloat = function (mask) {
    var value = this;
    if (value == undefined || value == "" || value == null)
        return 0;
    else {
        try {
            return parseFloat(parseFloat(value.toString()).toFixed(mask));
        } catch (e) {
            return 0;
        }
    }
}
this.REGX_HTML_ENCODE = /"|&|'|<|>|[\x00-\x20]|[\x7F-\xFF]|[\u0100-\u2700]/g;
this.REGX_HTML_DECODE = /&\w+;|&#(\d+);/g;
this.REGX_TRIM = /(^\s*)|(\s*$)/g;
this.HTML_DECODE = {
    "&lt;": "<",
    "&gt;": ">",
    "&amp;": "&",
    "&nbsp;": " ",
    "&quot;": "\"",
    "©": ""
    // Add more
};
//转义
String.prototype.encodeHtml = function (s) {
    s = (s != undefined) ? s : this.toString();
    return (typeof s != "string") ? s :
        s.replace(this.REGX_HTML_ENCODE,
            function ($0) {
                var c = $0.charCodeAt(0), r = ["&#"];
                c = (c == 0x20) ? 0xA0 : c;
                r.push(c); r.push(";");
                return r.join("");
            });
};
//反转义
String.prototype.decodeHtml = function (s) {
    var HTML_DECODE = this.HTML_DECODE;
    s = (s != undefined) ? s : this.toString();
    return (typeof s != "string") ? s :
        s.replace(this.REGX_HTML_DECODE,
            function ($0, $1) {
                var c = HTML_DECODE[$0];
                if (c == undefined) {
                    // Maybe is Entity Number
                    if (!isNaN($1)) {
                        c = String.fromCharCode(($1 == 160) ? 32 : $1);
                    } else {
                        c = $0;
                    }
                }
                return c;
            });
};
String.prototype.hashCode = function () {
    var hash = this.__hash__, _char;
    if (hash == undefined || hash == 0) {
        hash = 0;
        for (var i = 0, len = this.length; i < len; i++) {
            _char = this.charCodeAt(i);
            hash = 31 * hash + _char;
            hash = hash & hash; // Convert to 32bit integer
        }
        hash = hash & 0x7fffffff;
    }
    this.__hash__ = hash;
    return this.__hash__;
};
//数组max方法和min方法
Array.prototype.max = function () {
    return Math.max.apply({}, this)
}


//数组max方法和min方法
Array.prototype.min = function () {
    return Math.min.apply({}, this)
}
Array.prototype.ulineToPoint = function () {
    return this.replace("_", ".");
}
/*==============================扩展 结束==============================*/


/*==============================判断 开始==============================*/
//空判断
function isEmpty(obj) {
    if (typeof obj == "undefined" || obj == null || obj == "") {
        return true;
    } else {
        return false;
    }
}
/*==============================判断 结束==============================*/

/*==============================数据分组 开始==============================*/
//分组 
//array 数据  分组函数
//用例： var updateArry = groupBy(dirtyItems, function (arryitem) {  return [arryitem.Mark, arryitem.Source];   }); 
function groupBy(array, f) {

    const groups = {};
    array.forEach(function (o) {
        const group = JSON.stringify(f(o));
        groups[group] = groups[group] || [];
        groups[group].push(o);
    });
    return Object.keys(groups).map(function (group) {
        return groups[group];
    });
}
/*==============================数据分组 结束==============================*/

/*==============================其他通用 开始==============================*/
//生成guid
function getGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
/*==============================其他通用 结束==============================*/


/*==============================返回ID 的第一个对象的引用 开始==============================*/
//返回ID 的第一个对象的引用
function _getElementById(id) {
    return document.getElementById(id);
}
/*==============================返回ID 的第一个对象的引用 开始==============================*/

/*==============================下拉框 开始==============================*/
// 绑定数据
$.fn.SelectToHtml = function (options) {
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
            if (obj.code === 0) {
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
            if (obj.code === 0) {
                defaults.callback(obj);
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        },
        complete: function (XMLHttpRequest, textStatus) {
        }
    });
};
/*==============================下拉框 开始==============================*/

/*==============================HTM编码 开始==============================*/
//HTML转义
function HTMLEncode(html) {
    var temp = document.createElement("div");
    (temp.textContent != null) ? (temp.textContent = html) : (temp.innerText = html);
    var output = temp.innerHTML;
    temp = null;
    return output;
}
//HTML反转义
function HTMLDecode(text) {
    var temp = document.createElement("div");
    temp.innerHTML = text;
    var output = temp.innerText || temp.textContent;
    temp = null;
    return output;
}
/*==============================HTM编码 开始==============================*/








