﻿using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Solidity
{
    /// <summary>
    /// Classification type definition export for SolEditorClassifier
    /// </summary>
    internal static class SolEditorClassifierClassificationDefinition
    {
        // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169

        /// <summary>
        /// Defines the "SolEditorClassifier" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Classification.SolKeyword)]
        [BaseDefinition(PredefinedClassificationTypeNames.Keyword)]
        private static ClassificationTypeDefinition typeDefinition;

#pragma warning restore 169
    }
}
