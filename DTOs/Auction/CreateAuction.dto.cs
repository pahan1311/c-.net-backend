namespace backend.DTOs.Auction{

   public class CreateAuctionDTO
{
      public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal StartingPrice { get; set; }
    public required Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public decimal AuctionDuration { get; set; }
    public required IFormFile Image { get; set; }
}

}