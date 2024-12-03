using backend.Data;
using backend.DTOs.Auction;
using backend.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {

        private readonly ApplicationDBContext dbContext;

        public AuctionController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpPost("create")]
        public async Task<IActionResult> CreateAuction([FromForm] CreateAuctionDTO dto)
        {
            if (dto.Image == null || dto.Image.Length == 0)
            {
                return BadRequest("Image is required.");
            }

            // Define the directory path
            var imagesDirectory = Path.Combine("wwwroot", "images");

            // Check if the directory exists, if not, create it
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }

            // Save the image to the server
            var imagePath = Path.Combine(imagesDirectory, dto.Image.FileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            // Create a new auction
            var auction = new Auction
            {
                Title = dto.Title,
                Description = dto.Description,
                StartTime = dto.StartTime,
                StartingPrice = dto.StartingPrice,
                CurrentPrice = dto.StartingPrice,
                UserId = dto.UserId,
                AuctionDuration = dto.AuctionDuration,
                Image = $"/images/{dto.Image.FileName}" // Store relative path
            };

            dbContext.Auctions.Add(auction);
            await dbContext.SaveChangesAsync();

            return Ok(auction);
        }




        [HttpPut("update/{id}")]
        public IActionResult UpdateAuction(Guid id, UpdateAuctionDTO dto)
        {
            var auction = dbContext.Auctions.FirstOrDefault(a => a.Id == id);
            if (auction == null)
            {
                return NotFound(new { message = "Auction not found" });
            }

            // Update the auction fields
            auction.Title = dto.Title;
            auction.Description = dto.Description;
            auction.StartingPrice = dto.StartingPrice;
            auction.StartTime = dto.StartTime;
            auction.AuctionDuration = dto.AuctionDuration;

            dbContext.SaveChanges();

            return Ok(auction);
        }


        [HttpGet("all")]
        public IActionResult GetAllProduct()
        {
            var allProducts = dbContext.Auctions.ToList();
            return Ok(allProducts);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuctionById(Guid id)
        {

            var auction = dbContext.Auctions.FirstOrDefault(a => a.Id == id);

            if (auction == null)
            {
                return NotFound("Auction not found.");
            }

            return Ok(auction);
        }

        [HttpGet("user/{userId}/auctions")]
        public IActionResult GetUserAuctionsWithBids(Guid userId)
        {
            // Fetch all auctions created by the user
            var auctions = dbContext.Auctions
                .Where(a => a.UserId == userId)
                .ToList();

            if (!auctions.Any())
            {
                return NotFound("No auctions found for this user.");
            }

            // Create a list to hold auction and bids information
            var auctionWithBids = auctions.Select(auction => new
            {
                Auction = auction,
                Bids = dbContext.Bids.Where(b => b.AuctionId == auction.Id).ToList()
            });

            return Ok(auctionWithBids);
        }






    }
}