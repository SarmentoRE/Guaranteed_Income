import { RouterConfiguration, Router } from 'aurelia-router';
import { PLATFORM } from 'aurelia-pal';
import { Script } from 'vm';
import { Chart } from '../node_modules/chart.js/dist/Chart.js';
import Person from "person";
import HTTPPost from "httpPost";
import { resolveTxt } from 'dns';

export class Results {

  client = new Person();
  myLineChart;
  results;

  attached() {
    document.getElementById("tile1").addEventListener("mouseover",
      function () {
        console.log("SEEN")
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


    // document.getElementById("retireDateInput").addEventListener("blur", function(){self.ShowElement("retireDate","retireDateInput")})
    // document.getElementById("genderInput").addEventListener("blur", function(){self.ShowElement("gender","genderInput")})
  }
}
