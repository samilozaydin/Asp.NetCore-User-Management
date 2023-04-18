var lat;
var lon;
var accessKey = "8828c53949018081b889d7e66c26b34c";

async function useAPI() {
    let result = await getLocation();
    lat = result.latitude;
    lon = result.longitude;

    let accessToAPI =
        "https://api.openweathermap.org/data/2.5/weather?" +
        "lat=" +
        lat +
        "&lon=" +
        lon +
        "&APPID=" +
        accessKey +
        "&units=metric";
    let jsonData = await callAPI(accessToAPI);
    putInformationIntoView(jsonData);
}
function getLocation() {
    return new Promise((resolve, reject) => {
        navigator.geolocation.getCurrentPosition(
            position => {
                resolve(position.coords);
            },
            error => {
                reject(error);
            }
        );
    });
}
async function callAPI(accessToAPI) {
    let call = await fetch(accessToAPI);
    return call.json();
}
async function getIconURL(iconCode) {

    let url = "https://openweathermap.org/img/w/" + iconCode + ".png";
    return url;
}
async function putInformationIntoView(jsonData) {
    const iconURL = await getIconURL(jsonData.weather[0]["icon"]);
    let icon = document.getElementById("weather-img");
    icon.src = iconURL;

    let information = document.getElementsByClassName("APIinfo");
    let str = jsonData.weather[0]["description"] + " "
    str += jsonData.main["temp"] + "° ";
    str += "wind: " + jsonData.wind["speed"] + " ";
    str += "degree: " + jsonData.wind["deg"] + " ";
    str = capitalizeAllWords(str);
    information[0].innerHTML = str;
}
function capitalizeAllWords(str) {
    // Convert string to array of words
    let words = str.split(" ");

    // Loop through array and capitalize first letter of each word
    for (let i = 0; i < words.length; i++) {
        let word = words[i];
        words[i] = word.charAt(0).toUpperCase() + word.slice(1);
    }

    // Join array back into single string
    let capitalizedStr = words.join(" ");

    return capitalizedStr;
}
useAPI();