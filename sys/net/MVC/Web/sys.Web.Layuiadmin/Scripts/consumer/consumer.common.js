/*----------内容浏览-------------
一、 扩展
 1.时间格式 dateFormat    例:str.dateFormat("yyyy-MM-dd")
 2-3.数组max方法和min方法   例:ary.max(),ary.min()
 
二、  判断
 1.空判断：  例:isEmpty(obj)


三、 数据分组
 1.分组    例:var updateArry = groupBy(dirtyItems, function (arryitem) {  return [arryitem.Mark, arryitem.Source];   });

四、 其他通用
 1.生成guid  例：guid()
 */
/*==============================扩展 开始============================= */
/*==============================扩展 结束=============================*/
/*==============================扩展 开始=============================  */
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
//数组max方法和min方法
Array.prototype.max = function () {
    return Math.max.apply({}, this)
}
//数组max方法和min方法
Array.prototype.min = function () {
    return Math.min.apply({}, this)
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
function guid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
/*==============================其他通用 结束==============================*/
