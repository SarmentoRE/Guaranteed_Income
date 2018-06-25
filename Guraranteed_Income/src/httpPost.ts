export default class HTTPPost {

  SendData(personOne, personTwo) {
    var personArray = [personOne, personTwo];
    (async () => {
      const response = await fetch('http://localhost:52079/api/values', {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(personArray)
      });
      const content = await response.json();
      
      return JSON.parse(content);
    })();
  }
  
}
