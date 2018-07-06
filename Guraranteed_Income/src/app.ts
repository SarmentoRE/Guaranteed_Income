import { RouterConfiguration, Router, Redirect } from 'aurelia-router';
import { PLATFORM } from 'aurelia-pal';
import { Chart } from '../node_modules/chart.js/dist/Chart.js';
import Person from "person";
import { inject } from 'aurelia-framework';


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

  i = 0;
  iter = 0;

  products = [
    { id: '1', name: '401(k)' },
    { id: '2', name: 'R401(k)' },
    { id: '3', name: 'IRA' },
    { id: '4', name: 'RIRA' },
  ];

  results;
  selected = "initial";
  selectedNQ = "initial";

  selectedVal = "initial";
  selectedNQVal = "initial";

  attached() {
    var self = this;
    this.Validate()
    var url = window.location.href;
    console.log(url)

    if (url == "http://localhost:8080/results/") {
      this.ResultsListener();
    }
    else if (url == "http://localhost:8080/" || url == "http://localhost:8080/home") {
      this.HomeListener();
    }
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
    console.log(document.referrer);
    if (document.referrer == "http://localhost:8080/results/") {
      this.client = JSON.parse(localStorage.getItem('client'));
      console.log(this.client)
      this.ReconstructTable();
      this.DepressTile(-1)
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
    this.client = JSON.parse(localStorage.getItem('client'))
    window.onload = function () {
      console.log(self.results)
      self.PopulateCharts();
      self.DepressTile(-1);
    }
  }

  // THIS FUNCTION CONTROLS THE TILES AND BOOLEAN FOR DISPLAYING THE RIDERS:
  DepressTile(number) {
    //RIDERS
    var tile1 = document.getElementById("tile1")
    var tile2 = document.getElementById("tile2")
    var tile3 = document.getElementById("tile3")
    var tile4 = document.getElementById("tile4")

    if (number < 5 && number > 0) {
      this.client.concerns[number - 1] = !this.client.concerns[number - 1]
      localStorage.setItem('client', JSON.stringify(this.client));
    }
    else if (number > 4) {
      var graphButton = document.getElementById("tile" + number)
      if (graphButton.style.backgroundColor == "hsl(204, 86%, 53%)") {
        graphButton.style.backgroundColor = "#49804A"
      }
      else {
        if( number > 4 && number < 9) {
          for (var e = 5; e < 9; e++) {
            var otherTiles = document.getElementById("tile" + e)
            console.log("tile" + e)
            otherTiles.style.backgroundColor = "#49804A";
            console.log("HERE")
          }
          graphButton.style.backgroundColor = "hsl(204, 86%, 53%)";
        }
        else {
          for (var e = 9; e < 13; e++) {
            var otherTiles = document.getElementById("tile" + e)
            console.log("tile" + e)
            otherTiles.style.backgroundColor = "#49804A";
            console.log("HERE")
          }
          graphButton.style.backgroundColor = "hsl(204, 86%, 53%)";
        }


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

  //THE GRAPHING FUNCTIONS FOLLOW:
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
    this.ShowNQFixedImm();
  }

  ShowQFixedImm() {
    this.DepressTile(5);
    this.myLineChart.destroy();
    this.selected = "Fixed Immediate";
    this.selectedVal = this.results.qualified.fixedImYearly;
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident25.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart = new Chart(document.getElementById("line-chart-q"), {
      type: 'bar',
      data: {
        labels: years,
        datasets: [{
          data: this.results.qualified.fixedIm[0],
          label: "Annuities Income",
          type: "line",
          bezierCurve: true,
          pointRadius: 0,
          backgroundColor: 'rgba(255, 99, 132, 0.6)',
          fill: true,
        }, {
          data: this.results.brokerage.confident75,
          label: "Brokerage (75% Confidence)",
          type: "bar",
          backgroundColor: "#C70039",
          fill: true
        }, {
          data: this.results.brokerage.confident90,
          label: "Brokerage (90% Confidence)",
          type: "bar",
          backgroundColor: "#A3392E",
          fill: true
        }
        ]
      },
      options: {
        title: {
          display: false,
          fontFamily: "Fjalla One",
          text: 'Income',
          fontSize: 1,
          fontWeight: 100,
          fontColor: "#222222",
        },
        labels: {
          display: false,
          fontSize: 1,
          fontFamily: "Fjalla One",
          fontColor: "#222222",
        },
        legend: {
          display: false,
        },
        scales: {
          yAxes: [{
            ticks: {
              // Include a dollar sign in the ticks
              callback: function (value, index, values) {
                return '$' + value;
              }
            }
          }]
        },
        tooltips: {
          mode: "x-axis",
          callbacks: {
            label: function (tooltipItems, data) {
              var dataset = data.datasets[tooltipItems.datasetIndex];
              return dataset.label + ':  $' + tooltipItems.yLabel;
            }
          }
        },
      }
    });

  }

  ShowQFixedDeff() {
    this.DepressTile(6);
    this.myLineChart.destroy();
    this.selected = "Fixed Deferred"
    this.selectedVal = this.results.qualified.fixedDefYearly;
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident50.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart = new Chart(document.getElementById("line-chart-q"), {
      type: 'bar',
      data: {
        labels: years,
        datasets: [{
          data: this.results.qualified.fixedDef[0],
          label: "Annuities Income",
          type: "line",
          bezierCurve: true,
          pointRadius: 0,
          backgroundColor: 'rgba(255, 99, 132, 0.6)',
          fill: true,
        }, {
          data: this.results.brokerage.confident75,
          label: "Brokerage (75% Confidence)",
          type: "bar",
          backgroundColor: "#C70039",
          fill: true
        }, {
          data: this.results.brokerage.confident90,
          label: "Brokerage (90% Confidence)",
          type: "bar",
          backgroundColor: "#A3392E",
          fill: true
        }
        ]
      },
      options: {
        title: {
          display: false,
          fontFamily: "Fjalla One",
          text: 'Income',
          fontSize: 1,
          fontWeight: 100,
          fontColor: "#222222",
        },
        labels: {
          display: false,
          fontSize: 1,
          fontFamily: "Fjalla One",
          fontColor: "#222222",
        },
        legend: {
          display: false,
        },
        scales: {
          yAxes: [{
            ticks: {
              // Include a dollar sign in the ticks
              callback: function (value, index, values) {
                return '$' + value;
              }
            }
          }]
        },
        tooltips: {
          mode: "x-axis",
          callbacks: {
            label: function (tooltipItems, data) {
              var dataset = data.datasets[tooltipItems.datasetIndex];
              return dataset.label + ':  $' + tooltipItems.yLabel;
            }
          }
        },
      }
    });
  }

  ShowQVarImm() {
    this.DepressTile(7);
    this.myLineChart.destroy();
    this.selected = "Variable Immediate";
    this.selectedVal = this.results.qualified.varImYearly
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident75.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart = new Chart(document.getElementById("line-chart-q"), {
      type: 'bar',
      data: {
        labels: years,
        datasets: [{
          data: this.results.qualified.varIm[0],
          label: "Annuities Income",
          type: "line",
          bezierCurve: true,
          pointRadius: 0,
          backgroundColor: 'rgba(255, 99, 132, 0.6)',
          fill: true,
        }, {
          data: this.results.brokerage.confident75,
          label: "Brokerage (75% Confidence)",
          type: "bar",
          backgroundColor: "#C70039",
          fill: true
        }, {
          data: this.results.brokerage.confident90,
          label: "Brokerage (90% Confidence)",
          type: "bar",
          backgroundColor: "#A3392E",
          fill: true
        }
        ]
      },
      options: {
        title: {
          display: false,
          fontFamily: "Fjalla One",
          text: 'Income',
          fontSize: 1,
          fontWeight: 100,
          fontColor: "#222222",
        },
        labels: {
          display: false,
          fontSize: 1,
          fontFamily: "Fjalla One",
          fontColor: "#222222",
        },
        legend: {
          display: false,
        },
        scales: {
          yAxes: [{
            ticks: {
              // Include a dollar sign in the ticks
              callback: function (value, index, values) {
                return '$' + value;
              }
            }
          }]
        },
        tooltips: {
          mode: "x-axis",
          callbacks: {
            label: function (tooltipItems, data) {
              var dataset = data.datasets[tooltipItems.datasetIndex];
              return dataset.label + ':  $' + tooltipItems.yLabel;
            }
          }
        },
      }
    });

  }

  ShowQVarDeff() {
    this.DepressTile(8);
    this.myLineChart.destroy();
    this.selected = "Variable Deferred";
    this.selectedVal = this.results.qualified.varDefYearly;
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident90.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart = new Chart(document.getElementById("line-chart-q"), {
      type: 'bar',
      data: {
        labels: years,
        datasets: [{
          data: this.results.qualified.varDef[0],
          label: "Annuities Income",
          type: "line",
          bezierCurve: true,
          pointRadius: 0,
          backgroundColor: 'rgba(255, 99, 132, 0.6)',
          fill: true,
        }, {
          data: this.results.brokerage.confident75,
          label: "Brokerage (75% Confidence)",
          type: "bar",
          backgroundColor: "#C70039",
          fill: true
        }, {
          data: this.results.brokerage.confident90,
          label: "Brokerage (90% Confidence)",
          type: "bar",
          backgroundColor: "#A3392E",
          fill: true
        }
        ]
      },
      options: {
        title: {
          display: false,
          fontFamily: "Fjalla One",
          text: 'Income',
          fontSize: 1,
          fontWeight: 100,
          fontColor: "#222222",
        },
        labels: {
          display: false,
          fontSize: 1,
          fontFamily: "Fjalla One",
          fontColor: "#222222",
        },
        legend: {
          display: false,
        },
        scales: {
          yAxes: [{
            ticks: {
              // Include a dollar sign in the ticks
              callback: function (value, index, values) {
                return '$' + value;
              }
            }
          }]
        },
        tooltips: {
          mode: "x-axis",
          callbacks: {
            label: function (tooltipItems, data) {
              var dataset = data.datasets[tooltipItems.datasetIndex];
              return dataset.label + ':  $' + tooltipItems.yLabel;
            }
          }
        },
      }
    });
  }
  
  ShowNQFixedImm() {
    this.DepressTile(9);
    this.myLineChart2.destroy();
    this.selectedNQ = "Fixed Immediate";
    this.selectedNQVal = this.results.nonQualified.fixedImYearly
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident25.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart2 = new Chart(document.getElementById("line-chart-nq"), {
      type: 'bar',
      data: {
        labels: years,
        datasets: [{
          data: this.results.nonQualified.fixedIm[0],
          label: "Annuities Income",
          type: "line",
          bezierCurve: true,
          pointRadius: 0,
          backgroundColor: 'rgba(255, 99, 132, 0.6)',
          fill: true,
        }, {
          data: this.results.brokerage.confident75,
          label: "Brokerage (75% Confidence)",
          type: "bar",
          backgroundColor: "#C70039",
          fill: true
        }, {
          data: this.results.brokerage.confident90,
          label: "Brokerage (90% Confidence)",
          type: "bar",
          backgroundColor: "#A3392E",
          fill: true
        }
        ]
      },
      options: {
        title: {
          display: false,
          fontFamily: "Fjalla One",
          text: 'Income',
          fontSize: 1,
          fontWeight: 100,
          fontColor: "#222222",
        },
        labels: {
          display: false,
          fontSize: 1,
          fontFamily: "Fjalla One",
          fontColor: "#222222",
        },
        legend: {
          display: false,
        },
        scales: {
          yAxes: [{
            ticks: {
              // Include a dollar sign in the ticks
              callback: function (value, index, values) {
                return '$' + value;
              }
            }
          }]
        },
        tooltips: {
          mode: "x-axis",
          callbacks: {
            label: function (tooltipItems, data) {
              var dataset = data.datasets[tooltipItems.datasetIndex];
              return dataset.label + ':  $' + tooltipItems.yLabel;
            }
          }
        },
      }
    });

  }

  ShowNQFixedDeff() {
    this.DepressTile(10);
    this.myLineChart2.destroy();
    this.selectedNQ = "Fixed Deferred"
    this.selectedNQVal = this.results.nonQualified.fixedDefYearly
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident50.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart2 = new Chart(document.getElementById("line-chart-nq"), {
      type: 'bar',
      data: {
        labels: years,
        datasets: [{
          data: this.results.nonQualified.fixedDef[0],
          label: "Annuities Income",
          type: "line",
          bezierCurve: true,
          pointRadius: 0,
          backgroundColor: 'rgba(255, 99, 132, 0.6)',
          fill: true,
        }, {
          data: this.results.brokerage.confident75,
          label: "Brokerage (75% Confidence)",
          type: "bar",
          backgroundColor: "#C70039",
          fill: true
        }, {
          data: this.results.brokerage.confident90,
          label: "Brokerage (90% Confidence)",
          type: "bar",
          backgroundColor: "#A3392E",
          fill: true
        }
        ]
      },
      options: {
        title: {
          display: false,
          fontFamily: "Fjalla One",
          text: 'Income',
          fontSize: 1,
          fontWeight: 100,
          fontColor: "#222222",
        },
        labels: {
          display: false,
          fontSize: 1,
          fontFamily: "Fjalla One",
          fontColor: "#222222",
        },
        legend: {
          display: false,
        },
        scales: {
          yAxes: [{
            ticks: {
              // Include a dollar sign in the ticks
              callback: function (value, index, values) {
                return '$' + value;
              }
            }
          }]
        },
        tooltips: {
          mode: "x-axis",
          callbacks: {
            label: function (tooltipItems, data) {
              var dataset = data.datasets[tooltipItems.datasetIndex];
              return dataset.label + ':  $' + tooltipItems.yLabel;
            }
          }
        },
      }
    });
  }

  ShowNQVarImm() {
    this.DepressTile(11);
    this.myLineChart2.destroy();
    this.selectedNQ = "Variable Immediate";
    this.selectedNQVal = this.results.nonQualified.varImYearly
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident75.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart2 = new Chart(document.getElementById("line-chart-nq"), {
      type: 'bar',
      data: {
        labels: years,
        datasets: [{
          data: this.results.nonQualified.varIm[0],
          label: "Annuities Income",
          type: "line",
          bezierCurve: true,
          pointRadius: 0,
          backgroundColor: 'rgba(255, 99, 132, 0.6)',
          fill: true,
        }, {
          data: this.results.brokerage.confident75,
          label: "Brokerage (75% Confidence)",
          type: "bar",
          backgroundColor: "#C70039",
          fill: true
        }, {
          data: this.results.brokerage.confident90,
          label: "Brokerage (90% Confidence)",
          type: "bar",
          backgroundColor: "#A3392E",
          fill: true
        }
        ]
      },
      options: {
        title: {
          display: false,
          fontFamily: "Fjalla One",
          text: 'Income',
          fontSize: 1,
          fontWeight: 100,
          fontColor: "#222222",
        },
        labels: {
          display: false,
          fontSize: 1,
          fontFamily: "Fjalla One",
          fontColor: "#222222",
        },
        legend: {
          display: false,
        },
        scales: {
          yAxes: [{
            ticks: {
              // Include a dollar sign in the ticks
              callback: function (value, index, values) {
                return '$' + value;
              }
            }
          }]
        },
        tooltips: {
          mode: "x-axis",
          callbacks: {
            label: function (tooltipItems, data) {
              var dataset = data.datasets[tooltipItems.datasetIndex];
              return dataset.label + ':  $' + tooltipItems.yLabel;
            }
          }
        },
      }
    });

  }

  ShowNQVarDeff() {
    this.DepressTile(12);
    this.myLineChart2.destroy();
    this.selectedNQ = "Variable Deferred";
    this.selectedNQVal = this.results.nonQualified.varDefYearly
    var years = [];
    for (var z = 0; z < this.results.brokerage.confident90.length; z++) {
      years[z] = 2018 + z;
    }
    this.myLineChart2 = new Chart(document.getElementById("line-chart-nq"), {
      type: 'bar',
      data: {
        labels: years,
        datasets: [{
          data: this.results.nonQualified.varDef[0],
          label: "Annuities Income",
          type: "line",
          bezierCurve: true,
          pointRadius: 0,
          backgroundColor: 'rgba(255, 99, 132, 0.6)',
          fill: true,
        }, {
          data: this.results.brokerage.confident75,
          label: "Brokerage (75% Confidence)",
          type: "bar",
          backgroundColor: "#C70039",
          fill: true
        }, {
          data: this.results.brokerage.confident90,
          label: "Brokerage (90% Confidence)",
          type: "bar",
          backgroundColor: "#A3392E",
          fill: true
        }
        ]
      },
      options: {
        title: {
          display: false,
          fontFamily: "Fjalla One",
          text: 'Income',
          fontSize: 1,
          fontWeight: 100,
          fontColor: "#222222",
        },
        labels: {
          display: false,
          fontSize: 1,
          fontFamily: "Fjalla One",
          fontColor: "#222222",
        },
        legend: {
          display: false,
        },
        scales: {
          yAxes: [{
            ticks: {
              // Include a dollar sign in the ticks
              callback: function (value, index, values) {
                return '$' + value;
              }
            }
          }]
        },
        tooltips: {
          mode: "x-axis",
          callbacks: {
            label: function (tooltipItems, data) {
              var dataset = data.datasets[tooltipItems.datasetIndex];
              return dataset.label + ':  $' + tooltipItems.yLabel;
            }
          }
        },
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
      '<div class="select is-small">' +
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
      '<div class="control">' +
      '<input class="input is-small" placeholder="' + this.client.matchHolder[this.i] + '" id="inputAmount0" disabled style="width:80%">' +
      '</div>' +
      '<div class="control">' +
      '<input class="input is-small" placeholder="' + this.client.capHolder[this.i] + '" id="inputAmount0" disabled style="width:80%">' +
      '</div>' +
      '</td>' +
      '<td>' +
      '<div class="control">' +
      '<input class="input is-small" placeholder="' + this.client.amountHolder[this.i] + '" id="inputAmount0" disabled>' +
      '</div>' +
      '</td>' +
      '<td>' +
      '<div class="control">' +
      '<input class="input is-small" placeholder="' + this.client.additionsHolder[this.i] + '" id="inputAmount0" disabled>' +
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
    for (var e = 0; e < this.client.htmlHolder.length; e++) {
      table.innerHTML += this.client.htmlHolder[e];
    }
  }

  DetermineScroll(direction) {
    if (direction == -1) {
      this.iter--;
    }
    else if (direction == 1) {
      this.iter++;
    }

    if (this.iter < 0) {
      this.iter = 0;
    }

    if (this.iter > 2) {
      this.iter = 2;
    }

    var jumpIDs = ['results/#Qualified', 'results/#Nonqualified', 'results/#tile1']
    console.log(this.iter)
    console.log(jumpIDs[this.iter])

    window.location.href = jumpIDs[this.iter];
  }

  // THIS FUNCTION IS USED TO POST AND RECEIVE DATA
  SendData() {
    var self = this;
    async function f() {
      const response = await fetch('http://localhost:64655/api/', {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(self.client)
      });
      self.results = await response.json();
    }

    var Redirect = () => {
      localStorage.setItem('results', JSON.stringify(self.results));
      localStorage.setItem('client', JSON.stringify(self.client));
      var url = window.location.href;
      if (url == "http://localhost:8080/" || url == "http://localhost:8080/home" || url == "http://localhost:8080/#generalInfo" || url == "http://localhost:8080/#financesInfo") {
        window.location.href = "http://localhost:8080/results/#"
        // this.router.navigate("/results")
      }
      return 1;
    }

    f().then(Redirect)
  }
}


