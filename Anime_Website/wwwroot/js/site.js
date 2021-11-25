function displayNextImage() {
    x = (x === images.length - 1) ? 0 : x + 1;
document.getElementById("img").src = images[x];
}

function startTimer() {
    setInterval(displayNextImage, 10000);
}

function fullFett() {
    var el = document.documentElement,
        rfs = el.requestFullScreen
            || el.webkitRequestFullScreen
            || el.mozRequestFullScreen;
    rfs.call(el);
};

var images = [];
x = -1;
images[0] = "../images/1.jpg";
images[1] = "../images/2.jpg";

