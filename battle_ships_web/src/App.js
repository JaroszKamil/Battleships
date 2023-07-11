import './battleShips.css';
import BattleShipsGrid from './components/battleShipsGrid';
import GameService from "./services/gameplayService";
import React, { useEffect, useState } from "react";

function App() {

  const [board, setBoard] = useState(null);

  useEffect(() => {
    GameService.startGame()
        .then(data => setBoard(data))
        .catch(error => console.error(error));
}, []);

const handlePlayerShoot = (coordinates) => {
  GameService.playerShoot(coordinates)
    .then(updatedCell => {
      setBoard(prevBoard => {
        const updatedBoard = { ...prevBoard }; // Create a copy of the current board object

        const { targetGrid } = updatedBoard;

        // Adjust the coordinates to match the indexing of targetGrid
        const row = coordinates.row + 1;
        const column = coordinates.column + 1;

        // Find the corresponding cell in the targetGrid and update its cellStatus
        const cell = targetGrid.find(cell => cell.row === row && cell.column === column);

        if (cell) {
          cell.cellStatus = updatedCell.cellStatus;
        }

        return updatedBoard;
      });
    })
    .catch(error => console.error(error));
};


  if (!board) return <div>Loading...</div>;

  return (
      <div className="App">
          <BattleShipsGrid 
          board={board}
          onPlayerShoot = {handlePlayerShoot} />
      </div>
  );
}

export default App;
