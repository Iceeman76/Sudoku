using System;
using DotNetLab.Sudoku;
using NUnit.Framework;

namespace DotNetLab.SudokuTests;

[TestFixture]
public sealed class ValidationTests
{
    [TestCase(3)]
    [TestCase(27)]
    [TestCase(10)]
    public void RejectsDuplicateGivensInARegion(int duplicateIndex)
    {
        var values = new string('0', 81).ToCharArray();

        values[0] = '1';
        values[duplicateIndex] = '1';

        var puzzle = new Puzzle(new string(values));

        Assert.That(() => SudokuSolver.Solve(puzzle), Throws.ArgumentException);
    }

    [Test]
    public void RejectsAnInvalidCompletedBoard()
    {
        var puzzle = new Puzzle(new string('1', 81));

        Assert.That(() => SudokuSolver.Solve(puzzle), Throws.ArgumentException);
    }

    [Test]
    public void RejectsAPuzzleWithoutASolution()
    {
        var puzzle = new Puzzle("531070000600195000098000060800060003400803001700020006060000280000419005000080079");

        Assert.That(() => SudokuSolver.Solve(puzzle), Throws.TypeOf<InvalidOperationException>());
        Assert.That(puzzle.Solution, Is.Empty);
    }
}
