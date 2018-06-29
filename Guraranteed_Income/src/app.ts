import { RouterConfiguration, Router } from 'aurelia-router';
import { PLATFORM } from 'aurelia-pal';
import { Script } from 'vm';
import { Chart } from '../node_modules/chart.js/dist/Chart.js';
import Person from "person";
import HTTPPost from "httpPost";
import { resolveTxt } from 'dns';

export class App {
  router: Router;
  configureRouter(config, router) {
    config.title = 'Tax Planning';
    config.options.pushState = true;
    config.map([
      { route: ['', 'home'], name: 'home', moduleId: PLATFORM.moduleName('home.html'), nav: true, title: 'Home' },
      { route: 'results', name: 'results', moduleId: PLATFORM.moduleName('results.html'), nav: true, title: 'Results' },
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
    // document.getElementById("generalInfo").addEventListener("mouseover",
    //   function () {
    //     var elm = document.getElementById("generalHeader");
    //     elm.style.backgroundColor = "hsl(204, 86%, 53%)";
    //   })
    // document.getElementById("generalInfo").addEventListener("mouseout",
    //   function () {
    //     var elm = document.getElementById("generalHeader");
    //     elm.style.backgroundColor = "#49804A";
    //   })

    // document.getElementById("financesInfo").addEventListener("mouseover",
    //   function () {
    //     var elm = document.getElementById("financesHeader");
    //     elm.style.backgroundColor = "hsl(204, 86%, 53%)";
    //   })
    // document.getElementById("financesInfo").addEventListener("mouseout",
    //   function () {
    //     var elm = document.getElementById("financesHeader");
    //     elm.style.backgroundColor = "#89cc66";
    //   })

    // document.getElementById("otherInfo").addEventListener("mouseover",
    //   function () {
    //     var elm = document.getElementById("otherHeader");
    //     elm.style.backgroundColor = "hsl(204, 86%, 53%)";
    //   })
    // document.getElementById("otherInfo").addEventListener("mouseout",
    //   function () {
    //     var elm = document.getElementById("otherHeader");
    //     elm.style.backgroundColor = "#1e5532";
    //   })

    // document.getElementById("submit").addEventListener("mouseover",
    //   function () {
    //     var elm = document.getElementById("submitHeader");
    //     var submitText = document.getElementById("submitText");
    //     var submitArrow = document.getElementById("submitArrow");
    //     elm.style.backgroundColor = "hsl(204, 86%, 53%)";
    //     submitArrow.style.color = "#FAFCF9"
    //     submitText.style.color = "#FAFCF9"
    //   })
    // document.getElementById("submit").addEventListener("mouseout",
    //   function () {
    //     var elm = document.getElementById("submitHeader");
    //     var submitText = document.getElementById("submitText");
    //     var submitArrow = document.getElementById("submitArrow");
    //     submitArrow.style.color = "hsl(204, 86%, 53%)"
    //     submitText.style.color = "hsl(204, 86%, 53%)"
    //     elm.style.backgroundColor = "#FAFCF9";
    //   })

    // document.getElementById("tile1").addEventListener("mouseover",
    //   function () {
    //     console.log("SEEN")
    //     var icon = document.getElementById("icon1");
    //     var text = document.getElementById("fig1");
    //     icon.style.display = "none";
    //     text.style.display = "block";
    //   })
    // document.getElementById("tile1").addEventListener("mouseout",
    //   function () {
    //     var icon = document.getElementById("icon1");
    //     var text = document.getElementById("fig1");
    //     icon.style.display = "block";
    //     text.style.display = "none";
    //   })


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
    if (menu.style.display == "none") {
      menu.style.display = "block";
    }
    else {
      menu.style.display = "none";
    }

    this.myLineChart = new Chart(document.getElementById("line-chart"), {
      type: 'line',
      data: {
        datasets: [{
          data: [2478, 5267, 734, 784, 433]
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
    for (var i = 0; i < 100; i++) {
      this.myLineChart.data.datasets.push({
        data: this.results.trialsList[i]
      });
    }
    this.myLineChart.update()

  }

  Reversal(elementOne) {
    var link = document.getElementById(elementOne);

    var generalHead = document.getElementById("generalInfo");
    var financeHead = document.getElementById("financesInfo");
    var otherHead = document.getElementById("otherInfo");
    var submit = document.getElementById("submit");

    var generalID = document.getElementById("generalBody");
    var financesID = document.getElementById("financesBody");


    var ids = [generalID, financesID];

    if (link.style.display == "none") {
      for (var i = 0; i < ids.length; i++) {
        if (ids[i].style.display == "block") {
          ids[i].style.display = "none";
        }
      }
      link.style.display = "block";
      if (elementOne == "generalBody") {
        financeHead.style.display = "none";
        otherHead.style.display = "none";
        submit.style.display = "none";
        window.location.href = "#generalInfo";
      }
      else if (elementOne == "financesBody") {
        generalHead.style.display = "none";
        otherHead.style.display = "none";
        submit.style.display = "none";
        window.location.href = "#financesInfo"
      }
    }
    else {
      link.style.display = "none";
      window.location.href = "#generalInfo";
      window.location.href = "#generalInfo"
      financeHead.style.display = "block";
      otherHead.style.display = "block";
      submit.style.display = "block";
      generalHead.style.display = "block";
    }
  }


  Send(client) {
    var send = new HTTPPost();
    send.SendData(client);
  }
}
