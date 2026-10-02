# Spreadsheet Project

This repository contains coursework for an object-oriented programming class, and the main project is a spreadsheet application built in C# using Windows Forms. The project demonstrates core object-oriented design principles, formula evaluation, event-driven behavior, and undo/redo functionality.

## Overview

The spreadsheet application is split between:
- SpreadSheetEngine: the spreadsheet logic and formula engine
- Spreadsheet_Flavio_Alvarez: the Windows Forms interface that lets users interact with the spreadsheet

The project models a spreadsheet grid with cell-based data, formula support, dependency tracking, color formatting, and XML save/load capabilities.

## Features

- 50x26 spreadsheet grid
- Cell text input and display
- Formula support using spreadsheet-style references, such as:
  - =A1 + B1
  - =C3 * 2
  - =SUM? (implemented through expression parsing patterns in the engine)
- Automatic recalculation when referenced cells change
- Circular reference detection
- Cell background color changes
- Undo/redo support for text and color edits
- XML-based save/load of spreadsheet state

## Project Structure

```text
Class Projects/
├── BlankSolution.sln
├── SpreadSheetEngine/
│   ├── Cell.cs
│   ├── SpreadSheet.cs
│   ├── ExpressionTree.cs
│   ├── ShuntingYard.cs
│   ├── OperatorNode.cs
│   ├── VariableNode.cs
│   ├── ConstantNode.cs
│   ├── UndoRedoCellText.cs
│   ├── UndoRedoCellColor.cs
│   └── ...
├── Spreadsheet_Flavio_Alvarez/
│   ├── Form1.cs
│   ├── Form1.Designer.cs
│   ├── Program.cs
│   └── Spreadsheet_Flavio_Alvarez.csproj
├── SpreadsheetTests/
└── ...
```

## Spreadsheet Engine

The core logic lives in `Class Projects/SpreadSheetEngine`. It provides the spreadsheet model and formula evaluation infrastructure.

Key responsibilities of the engine include:
- Managing a 2D grid of cells
- Storing cell text and evaluated values
- Parsing expressions and building an expression tree
- Resolving cell references
- Recalculating dependent cells when source values change
- Detecting invalid formulas and circular references
- Tracking undo/redo stacks

## User Interface

The interactive spreadsheet front end is in `Class Projects/Spreadsheet_Flavio_Alvarez`. It uses a `DataGridView` to display the spreadsheet and supports:
- editing cells
- showing evaluated cell values
- applying cell colors
- loading and saving spreadsheet files
- using undo/redo commands from the menu

## How It Works

Users type values or formulas into cells. If a cell contains a formula beginning with `=`, the engine evaluates it using the referenced cells. When a referenced cell updates, dependent cells are refreshed automatically.

Examples:
- `=B2` copies the value of cell B2
- `=A1 + A2` calculates the sum of two cells
- `=A3 * 5` multiplies a cell value by 5

If a formula references a cell that is missing, invalid, or creates a circular dependency, the spreadsheet displays an error message instead of evaluating incorrectly.

## Getting Started

1. Open `Class Projects/BlankSolution.sln` in Visual Studio.
2. Set the spreadsheet project as the startup project if needed.
3. Build the solution.
4. Run the application.

## Requirements

- Windows
- Visual Studio
- .NET Framework / .NET-compatible C# project setup

## Notes

This project is part of an academic object-oriented programming course, and it is designed to explore:
- encapsulation
- event-driven programming
- inheritance and polymorphism
- data structures
- expression parsing
- dependency management

## Author

Flavio Raul Alvarez Penate
