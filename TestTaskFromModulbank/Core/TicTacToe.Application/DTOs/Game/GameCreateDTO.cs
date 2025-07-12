namespace TicTacToe.Application.DTOs.Game
{
    public class GameCreateDTO
    {
        public string PlayerX { get; set; }
        public string PlayerO { get; set; }
        public int FieldSize { get; set; }
        public int VictoryCondition { get; set; }
    }
}
