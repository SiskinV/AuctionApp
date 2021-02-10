// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//ovo dodajes sve to je za signalR

// connection = new signalR.HubConnectionBuilder().withUrl("/auctionUpdate").build();

// async function start() {

//     console.log("USAO U START ZNACI POKRENUO KONEKCIJU");
//     await connection.start().then(
//         function () {
//             connection.invoke("UpdateAuctions", "all").catch(err => console.error(err));
//         }
//     );
//     console.log("ISPOD CONNECTION.START");

//     //await connection.invoke("AddToGroup", "all").catch(handleError)
//     //await connection.invoke("UpdateAuctions", "all").catch(err => console.error(err));
//     // connection.invoke ( "UpdateAuctions", "all" )
//     //  .catch ( handleError );
//     $.ajax({
//         type: "POST",
//         url: "/Auction/updateAuctions",
//         dataType: "text",
//         data: {
//         },
//         success: function (response) {
//             updatePage();
//         },
//         error: function (response) {
//             alert(response)
//         }
//     })

// }


// start();


// umestoSignal();

function updateAllAuctions(num) {



    var words = $("#wordSearch").val();
    var min = $("#minSearch").val();
    var max = $("#maxSearch").val();
    var state = $("#stateSearch").val();
    var numOfAuctions = $("#numOfAuctions").val();

    //console.log(num);
    console.log(numOfAuctions);
    console.log("Usao u updateAllAuctions");

    if (localStorage.getItem("Num") == null) {
        localStorage.setItem("Num", numOfAuctions);
    } else {
        localStorage.setItem("Num", numOfAuctions);
    }

    $.ajax({
        type: "GET",
        url: "/Auction/getAllAuctions",
        dataType: "text",
        data: {
            num: num,
            words: words,
            min: min,
            max: max,
            state: state,
            numOfAuctions: numOfAuctions
        },
        success: function (response) {
            $("#auctionList").html(response)
        },
        error: function (response) {
            console.log(response)
            alert(response.error)
        }
    })
}

function updateDetails() {
    var id = $("#id").val();
    $.ajax({
        type: "GET",
        url: "/Auction/getDetails?id=" + id,
        dataType: "text",
        success: function (response) {
            $("#details").html(response);
        },
        error: function (err) {
            alert(err);
        }
    })
}

function updateDrafts(num) {
    console.log("Pozvao updateDrafts");
    $.ajax({
        type: "GET",
        url: "/Auction/getDraftAuction?num=" + num,
        dataType: "text",
        success: function (response) {
            $("#auctions").html(response)
        },
        error: function (response) {
            alert(response);
        }
    })
}

function updateAuctions(num) {
    console.log("Update auctions");
    console.log(num);
    var numOfAuctions = localStorage.getItem("Num");

    $.ajax({
        type: "GET",
        url: "/Auction/getAuctions",
        dataType: "text",
        data: {
            num: num,
            numOfAuctions: numOfAuctions
        },
        success: function (response) {
            $("#myAuctions").html(response)
        },
        error: function (response) {
            alert(response)
        }
    })
}


// function umestoSignal() {
//     var f = setInterval(function () {
//         console.log("UmestoSignalR");
//         $.ajax({
//             type: "POST",
//             url: "/Auction/updateAuctions"
//         })
//     }, 10000)
// }

function countdown(element, hours, minutes, seconds) {
    localStorage.setItem(element, JSON.stringify(true));
    var interval = setInterval(function () {
        if (seconds == 0) {
            if (minutes == 0) {
                if (hours == 0) {
                    clearInterval(interval);
                    localStorage.removeItem(element);
                    //connection.invoke("UpdateAuctions", "all").catch(handleError)
                    return;
                }
                else {
                    hours--;
                    minutes = 60;
                    seconds = 60;
                }
            } else {
                minutes--;
                seconds = 60;
            }
        }
        hrtxt = hours;
        mintxt = minutes;
        sectxt = seconds;
        if (hrtxt < 10) hrtxt = "0" + hrtxt;
        if (mintxt < 10) mintxt = "0" + mintxt;
        if (sectxt < 10) sectxt = "0" + sectxt;

        $(element).text(hrtxt + ":" + mintxt + ":" + sectxt);
        seconds--;

        //updateAuctions(0);
    }, 1000);
}


// function updatePage() {
//     var pathname = window.location.pathname;
//     if (pathname.length < 5)
//         updateIndex();
//     else if (pathname.includes("Auction/Details"))
//         updateDetails();
// }


// function invoke() {
//     console.log("Usao u glupi invoke");
//     // connection.invoke("UpdateAuctions", "all").catch(handleError);
//     connection.invoke("UpdateAuctions", "all").catch(err => console.error(err));
// }


// connection.on(
//     "updateAuctions",
//     function () {

//         var verificationToken = $("input[name='__RequestVerificationToken']").val();
//         $.ajax({
//             type: "POST",
//             url: "/Auction/updateAuctions",
//             dataType: "text",
//             data: {
//             },
//             success: function (response) {
//                 updatePage();
//             },
//             error: function (response) {
//                 alert(response)
//             }
//         })


//     }
// )

