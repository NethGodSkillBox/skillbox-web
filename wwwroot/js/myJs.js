    function myFunction(str, str2) {

        let filt = str;
        var input, filter, table, tr, td, i;

        document.getElementsByClassName("dropdown-toggle")[0].innerHTML = str;

        filter = filt.toUpperCase();
        table = document.getElementById("infoTable");
        tr = table.getElementsByTagName("tr");

        for (i = 0; i < tr.length; i++) {
            td = tr[i].getElementsByTagName("td")[5];
            if (td) {
                td2 = tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0];
                let time = new Date(Date.parse(td2.value));
		
		time.setHours(0,0,0,0);
                if (td.getElementsByClassName("btn")[0].innerHTML.toUpperCase().indexOf(filter) > -1
                    && time >= globalTime1 && time <= globalTime2
                ) {
                    tr[i].style.display = "";
                }
                else if (str == "Все"
                    && time >= globalTime1 && time <= globalTime2) { tr[i].style.display = ""; }
                else {
                    tr[i].style.display = "none";
                }

            }
        }
    }

    function myFunction2(str) {
	var now = new Date();
        if (str == "Сегодня") {
            globalTime1 = new Date();
            globalTime2 = new Date();
        }
        if (str == "Вчера") {
            var today = new Date(now.getFullYear(), now.getMonth(), now.getDate());
            globalTime1 = new Date(today.valueOf() - 86400000);
            globalTime2 = new Date(today.valueOf() - 86400000);
        }
        if (str == "Неделя") {

            var today = new Date(now.getFullYear(), now.getMonth(), now.getDate());
            globalTime1 = new Date(today.valueOf() - 604800000);
            globalTime2 = new Date();
        }
        if (str == "Месяц") {
            globalTime1 = new Date(now.getFullYear(), now.getMonth(), 1);
            globalTime2 = new Date();
        }

        let time1 = document.getElementById("picker1");
        let time2 = document.getElementById("picker2");

        var input, filter, table, tr, td, i, td2, status;

        table = document.getElementById("infoTable");
        status = document.getElementsByClassName("dropdown-toggle")[0].innerHTML.trim();
        tr = table.getElementsByTagName("tr");

        for (i = 0; i < tr.length; i++) {
            td = tr[i].getElementsByTagName("td")[4];
            if (td) {
		td = td.getElementsByTagName("input")[0];
                td2 = tr[i].getElementsByTagName("td")[5].getElementsByClassName("btn")[0].innerHTML.trim();
 		let dt = new Date(Date.parse(td.value));
                let dtNow = new Date();
                let check = false;

		dt.setHours(0,0,0,0);
		dtNow.setHours(0,0,0,0);

                if (time1.value != "" && time2.value != "" &&str == "") {
                        let d1 = new Date(time1.value);
                        let d2 = new Date(time2.value);

                        globalTime1 = d1;
                        globalTime2 = d2;

                        if (dt >= d1 && dt <= d2 && status == td2) { check = true; }
                        else if (dt >= d1 && dt <= d2 && status == "Все") { check = true; }

                }
                else {
                    if (str == "Сегодня" && status == td2 || str == "Сегодня" && status == "Все") {
                        if (dt.getDate() == dtNow.getDate()) {
                            check = true;
                        }
                    }
                    if (str == "Вчера" && status == td2 || str == "Вчера" && status == "Все") {
                        
			globalTime1.setHours(0,0,0,0);
			globalTime2.setHours(0,0,0,0);

                        if (dt >= globalTime1 && dt <= globalTime2) {
                            check = true;
                        }
                    }
                    if (str == "Неделя" && status == td2 || str == "Неделя" && status == "Все") {
                        if (dtNow - dt <= 604800000) {
                            check = true;
                        }
                    }
                    if (str == "Месяц" && status == td2 || str == "Месяц" && status == "Все") {
                        if (dt.getUTCMonth() == dtNow.getUTCMonth()) {
                            check = true;
                        }
                    }
                }
                if (check == false) { tr[i].style.display = "none"; }
                else { tr[i].style.display = ""; }
            }
        }

        var all = 0, work = 0, reject = 0, cancel = 0, get = 0, success = 0, td3 = 0, td4 = 0;

        for (i = 0; i < tr.length; i++) {
            
            td3 = tr[i].getElementsByTagName("td")[4];
            if (td3) {
		td3 = td3.getElementsByTagName("input")[0];
                td4 = tr[i].getElementsByTagName("td")[5].getElementsByClassName("btn")[0].innerHTML;
		let noteTime = new Date(Date.parse(td3.value));
		
		noteTime.setHours(0,0,0,0);
		globalTime1.setHours(0,0,0,0);
		globalTime2.setHours(0,0,0,0);

		if(noteTime >= globalTime1 && noteTime <= globalTime2){
			all += 1;
                	if (td4.trim() == "Получена") { get += 1; }
                	if (td4.trim() == "В работе") { work += 1; }
                	if (td4.trim() == "Выполнена") { success += 1; }
                	if (td4.trim() == "Отклонена") { reject += 1; }
                	if (td4.trim() == "Отменена") { cancel += 1; }
		}
                document.getElementById("allLabel").innerHTML = all;
		document.getElementById("successLabel").innerHTML = success;
 		document.getElementById("inWorkLabel").innerHTML =  work;
 		document.getElementById("rejLabel").innerHTML =  reject;
 		document.getElementById("cancelLabel").innerHTML =  cancel;
 		document.getElementById("getLabel").innerHTML = get;
            }
        }
    }

function changeStatus(obj, str) {
	obj.parentNode.parentNode.getElementsByTagName("button")[0].innerHTML = str;
	obj.parentNode.parentNode.parentNode.parentNode.getElementsByTagName("input")[0].value = str;
}