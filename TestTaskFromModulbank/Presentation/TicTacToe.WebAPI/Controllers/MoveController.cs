using Microsoft.AspNetCore.Mvc;
using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.DTOs.Move;
using TicTacToe.Application.Requests.Moves.Write.Create;
using TicTacToe.WebAPI.Controllers.Base;

namespace TicTacToe.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MoveController : BaseController
    {
        /// <summary>
        /// Creates the move
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Move/Create
        /// {
        ///
        ///     Column: "vertical coordinate",
        ///     Row: "horizontal coordinate",
        ///     PlayerName: "player name",
        ///     GameId: "game id (Guid)"
        /// }
        /// </remarks>
        /// <param name="dto">MoveCreateDTO object</param>
        /// <returns>Returns GameGetDTO</returns>
        /// <response code="201">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] MoveCreateDTO dto)
        {
            CreateMoveRequest request = new CreateMoveRequest
            {
                Column = dto.Column,
                Row = dto.Row,
                PlayerName = dto.PlayerName,
                GameId = dto.GameId
            };

            GameGetDTO response = await Mediator.Send(request);
            return Created("", response);
        }
    }
}
