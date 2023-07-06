﻿namespace BattleShipsApi.Models
{
    public class Board
    {
        public List<GridCoordinates> OceanGrid { get; set; }
        public List<GridCoordinates> TargetGrid { get; set; }
        public List<GridCoordinates> ComputerPossibleTargets { get; set; }
    }
}
