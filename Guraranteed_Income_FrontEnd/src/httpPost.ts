export default class HTTPPost {

  SendData(personOne) {
    (async () => {
      const response = await fetch('http://localhost:64655/api/', {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(personOne)
      });
      const content = await response.json();
      return content;
    })();
  }
  
}
