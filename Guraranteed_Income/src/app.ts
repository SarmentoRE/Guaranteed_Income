import Person from "person";
import HTTPPost from "httpPost"

export class App {
  personOne = new Person();

  attached() {
    var self = this;
    this.personOne.income = 50000;
    this.personOne.age = 60;

    var post = new HTTPPost();
    var results = post.SendData(this.personOne)
    document.getElementById("ageInput").addEventListener("blur", function(){self.ShowElement("age","ageInput")})
    document.getElementById("retireDateInput").addEventListener("blur", function(){self.ShowElement("retireDate","retireDateInput")})
  }

  OpenSidebar() {
    document.getElementById("sidebar").style.width = "20%";
    document.getElementById("sidebar").style.display = "block";
  }
  CloseSidebar() {
    document.getElementById("sidebar").style.display = "none";
  }

  HideElement(elementOne, elementTwo) {
    var link = document.getElementById(elementOne);
    var text = document.getElementById(elementTwo);
    link.style.display = "none";
    text.style.display = "block";
    text.focus();
  }

  ShowElement(elementOne, elementTwo) {
    var link = document.getElementById(elementOne);
    var text = document.getElementById(elementTwo);
    link.style.display = "block";
    text.style.display = "none";
  }


  results;
}
