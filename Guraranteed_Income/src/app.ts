import {RouterConfiguration, Router} from 'aurelia-router';
import {PLATFORM} from 'aurelia-pal';
import { Script } from 'vm';
import {Chart} from '../node_modules/chart.js/dist/Chart.js';
import Person from "person";

export class App {
  router:Router;
  configureRouter(config, router) {
    config.title = 'Tax Planning';
    config.options.pushState = true;
    config.map([
      {route: ['', 'home'], name: 'home', moduleId: PLATFORM.moduleName('home.html'), nav: true, title: 'Home'},
      {route: 'results', name: 'results', moduleId:  PLATFORM.moduleName('results.html'), nav: true, title: 'Results'},
    ]);
    this.router = router;
  }

  personOne = new Person();
  myLineChart;

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

    this.myLineChart = new Chart(document.getElementById("line-chart"), {
      type: 'line',
      data: {
        labels: ["Africa", "Asia", "Europe", "Latin America", "North America"],
        datasets: [{
          label: "Population (millions)",
          backgroundColor: ["#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850"],
          data: [2478,5267,734,784,433]
        }]
      },
      options: {
        title: {
          display: true,
          text: 'Predicted world population (millions) in 2050'
        }
      }
  });
  }

  Reversal(elementOne) {
    var link = document.getElementById(elementOne);

    var generalID = document.getElementById("generalBody");
    var financesID = document.getElementById("financesBody");


    var ids= [generalID, financesID];

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
