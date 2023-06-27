import * as React from 'react';
import { experimentalStyled as styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';

const Item = styled(Paper)(({ theme, isOceanGrid, isTargetGrid  }) => ({
  backgroundColor: isOceanGrid ? 'blue' : isTargetGrid ? 'green' : theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
  width: '100%',
  height: '100%',
  border: isOceanGrid ? '2px solid green' : isTargetGrid ? '2px solid gray' : '1px solid black',
}));

class BattleShipsGrid extends React.Component {
  render() {
    const letters = 'ABCDEFGHIJ'; // Letters to use in the grid
    const rows = 11; // Number of rows
    const columns = 11; // Number of columns

    return (
        <div class="gameBoard">
          <h1>Battleships</h1>
          <Box sx={{ flexGrow: 1, padding: '16px' }}>
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
                        <Item isTargetGrid ={colIndex > 0 && rowIndex > 0}>
                          {"TargetGrid"}
                        </Item>
                      ) : (
                        <Item isTargetGrid ={colIndex > 0 && rowIndex > 0}>
                          {colIndex > 0 && rowIndex === 0 ? colIndex : ""}
                        </Item>
                      )}
                    </Grid>
                  ))}
                </React.Fragment>
              ))}
            </Grid>
          </Box>
          <Box sx={{ flexGrow: 1, padding: '16px' }}>
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
                        <Item isOceanGrid={colIndex > 0 && rowIndex > 0}>
                          {"OceanGrid"}
                        </Item>
                      ) : (
                        <Item isOceanGrid={colIndex > 0 && rowIndex > 0}>
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
