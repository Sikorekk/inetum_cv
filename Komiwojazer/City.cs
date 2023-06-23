namespace Komiwojazer
{
    public class City
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                name = value;
            }
        }
        public int X { get; set; }
        public int Y { get; set; }
        public City() { Name = "Default"; X = 0; Y = 0; }
        public City(string name, int x, int y) : this() { Name = name; X = x; Y = y; }
        public override string ToString() { return $"Nazwa: {Name}\nX: {X} Y: {Y}"; }
        public static bool operator ==(City c1, City c2)
        {
            if (c1.Name == c2.Name && c1.X == c2.X && c1.Y == c2.Y)
                return true;
            return false;
        }
        public static bool operator !=(City c1, City c2)
        {
            if (!(c1 == c2))
                return true;
            return false;
        }

    }
}
