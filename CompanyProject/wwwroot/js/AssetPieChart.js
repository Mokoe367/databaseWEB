$(document).ready(function () {
    $('table').DataTable();
});

google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart1);
google.charts.setOnLoadCallback(drawChart2);
google.charts.setOnLoadCallback(drawChart3);
google.charts.setOnLoadCallback(drawChart4);
google.charts.setOnLoadCallback(drawChart5);
google.charts.setOnLoadCallback(drawChart6);

function drawChart1() {
    type = new Array();
    cost = new Array();
    var table = document.getElementById("department");
    for (let row of table.rows) {
        let i = 0;
        for (let cell of row.cells) {
            if (i == 1) {
                type.push(cell.innerText);
                i++;
            }
            else if (i == 4) {
                cost.push(cell.innerText);
                i++;
            }
            else {
                i++;
            }
        }
    }
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'type');
    data.addColumn('number', 'cost');

    for (let i = 1; i < type.length; i++) {
        data.addRow([type[i], parseFloat(cost[i].replace('$', ''))]);
    }
    var options = {
        title: 'Department Distributions'
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart'));

    chart.draw(data, options);
}

function drawChart2() {
    supplier = new Array();
    cost = new Array();
    var table = document.getElementById("departmentTotal");
    for (let row of table.rows) {
        let i = 0;
        for (let cell of row.cells) {
            if (i == 0) {
                supplier.push(cell.innerText);
                i++;
            }
            else {
                cost.push(cell.innerText);
                i++
            }
        }
    }
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'supplier');
    data.addColumn('number', 'cost');

    for (let i = 1; i < supplier.length; i++) {
        data.addRow([supplier[i], parseFloat(cost[i].replace('$', ''))]);
    }
    var options = {
        title: 'Supplier Totals'
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart3'));

    chart.draw(data, options);
}

function drawChart3() {
    employee = new Array();
    cost = new Array();
    var table = document.getElementById("employee");
    for (let row of table.rows) {
        let i = 0;
        let string = "";
        for (let cell of row.cells) {
            if (i == 0) {
                string += cell.innerText;
                i++;
            }
            else if (i == 2) {
                string += " - " + cell.innerText;
                employee.push(string);
                i++;
            }
            else if (i == 5) {
                cost.push(cell.innerText);
                i++;
            }
            else {
                i++;
            }
        }
    }
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'employee');
    data.addColumn('number', 'cost');

    for (let i = 1; i < employee.length; i++) {
        data.addRow([employee[i], parseFloat(cost[i].replace('$', ''))]);
    }
    var options = {
        title: 'Employee Used Assets'
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart2'));

    chart.draw(data, options);
}

function drawChart4() {
    employee = new Array();
    cost = new Array();
    var table = document.getElementById("ByEmployeeTable");
    for (let row of table.rows) {
        let i = 0;
        for (let cell of row.cells) {
            if (i == 0) {
                employee.push(cell.innerText);
                i++;
            }
            else {
                cost.push(cell.innerText);
                i++
            }
        }
    }
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'employee');
    data.addColumn('number', 'cost');

    for (let i = 1; i < employee.length; i++) {
        data.addRow([employee[i], parseFloat(cost[i].replace('$', ''))]);
    }
    var options = {
        title: 'Employee Totals',
        width: 900,
        height: 500,
        pieSliceText: 'none'
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart4'));

    chart.draw(data, options);
}

function drawChart5() {
    supplier = new Array();
    cost = new Array();
    var table = document.getElementById("BySupplierTable");
    for (let row of table.rows) {
        let i = 0;
        for (let cell of row.cells) {
            if (i == 0) {
                supplier.push(cell.innerText);
                i++;
            }
            else {
                cost.push(cell.innerText);
                i++
            }
        }
    }
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'supplier');
    data.addColumn('number', 'cost');

    for (let i = 1; i < supplier.length; i++) {
        data.addRow([supplier[i], parseFloat(cost[i].replace('$', ''))]);
    }
    var options = {
        title: 'Supplier Totals',
        width: 900,
        height: 500,
        pieSliceText: 'none'
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart5'));

    chart.draw(data, options);
}

function drawChart6() {
    type = new Array();
    cost = new Array();
    var table = document.getElementById("ByTypeTable");
    for (let row of table.rows) {
        let i = 0;
        for (let cell of row.cells) {
            if (i == 0) {
                type.push(cell.innerText);
                i++;
            }
            else {
                cost.push(cell.innerText);
                i++
            }
        }
    }
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'type');
    data.addColumn('number', 'cost');

    for (let i = 1; i < type.length; i++) {
        data.addRow([type[i], parseFloat(cost[i].replace('$', ''))]);
    }
    var options = {
        title: 'Type Totals', 
        width: 900,
        height: 500,
        pieSliceText: 'none'
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart6'));

    chart.draw(data, options);
}

document.getElementById("defaultOpen").click();

function openDiv(evt, Name) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(Name).style.display = "block";
    evt.currentTarget.className += " active";
}

