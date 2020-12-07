namespace _Project.Scripts.City.Systems.Builders.Grid
{
    public class CityGrid
    {
        private int _width;
        private int _height;

        private CellType[,] _grid;

        public int Width => _width;
        public int Height => _height;

        public CityGrid(int width, int height)
        {
            _width = width;
            _height = height;

            _grid = new CellType[width, height];
        }

        public CellType this[int i, int j]
        {
            get => _grid[i, j];
            set => _grid[i, j] = value;
        }
    }
}