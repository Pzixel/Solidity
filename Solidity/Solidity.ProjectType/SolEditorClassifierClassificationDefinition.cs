using System.ComponentModel.Composition;
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
            
        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Classification.SolComment)]
        [BaseDefinition(PredefinedClassificationTypeNames.Comment)]
        private static ClassificationTypeDefinition _commentTypeDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Classification.SolString)]
        [BaseDefinition(PredefinedClassificationTypeNames.String)]
        private static ClassificationTypeDefinition _stringTypeDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Classification.SolNumber)]
        [BaseDefinition(PredefinedClassificationTypeNames.Number)]
        private static ClassificationTypeDefinition _numberTypeDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Classification.SolKeyword)]
        [BaseDefinition(PredefinedClassificationTypeNames.Keyword)]
        private static ClassificationTypeDefinition _keywordTypeDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Classification.SolType)]
        [BaseDefinition(PredefinedClassificationTypeNames.Type)]
        private static ClassificationTypeDefinition _typeTypeDefinition;

#pragma warning restore 169
    }
}
