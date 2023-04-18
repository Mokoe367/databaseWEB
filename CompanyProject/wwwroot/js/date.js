let today = new Date();
let todayDay = today.getDate();
let todayMonth = today.getMonth() + 1;
let todayYear = today.getFullYear() - 18;

if (todayMonth < 10) {
    todayMonth = "0" + todayMonth.toString();
}
if (todayDay < 10) {
    todayDay = "0" + todayDay.toString();
}

let string = todayYear.toString() + "-" + todayMonth.toString() + "-" + todayDay.toString();
document.getElementById("BirthDate").setAttribute('max', string);