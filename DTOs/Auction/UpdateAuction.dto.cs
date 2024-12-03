namespace backend.DTOs.Auction{
    public class UpdateAuctionDTO
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public decimal StartingPrice { get; set; }
    public DateTime StartTime { get; set; }
    public decimal AuctionDuration { get; set; }
    public required string Category { get; set; }
}
}
