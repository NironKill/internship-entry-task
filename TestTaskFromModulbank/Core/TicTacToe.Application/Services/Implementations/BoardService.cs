using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.DTOs.GamePlayer;
using TicTacToe.Application.DTOs.Move;
using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Services.Interfaces;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Services.Implementations
{
    public class BoardService : IBoardService
    {
        private readonly IMoveRepository _move;
        private readonly IGamePlayerRepository _gamePlayer;
        private readonly IGameRepository _game;
        private readonly IPlayerRepository _player;

        public BoardService(IMoveRepository move, IGamePlayerRepository gamePlayer, IGameRepository game, IPlayerRepository player)
        {
            _move = move;
            _gamePlayer = gamePlayer;
            _game = game;
            _player = player;
        }

        public async Task<GameGetDTO> GameСompletion(Guid moveId, CancellationToken cancellationToken)
        {
            MoveGetDTO move = await _move.Get(x => x.Id == moveId, entity => new MoveGetDTO
            {
                GameId = entity.GameId,
                PlayerId = entity.PlayerId,
                Row = entity.Row,
                Column = entity.Column,
                PlayerRole = _gamePlayer.Get(x => x.PlayerId == entity.PlayerId && x.GameId == entity.GameId, gp => new GamePlayerGetDTO
                {
                    Role = ((PlayerRole)gp.Role).ToString()
                }, cancellationToken).Result.Role
            }, cancellationToken);

            GameGetDTO result = await _game.Get(x => x.Id == move.GameId, entity => new GameGetDTO
            {
                Id = entity.Id,
                StartAt = entity.StartAt,
                EndAt = entity.EndAt,
                VictoryCondition = entity.VictoryCondition,
                FieldSize = entity.FieldSize,
                Status = ((StatusGame)entity.Status).ToString(),
                Players = _gamePlayer.GetAll(x => x.GameId == entity.Id, gp => new GamePlayerGetDTO
                {
                    Role = ((PlayerRole)gp.Role).ToString(),
                    PlayerName = _player.Get(x => x.Id == gp.PlayerId, p => new PlayerGetDTO
                    {
                        Id = p.Id,
                        Name = p.Name
                    }, cancellationToken).Result.Name
                }, cancellationToken).Result.ToList(),
                Moves = MoveMapVisualisation(_move.GetAll(x => x.GameId == entity.Id, move => new MoveGetDTO
                {
                    Column = move.Column,
                    Row = move.Row,
                    PlayerRole = _gamePlayer.Get(x => x.PlayerId == move.PlayerId && x.GameId == move.GameId, gp => new GamePlayerGetDTO
                    {
                        Role = ((PlayerRole)gp.Role).ToString()
                    }, cancellationToken).Result.Role
                }, cancellationToken).Result.ToList(), entity.FieldSize)
            }, cancellationToken);

            if (HasVictory(result.Moves, move.Row, move.Column, result.VictoryCondition, move.PlayerRole))
            {
                return await _game.Update(x => x.Id == result.Id, (update) =>
                {
                    update.WinnerId = move.PlayerId;
                    update.Status = (int)StatusGame.Completed;
                    update.EndAt = DateTime.UtcNow;
                }, entity => new GameGetDTO
                {
                    Id = result.Id,
                    StartAt = result.StartAt,
                    EndAt = entity.EndAt,
                    VictoryCondition = result.VictoryCondition,
                    FieldSize = result.FieldSize,
                    Status = ((StatusGame)entity.Status).ToString(),
                    WinnerName = _player.Get(x => x.Id == move.PlayerId, player => new PlayerGetDTO
                    {
                        Id = player.Id,
                        Name = player.Name
                    }, cancellationToken).Result.Name,
                    Players = result.Players,
                    Moves = result.Moves,
                }, cancellationToken);
            }
            else if (IsBoardFull(result.Moves))
            {
                return await _game.Update(x => x.Id == result.Id, (update) =>
                {
                    update.Status = (int)StatusGame.Tie;
                    update.EndAt = DateTime.UtcNow;
                }, entity => new GameGetDTO
                {
                    Id = result.Id,
                    StartAt = result.StartAt,
                    EndAt = entity.EndAt,
                    VictoryCondition = result.VictoryCondition,
                    FieldSize = result.FieldSize,
                    Status = ((StatusGame)entity.Status).ToString(),
                    WinnerName = result.WinnerName,
                    Players = result.Players,
                    Moves = result.Moves,
                }, cancellationToken);
            }
            return result;
        }
        public string[][] MoveMapVisualisation(List<MoveGetDTO>? dto, int fieldSize)
        {
            string[][] board = new string[fieldSize][];

            for (int i = 0; i < fieldSize; i++)
            {
                board[i] = new string[fieldSize];
                for (int j = 0; j < fieldSize; j++)
                    board[i][j] = "_";
            }

            if (dto is not null)
            {
                foreach (MoveGetDTO move in dto)
                {
                    board[move.Row][move.Column] = move.PlayerRole;
                }
            }
            return board;
        }

        private bool HasVictory(string[][] board, int row, int column, int victoryCondition, string role)
        {
            return Count(board, row, column, 0, 1, role) + Count(board, row, column, 0, -1, role) - 1 >= victoryCondition ||
                   Count(board, row, column, 1, 0, role) + Count(board, row, column, -1, 0, role) - 1 >= victoryCondition ||
                   Count(board, row, column, 1, 1, role) + Count(board, row, column, -1, -1, role) - 1 >= victoryCondition ||
                   Count(board, row, column, 1, -1, role) + Count(board, row, column, -1, 1, role) - 1 >= victoryCondition;
        }
        private int Count(string[][] board, int row, int column, int dRow, int dCol, string role)
        {
            int size = board.Length;
            int count = 0;

            while (row >= 0 && row < size && column >= 0 && column < size && board[row][column] == role)
            {
                count++;
                row += dRow;
                column += dCol;
            }

            return count;
        }
        private bool IsBoardFull(string[][] board)
        {
            int size = board.GetLength(0);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (board[i][j] == "_")
                        return false;
            return true;
        }
    }
}
