using Mappy.Models;

namespace Mappy.Models.Responses
{
    public class Location : Coordinates
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
    }
}