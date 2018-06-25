import Person from "person";
import HTTPPost from "httpPost"

export class App {
  personOne = new Person();

  attached() {
    this.personOne.income = 50000;
    this.personOne.age = 60;

    var post = new HTTPPost();
    var results = post.SendData(this.personOne)
  }

  OpenSidebar() {
    document.getElementById("mySidebar").style.width = "100%";
    document.getElementById("mySidebar").style.display = "block";
}
  CloseSidebar() {
    document.getElementById("mySidebar").style.display = "none";
}

  results;
}
