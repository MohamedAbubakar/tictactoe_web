using System.Collections.Generic;

namespace TicTacToe_Web.Models
{
    public class ResultViewModel
    {
        public int ScoreUser1 { get; set; }
        public int ScoreUser2 { get; set; }
        public List<int> SelectedValuesUser1 { get; set; }
        public List<int> SelectedValuesUser2 { get; set; }
    }
}