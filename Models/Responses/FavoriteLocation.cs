using Mappy.Models;

namespace Mappy.Models.Responses
{
    public class FavoriteLocation : Coordinates
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}