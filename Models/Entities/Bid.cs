using System.Text.Json.Serialization;

namespace backend.Models.Entities
{
    public class Bid
    {
        public Guid Id { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; }

        public Guid? UserId { get; set; }
        // Foreign key reference to Auction
        public Guid AuctionId { get; set; }
        [JsonIgnore] // Prevent serialization of the auction in the bid
        public virtual Auction Auction { get; set; }

        [JsonIgnore] // Optionally prevent serialization of the User
        public virtual User User { get; set; } // Navigation property for the related auction
    }
}
