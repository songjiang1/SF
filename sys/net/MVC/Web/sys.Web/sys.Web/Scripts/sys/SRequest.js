var SRequest_apihost = 'http://' + window.location.host + '/';
//SRequest_apihost = "http://127.0.0.1:8088/";
var defaults_req = {
    url: null,
    data: null,
    method: 'null',
    done: null,
    error: null, 
    async: null,
    wait: null,
    code: 0,
};
var sys = {
    req: function (options) {
        var defaults = defaults_req;
        defaults = $.extend(defaults, options);
        var senddata = '';
        defaults.$async = defaults.$async == undefined ? true : false;
        defaults.wait = defaults.wait == undefined ? "" : defaults.wait;
        if (defaults.wait) { }
            //Msg.log("正在请求...");
        if (defaults.method == "POST" || defaults.method == "PUT") {
            //url = url + '?' + Signature();
            if (typeof (defaults.data) == "object")
                senddata = JSON.stringify(defaults.data);
            else
                senddata = defaults.data.toString();
        } else {
            defaults.url = defaults.url + '?' + Signature(defaults.data);
        } 
        defaults.url = SRequest_apihost + defaults.url;
        $.ajax({
            type: defaults.method,
            data: senddata,
            dataType: 'json',
            async: defaults.$async,
            contentType: 'application/json',
            url: defaults.url,
            crossDomain: true,
            error: function (res) {
                if (defaults.error) {
                    defaults.error();
                } 
                //if (wait)
                //    Msg.CloseLoading();
                //Msg.Notify('网络不好，请重试');
            },
            success: function (d) {
                if (defaults.wait) { }
                    //Msg.CloseLoading();
                if (typeof (d) == "string") {
                    d = JSON.parse(d);
                }
                var obj = d;
                if (obj.code == defaults.code) {
                    var data = "";
                    if (typeof (obj.data) == "string") {
                        data = JSON.parse(obj.data);
                    }
                    if (defaults.done) { 
                        defaults.done(obj.data);
                    } 
                } else {
                    layer.msg(obj.msg);
                }
                
            }
        });
    },
    Get: function (options) {  var defaults = defaults_req;  defaults.method = "GET";  defaults = $.extend(defaults, options);  this.req(defaults);   },
    Post: function (options) { var defaults = defaults_req; defaults.method = "POST"; defaults = $.extend(defaults, options); this.req(defaults); },
    Put: function (options) { var defaults = defaults_req; defaults.method = "PUT"; defaults = $.extend(defaults, options); this.req(defaults); },
    Delete: function (options) { var defaults = defaults_req; defaults.method = "DELETE"; defaults = $.extend(defaults, options); this.req(defaults); },
    GetHost: function() {
        return window.location.host;
    }
};

function Signature(data) {
    if (data == undefined || data == null)
        data = {};
    //data.token = GetToken();
    //data.key = appkey;
    //data.timestamp = parseInt(new Date().getTime() / 1000);
    var ps = [];
    var routeparam = '';
    for (var p in data) {
        ps.push(p);
    }
    ps = ps.sort();
    //console.log(ps);
    var empty = '';
    for (var i = 0; i < ps.length; i++) {
        if (data[ps[i]] != null && (data[ps[i]].toString() != '' || data[ps[i]].toString() == '0')) {
            routeparam += (routeparam == '' ? '' : '&') + ps[i] + '=' + EscapeDataString(data[ps[i]]);
        }
        else {
            if (ps[i])
                empty += ps[i] + '=&';
        }
    }
    if (empty)
        empty = '&' + empty.substr(0, empty.length - 1);
    //console.log(routeparam);
    //return routeparam + '&sign=' + new MD5().getStr(routeparam + '&secret=' + appsecret).toUpperCase() + empty;
    return routeparam + '&sign=' + empty;
}
function EscapeDataString(str) {
    str = encodeURIComponent(str);
    str = str.replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/\*/g, '%2A');
    return str;
}
function Resize(url, m, w, h) {
    if (url.indexOf('?') > -1)
        url = url.split('?')[0];
    var rel = url + "?x-oss-process=image/resize,";
    if (m)
        rel += "m_" + m;
    if (w > 0)
        rel += ",w_" + w;
    if (h > 0)
        rel += ",h_" + h;
    return rel;
}; 
