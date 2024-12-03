using backend.Data;
using backend.DTOs.Bid;
using backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public BidController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
public async Task<ActionResult<Bid>> PostBid([FromBody] CreateBidDTO bidDTO)
{
    // Validate incoming data
    if (bidDTO.BidAmount <= 0)
    {
        return BadRequest("Bid amount must be greater than zero.");
    }

    if (bidDTO.AuctionId == Guid.Empty)
    {
        return BadRequest("AuctionId must be provided.");
    }

    // Check if the auction exists
    var auction = await _context.Auctions
        .FirstOrDefaultAsync(a => a.Id == bidDTO.AuctionId);

    if (auction == null)
    {
        return NotFound("Auction not found.");
    }

    // Create a new bid entity
    var bid = new Bid
    {
        Id = Guid.NewGuid(),
        BidAmount = bidDTO.BidAmount,
        BidTime = DateTime.UtcNow, 
        AuctionId = bidDTO.AuctionId,
        UserId = bidDTO.UserId
    };

  
    if (bidDTO.BidAmount > auction.CurrentPrice)
    {
        auction.CurrentPrice = bidDTO.BidAmount; 
    }
    else
    {
        return BadRequest("Bid amount must be higher than the current auction price.");
    }

    // Add the bid to the database
    _context.Bids.Add(bid);

    // Mark the auction as modified
    _context.Auctions.Update(auction); // Optional, as it’s being tracked

    // Save changes
    await _context.SaveChangesAsync();

    // Return the created bid with a 201 Created response
    return CreatedAtAction(nameof(PostBid), new { id = bid.Id }, bid);
}




        // Get all bids for an auction
        [HttpGet("all/auction/{id}")]
        public async Task<IActionResult> GetBidsForAuction(Guid id)
        {
            var bids = await _context.Bids
                .Where(b => b.AuctionId == id)
                .ToListAsync();

            return Ok(bids);
        }


    }



}

