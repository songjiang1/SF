var fromRange, fromSheet;
//sheet:shee  row:开始行, col：开始列, rowcount ：行数, colcount：列数, step ：步长（跨行数）,forNumber：循环数 point 循环指向
function addTableStyle(sheet, row, col, rowcount, colcount, step, forNumber, point) {

    row = parseInt(row);
    col = parseInt(col);
    if (point === undefined || point === null || point === "") {
        point = "Portrait";
    }
    rowcount = parseInt(rowcount);
    colcount = parseInt(colcount);
    step = parseInt(step);
    var selectionRange = sheet.getRange(row, col, rowcount, colcount);
    fromRange = selectionRange;
    fromSheet = sheet;
    //sheet.isPaintSuspended(true);
    var addRowIndex = row;
    var addColIndex = col;
    if (point.toLowerCase() === "transverse") {
        addColIndex = col + step * forNumber;
    }
    else {
        addRowIndex = row + step * forNumber;
        //sheet.addRows(addRowIndex, step);  
    }
    //sheet.repaint();
    //spread.resumePaint();
    var toRange = sheet.getRange(addRowIndex, addColIndex, rowcount, colCount);
    //toRange biger than fromRange
    if (fromRange.rowCount > toRange.rowCount) {
        toRange.rowCount = fromRange.rowCount;
    }
    if (fromRange.colCount > toRange.colCount) {
        toRange.colCount = fromRange.colCount;
    }
    //toRange must in Sheet
    if (toRange.row + toRange.rowCount > sheet.getRowCount()) {
        toRange.rowCount = sheet.getRowCount() - toRange.row;
    }
    if (toRange.col + toRange.colCount > sheet.getColumnCount()) {
        toRange.colCount = sheet.getColumnCount() - toRange.col;
    }

    var rowStep = fromRange.rowCount,
        colStep = fromRange.colCount;
    var endRow = toRange.row + toRange.rowCount - 1,
        endCol = toRange.col + toRange.colCount - 1;

    // if toRange bigger than fromRange, repeat paint
    for (var startRow = toRange.row; startRow <= endRow; startRow = startRow + rowStep) {
        for (var startCol = toRange.col; startCol <= endCol; startCol = startCol + colStep) {

            var rowCount = startRow + rowStep > endRow + 1 ? endRow - startRow + 1 : rowStep;
            var colCount = startCol + colStep > endCol + 1 ? endCol - startCol + 1 : colStep;
            // sheet.copyTo(fromRange.row,fromRange.col, startRow, startCol, rowCount, colCount,GC.Spread.Sheets.CopyToOptions.style | GC.Spread.Sheets.CopyToOptions.span);
            var fromRanges = new GC.Spread.Sheets.Range(fromRange.row, fromRange.col, rowCount, colCount);
            var pastedRange = new GC.Spread.Sheets.Range(startRow, startCol, rowCount, colCount);
            spread.commandManager().execute({
                cmd: "clipboardPaste",
                sheetName: sheet.name(),
                fromSheet: fromSheet,
                fromRanges: [fromRanges],
                pastedRanges: [pastedRange],
                isCutting: false,
                clipboardText: "",
                pasteOption: GC.Spread.Sheets.ClipboardPasteOptions.formulas
            });
        }
    }

    //sheet.isPaintSuspended(false);
}
var copyFromRange, copyFromSheet;
//sheet:shee  row:开始行, col：开始列, rowcount ：行数, colcount：列数, repeaterCount:循环个数,  point 循环指向 ,pasteOption枚举值：GC.Spread.Sheets.ClipboardPasteOptions.formulas
// all//粘贴所有数据对象，包括值，格式和公式。 formatting	//仅粘贴格式。 formulas	//仅粘贴公式。 formulasAndFormatting	//粘贴公式和格式。 values	//仅粘贴值。 valuesAndFormatting	 //粘贴值和格式。
function addCopyStyle(sheet, row, col, rowcount, colcount, step, repeaterCount, forNumber, point, pasteOption, isAdd = true) {

    var row = parseInt(row);
    var rowcount = parseInt(rowcount);
    var colcountAll = sheet.getColumnCount();
    if (point === undefined || point === null || point === "") {
        point = "Portrait";
    }
    var selectionRange;
    if (isAdd) {
        //selectionRange = sheet.getRange(row, 0, rowcount, colcountAll); 
        if (point != "Portrait") {
            selectionRange = sheet.getRange(row, 0, rowcount, (step * repeaterCount + col));
        } else {
            selectionRange = sheet.getRange(row, 0, rowcount, colcount + col);
        }
    } else {
        selectionRange = sheet.getRange(row, 0, rowcount, colcount);
    }
    copyFromRange = selectionRange;
    copyFromSheet = sheet;
    //sheet.isPaintSuspended(true);  
    var addRowIndex = row;
    var addColIndex = col;

    addRowIndex = row + rowcount;
    if (isAdd) {
        if (point != "Portrait") {
            sheet.addRows(addRowIndex, rowcount);
        } else {
            sheet.addRows((row + forNumber * step), rowcount);
        }

    }

    //sheet.repaint();
    //spread.resumePaint();
    var toRange;
    if (point != "Portrait") {
        toRange = sheet.getRange(addRowIndex, 0, rowcount, (step * repeaterCount + col));
    } else {
        toRange = sheet.getRange((row + forNumber * step), 0, rowcount, colcount + col)
    }
    //toRange biger than fromRange
    if (copyFromRange.rowCount > toRange.rowCount) {
        toRange.rowCount = copyFromRange.rowCount;
    }
    if (copyFromRange.colCount > toRange.colCount) {
        toRange.colCount = copyFromRange.colCount;
    }
    //toRange must in Sheet
    if (toRange.row + toRange.rowCount > sheet.getRowCount()) {
        toRange.rowCount = sheet.getRowCount() - toRange.row;
    }
    if (toRange.col + toRange.colCount > sheet.getColumnCount()) {
        toRange.colCount = sheet.getColumnCount() - toRange.col;
    }

    var rowStep = copyFromRange.rowCount,
        colStep = copyFromRange.colCount;
    var endRow = toRange.row + toRange.rowCount - 1,
        endCol = toRange.col + toRange.colCount - 1;

    // if toRange bigger than fromRange, repeat paint
    for (var startRow = toRange.row; startRow <= endRow; startRow = startRow + rowStep) {
        for (var startCol = toRange.col; startCol <= endCol; startCol = startCol + colStep) {

            var rowCount = startRow + rowStep > endRow + 1 ? endRow - startRow + 1 : rowStep;
            var colCount = startCol + colStep > endCol + 1 ? endCol - startCol + 1 : colStep;
            // sheet.copyTo(fromRange.row,fromRange.col, startRow, startCol, rowCount, colCount,GC.Spread.Sheets.CopyToOptions.style | GC.Spread.Sheets.CopyToOptions.span);
            var fromRanges = new GC.Spread.Sheets.Range(copyFromRange.row, copyFromRange.col, rowCount, colCount);
            var pastedRange = new GC.Spread.Sheets.Range(startRow, startCol, rowCount, colCount);
            spread.commandManager().execute({
                cmd: "clipboardPaste",
                sheetName: sheet.name(),
                fromSheet: copyFromSheet,
                fromRanges: [fromRanges],
                pastedRanges: [pastedRange],
                isCutting: false,
                clipboardText: "",
                pasteOption: pasteOption
            });
        }
    }

    //sheet.isPaintSuspended(false);
}


$.download = function (n, t, i) {
    var url = n;
    var data = t;
    var method = i;
    if (url && data) {
        data = typeof data === 'string' ? data : jQuery.param(data);
        var inputs = '';
        $.each(data.split('&'), function () {
            var pair = this.split('=');
            inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
        });
        $('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>').appendTo('body').submit().remove();
    };

};

function customFunction(spread) {
    var xiuyue465_ = new xiuyue465Function();
    spread.addCustomFunction(xiuyue465_);
}
function xiuyue465Function() {
    this.name = "XIUYUE465";
    this.maxArgs = 2;
    this.minArgs = 1;
}
xiuyue465Function.prototype = new GC.Spread.CalcEngine.Functions.Function();
xiuyue465Function.prototype.evaluate = function (value, number) {
    if (value != undefined && value != null && value != "" && value != 0 && !isNaN(parseInt(number))) {
        return value.toFixed(number);
    }
    return "#VALUE!";
};

