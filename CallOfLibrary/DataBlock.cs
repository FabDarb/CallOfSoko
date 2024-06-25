﻿namespace CallOfLibrary
{
    public enum DataBlockType
    {
        Wall,
        Player,
    }
    public class DataBlock
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Type Type { get; set; }
    }
}
