using Microsoft.AspNetCore.Mvc;
using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.Requests.Games.Read.GetAll;
using TicTacToe.Application.Requests.Games.Read.GetById;
using TicTacToe.Application.Requests.Games.Write.Create;
using TicTacToe.Application.Requests.Games.Write.Delete;
using TicTacToe.Domain.Enums;
using TicTacToe.WebAPI.Controllers.Base;

namespace TicTacToe.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GameController : BaseController
    {
        /// <summary>
        /// Creates the game
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Game/Create
        /// {
        ///
        ///     FieldSize: "playing field size (NxN: N >= 3)",
        ///     VictoryCondition: "the required number of vertical, horizontal and diagonal matches to win the game",
        ///     PlayerX: "player name X",
        ///     PlayerO: "player name O"
        /// }
        /// </remarks>
        /// <param name="dto">GameCreateDTO object</param>
        /// <returns>Returns Guid</returns>
        /// <response code="201">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] GameCreateDTO dto)
        {
            CreateGameRequest request = new CreateGameRequest
            {
                FieldSize = dto.FieldSize,
                VictoryCondition = dto.VictoryCondition,
                Players = new Dictionary<string, int>()
                {
                    { dto.PlayerX, (int)PlayerRole.X },
                    { dto.PlayerO, (int)PlayerRole.O }
                }
            };

            Guid response = await Mediator.Send(request);
            return Created("", response);
        }

        /// <summary>
        /// Delete the game
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /game/Delete/id
        /// </remarks>
        /// <param name="id">Game id (guid)</param>
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
            DeleteGameRequest request = new DeleteGameRequest
            {
                Id = id,
            };
            bool response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Gets a list of all games
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /game/GetAll
        /// </remarks>
        /// <returns>Returns GameGetDTO</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            GetAllGameRequest request = new GetAllGameRequest();

            ICollection<GameGetDTO> responce = await Mediator.Send(request);
            return Ok(responce);
        }

        /// <summary>
        /// Gets the game by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /game/GetById/id
        /// </remarks>
        /// <param name="id">Game id (guid)</param>
        /// <returns>Returns GameGetDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        /// <response code="404">The server can not find the requested resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetByIdGameRequest request = new GetByIdGameRequest
            {
                Id = id,
            };
            GameGetDTO responce = await Mediator.Send(request);
            return Ok(responce);
        }
    }
}
