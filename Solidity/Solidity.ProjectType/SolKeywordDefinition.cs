using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Solidity
{
    /// <summary>
    /// Defines an editor format for the SolKeyword type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Classification.SolKeyword)]
    [Name("SolKeyword")]
    [DisplayName("Solidity keyword")]
    [UserVisible(true)]
    internal sealed class SolKeywordDefinition : ClassificationFormatDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolKeywordDefinition"/> class.
        /// </summary>
        public SolKeywordDefinition()
        {
            this.DisplayName = "SolKeyword"; // Human readable version of the name
            this.ForegroundColor = Colors.CornflowerBlue;
        }
    }
}
