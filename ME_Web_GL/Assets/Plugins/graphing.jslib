mergeInto(LibraryManager.library, {

AddPointCarriage00: function (x, y) {
		var table = document.getElementById("carriage00_table");
		var rowCount = table.rows.length;
    		var row = table.insertRow(rowCount);
		row.insertCell(0).innerHTML= x;
    		row.insertCell(1).innerHTML= y;
	},

AddPointCarriage01: function (x, y) {
		var table = document.getElementById("carriage01_table");
		var rowCount = table.rows.length;
    		var row = table.insertRow(rowCount);
		row.insertCell(0).innerHTML= x;
    		row.insertCell(1).innerHTML= y;
	},

AddPointCarriage02: function (x, y) {
		var table = document.getElementById("carriage02_table");
		var rowCount = table.rows.length;
    		var row = table.insertRow(rowCount);
		row.insertCell(0).innerHTML= x;
    		row.insertCell(1).innerHTML= y;
	},
});