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
            map.Add(new DataBlock(200, 50, DataBlockType.Wall));
            map.Add(new DataBlock(200, 100, DataBlockType.Wall));
            map.Add(new DataBlock(200, 150, DataBlockType.Wall));
            map.Add(new DataBlock(200, 200, DataBlockType.Wall));
            map.Add(new DataBlock(150, 200, DataBlockType.Wall));
            map.Add(new DataBlock(250, 200, DataBlockType.Wall));
        }

    }
}
