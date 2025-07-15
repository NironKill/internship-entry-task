using Microsoft.AspNetCore.Mvc;
using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Requests.Players.Read.GetAll;
using TicTacToe.Application.Requests.Players.Read.GetById;
using TicTacToe.Application.Requests.Players.Write.Create;
using TicTacToe.Application.Requests.Players.Write.Delete;
using TicTacToe.Application.Requests.Players.Write.Patch;
using TicTacToe.WebAPI.Controllers.Base;

namespace TicTacToe.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlayerController : BaseController
    {
        /// <summary>
        /// Creates the player
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Player/Create
        /// {
        ///
        ///     Name: "player name (string)",
        /// }
        /// </remarks>
        /// <param name="dto">PlayerCreateDTO object</param>
        /// <returns>Returns Guid</returns>
        /// <response code="201">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PlayerCreateDTO dto)
        {
            CreatePlayerRequest request = new CreatePlayerRequest
            {
                Name = dto.Name,
            };

            Guid response = await Mediator.Send(request);
            return Created("", response);
        }

        /// <summary>
        /// Change player name
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PATCH /Player/Patch/id
        /// {
        ///
        ///     Name: "player name (string)",
        /// }
        /// </remarks>
        /// <param name="id">Player id (guid)</param>
        /// <returns>Returns PlayerGetDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        /// <response code="404">The server can not find the requested resource.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(Guid id, [FromBody] PlayerUpdateDTO dto)
        {
            PatchPlayerRequest request = new PatchPlayerRequest
            {
                Id = id,
                Name = dto.Name
            };

            PlayerGetDTO response = await Mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Delete the player
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /Player/Delete/id
        /// </remarks>
        /// <param name="id">Player id (guid)</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        /// <response code="404">The server can not find the requested resource.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeletePlayerRequest request = new DeletePlayerRequest
            {
                Id = id,
            };
            bool response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Gets a list of all players
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Player/GetAll
        /// </remarks>
        /// <returns>Returns PlayerGetDTO</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            GetAllPlayerRequest request = new GetAllPlayerRequest();

            ICollection<PlayerGetDTO> responce = await Mediator.Send(request);
            return Ok(responce);
        }

        /// <summary>
        /// Gets the player by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Player/GetById/id
        /// </remarks>
        /// <param name="id">Player id (guid)</param>
        /// <returns>Returns PlayerGetDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        /// <response code="404">The server can not find the requested resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetByIdPlayerRequest request = new GetByIdPlayerRequest
            {
                Id = id,
            };
            PlayerGetDTO responce = await Mediator.Send(request);
            return Ok(responce);
        }
    }
}
