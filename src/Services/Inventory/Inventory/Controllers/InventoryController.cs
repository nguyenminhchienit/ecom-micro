
using Inventory.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Inventory;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService inventoryService;

        public InventoryController(IInventoryService _inventoryService)
        {
            inventoryService = _inventoryService;
        }

        
        [HttpGet]
        [Route("items/{itemNo}", Name = "GetAllByItemNo")]
        [ProducesResponseType(type: typeof(IEnumerable<InventoryEntryDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<InventoryEntryDto>>> GetAllByItemNo([Required] string itemNo)
        {
            var result = await inventoryService.GetAllByItemNoAsync(itemNo);
            return Ok(result);
        }

        [HttpGet]
        [Route("items/{itemNo}/paging", Name = "GetAllByItemNoPagingAsync")]
        [ProducesResponseType(typeof(IEnumerable<InventoryEntryDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<InventoryEntryDto>>> GetAllByItemNoPagingAsync([Required] string itemNo, 
            [FromQuery] GetInventoryPagingQuery query)
        {
            query.SetItemNo(itemNo);
            var result = await inventoryService.GetAllByItemNoPagingAsync(query);
            var obj = new
            {
                result = result,
                extraData = result.GetMetaData()
            };
            return Ok(obj);

        }

        [Route("{id}", Name = "GetInventoryById")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<InventoryEntryDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<InventoryEntryDto>>> GetInventoryById([Required] string id)
        {
            var result = await inventoryService.GetByIdAsync(id);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost("purchase/{itemNo}", Name = "PurchaseOrder")]
        [ProducesResponseType(typeof(InventoryEntryDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<InventoryEntryDto>> PurchaseOrder([Required] string itemNo,
            [FromBody] PurchaseProductDto model)
        {
            model.SetItemNo(itemNo);
            var result = await inventoryService.PurchaseItemAsync(itemNo, model);
            return Ok(result);
        }

        [Route("{id}", Name = "DeleteById")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteById([Required] string id)
        {
            var entity = await inventoryService.GetByIdAsync(id);
            if (entity is null) return NotFound();
            await inventoryService.DeleteAsync(id);
            return NoContent();
        }


    }
}
