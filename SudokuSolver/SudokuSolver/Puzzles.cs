using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public static class Puzzles
    {
        private static List<List<Tuple<int, int>>> EasyPuzzles
        {
            get
            {
                return new List<List<Tuple<int, int>>>
                {
                    new List<Tuple<int, int>>
                    {
                        new Tuple<int, int>(1, 2),
                        new Tuple<int, int>(4, 8),
                        new Tuple<int, int>(6, 4),
                        new Tuple<int, int>(9, 6),
                        new Tuple<int, int>(12, 6),
                        new Tuple<int, int>(16, 5),
                        new Tuple<int, int>(20, 7),
                        new Tuple<int, int>(21, 4),
                        new Tuple<int, int>(25, 9),
                        new Tuple<int, int>(26, 2),
                        new Tuple<int, int>(28, 3),
                        new Tuple<int, int>(32, 4),
                        new Tuple<int, int>(36, 7),
                        new Tuple<int, int>(40, 3),
                        new Tuple<int, int>(42, 5),
                        new Tuple<int, int>(46, 4),
                        new Tuple<int, int>(50, 6),
                        new Tuple<int, int>(54, 9),
                        new Tuple<int, int>(56, 1),
                        new Tuple<int, int>(57, 9),
                        new Tuple<int, int>(61, 7),
                        new Tuple<int, int>(62, 4),
                        new Tuple<int, int>(66, 8),
                        new Tuple<int, int>(70, 2),
                        new Tuple<int, int>(73, 5),
                        new Tuple<int, int>(76, 6),
                        new Tuple<int, int>(78, 8),
                        new Tuple<int, int>(81, 1)
                    },
                    new List<Tuple<int, int>>
                    {
                        new Tuple<int, int>(3, 4),
                        new Tuple<int, int>(6, 9),
                        new Tuple<int, int>(11, 1),
                        new Tuple<int, int>(16, 4),
                        new Tuple<int, int>(17, 8),
                        new Tuple<int, int>(18, 3),
                        new Tuple<int, int>(19, 6),
                        new Tuple<int, int>(22, 8),
                        new Tuple<int, int>(24, 4),
                        new Tuple<int, int>(25, 1),
                        new Tuple<int, int>(27, 2),
                        new Tuple<int, int>(28, 3),
                        new Tuple<int, int>(32, 2),
                        new Tuple<int, int>(35, 4),
                        new Tuple<int, int>(36, 5),
                        new Tuple<int, int>(41, 7),
                        new Tuple<int, int>(46, 5),
                        new Tuple<int, int>(47, 6),
                        new Tuple<int, int>(50, 8),
                        new Tuple<int, int>(54, 1),
                        new Tuple<int, int>(55, 7),
                        new Tuple<int, int>(57, 8),
                        new Tuple<int, int>(58, 6),
                        new Tuple<int, int>(60, 5),
                        new Tuple<int, int>(63, 4),
                        new Tuple<int, int>(64, 1),
                        new Tuple<int, int>(65, 4),
                        new Tuple<int, int>(66, 9),
                        new Tuple<int, int>(71, 7),
                        new Tuple<int, int>(76, 1),
                        new Tuple<int, int>(79, 8)
                    }
                };
            }
        }

        private static List<List<Tuple<int, int>>> MediumPuzzles
        {
            get
            {
                return new List<List<Tuple<int, int>>>
                {
                    new List<Tuple<int, int>>
                    {
                        new Tuple<int, int>(3, 4),
                        new Tuple<int, int>(6, 9),
                        new Tuple<int, int>(8, 3),
                        new Tuple<int, int>(9, 8),
                        new Tuple<int, int>(11, 5),
                        new Tuple<int, int>(14, 1),
                        new Tuple<int, int>(17, 2),
                        new Tuple<int, int>(22, 7),
                        new Tuple<int, int>(23, 4),
                        new Tuple<int, int>(24, 6),
                        new Tuple<int, int>(27, 9),
                        new Tuple<int, int>(29, 1),
                        new Tuple<int, int>(30, 8),
                        new Tuple<int, int>(33, 5),
                        new Tuple<int, int>(37, 5),
                        new Tuple<int, int>(40, 4),
                        new Tuple<int, int>(43, 9),
                        new Tuple<int, int>(44, 8),
                        new Tuple<int, int>(45, 1),
                        new Tuple<int, int>(47, 6),
                        new Tuple<int, int>(50, 2),
                        new Tuple<int, int>(54, 5),
                        new Tuple<int, int>(55, 3),
                        new Tuple<int, int>(56, 9),
                        new Tuple<int, int>(58, 5),
                        new Tuple<int, int>(61, 2),
                        new Tuple<int, int>(62, 7),
                        new Tuple<int, int>(63, 6),
                        new Tuple<int, int>(67, 6),
                        new Tuple<int, int>(68, 9),
                        new Tuple<int, int>(70, 1),
                        new Tuple<int, int>(75, 5),
                        new Tuple<int, int>(76, 3)
                    },
                    new List<Tuple<int, int>>
                    {
                        new Tuple<int, int>(4, 6),
                        new Tuple<int, int>(7, 5),
                        new Tuple<int, int>(10, 2),
                        new Tuple<int, int>(15, 9),
                        new Tuple<int, int>(19, 7),
                        new Tuple<int, int>(21, 6),
                        new Tuple<int, int>(23, 5),
                        new Tuple<int, int>(25, 4),
                        new Tuple<int, int>(28, 4),
                        new Tuple<int, int>(29, 5),
                        new Tuple<int, int>(30, 8),
                        new Tuple<int, int>(32, 9),
                        new Tuple<int, int>(34, 3),
                        new Tuple<int, int>(37, 1),
                        new Tuple<int, int>(45, 4),
                        new Tuple<int, int>(46, 9),
                        new Tuple<int, int>(47, 6),
                        new Tuple<int, int>(50, 7),
                        new Tuple<int, int>(55, 3),
                        new Tuple<int, int>(56, 2),
                        new Tuple<int, int>(57, 4),
                        new Tuple<int, int>(60, 1),
                        new Tuple<int, int>(62, 5),
                        new Tuple<int, int>(63, 7),
                        new Tuple<int, int>(67, 7),
                        new Tuple<int, int>(69, 5),
                        new Tuple<int, int>(70, 9),
                        new Tuple<int, int>(71, 3),
                        new Tuple<int, int>(72, 2),
                        new Tuple<int, int>(75, 9),
                        new Tuple<int, int>(76, 8),
                        new Tuple<int, int>(77, 2),
                        new Tuple<int, int>(78, 3)
                    }
                };
            }
        }

        private static List<List<Tuple<int, int>>> HardPuzzles
        {
            get
            {
                return new List<List<Tuple<int, int>>>
                {
                    new List<Tuple<int, int>>
                    {
                        new Tuple<int, int>(2, 1),
                        new Tuple<int, int>(3, 4),
                        new Tuple<int, int>(5, 9),
                        new Tuple<int, int>(6, 5),
                        new Tuple<int, int>(7, 8),
                        new Tuple<int, int>(10, 3),
                        new Tuple<int, int>(19, 9),
                        new Tuple<int, int>(22, 6),
                        new Tuple<int, int>(23, 2),
                        new Tuple<int, int>(26, 3),
                        new Tuple<int, int>(27, 5),
                        new Tuple<int, int>(32, 3),
                        new Tuple<int, int>(35, 8),
                        new Tuple<int, int>(36, 6),
                        new Tuple<int, int>(44, 2),
                        new Tuple<int, int>(46, 8),
                        new Tuple<int, int>(47, 3),
                        new Tuple<int, int>(51, 4),
                        new Tuple<int, int>(53, 5),
                        new Tuple<int, int>(55, 1),
                        new Tuple<int, int>(62, 4),
                        new Tuple<int, int>(64, 5),
                        new Tuple<int, int>(66, 6),
                        new Tuple<int, int>(77, 5),
                        new Tuple<int, int>(78, 3),
                        new Tuple<int, int>(79, 1)
                    },
                    new List<Tuple<int, int>>
                    {
                        new Tuple<int, int>(4, 6),
                        new Tuple<int, int>(5, 1),
                        new Tuple<int, int>(6, 3),
                        new Tuple<int, int>(7, 7),
                        new Tuple<int, int>(11, 1),
                        new Tuple<int, int>(12, 9),
                        new Tuple<int, int>(23, 8),
                        new Tuple<int, int>(26, 2),
                        new Tuple<int, int>(29, 5),
                        new Tuple<int, int>(32, 9),
                        new Tuple<int, int>(34, 1),
                        new Tuple<int, int>(37, 6),
                        new Tuple<int, int>(39, 4),
                        new Tuple<int, int>(43, 3),
                        new Tuple<int, int>(44, 5),
                        new Tuple<int, int>(46, 1),
                        new Tuple<int, int>(53, 8),
                        new Tuple<int, int>(54, 6),
                        new Tuple<int, int>(59, 4),
                        new Tuple<int, int>(62, 9),
                        new Tuple<int, int>(63, 5),
                        new Tuple<int, int>(64, 9),
                        new Tuple<int, int>(65, 4),
                        new Tuple<int, int>(71, 1),
                        new Tuple<int, int>(73, 3),
                        new Tuple<int, int>(76, 5)
                    }
                };
                // Puzzle 1
                // puzzle 2
            }
        }

        private static List<List<Tuple<int, int>>> InsanePuzzles
        {
            get
            {
                return new List<List<Tuple<int, int>>>
                {
                    new List<Tuple<int, int>>
                    {
                        new Tuple<int, int>(1, 5),
                        new Tuple<int, int>(4, 8),
                        new Tuple<int, int>(10, 4),
                        new Tuple<int, int>(13, 6),
                        new Tuple<int, int>(14, 2),
                        new Tuple<int, int>(21, 1),
                        new Tuple<int, int>(28, 3),
                        new Tuple<int, int>(29, 8),
                        new Tuple<int, int>(45, 3),
                        new Tuple<int, int>(51, 1),
                        new Tuple<int, int>(54, 5),
                        new Tuple<int, int>(57, 2),
                        new Tuple<int, int>(61, 6),
                        new Tuple<int, int>(62, 9),
                        new Tuple<int, int>(65, 7),
                        new Tuple<int, int>(70, 8),
                        new Tuple<int, int>(78, 3)
                    },
                    new List<Tuple<int, int>>
                    {
                        new Tuple<int, int>(7, 9),
                        new Tuple<int, int>(11, 7),
                        new Tuple<int, int>(16, 6),
                        new Tuple<int, int>(20, 3),
                        new Tuple<int, int>(24, 8),
                        new Tuple<int, int>(30, 9),
                        new Tuple<int, int>(37, 7),
                        new Tuple<int, int>(39, 6),
                        new Tuple<int, int>(40, 1),
                        new Tuple<int, int>(41, 5),
                        new Tuple<int, int>(51, 3),
                        new Tuple<int, int>(53, 2),
                        new Tuple<int, int>(57, 5),
                        new Tuple<int, int>(59, 6),
                        new Tuple<int, int>(62, 8),
                        new Tuple<int, int>(71, 3),
                        new Tuple<int, int>(72, 4)
                    }
                };
                // Puzzle 1
                // puzzle 2
            }
        }

        public static List<Tuple<int, int>> Get(Difficulty difficulty)
        {
            var rng = new Random();

            switch (difficulty)
            {
                case Difficulty.Easy:
                    return EasyPuzzles[rng.Next(0, EasyPuzzles.Count)];
                case Difficulty.Medium:
                    return MediumPuzzles[rng.Next(0, MediumPuzzles.Count)];
                case Difficulty.Hard:
                    return HardPuzzles[rng.Next(0, HardPuzzles.Count)];
                case Difficulty.Insane:
                default:
                    return InsanePuzzles[rng.Next(0, InsanePuzzles.Count)];
            }
        }
    }
}
