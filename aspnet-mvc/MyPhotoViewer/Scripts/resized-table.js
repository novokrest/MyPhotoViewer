function HtmlElement($container, parent) {
    this.$container = $container;
    $(parent).append($container);
}
HtmlElement.prototype.get = function() { return this.$container; }
HtmlElement.prototype.remove = function() { this.$container.remove(); }
HtmlElement.prototype.width = function() { return this.$container.width(); }
HtmlElement.prototype.addChild = function(htmlElement) { this.$container.append(htmlElement.$container); }


function ResizedTableCell(content, parent, width) {
    HtmlElement.call(this, $('<div class="resized-table-cell"></div>'), parent);
    this.content = content;
    this.get().outerWidth(width);
    this.get().append(content);
}
ResizedTableCell.prototype = Object.create(HtmlElement.prototype);
ResizedTableCell.prototype.constructor = ResizedTableCell;


function ResizedTableRow(parent, height) {
    HtmlElement.call(this, $('<div class="resized-table-row"></div>'), parent);
    this.get().outerHeight(height);
    this.cells = [];
}
ResizedTableRow.prototype = Object.create(HtmlElement.prototype);
ResizedTableRow.prototype.constructor = ResizedTableRow;

ResizedTableRow.prototype.addCell = function(content, width) {
    var cell = new ResizedTableCell(content, this.get(), width);
    this.cells.push(cell);
}

ResizedTableRow.prototype.getCell = function(cellNumber) {
    return this.cells[cellNumber];
}

function ResizedTable(elements, parent) {
    HtmlElement.call(this, $('<div class="resized-table"></div>'), parent);
    this.elements = elements;
    this.columnsCount = 0;
    this.rowsCount = 0;
    this.rowHeight = 200;
    this.columnWidth = 300;

    var resize = (function(resizedTable) {
        return function() { resizedTable.resize(); }
    })(this);

    $(window).on('resize', resize)
             .trigger('resize');
}
ResizedTable.prototype = Object.create(HtmlElement.prototype);
ResizedTable.prototype.constructor = ResizedTable;

ResizedTable.prototype.sayHello = function() { console.log("Hello"); }
ResizedTable.prototype.toString = function() { return "ResizedTable"; };

ResizedTable.prototype.resize = function() {
    console.log(this + ': resize()');

    var newColumnsCount = this.computeColumnsCount();
    if (newColumnsCount != this.columnsCount) {
        this.changeColumnsCount(newColumnsCount);
    }

    console.log('columns count: ' + this.columnsCount);
}

ResizedTable.prototype.computeColumnsCount = function() {
    var availableWidth = this.get().width();
    var columnWidth = this.getCellWidth();
    console.log('available width: %s, column width: %s', availableWidth, columnWidth);

    var columnsCount = ~~(availableWidth / columnWidth);
    return columnsCount;
}

ResizedTable.prototype.getCellWidth = function() {
    if (this.elements.length == 0) return this.$container.width();
    return this.columnWidth;
}

ResizedTable.prototype.changeColumnsCount = function(columnsCount) {
    console.log(this + ': changeColumnsCount');
    this.columnsCount = columnsCount;
    this.rowsCount = Math.ceil(this.elements.length / columnsCount);
    console.log('columns count: %s, rows count: %s', this.columnsCount, this.rowsCount);

    this.updateRowsCount(this.rowsCount);
}

ResizedTable.prototype.updateRowsCount = function(rowsCount) {
    this.removeRows();
    this.createRows(rowsCount);
    this.shuffleElements();
}

ResizedTable.prototype.removeRows = function() {
    $.each(this.rows, function(index, row) {
        row.remove();
    } );
}

ResizedTable.prototype.createRows = function(rowsCount) {
    var rows = [];
    var resizedTable = this;
    $.each(Array(rowsCount), function(index, row) {
        var row = new ResizedTableRow(resizedTable.get(), resizedTable.rowHeight);
        rows.push(row);
    });
    this.rows = rows;

    console.log('rows created: %s', this.rows.length);
}

ResizedTable.prototype.getRow = function(rowNumber) {
    console.log('getRow(): %s', rowNumber);
    return this.rows[rowNumber];
}

ResizedTable.prototype.shuffleElements = function() {
    console.log('shuffling...');
    var resizedTable = this;
    $.each(this.elements, function(index, element) {
        var rowNumber = Math.floor(index / resizedTable.columnsCount);
        var columnNumber = index % resizedTable.columnsCount;
        resizedTable.getRow(rowNumber).addCell(element, resizedTable.columnWidth);
    });
    console.log('shuffled');
}
