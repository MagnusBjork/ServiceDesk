let navToggleStatus = true;
let asideToggleStatus = false;

document.querySelector(".nav-toggle").addEventListener("click", () => {
    let nav = document.querySelector("nav");

    if (navToggleStatus === false) {
        nav.style.width = "200px";
    } else {
        nav.style.width = "0px";
    }

    navToggleStatus = !navToggleStatus;
});

let asideToggleElements = document.querySelectorAll(".aside-toggle");
asideToggleElements.forEach(function (elem) {
    elem.addEventListener("click", () => {
        let aside = document.querySelector("aside");

        if (asideToggleStatus === false) {
            aside.style.width = "300px";
        } else {
            aside.style.width = "0px";
        }
        asideToggleStatus = !asideToggleStatus;
    });
});



const formMessage = document.querySelector(".form-message");
if (getComputedStyle(formMessage).display == 'block') {
    setTimeout(function () {
        formMessage.style.opacity = '0';
    }, 3000);
    setTimeout(function () {
        formMessage.style.display = 'none';
    }, 5000);
}