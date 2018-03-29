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
        private static ClassificationTypeDefinition commentTypeDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Classification.SolString)]
        [BaseDefinition(PredefinedClassificationTypeNames.String)]
        private static ClassificationTypeDefinition stringTypeDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Classification.SolNumber)]
        [BaseDefinition(PredefinedClassificationTypeNames.Number)]
        private static ClassificationTypeDefinition numberTypeDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Classification.SolKeyword)]
        [BaseDefinition(PredefinedClassificationTypeNames.Keyword)]
        private static ClassificationTypeDefinition keywordTypeDefinition;

#pragma warning restore 169
    }
}
