import { RouterConfiguration, Router } from 'aurelia-router';
import { PLATFORM } from 'aurelia-pal';
import { Script } from 'vm';
import { Chart } from '../node_modules/chart.js/dist/Chart.js';
import Person from "person";
import { resolveTxt } from 'dns';



export class App {

  router: Router;
  configureRouter(config, router) {
    config.title = '??????';
    config.options.pushState = true;
    config.map([
      { route: ['', 'home'], name: 'home', moduleId: PLATFORM.moduleName('home.html'), nav: true, title: 'Home' },
      { route: 'results', name: 'results', moduleId: PLATFORM.moduleName('results.html'), nav: true, title: 'Results' },
    ]);
    this.router = router;
  }

  client = new Person();
  myLineChart;
  myLineChart2;

  rider1 = false;
  rider2 = false;
  rider3 = false;
  rider4 = false;

  i = 0;


  products = [
    { id: 1, name: '401(k)' },
    { id: 2, name: 'R401(k)' },
    { id: 3, name: 'IRA' },
    { id: 4, name: 'RIRA' },
  ];

  results;

  attached() {
    var self = this;

    var url = window.location.href;

    if (url == "http://localhost:8080/results") {
      this.ResultsListener();
    }
    else if (url == "http://localhost:8080/" || url == "http://localhost:8080/home") {
      this.HomeListener();
    }
    // this.SendData();
    this.Validate();



    // document.getElementById("retireDateInput").addEventListener("blur", function(){self.ShowElement("retireDate","retireDateInput")})
    // document.getElementById("genderInput").addEventListener("blur", function(){self.ShowElement("gender","genderInput")})
  }

  Start() {
    window.location.href = "http://localhost:8080"
  }

  Validate() {
    document.onkeydown = function (e) {
      if (e.shiftKey) {
        e.preventDefault();
      }
      if (!((e.keyCode > 95 && e.keyCode < 106)
        || (e.keyCode > 47 && e.keyCode < 58)
        || e.keyCode == 8 || e.keyCode == 9)) {
        return false;
      }
    }
  }

  ConstantRun() {
    var self = this;
    var element = document.getElementById("final")
    element.onkeyup = function (event) {
      self.SendData();
    };
  }

  ShowElement(elementOne, elementTwo) {
    var link = document.getElementById(elementOne);
    var refer = document.getElementById(elementTwo)
    link.style.display = "block";
    refer.style.display = "none";
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
  }

