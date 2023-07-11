import * as React from 'react';
import { styled } from '@mui/material/styles'
import GameService from '../services/gameplayService'
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';

class BattleShipsGrid extends React.Component {
  setBackgroundColor = (cellStatus, isTargetGrid) => {
   
    let backgroundColor;


    if (isTargetGrid) {
      switch (cellStatus) {
        case 2:
          backgroundColor = 'orange';
          break;
        case 3:
          backgroundColor = 'gray';
          break;
        default:
          backgroundColor = 'green';
          break;
      }
    }
    else
    {
      switch (cellStatus) {
        case 0:
          backgroundColor = 'blue';
          break;
        case 1:
          backgroundColor = 'gray';
          break;
        case 2:
          backgroundColor = 'orange';
          break;
        default:
          backgroundColor = 'white';
          break;
      }
    }

    return backgroundColor;
  };

  getCellStatus(rowIndex, colIndex, isTargetGrid) {
    let cell;
    const { oceanGrid, targetGrid } = this.props.board;

    if(isTargetGrid)
    {
      cell = targetGrid
    }
    else
    {
      cell = oceanGrid
    }

    const oceanCell = cell.find((cell) => cell.row === rowIndex && cell.column === colIndex);
    return oceanCell.cellStatus;
  }

  handleCellClick = (rowIndex, colIndex) => {
    const { onPlayerShoot } = this.props;
    const coordinates = { row: --rowIndex, column: --colIndex };

    if (onPlayerShoot) {
      onPlayerShoot(coordinates);
    }
  };

  render() {
    const { board } = this.props;
    const letters = 'ABCDEFGHIJ';
    const rows = 10;
    const columns = 10;

    if (!board) {
      return <div>No board data</div>;
    }

    const { oceanGrid, targetGrid } = board;

    // If oceanGrid or targetGrid is undefined, return an alternative component or null
    if (!oceanGrid || !targetGrid) {
      return <div>Missing grid data</div>;
    }

    const Item = styled(Paper)(({ theme, cellStatus, isTargetGrid }) => ({
      backgroundColor: this.setBackgroundColor(cellStatus, isTargetGrid),
      ...theme.typography.body2,
      padding: theme.spacing(0.1),
      textAlign: 'center',
      color: theme.palette.text.secondary,
      width: '100%',
      height: '100%',
      border: isTargetGrid ? '2px solid green' : '2px solid gray'
    }));

    return (
      <div className="gameBoard">
        <h1>Battleships</h1>
        <Box sx={{ padding: '16px' }}>
          <Grid container spacing={1} columns={columns}>
            {Array.from(Array(rows)).map((_, rowIndex) => (
              <React.Fragment key={rowIndex}>
                {Array.from(Array(columns)).map((_, colIndex) => (
                  <Grid item xs={1} sm={1} md={1} key={colIndex}>
                    {colIndex === 0 && rowIndex > 0 ? (
                      <Item>
                        {letters[(rowIndex - 1) % letters.length]}
                      </Item>
                    ) : colIndex === 0 ? (
                      <Item>
                        {"TargetGrid"}
                      </Item>
                    ) : rowIndex !== 0 ? (
                      <Item
                        cellStatus={this.getCellStatus(rowIndex, colIndex, true)}
                        isTargetGrid={true}
                        onClick={() => this.handleCellClick(rowIndex, colIndex, true)}
                      >
                        {colIndex > 0 && rowIndex === 0 ? colIndex : ""}
                      </Item>
                    ) : (
                      <Item
                        cellStatus={99}
                        isTargetGrid={false}
                      >
                        {colIndex > 0 && rowIndex === 0 ? colIndex : ""}
                      </Item>
                    )}
                  </Grid>
                ))}
              </React.Fragment>
            ))}
          </Grid>
        </Box>
        <Box sx={{ padding: '16px' }}>
          <Grid container spacing={1} columns={columns}>
            {Array.from(Array(rows)).map((_, rowIndex) => (
              <React.Fragment key={rowIndex}>
                {Array.from(Array(columns)).map((_, colIndex) => (
                  <Grid item xs={1} sm={1} md={1} key={colIndex}>
                    {colIndex === 0 && rowIndex > 0 ? (
                      <Item>
                        {letters[(rowIndex - 1) % letters.length]}
                      </Item>
                    ) : colIndex === 0 ? (
                      <Item>
                        {"OceanGrid"}
                      </Item>
                    ) : rowIndex !== 0 ? (
                      <Item
                        cellStatus={this.getCellStatus(rowIndex, colIndex)}
                        isTargetGrid={false}
                      >
                        {colIndex > 0 && rowIndex === 0 ? colIndex : ""}
                      </Item>
                    ) : (
                      <Item
                        cellStatus={99}
                        isTargetGrid={false}
                      >
                        {colIndex > 0 && rowIndex === 0 ? colIndex : ""}
                      </Item>
                    )}
                  </Grid>
                ))}
              </React.Fragment>
            ))}
          </Grid>
        </Box>
      </div>
    );
  }
}

export default BattleShipsGrid;
