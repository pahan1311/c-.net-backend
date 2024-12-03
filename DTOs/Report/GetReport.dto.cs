// DTOs for Auction and Bids in the report
using backend.DTOs.Bid;

public class AuctionReportDTO
    {
        public Guid AuctionId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal StartingPrice { get; set; }
        public DateTime StartTime { get; set; }
        public decimal AuctionDuration { get; set; }
        public required string Image { get; set; }
        public required List<CreateBidDTO> Bids { get; set; }
    }