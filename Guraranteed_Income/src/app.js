import Person from "person";
var App = /** @class */ (function () {
    function App() {
        this.personOne = new Person();
    }
    App.prototype.attached = function () {
        // var self = this;
        // this.personOne.income = 50000;
        // this.personOne.age = 60;
        // var post = new HTTPPost();
        // var results = post.SendData(this.personOne)
        // document.getElementById("ageInput2").addEventListener("blur", function(){self.ShowElement("age","ageInput")})
        // document.getElementById("retireDateInput").addEventListener("blur", function(){self.ShowElement("retireDate","retireDateInput")})
        // document.getElementById("genderInput").addEventListener("blur", function(){self.ShowElement("gender","genderInput")})
    };
    App.prototype.OpenSidebar = function () {
        document.getElementById("sidebar").style.width = "20%";
        document.getElementById("sidebar").style.display = "block";
    };
    App.prototype.CloseSidebar = function () {
        document.getElementById("sidebar").style.display = "none";
    };
    App.prototype.ShowElement = function (elementOne) {
        var link = document.getElementById(elementOne);
        link.classList.add("is-active");
    };
    App.prototype.HideElement = function (elementOne) {
        var link = document.getElementById(elementOne);
        link.classList.remove("is-active");
    };
    App.prototype.ToggleMenu = function () {
        var menu = document.getElementById("menu");
        if (menu.style.display == "none") {
            menu.style.display = "block";
        }
        else {
            menu.style.display = "none";
        }
    };
    App.prototype.Reversal = function (elementOne) {
        var link = document.getElementById(elementOne);
        var genderID = document.getElementById("genderBody");
        var ageID = document.getElementById("ageBody");
        var incomeID = document.getElementById("incomeBody");
        var retireID = document.getElementById("retireBody");
        var deathID = document.getElementById("deathBody");
        var filingID = document.getElementById("filingBody");
        var ids = [genderID, ageID, incomeID, retireID, deathID, filingID];
        if (link.style.display == "none") {
            for (var i = 0; i < ids.length; i++) {
                if (ids[i].style.display == "block") {
                    ids[i].style.display = "none";
                }
            }
            link.style.display = "block";
        }
        else {
            link.style.display = "none";
        }
    };
    return App;
}());
export { App };
//# sourceMappingURL=app.js.map