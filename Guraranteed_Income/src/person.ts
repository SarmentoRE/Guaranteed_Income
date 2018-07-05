export default class Person {

  gender : string = "male";
  income : number;
  age : number;
  retirementDate = "2035"
  deathDate = "2055"
  filingStatus : string = "single";
  concerns = [false, false, false, false]
  assetHolder = [];
  matchHolder = [];
  capHolder = [];
  amountHolder = [];
  additionsHolder = [];
  htmlHolder = [];
  lumpSum : number;


  // retirementDateYear = this.retirementDate.getFullYear();
  // deathDateYear = this.deathDate.getFullYear();
  assets = {  "assets" : this.assetHolder,
                      "matching" : this.matchHolder,
                      "caps" : this.capHolder,
                      "amounts" : this.amountHolder,
                      "additions" : this.additionsHolder }
}
