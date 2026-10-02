// <copyright file="UndoRedoCollection.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetEngine
{
    /// <summary>
    /// Interface for redo/undo implementation.
    /// </summary>
    public abstract class UndoRedoCollection
    {
        /// <summary>
        /// Executes a command that either can undo or redo the last change made.
        /// </summary>
        public abstract void ExecuteCommand();
    }
}
