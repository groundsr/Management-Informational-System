$(document).ready(function () {
    const calendar = document.querySelector("#app-calendar")
    var monthDays;
    var now = new Date();
    var currentYear = now.getFullYear();
    var currentMonth = now.getMonth();
    monthDays = new Date(now.getFullYear(), now.getMonth() + 1, 0).getDate();

    const isWeekend = day => {
        return day == "Sat" || day == "Sun";
    }

    const getDayName = (day) => {

        var date = new Date(Date.UTC(currentYear, currentMonth, day));
        const options = { "weekday": "short" };
        return new Intl.DateTimeFormat("en-US", options).format(date);
    }


    for (let day = 1; day <= monthDays; day++) {
        const dayName = getDayName(day);
        const weekend = isWeekend(dayName);
        var name = "";
        if (day <= 7) {
            name = `<span class="day-name">${dayName}</span>`;
        }
        calendar.insertAdjacentHTML("beforeend",
            `<div class ="day ${weekend ? "weekend" : ""}"><div>${day} ${name}</div></div>`
        );
    }



})