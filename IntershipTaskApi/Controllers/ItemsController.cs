using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntershipTaskApi.Data;
using IntershipTaskApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace IntershipTaskApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItems(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.UniqueId == id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: Home/Create
        [HttpPost]
        public async Task<IActionResult> PostItems([FromBody] Item item)
        {
            try
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetItems", new { id = item.UniqueId }, item);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.ToLower().Contains("violation of primary key".ToLower()))
                {
                    return new JsonResult(ex.InnerException.Message);
                }

                return new JsonResult(ex.Message);
            }
        }

        // POST: Home/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItems(string id, [FromBody] Item item)
        {
            if (id != item.UniqueId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _context.Items.FirstOrDefaultAsync(f => f.UniqueId == id);
                    if (result != null)
                    {
                        result.UniqueId = item.UniqueId;
                        result.Title = item.Title;
                        result.Description = item.Description;
                        result.UnitType = item.UnitType;
                        result.Rate = item.Rate;
                        await _context.SaveChangesAsync();
                        return Ok("Item has been updated successfully.");
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!ItemExists(item.UniqueId))
                    {
                        return NotFound(ex.Message);
                    }
                }
            }
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItems(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.UniqueId == id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        private bool ItemExists(string id)
        {
            return _context.Items.Any(e => e.UniqueId == id);
        }
    }
}
