export default class Person {

  gender : string = "male";
  income : number;
  age : number;
  retirementDate : string;
  deathDate : string;
  filingStatus : string = "single";
  concerns = [false, false, false, false]
  assetHolder = [];
  matchHolder = [];
  capHolder = [];
  amountHolder = [];
  additionsHolder = [];
  htmlHolder = [];

  assets = {  "assets" : this.assetHolder,
                      "matching" : this.matchHolder,
                      "caps" : this.capHolder,
                      "amounts" : this.amountHolder,
                      "additions" : this.additionsHolder }
}