  PopulateCharts() {
    this.myLineChart = new Chart(document.getElementById("line-chart-q"), {
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

    this.myLineChart2 = new Chart(document.getElementById("line-chart-nq"), {
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

    this.ShowQFixedImm();
  }

  Reversal(elementOne) {
    var link = document.getElementById(elementOne);

    var generalHead = document.getElementById("generalInfo");
    var financeHead = document.getElementById("financesInfo");
    var otherHead = document.getElementById("otherInfo");
    var submit = document.getElementById("submit");
    var generalID = document.getElementById("generalBody");
    var financesID = document.getElementById("financesBody");
    var otherID = document.getElementById("otherBody")


    var ids = [generalID, financesID, otherID];

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
      else {
        generalHead.style.display = "none";
        financeHead.style.display = "none";
        submit.style.display = "none";
        window.location.href = "#otherInfo"
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

  // A LISTENER TO CONTROL EVENTS ON THE HOME PAGE
  HomeListener() {

    if (document.referrer == "http://localhost:8080/results") {
      this.client = JSON.parse(localStorage.getItem('client'));
      this.ReconstructTable();
      this.i = this.client.assetHolder.length;
    }


    document.getElementById("generalInfo").addEventListener("mouseover",
      function () {
        var elm = document.getElementById("generalHeader");
        elm.style.backgroundColor = "hsl(204, 86%, 53%)";
      })
    document.getElementById("generalInfo").addEventListener("mouseout",
      function () {
        var elm = document.getElementById("generalHeader");
        elm.style.backgroundColor = "#49804A";
      })

    document.getElementById("financesInfo").addEventListener("mouseover",
      function () {
        var elm = document.getElementById("financesHeader");
        elm.style.backgroundColor = "hsl(204, 86%, 53%)";
      })
    document.getElementById("financesInfo").addEventListener("mouseout",
      function () {
        var elm = document.getElementById("financesHeader");
        elm.style.backgroundColor = "#89cc66";
      })

    document.getElementById("otherInfo").addEventListener("mouseover",
      function () {
        var elm = document.getElementById("otherHeader");
        elm.style.backgroundColor = "hsl(204, 86%, 53%)";
      })
    document.getElementById("otherInfo").addEventListener("mouseout",
      function () {
        var elm = document.getElementById("otherHeader");
        elm.style.backgroundColor = "#1e5532";
      })

    document.getElementById("submit").addEventListener("mouseover",
      function () {
        var elm = document.getElementById("submitHeader");
        var submitText = document.getElementById("submitText");
        var submitArrow = document.getElementById("submitArrow");
        elm.style.backgroundColor = "hsl(204, 86%, 53%)";
        submitArrow.style.color = "#FAFCF9"
        submitText.style.color = "#FAFCF9"
      })
    document.getElementById("submit").addEventListener("mouseout",
      function () {
        var elm = document.getElementById("submitHeader");
        var submitText = document.getElementById("submitText");
        var submitArrow = document.getElementById("submitArrow");
        submitArrow.style.color = "hsl(204, 86%, 53%)"
        submitText.style.color = "hsl(204, 86%, 53%)"
        elm.style.backgroundColor = "#FAFCF9";
      })

  }

  // A LISTENER TO CONTROL EVENTS ON THE RESULTS PAGE
  ResultsListener() {
    var self = this;

    this.results = JSON.parse(localStorage.getItem('results'));

    window.onload = function () {
      self.PopulateCharts();
    }

    document.getElementById("tile1").addEventListener("mouseover",
      function () {
        var icon = document.getElementById("icon1");
        var text = document.getElementById("fig1");
        icon.style.display = "none";
        text.style.display = "block";
      })
    document.getElementById("tile1").addEventListener("mouseout",
      function () {
        var icon = document.getElementById("icon1");
        var text = document.getElementById("fig1");
        icon.style.display = "block";
        text.style.display = "none";
      })

    document.getElementById("tile2").addEventListener("mouseover",
      function () {
        var icon = document.getElementById("icon2");
        var text = document.getElementById("fig2");
        icon.style.display = "none";
        text.style.display = "block";
      })
    document.getElementById("tile2").addEventListener("mouseout",
      function () {
        var icon = document.getElementById("icon2");
        var text = document.getElementById("fig2");
        icon.style.display = "block";
        text.style.display = "none";
      })

    document.getElementById("tile3").addEventListener("mouseover",
      function () {
        var icon = document.getElementById("icon3");
        var text = document.getElementById("fig3");
        icon.style.display = "none";
        text.style.display = "block";
      })
    document.getElementById("tile3").addEventListener("mouseout",
      function () {
        var icon = document.getElementById("icon3");
        var text = document.getElementById("fig3");
        icon.style.display = "block";
        text.style.display = "none";
      })

    document.getElementById("tile4").addEventListener("mouseover",
      function () {
        var icon = document.getElementById("icon4");
        var text = document.getElementById("fig4");
        icon.style.display = "none";
        text.style.display = "block";
      })
    document.getElementById("tile4").addEventListener("mouseout",
      function () {
        var icon = document.getElementById("icon4");
        var text = document.getElementById("fig4");
        icon.style.display = "block";
        text.style.display = "none";
      })

  }


  // THIS FUNCTION CONTROLS THE TILES AND BOOLEAN FOR DISPLAYING THE RIDERS
  DepressTile(number) {
    //RIDERS
    var tile1 = document.getElementById("tile1")
    var tile2 = document.getElementById("tile2")
    var tile3 = document.getElementById("tile3")
    var tile4 = document.getElementById("tile4")

    if (number < 5) {
      this.client.concerns[number - 1] = !this.client.concerns[number - 1]
    }
    else if (number > 4) {
      var graphButton = document.getElementById("tile" + number)
      if (graphButton.style.backgroundColor == "hsl(204, 86%, 53%)") {
        graphButton.style.backgroundColor = "#1e5532"
      }
      else {
        for (var e = 5; e < 13; e++) {
          var otherTiles = document.getElementById("tile" + e)
          console.log("tile" + e)
          otherTiles.style.backgroundColor = "#1e5532";
          console.log("HERE")
        }
        graphButton.style.backgroundColor = "hsl(204, 86%, 53%)";

      }
    }

    if (this.client.concerns[0] == true) {
      tile1.style.backgroundColor = "hsl(204, 86%, 53%)"
    }
    else {
      tile1.style.backgroundColor = "#5E005E"
    }

    if (this.client.concerns[1] == true) {
      tile2.style.backgroundColor = "hsl(204, 86%, 53%)"
    }
    else {
      tile2.style.backgroundColor = "#AB2F52"
    }

    if (this.client.concerns[2] == true) {
      tile3.style.backgroundColor = "hsl(204, 86%, 53%)"
    }
    else {
      tile3.style.backgroundColor = "#E55D4A"
    }

    if (this.client.concerns[3] == true) {
      tile4.style.backgroundColor = "hsl(204, 86%, 53%)"
    }
    else {
      tile4.style.backgroundColor = "#E88554"
    }
  }

  ShowQFixedImm() {
    this.DepressTile(5);
    this.myLineChart.destroy();
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident25.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart = new Chart(document.getElementById("line-chart-q"), {

      type: 'line',
      data: {
        labels: years,
        datasets: [{
          data: this.results.brokerage.confident25,
          label: "Confident 25",
          borderColor: "#3e95cd",
          fill: false
        }
        ]
      },
      options: {
        title: {
          display: true,
          text: 'Confidence Interval 25'
        }
      }
    });

  }

  ShowQFixedDeff() {
    this.DepressTile(6);
    this.myLineChart.destroy();
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident50.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart = new Chart(document.getElementById("line-chart-q"), {

      type: 'line',
      data: {
        labels: years,
        datasets: [{
          data: this.results.brokerage.confident50,
          label: "Confident 50",
          borderColor: "#3e95cd",
          fill: false
        }
        ]
      },
      options: {
        title: {
          display: true,
          text: 'Confidence Interval 50'
        }
      }
    });
  }

  ShowQVarImm() {
    this.DepressTile(7);
    this.myLineChart.destroy()
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident75.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart = new Chart(document.getElementById("line-chart-q"), {

      type: 'line',
      data: {
        labels: years,
        datasets: [{
          data: this.results.brokerage.confident75,
          label: "Confident 75",
          borderColor: "#3e95cd",
          fill: false
        }
        ]
      },
      options: {
        title: {
          display: true,
          text: 'Confidence Interval 75'
        }
      }
    });

  }

  ShowQVarDeff() {
    this.DepressTile(8);
    this.myLineChart.destroy()
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident90.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart = new Chart(document.getElementById("line-chart-q"), {

      type: 'line',
      data: {
        labels: years,
        datasets: [{
          data: this.results.brokerage.confident90,
          label: "Confident 90",
          borderColor: "#3e95cd",
          fill: false
        }
        ]
      },
      options: {
        title: {
          display: true,
          text: 'Confidence Interval 90'
        }
      }
    });

  }

  AddRow() {
    var table = document.getElementById("appendThis");
    var html = 
    '<tr>' +
    '<td>' +
    '<div class="field">' +
    '<div class="control">' +
    '<div class="select">' +
    '<select id="inputAsset0">' +
    '<option selected= "' + this.client.assetHolder[this.i] + '" disabled>' +
    this.client.assetHolder[this.i] +
    '</option>' +
    '</select>' +
    '</div>' +
    '</div>' +
    '</div>' +
    '</td>' +
    '<td>' +
    '<div class="control has-icons-left has-icons-right">' +
    '<input class="input" placeholder="' + this.client.amountHolder[this.i] + '" id="inputAmount0" disabled>' +
    '<span class="icon is-small is-left">' +
    '<i class="fas fa-envelope"></i>' +
    '</span>' +
    '<span class="icon is-small is-right">' +
    '<i class="fas fa-check"></i>' +
    '</span>' +
    '</div>' +
    '</td>' +
    '<td>' +
    '<div class="control has-icons-left has-icons-right">' +
    '<input class="input" placeholder="' + this.client.additionsHolder[this.i] + '" id="inputAmount0" disabled>' +
    '<span class="icon is-small is-left">' +
    '<i class="fas fa-envelope"></i>' +
    '</span>' +
    '<span class="icon is-small is-right">' +
    '<i class="fas fa-check"></i>' +
    '</span>' +
    '</div>' +
    '</td>' +
    '</tr>'
    table.innerHTML += html

    this.client.htmlHolder[this.i] = html
    this.i++;
    for (var e = 0; e < this.i; e++) {
      console.log(this.client.assetHolder[e] + " " + this.client.amountHolder[e] + " " + this.client.additionsHolder[e])
    }
  }

  ReconstructTable() {
    var table = document.getElementById("appendThis");
    for (var e= 0; e < this.client.htmlHolder.length; e++) {
      table.innerHTML += this.client.htmlHolder[e];
    }
  }

  // THIS FUNCTION IS USED TO POST AND RECEIVE DATA
  SendData() {

    (async () => {
      const response = await fetch('http://localhost:64655/api/', {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.client)
      });

      this.results = await response.json();

      localStorage.setItem('results', JSON.stringify(this.results));
      localStorage.setItem('client', JSON.stringify(this.client));
    })();

    var url = window.location.href;

    if (url == "http://localhost:8080/" || url == "http://localhost:8080/home" || url == "http://localhost:8080/#generalInfo" || url == "http://localhost:8080/#financesInfo") {
      window.location.href = "http://localhost:8080/results"
    }

  }
}
