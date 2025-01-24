using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;

namespace MultiShop.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly GetAddressQueryHandler _getAddressQueryHandler;
        private readonly GetAddressByIdQueryHandler _getAddressByIdQueryHandler;
        private readonly CreateAddressCommandHandler _createAddressCommandHandler;
        private readonly UpdateAddresCommandHandler _updateAddresCommandHandler;
        private readonly RemoveAddressCommandHandler _removeAddressCommandHandler;

        public AddressesController(GetAddressQueryHandler getAddressQueryHandler, GetAddressByIdQueryHandler getAddressByIdQueryHandler, CreateAddressCommandHandler createAddressCommandHandler, UpdateAddresCommandHandler updateAddresCommandHandler, RemoveAddressCommandHandler removeAddressCommandHandler)
        {
            _getAddressQueryHandler = getAddressQueryHandler;
            _getAddressByIdQueryHandler = getAddressByIdQueryHandler;
            _createAddressCommandHandler = createAddressCommandHandler;
            _updateAddresCommandHandler = updateAddresCommandHandler;
            _removeAddressCommandHandler = removeAddressCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> AddresList()
        {
            var values = await _getAddressQueryHandler.Handle();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> AddressListById (int id)
        {
            var values = await _getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressCommand command)
        {
            await _createAddressCommandHandler.Handle(command);
            return Ok("Adres bilgisi başarıyla eklendi.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAddress(UpdateAddressCommand command)
        {
            await _updateAddresCommandHandler.Handle(command);
            return Ok("Adres bilgisi başarıyla güncellendi.");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveAddress(int id)
        {
            await _removeAddressCommandHandler.Handle(new RemoveAddressCommand(id));
            return Ok("Adres bilgisi başarıyla silindi.");
        }


    }
}
