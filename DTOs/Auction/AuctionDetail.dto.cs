namespace backend.DTOs.Auction
{


    public class AuctionDetailDTO
    {

        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? StartingPrice { get; set; }
        public DateTime? StartTime { get; set; }
        public decimal? AuctionDuration { get; set; }
        public IFormFile? Image { get; set; }
    }
}