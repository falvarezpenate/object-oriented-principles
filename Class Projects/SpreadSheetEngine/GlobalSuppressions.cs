// <copyright file="GlobalSuppressions.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "Members are supposed to be protected not private.", Scope = "member", Target = "~F:SpreadSheetEngine.SpreadSheet.rowCount")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "Following syntax professor showed in slides.", Scope = "member", Target = "~E:SpreadSheetEngine.Cell.PropertyChanged")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Required to be protected.", Scope = "member", Target = "~F:SpreadSheetEngine.Cell.rowIndex")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Required to be protected.", Scope = "member", Target = "~F:SpreadSheetEngine.Cell.columnIndex")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Required to be protected.", Scope = "member", Target = "~F:SpreadSheetEngine.Cell.text")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Required to be protected.", Scope = "member", Target = "~F:SpreadSheetEngine.Cell.value")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Following professor's syntax.", Scope = "member", Target = "~E:SpreadSheetEngine.SpreadSheet.PropertyChanged")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "Following the syntax the professor showed in slides.", Scope = "member", Target = "~E:SpreadSheetEngine.SpreadSheet.PropertyChanged")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "Following syntax learned in class.", Scope = "member", Target = "~E:SpreadSheetEngine.SpreadSheet.ReferencePropertyChanged")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Members supposed to be protected.", Scope = "member", Target = "~F:SpreadSheetEngine.Cell.bgColor")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Needs to be protected.", Scope = "member", Target = "~F:SpreadSheetEngine.UndoRedoCollection.unexecuteCommand")]
