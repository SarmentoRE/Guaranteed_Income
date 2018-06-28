import {RouterConfiguration, Router} from 'aurelia-router';
import {PLATFORM} from 'aurelia-pal';
import { Script } from 'vm';
import {Chart} from '../node_modules/chart.js/dist/Chart.js';
import Person from "person";
import HTTPPost from "httpPost";

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

  client = new Person();
  myLineChart;
  results;

  attached() {
    // var self = this;
    // this.personOne.income = 50000;
    // this.personOne.age = 60;

    // (async () => {
    //   const response = await fetch('http://localhost:64655/api/', {
    //     method: 'POST',
    //     headers: {
    //       'Accept': 'application/json',
    //       'Content-Type': 'application/json'
    //     },
    //     body: JSON.stringify("tash")
    //   });
    //   const content = await response.json();
    //   console.log("DONE")
    //   this.results = content;
    // })();   
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
        datasets: [{
          data: [2478,5267,734,784,433]
        }]
      },
      options: {
        title: {
          display: true,
          text: 'Predicted world population (millions) in 2050'
        },
        legend: {
          display: false
       },
       tooltips: {
          enabled: false
       }
      }
  });
    for(var i = 0; i < 100; i ++){
      this.myLineChart.data.datasets.push({
        data: this.results.trialsList[i]
      });
    }
    this.myLineChart.update()

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

  Send(client) {
    var send = new HTTPPost();
    send.SendData(client);
  }
}
