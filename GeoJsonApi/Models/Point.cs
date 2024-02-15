namespace GeoJsonApi.Models
{
    public class Point
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Number { get; set; }
        public List<Coordinate> Coordinates { get; set; } = new List<Coordinate>();
    }
}
