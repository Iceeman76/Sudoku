using NUnit.Framework;
using Sudoku;
using Sudoku.Techniques;

namespace SudokuTests;

[TestFixture]
public sealed class TechniqueTests
{
    [Test]
    public void NakedPairChecksLaterPairsWhenFirstPairCannotEliminateCandidates()
    {
        var board = new Board(new Puzzle(new string('0', 81)));

        SetCandidates(board[0], 1, 2);
        SetCandidates(board[1], 1, 2);
        SetCandidates(board[2], 3, 4);
        SetCandidates(board[3], 3, 4);
        SetCandidates(board[4], 3, 4, 5);

        for (var index = 5; index < 9; index++)
        {
            SetCandidates(board[index], 5, 6, 7, 8, 9);
        }

        for (var index = 9; index < 27; index++)
        {
            SetCandidates(board[index], 5, 6, 7, 8, 9);
        }

        var changed = Technique.NakedPair(board);

        Assert.That(changed, Is.True);
        Assert.That(board[4].Candidates.Values, Is.EqualTo([5]));
    }

    [Test]
    public void NakedPairDoesNotUseThreeCellsWithTheSameCandidates()
    {
        var board = new Board(new Puzzle(new string('0', 81)));

        SetCandidates(board[0], 1, 2);
        SetCandidates(board[1], 1, 2);
        SetCandidates(board[2], 1, 2);

        var changed = Technique.NakedPair(board);

        Assert.That(changed, Is.False);
        Assert.That(board[3].Candidates.Count, Is.EqualTo(9));
    }

    [Test]
    public void HiddenPairRemovesOtherCandidatesOnlyFromThePair()
    {
        var board = new Board(new Puzzle(new string('0', 81)));

        for (var index = 0; index < 81; index++)
        {
            SetCandidates(board[index], 3, 4, 5, 6, 7, 8, 9);
        }

        SetCandidates(board[0], 1, 2, 3);
        SetCandidates(board[1], 1, 2, 4);

        Assert.That(Technique.HiddenPair(board), Is.True);
        Assert.That(board[0].Candidates.Values, Is.EqualTo([1, 2]));
        Assert.That(board[1].Candidates.Values, Is.EqualTo([1, 2]));
        Assert.That(board[2].Candidates.Count, Is.EqualTo(7));
        Assert.That(Technique.HiddenPair(board), Is.False);
    }

    [Test]
    public void BacktrackingKeepsCandidateEliminationsWhenTryingOtherValues()
    {
        var board = new Board(new Puzzle(new string('0', 81)));

        SetCandidates(board[0], 1, 2);
        SetCandidates(board[1], 1, 2);
        SetCandidates(board[2], 1, 2);

        Assert.That(Technique.Backtracking(board), Is.False);
        Assert.That(board.ToAnnotationString(), Is.EqualTo(new string('0', 81)));
        Assert.That(board[0].Candidates.Values, Is.EqualTo([1, 2]));
        Assert.That(board[1].Candidates.Values, Is.EqualTo([1, 2]));
        Assert.That(board[2].Candidates.Values, Is.EqualTo([1, 2]));
    }

    [Test]
    public void BacktrackingRestoresTheBoardWhenThereIsNoSolution()
    {
        var puzzle = new Puzzle("531070000600195000098000060800060003400803001700020006060000280000419005000080079");
        var board = new Board(puzzle);
        var originalCandidates = new int[81];

        for (var index = 0; index < 81; index++)
        {
            originalCandidates[index] = board[index].Candidates.Value;
        }

        Assert.That(Technique.Backtracking(board), Is.False);
        Assert.That(board.ToAnnotationString(), Is.EqualTo("531070000600195000098000060800060003400803001700020006060000280000419005000080079"));

        for (var index = 0; index < 81; index++)
        {
            Assert.That(board[index].Candidates.Value, Is.EqualTo(originalCandidates[index]));
        }
    }

    private static void SetCandidates(Cell cell, params int[] values)
    {
        cell.Candidates.Clear();

        foreach (var value in values)
        {
            cell.Candidates.SetValue(value);
        }
    }
}
