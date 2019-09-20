/*jshint -W030 */   // Expected an assignment or function call and instead saw an expression (a && a.fun1())
/*jshint -W004 */   // {a} is already defined (can use let instead of var in es6)
var spreadNS = GC.Spread.Sheets;
var DataValidation = spreadNS.DataValidation;
var ConditionalFormatting = spreadNS.ConditionalFormatting;
var ComparisonOperators = ConditionalFormatting.ComparisonOperators;
var Calc = GC.Spread.CalcEngine;
var ExpressionType = Calc.ExpressionType;
var SheetsCalc = spreadNS.CalcEngine;
var Sparklines = spreadNS.Sparklines;
var Barcode = spreadNS.Barcode;
var isSafari = (function () {
    var tem, M = navigator.userAgent.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
    if (!/trident/i.test(M[1]) && M[1] !== 'Chrome') {
        M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
        if ((tem = navigator.userAgent.match(/version\/(\d+)/i)) != null) M.splice(1, 1, tem[1]);
        return M[0].toLowerCase() === "safari";
    }
    return false;
})();
var isIE = navigator.userAgent.toLowerCase().indexOf('compatible') < 0 && /(trident)(?:.*? rv ([\w.]+)|)/.exec(navigator.userAgent.toLowerCase()) !== null;
var DOWNLOAD_DIALOG_WIDTH = 300;

var spread, excelIO;
var tableIndex = 1, pictureIndex = 1;
var fbx, isShiftKey = false;
var resourceMap = {},
    conditionalFormatTexts = {};
var mergable = false, unmergable = false;
var isFirstChart = true;
var showValue = false;
var showSeriesName = false;
var showCategoryName = false;
var defaultParagraphSeparator = 'p';
 
$(document).ready(function () { 
    //excelIO = new GC.Spread.Excel.IO();
   
});
 
function importFile(file) {
    var fileName = file.name;
    var index = fileName.lastIndexOf('.');
    var fileExt = fileName.substr(index + 1).toLowerCase();
    if (fileExt === 'json' || fileExt === 'ssjson') {
        importSpreadFromJSON(file);
    } else if (fileExt === 'xlsx') {
        importSpreadFromExcel(file);
    } else {
        alert(getResource("messages.invalidImportFile"));
    }
}

function importSpreadFromExcel(file, options) {
    function processPasswordDialog() {
        importSpreadFromExcel(file, {password: getTextValue("txtPassword")});
        setTextValue("txtPassword", "");
    }

    var PASSWORD_DIALOG_WIDTH = 300;
    excelIO.open(file, function (json) {
        importJson(json);
    }, function (e) {
        if (e.errorCode === 0 || e.errorCode === 1) {
            alert(getResource("messages.invalidImportFile"));
        } else if (e.errorCode === 2) {
            $("#passwordError").hide();
            showModal(uiResource.passwordDialog.title, PASSWORD_DIALOG_WIDTH, $("#passwordDialog").children(), processPasswordDialog);
        } else if (e.errorCode === 3) {
            $("#passwordError").show();
            showModal(uiResource.passwordDialog.title, PASSWORD_DIALOG_WIDTH, $("#passwordDialog").children(), processPasswordDialog);
        }
    }, options);
}

function importSpreadFromJSON(file) {
    function importSuccessCallback(responseText) {
        var spreadJson = JSON.parse(responseText);
        importJson(spreadJson);
    }

    var reader = new FileReader();
    reader.onload = function () {
        importSuccessCallback(this.result);
    };
    reader.readAsText(file);
    return true;
}

function importJson(spreadJson) {
    function updateActiveCells() {
        for (var i = 0; i < spread.getSheetCount(); i++) {
            var sheet = spread.getSheet(i);
            columnIndex = sheet.getActiveColumnIndex(),
                rowIndex = sheet.getActiveRowIndex();
            if (columnIndex !== undefined && rowIndex !== undefined) {
                spread.getSheet(i).setActiveCell(rowIndex, columnIndex);
            } else {
                spread.getSheet(i).setActiveCell(0, 0);
            }
        }
    }

    if (spreadJson.version && spreadJson.sheets) {
        spread.unbindAll();
        spread.fromJSON(spreadJson);
        attachSpreadEvents(true);
        updateActiveCells();
        spread.focus();
        fbx.workbook(spread);
        onCellSelected();
        syncSpreadPropertyValues();
        syncSheetPropertyValues();
    } else {
        alert(getResource("messages.invalidImportFile"));
    }
}

function getFileName() {
    function to2DigitsString(num) {
        return ("0" + num).substr(-2);
    }

    var date = new Date();
    return [
        "export",
        date.getFullYear(), to2DigitsString(date.getMonth() + 1), to2DigitsString(date.getDate()),
        to2DigitsString(date.getHours()), to2DigitsString(date.getMinutes()), to2DigitsString(date.getSeconds())
    ].join("");
}

function exportToJSON() {
    var json = spread.toJSON({includeBindingSource: true}),
        text = JSON.stringify(json);
    var fileName = getFileName();
    if (isSafari) {
        showModal(uiResource.toolBar.downloadTitle, DOWNLOAD_DIALOG_WIDTH, $("#downloadDialog").children(), function () {
            $("#downloadDialog").hide();
        });
        var link = $("#download");
        link[0].href = "data:text/plain;" + text;
    } else {
        saveAs(new Blob([text], {type: "text/plain;charset=utf-8"}), fileName + ".json");
    }
}

function exportToExcel() {
    var fileName = getFileName();
    var json = spread.toJSON({includeBindingSource: true});
    excelIO.save(json, function (blob) {
        if (isSafari) {
            var reader = new FileReader();
            reader.onloadend = function () {
                showModal(uiResource.toolBar.downloadTitle, DOWNLOAD_DIALOG_WIDTH, $("#downloadDialog").children(), function () {
                    $("#downloadDialog").hide();
                });
                var link = $("#download");
                link[0].href = reader.result;
            };
            reader.readAsDataURL(blob);
        } else {
            saveAs(blob, fileName + ".xlsx");
        }
    }, function (e) {
        alert(e);
    });
}
 
