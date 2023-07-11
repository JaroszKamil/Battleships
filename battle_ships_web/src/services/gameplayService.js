class GameService {

    constructor() {
      this.apiUrl = 'https://localhost:49159/api/v1';
    }
  
    async startGame() {
        const response = await fetch(`${this.apiUrl}/Gameplay`);
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
      }
  
    async endGame() {
      const response = await fetch(`${this.apiUrl}/endGame`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return await response.json();
    }

    async playerShoot(cell) {
        const response = await fetch(`${this.apiUrl}/Gameplay/shoot/player`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(cell)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
    }

    async computerShoot() {
        const response = await fetch(`${this.apiUrl}/Gameplay/shoot/computer`, {
            method: 'PUT'
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
    }
  
    // Add more methods as needed
  
  }
  
  export default new GameService();