using CallOfLibrary;

namespace CallOfSokoHub
{
    public class Model
    {
        public List<DataBlock> map { get; set; }

        public Model()
        {
            map = new List<DataBlock>();
        }

        public void UseTemplateMap()
        {
            map.Clear();
            map.Add(new DataBlock(0, 0, DataBlockType.Wall));
            map.Add(new DataBlock(50, 0, DataBlockType.Wall));
            map.Add(new DataBlock(100, 0, DataBlockType.Wall));
            map.Add(new DataBlock(0, 50, DataBlockType.Wall));
            map.Add(new DataBlock(0, 100, DataBlockType.Wall));
            map.Add(new DataBlock(50, 50, DataBlockType.Wall));
        }

    }
}
