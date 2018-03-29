using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Solidity
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Classification.SolKeyword)]
    [Name(Classification.SolKeyword)]
    [DisplayName("Solidity keyword")]
    [UserVisible(true)]
    internal sealed class SolKeywordDefinition : ClassificationFormatDefinition
    {

    }
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Classification.SolComment)]
    [Name(Classification.SolComment)]
    [DisplayName("Solidity comment")]
    [UserVisible(true)]
    internal sealed class SolCommentDefinition : ClassificationFormatDefinition
    {
    }
}
