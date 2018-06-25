import Person from "person";
import HTTPPost from "httpPost"

export class App {
  personOne = new Person();
  personTwo = new Person();

  attached() {
    this.personOne.income = 50000;
    this.personOne.age = 60;

    var post = new HTTPPost();
    var results = post.SendData(this.personOne, this.personTwo)
  }

  results;
}
