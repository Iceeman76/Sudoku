# Sudoku Solver

A high-performance C# Sudoku solver that uses human-style solving techniques before falling back to backtracking. Supports batch solving of large puzzle sets with multi-threaded parallelism.

## Features

- **Human-style techniques** — solves puzzles using logical deduction before brute force
  - Naked Singles
  - Hidden Singles
  - Naked Pairs
  - Hidden Pairs
- **Backtracking fallback** — handles puzzles that require trial-and-error
- **Multi-threaded batch solving** via `Parallel.For`
- **Bitmask-based candidate tracking** for efficient cell state management
- Included puzzle datasets: 50, 95, 10,000, and 49,151 puzzles

## Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

## Getting Started

### Build

```bash
dotnet build
```

### Run

The solver reads puzzles from a CSV file (`sudoku.csv`) in the working directory. Each row should be `puzzle,solution` with 81-character strings using `0` for empty cells.

```bash
dotnet run --project Sudoku -- <threadCount> <puzzleCount>
```

Defaults to 4 threads and 400,000 puzzles when no arguments are provided.

**Example:**
```bash
dotnet run --project Sudoku -- 8 10000
```

For maximum performance, use a Release build:

```bash
dotnet run -c Release --project Sudoku -- 8 10000
```

### Test

```bash
dotnet test
```

The test suite covers 17 puzzles across varying difficulty levels and ensures each solving technique works correctly in isolation.

## Project Structure

```
Sudoku/
├── Program.cs          # Entry point — parallel batch runner
├── SudokuSolver.cs     # Solving orchestration
├── Board.cs            # 9×9 grid
├── Cell.cs             # Cell state and candidate tracking
├── Region.cs           # Row, column, and 3×3 block logic
├── Bitmask.cs          # Bitwise candidate utilities
├── Puzzle.cs           # Puzzle parsing and formatting
├── Definitions.cs      # Constants and lookup tables
├── Extensions.cs       # Helper extensions
└── Techniques/
    ├── NakedSingle.cs
    ├── HiddenSingle.cs
    ├── NakedPair.cs
    ├── HiddenPair.cs
    └── Backtracking.cs

SudokuTests/
└── PuzzleTests.cs      # 17 named test cases
```

## Puzzle Format

Puzzles are represented as 81-character strings, left-to-right, top-to-bottom. Empty cells are `0`.

```
004300209005009001070060043006002087190007400050083000600000105003508690042910300
```
