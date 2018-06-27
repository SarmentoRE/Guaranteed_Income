import Person from "person";
import HTTPPost from "httpPost"

export class App {
  personOne = new Person();

  attached() {
    // var self = this;
    // this.personOne.income = 50000;
    // this.personOne.age = 60;

    // var post = new HTTPPost();
    // var results = post.SendData(this.personOne)
    // document.getElementById("ageInput2").addEventListener("blur", function(){self.ShowElement("age","ageInput")})
    // document.getElementById("retireDateInput").addEventListener("blur", function(){self.ShowElement("retireDate","retireDateInput")})
    // document.getElementById("genderInput").addEventListener("blur", function(){self.ShowElement("gender","genderInput")})
  }

  OpenSidebar() {
    document.getElementById("sidebar").style.width = "20%";
    document.getElementById("sidebar").style.display = "block";
  }
  CloseSidebar() {
    document.getElementById("sidebar").style.display = "none";
  }

  ShowElement(elementOne) {
    var link = document.getElementById(elementOne);
    link.classList.add("is-active");
  }

  HideElement(elementOne) {
    var link = document.getElementById(elementOne);
    link.classList.remove("is-active");
  }

  ToggleMenu() {
    var menu = document.getElementById("menu");
    if ( menu.style.display == "none" ) {
      menu.style.display = "block";
    }
    else {
      menu.style.display = "none";
    }
  }

  Reversal(elementOne) {
    var link = document.getElementById(elementOne);

    var genderID = document.getElementById("genderBody");
    var ageID = document.getElementById("ageBody");
    var incomeID = document.getElementById("incomeBody");
    var retireID = document.getElementById("retireBody");
    var deathID = document.getElementById("deathBody");
    var filingID = document.getElementById("filingBody");

    var ids= [genderID, ageID, incomeID, retireID, deathID, filingID];

    if(link.style.display == "none") {
      for (var i = 0; i < ids.length; i++) {
        if ( ids[i].style.display == "block") {
          ids[i].style.display = "none";
        }
      }
      link.style.display = "block";
    }
    else{
      link.style.display = "none";
    }
  }


  results;
}
