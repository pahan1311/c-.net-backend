using backend.Data;
using backend.DTOs.Bid;
using backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ApplicationDBContext _context;

    public ReportController(ApplicationDBContext context)
    {
        _context = context;
    }

   [HttpGet("AuctionReport")]
        public async Task<ActionResult<IEnumerable<AuctionReportDTO>>> GetAuctionReport()
        {
            // Retrieve auctions with associated bids
            var auctionsWithBids = await _context.Auctions
                .Include(a => a.Bids) // Include the related bids
                .Select(a => new AuctionReportDTO
                {
                    AuctionId = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    StartingPrice = a.StartingPrice,
                    StartTime = a.StartTime,
                    AuctionDuration = a.AuctionDuration,
                    Image = a.Image,
                    Bids = a.Bids.Select(b => new CreateBidDTO
                    {
                        BidAmount = b.BidAmount,
                        BidTime = b.BidTime
                    }).ToList()
                })
                .ToListAsync();

            // Return the report as JSON
            return Ok(auctionsWithBids);
        }

}
}

