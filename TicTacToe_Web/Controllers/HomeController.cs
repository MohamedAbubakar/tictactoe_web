using System.Collections.Generic;
using System.Web.Mvc;
using TicTacToe_Web.Models;

namespace TicTacToe_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<List<int>> _winningCombinations = new List<List<int>>
        {
            new List<int>{11,12,13},
            new List<int>{21,22,23},
            new List<int>{31,32,33},
            new List<int>{11,21,31},
            new List<int>{12,22,32},
            new List<int>{13,23,33},
            new List<int>{11,22,33},
            new List<int>{13,22,31}
        };

        private static bool _currentPersonIsUser1 = true;

        private static List<int> _selectedValuesUser1 = new List<int>();
        private static List<int> _selectedValuesUser2 = new List<int>();

        private static int _scoreUser1 = 0;
        private static int _scoreUser2 = 0;

        public ActionResult Index(int? value)
        {
            var model = new ResultViewModel();
            SetModelValues(model);

            if (!value.HasValue
                || _selectedValuesUser1.Contains(value.Value)
                || _selectedValuesUser2.Contains(value.Value))
            {
                return View(model);
            }

            if (_currentPersonIsUser1)
            {
                _selectedValuesUser1.Add(value.Value);
            }
            else
            {
                _selectedValuesUser2.Add(value.Value);
            }

            _currentPersonIsUser1 = !_currentPersonIsUser1;

            var isWinnerUser1 = IsWinner(_selectedValuesUser1);
            var isWinnerUser2 = IsWinner(_selectedValuesUser2);

            if (isWinnerUser2 || isWinnerUser1)
            {
                if (isWinnerUser1)
                {
                    _scoreUser1 += 1;
                }
                else
                {
                    _scoreUser2 += 1;
                }

                Reset();
            }

            if ((_selectedValuesUser1.Count + _selectedValuesUser2.Count) == 9)
            {
                Reset();
            }
            
            SetModelValues(model);

            return View(model);
        }

        private static void SetModelValues(ResultViewModel model)
        {
            model.ScoreUser1 = _scoreUser1;
            model.ScoreUser2 = _scoreUser2;
            model.SelectedValuesUser1 = _selectedValuesUser1;
            model.SelectedValuesUser2 = _selectedValuesUser2;
        }

        private static void Reset()
        {
            _selectedValuesUser1 = new List<int>();
            _selectedValuesUser2 = new List<int>();
            _currentPersonIsUser1 = true;
        }

        private bool IsWinner(List<int> selectedValues)
        {
            foreach (var winningCombination in _winningCombinations)
            {
                if (Contains(selectedValues, winningCombination))
                {
                    return true;
                }
            }

            return false;
        }

        private bool Contains(List<int> values, List<int> contains)
        {
            foreach (var item in contains)
            {
                if (!values.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}