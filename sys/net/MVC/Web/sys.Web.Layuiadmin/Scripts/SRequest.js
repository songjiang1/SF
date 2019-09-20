var SRequest_apihost = 'http://' + window.location.host + '/';
//SRequest_apihost = "http://127.0.0.1:8088/";
var SRequest = {
    req: function (url, data, callback, method, wait, $async) {
        var senddata = '';
        $async = $async == undefined ? true : false;
        wait = wait == undefined ? "" : wait;
        if (wait) { }
            //Msg.log("正在请求...");
        if (method == "POST" || method == "PUT") {
            //url = url + '?' + Signature();
            if (typeof (data) == "object")
                senddata = JSON.stringify(data);
            else
                senddata = data.toString();
        } else {
            url = url + '?' + Signature(data);
        } 
        url = SRequest_apihost + url;
        $.ajax({
            type: method,
            data: senddata,
            dataType: 'json',
            async: $async,
            contentType: 'application/json',
            url: url,
            crossDomain: true,
            error: function (res) {
                //console.log(res);
                //if (wait)
                //    Msg.CloseLoading();
                //Msg.Notify('网络不好，请重试');
            },
            success: function (data) {
                if (wait) { }
                    //Msg.CloseLoading();
                var obj = data;
                var data = "";
                if (obj.type == 1) {
                    data = eval("(" + obj.message + ")");
                    if (callback && typeof (callback) == "function") { 
                        callback(data);
                    }
                } 
                
            }
        });
    },
    Get: function (url, data, callback, wait, async) { this.req(url, data, callback, 'GET', wait, async); },
    Post: function (url, data, callback, wait, async) { this.req(url, data, callback, 'POST', wait, async); },
    Put: function (url, data, callback, wait, async) { this.req(url, data, callback, 'PUT', wait, async); },
    Delete: function (url, data, callback, wait, async) { this.req(url, data, callback, 'DELETE', wait, async); }
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
}
function(n, t) {
    "use strict";
    n.extend(t, { 
        reload: function () {
            return location.reload(),
                !1
        }, 
        dialogOpen: function (i) {
            t.loading({
                isShow: !0
            });
            var i = n.extend({
                id: null,
                title: "系统窗口",
                width: "100px",
                height: "100px",
                url: "",
                shade: .3,
                btn: ["确认", "关闭"],
                callBack: null
            },
                i),
                r = i.url,
                u = top.$.windowWidth() > parseInt(i.width.replace("px", "")) ? i.width : top.$.windowWidth() + "px",
                f = top.$.windowHeight() > parseInt(i.height.replace("px", "")) ? i.height : top.$.windowHeight() + "px";
            top.layer.open({
                id: i.id,
                type: 2,
                shade: i.shade,
                title: i.title,
                fix: !1,
                area: [u, f],
                content: top.contentPath + r,
                btn: i.btn,
                success: function () {
                    t.loading({
                        isShow: !1
                    })
                },
                yes: function () {
                    i.callBack(i.id)
                },
                cancel: function () {
                    return i.cancel != undefined && i.cancel(),
                        !0
                }
            })
        }, 
        dialogAlert: function (n) {
            n.type == -1 && (n.type = 2);
            top.layer.alert(n.msg, {
                icon: n.type,
                title: "系统提示",
                success: function () {
                    t.loading({
                        isShow: !1
                    })
                }
            })
        },
        dialogConfirm: function (n) {
            top.layer.confirm(n.msg, {
                icon: 7,
                title: "系统提示",
                btn: ["确认", "取消"],
                success: function () {
                    t.loading({
                        isShow: !1
                    })
                }
            },
                function () {
                    n.callBack(!0)
                },
                function () {
                    n.callBack(!1)
                })
        },
        dialogMsg: function (n) {
            n.type == -1 && (n.type = 2);
            top.layer.msg(n.msg, {
                icon: n.type,
                time: 4e3,
                shift: 5
            })
        },
        dialogClose: function () {
            try {
                var n = top.layer.getFrameIndex(window.name),
                    t = top.$("#layui-layer" + n).find(".layui-layer-btn").find("#IsdialogClose"),
                    i = t.is(":checked");
                t.length == 0 && (i = !0);
                i ? top.layer.close(n) : location.reload()
            } catch (r) {
                alert(r)
            }
        },
        downFile: function (t) {
            if (t.url && t.data) {
                t.data = typeof t.data == "string" ? t.data : jQuery.param(t.data);
                var i = "";
                n.each(t.data.split("&"),
                    function () {
                        var n = this.split("=");
                        i += '<input type="hidden" name="' + n[0] + '" value="' + n[1] + '" />'
                    });
                n('<form action="' + t.url + '" method="' + (t.method || "post") + '">' + i + "<\/form>").appendTo("body").submit().remove()
            }
        },
        request: function (n) {
            for (var u = location.search.slice(1), r = u.split("&"), i, t = 0; t < r.length; t++) if (i = r[t].split("="), i[0] == n) return unescape(i[1]) == "undefined" ? "" : unescape(i[1]);
            return ""
        },
        changeUrlParam: function (url, key, value) {
            var reg = new RegExp("(^|)" + key + "=([^&]*)(|$)"),
                tmp = key + "=" + value;
            return url.match(reg) != null ? url.replace(eval(reg), tmp) : url.match("[?]") ? url + "&" + tmp : url + "?" + tmp
        },
        getBrowserName: function () {
            var n = navigator.userAgent,
                t = n.indexOf("Opera") > -1;
            return t ? "Opera" : n.indexOf("Firefox") > -1 ? "FF" : n.indexOf("Chrome") > -1 ? window.navigator.webkitPersistentStorage == undefined ? "Edge" : window.navigator.webkitPersistentStorage.toString().indexOf("DeprecatedStorageQuota") > -1 ? "Chrome" : "360" : n.indexOf("Safari") > -1 ? "Safari" : n.indexOf("compatible") > -1 && n.indexOf("MSIE") > -1 && !t ? "IE" : void 0
        },
        changeStandTab: function (t) {
            n(".standtabactived").removeClass("standtabactived");
            n(t.obj).addClass("standtabactived");
            n(".standtab-pane").css("display", "none");
            n("#" + t.id).css("display", "block")
        },
        windowWidth: function () {
            return n(window).width()
        },
        windowHeight: function () {
            return n(window).height()
        },
        ajax: {
            asyncGet: function (t) {
                var i = null,
                    t = n.extend({
                        type: "GET",
                        dataType: "json",
                        async: !1,
                        cache: !1,
                        success: function (n) {
                            i = n
                        }
                    },
                        t);
                return n.ajax(t),
                    i
            }
        },
        createGuid: function () {
            for (var t = "",
                i, n = 1; n <= 32; n++) i = Math.floor(Math.random() * 16).toString(16),
                    t += i,
                    (n == 8 || n == 12 || n == 16 || n == 20) && (t += "-");
            return t
        },
        isNullOrEmpty: function (n) {
            return typeof n == "string" && n == "" || n == null || n == undefined ? !0 : !1
        },
        isNumber: function (t) {
            n("#" + t).bind("contextmenu",
                function () {
                    return !1
                });
            n("#" + t).css("ime-mode", "disabled");
            n("#" + t).keypress(function (n) {
                if (n.which != 8 && n.which != 0 && (n.which < 48 || n.which > 57)) return !1
            })
        },
        isMoney: function (t) {
            function i(n) {
                return n >= 48 && n <= 57
            }
            function r(n) {
                return n == 8 || n == 46 || n >= 37 && n <= 40 || n == 35 || n == 36 || n == 9 || n == 13
            }
            function u(n) {
                return n == 190 || n == 110
            }
            n("#" + t).bind("contextmenu",
                function () {
                    return !1
                });
            n("#" + t).css("ime-mode", "disabled");
            n("#" + t).bind("keydown",
                function (t) {
                    var f = window.event ? t.keyCode : t.which;
                    return u(f) ? n(this).val().indexOf(".") < 0 : r(f) || i(f) && !t.shiftKey
                })
        },
        isHasImg: function (n) {
            var t = new Image;
            return t.src = n,
                t.fileSize > 0 || t.width > 0 && t.height > 0 ? !0 : !1
        },
        formatDate: function (n, t) {
            var i, r, u;
            if (!n) return "";
            i = n;
            typeof n == "string" && (i = n.indexOf("/Date(") > -1 ? new Date(parseInt(n.replace("/Date(", "").replace(")/", ""), 10)) : new Date(Date.parse(n.replace(/-/g, "/").replace("T", " ").split(".")[0])));
            r = {
                "M+": i.getMonth() + 1,
                "d+": i.getDate(),
                "h+": i.getHours(),
                "m+": i.getMinutes(),
                "s+": i.getSeconds(),
                "q+": Math.floor((i.getMonth() + 3) / 3),
                S: i.getMilliseconds()
            };
            /(y+)/.test(t) && (t = t.replace(RegExp.$1, (i.getFullYear() + "").substr(4 - RegExp.$1.length)));
            for (u in r) new RegExp("(" + u + ")").test(t) && (t = t.replace(RegExp.$1, RegExp.$1.length == 1 ? r[u] : ("00" + r[u]).substr(("" + r[u]).length)));
            return t
        },
        toDecimal: function (n) {
            var r, i, t;
            for (n == null && (n = "0"), n = n.toString().replace(/\$|\,/g, ""), isNaN(n) && (n = "0"), r = n == (n = Math.abs(n)), n = Math.floor(n * 100 + .50000000001), i = n % 100, n = Math.floor(n / 100).toString(), i < 10 && (i = "0" + i), t = 0; t < Math.floor((n.length - (1 + t)) / 3); t++) n = n.substring(0, n.length - (4 * t + 3)) + "" + n.substring(n.length - (4 * t + 3));
            return (r ? "" : "-") + n + "." + i
        },
        countFileSize: function (n) {
            return n < 1024 ? t.toDecimal(n) + " 字节" : n >= 1024 && n < 1048576 ? t.toDecimal(n / 1024) + " KB" : n >= 1048576 && n < 1073741824 ? t.toDecimal(n / 1048576) + " MB" : n >= 1073741824 ? t.toDecimal(n / 1073741824) + " GB" : void 0
        },
        arrayCopy: function (t) {
            return n.map(t,
                function (t) {
                    return n.extend(!0, {},
                        t)
                })
        },
        stringArray: function (n, t) {
            var i = n.split(",");
            return i.splice(i.indexOf(t), 1),
                String(i)
        },
        checkedRow: function (n) {
            var i = !0;
            return n == undefined || n == "" || n == "null" || n == "undefined" ? (i = !1, t.dialogMsg({
                msg: "您没有选中任何数据项,请选中后再操作！",
                type: 0
            })) : n.split(",").length > 1 && (i = !1, t.dialogMsg({
                msg: "很抱歉,一次只能选择一条记录！",
                type: 0
            })),
                i
        },
        saveForm: function (i) {
            var i = n.extend({
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                loading: "正在处理数据...",
                success: null,
                close: !0
            },
                i);
            t.loading({
                isShow: !0,
                text: i.loading
            });
            n("[name=__RequestVerificationToken]").length > 0 && (i.param.__RequestVerificationToken = n("[name=__RequestVerificationToken]").val());
            window.setTimeout(function () {
                n.ajax({
                    url: i.url,
                    data: i.param,
                    type: i.type,
                    dataType: i.dataType,
                    success: function (n) {
                        n.type == "3" ? t.dialogAlert({
                            msg: n.message,
                            type: -1
                        }) : (t.loading({
                            isShow: !1
                        }), t.dialogMsg({
                            msg: n.message,
                            type: 1
                        }), i.success(n), i.close == !0 && t.dialogClose())
                    },
                    error: function (n, i, r) {
                        t.loading({
                            isShow: !1
                        });
                        t.dialogMsg({
                            msg: r,
                            type: -1
                        })
                    },
                    beforeSend: function () {
                        t.loading({
                            isShow: !0,
                            text: i.loading
                        })
                    },
                    complete: function () {
                        t.loading({
                            isShow: !1
                        })
                    }
                })
            },
                500)
        },
        setForm: function (i) {
            var i = n.extend({
                url: "",
                param: [],
                type: "get",
                dataType: "json",
                success: null,
                async: !1,
                cache: !1
            },
                i);
            n.ajax({
                url: i.url,
                data: i.param,
                type: i.type,
                dataType: i.dataType,
                async: i.async,
                success: function (n) {
                    n != null && n.type == "3" ? t.dialogAlert({
                        msg: n.message,
                        type: -1
                    }) : i.success(n)
                },
                error: function (n, i, r) {
                    t.dialogMsg({
                        msg: r,
                        type: -1
                    })
                },
                beforeSend: function () {
                    t.loading({
                        isShow: !0
                    })
                },
                complete: function () {
                    t.loading({
                        isShow: !1
                    })
                }
            })
        },
        removeForm: function (i) {
            var i = n.extend({
                msg: "注：您确定要删除吗？该操作将无法恢复",
                loading: "正在删除数据...",
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                success: null
            },
                i);
            t.dialogConfirm({
                msg: i.msg,
                callBack: function (r) {
                    r && (t.loading({
                        isShow: !0,
                        text: i.loading
                    }), window.setTimeout(function () {
                        var r = i.param;
                        n("[name=__RequestVerificationToken]").length > 0 && (r.__RequestVerificationToken = n("[name=__RequestVerificationToken]").val());
                        n.ajax({
                            url: i.url,
                            data: r,
                            type: i.type,
                            dataType: i.dataType,
                            success: function (n) {
                                n.type == "3" ? t.dialogAlert({
                                    msg: n.message,
                                    type: -1
                                }) : (t.dialogMsg({
                                    msg: n.message,
                                    type: 1
                                }), i.success(n))
                            },
                            error: function (n, i, r) {
                                t.loading({
                                    isShow: !1
                                });
                                t.dialogMsg({
                                    msg: r,
                                    type: -1
                                })
                            },
                            beforeSend: function () {
                                t.loading({
                                    isShow: !0,
                                    text: i.loading
                                })
                            },
                            complete: function () {
                                t.loading({
                                    isShow: !1
                                })
                            }
                        })
                    },
                        500))
                }
            })
        },
        confirmAjax: function (i) {
            var i = n.extend({
                msg: "提示信息",
                loading: "正在处理数据...",
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                success: null
            },
                i);
            t.dialogConfirm({
                msg: i.msg,
                callBack: function (r) {
                    r && (t.loading({
                        isShow: !0,
                        text: i.loading
                    }), window.setTimeout(function () {
                        var r = i.param;
                        n("[name=__RequestVerificationToken]").length > 0 && (r.__RequestVerificationToken = n("[name=__RequestVerificationToken]").val());
                        n.ajax({
                            url: i.url,
                            data: r,
                            type: i.type,
                            dataType: i.dataType,
                            success: function (n) {
                                t.loading({
                                    isShow: !1
                                });
                                n.type == "3" ? t.dialogAlert({
                                    msg: n.message,
                                    type: -1
                                }) : (t.dialogMsg({
                                    msg: n.message,
                                    type: 1
                                }), i.success(n))
                            },
                            error: function (n, i, r) {
                                t.loading({
                                    isShow: !1
                                });
                                t.dialogMsg({
                                    msg: r,
                                    type: -1
                                })
                            },
                            beforeSend: function () {
                                t.loading({
                                    isShow: !0,
                                    text: i.loading
                                })
                            },
                            complete: function () {
                                t.loading({
                                    isShow: !1
                                })
                            }
                        })
                    },
                        200))
                }
            })
        },
        existField: function (i, r, u) {
            var f = n("#" + i),
                e,
                o;
            if (!f.val()) return !1;
            e = {
                keyValue: t.request("keyValue")
            };
            e[i] = f.val();
            o = n.extend(e, u);
            n.ajax({
                url: r,
                data: o,
                type: "get",
                dataType: "text",
                async: !1,
                success: function (n) {
                    n.toLocaleLowerCase() == "false" ? (ValidationMessage(f, "已存在,请重新输入"), f.attr("fieldexist", "yes")) : f.attr("fieldexist", "no")
                },
                error: function (n, i, r) {
                    t.dialogMsg({
                        msg: r,
                        type: -1
                    })
                }
            })
        },
        getDataForm: function (i) {
            var i = n.extend({
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                loading: "正在获取数据...",
                success: null,
                async: !1,
                cache: !1
            },
                i);
            t.loading({
                isShow: !0,
                text: i.loading
            });
            n("[name=__RequestVerificationToken]").length > 0 && (i.param.__RequestVerificationToken = n("[name=__RequestVerificationToken]").val());
            n.ajax({
                url: i.url,
                data: i.param,
                type: i.type,
                dataType: i.dataType,
                async: i.async,
                success: function (n) {
                    n != null && n.type == "3" ? t.dialogAlert({
                        msg: n.message,
                        type: -1
                    }) : i.success(n)
                },
                error: function (n, i, r) {
                    t.dialogMsg({
                        msg: r,
                        type: -1
                    })
                },
                beforeSend: function () {
                    t.loading({
                        isShow: !0
                    })
                },
                complete: function () {
                    t.loading({
                        isShow: !1
                    })
                }
            })
        },
        getSystemFormFields: function (n) {
            var i = t.getIframe(n);
            return i.$ ? (i.$("body").find("[data-systemHideField]").hide(), i.getSystemFields ? i.getSystemFields() : []) : !1
        },
        loadSystemForm: function (i, r) {
            var u = document.getElementById(i),
                f = function () {
                    var n = t.getIframe(i); !n.$ || n.$("body").find("[data-systemHideField]").hide();
                    t.loading({
                        isShow: !1
                    })
                };
            u.attachEvent ? u.attachEvent("onload", f) : u.onload = f;
            n("#" + i).attr("src", r)
        },
        getSystemFormData: function (n) {
            var i = t.getIframe(n);
            return !i || !i.$ ? [] : i.getSystemData ? i.getSystemData() : []
        },
        saveSystemFormData: function (n, i) {
            var r = t.getIframe(n); !r.$ || !r.AcceptClick || r.AcceptClick(i)
        },
        setSystemFormFieldsAuthrize: function (n, i) {
            var r = t.getIframe(n); !r.$ || !r.setSystemFieldsAuthorize || r.setSystemFieldsAuthorize(i)
        }, 
        jsonWhere: function (t, i) {
            if (i != null) {
                var r = [];
                return n(t).each(function (n, t) {
                    i(t) && r.push(t)
                }),
                    r
            }
        }
    })
} (window.jQuery, window.learun)
