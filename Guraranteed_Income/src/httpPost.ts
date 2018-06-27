export default class HTTPPost {

  SendData(personOne) {
    (async () => {
      const response = await fetch('http://localhost:52079/api/values', {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(personOne)
      });
      const content = await response.json();
      
      return JSON.parse(content);
    })();
  }
  
}
