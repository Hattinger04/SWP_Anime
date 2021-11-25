function displayNextImage() {
    x = x + 1;
    x = x % 4;
document.getElementById("img").src = images[x];
}

function startTimer() {
    setInterval(displayNextImage, 10000);
}

function enterFullscreen(element) {
    if (element.requestFullscreen) {
        element.requestFullscreen();
    } else if (element.msRequestFullscreen) {      // for IE11 (remove June 15, 2022)
        element.msRequestFullscreen();
    } else if (element.webkitRequestFullscreen) {  // iOS Safari
        element.webkitRequestFullscreen();
    }
}



var images = [];
x = 0;
images[0] = "../images/1.jpg";
images[1] = "../images/2.jpg";
images[2] = "../images/3.jpg";
images[3] = "../images/4.jpg";

