﻿using Pony.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Data.Entities
{
    public class Maze
    {
        public Guid Id { get; set; }
        public int MazeWidth { get; set; }
        public int MazeHeight { get; set; }
        public int Difficulty { get; set; }
        public string MazePlayerName { get; set; }
        public int PonyPosition { get; set; }
        public int DomokunPostion { get; set; }
        public int EndpointPosition { get; set; }
        public List<string>[] data { get; set; }
        public GameState GameState { get; set; }
    }
}