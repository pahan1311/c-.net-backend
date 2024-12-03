using System.Text.Json.Serialization;

namespace backend.Models.Entities
{
    public class Auction
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal? CurrentPrice { get; set; }
        public DateTime StartTime { get; set; }
        public decimal AuctionDuration { get; set; }
        public required string Image { get; set; }
        public Guid? UserId { get; set; }

        // Navigation property to relate Auction to its Bids
       [JsonIgnore] // Prevent serialization of the bids in the auction
        public virtual ICollection<Bid> Bids { get; set; }// Initialize as an empty list

        [JsonIgnore] // Optionally prevent serialization of the User
        public virtual User User { get; set; }
    }
}
