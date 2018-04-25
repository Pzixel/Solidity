using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Solidity
{
    /// <summary>
    /// Classifier that classifies all text as an instance of the SolKeyword classification type.
    /// </summary>
    internal class SolKeyword : IClassifier
    {
        private readonly List<(Regex, IClassificationType)> _map;
        private readonly IClassificationType _typeClassification;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolKeyword"/> class.
        /// </summary>
        /// <param name="registry">Classification registry.</param>
        internal SolKeyword(IClassificationTypeRegistryService registry)
        {
            _map = new List<(Regex, IClassificationType)>
            {
                (new Regex(@"/\*.+\*/", RegexOptions.Compiled | RegexOptions.Multiline), registry.GetClassificationType(Classification.SolComment)),
                (new Regex(@"//.+", RegexOptions.Compiled), registry.GetClassificationType(Classification.SolComment)),
                (new Regex(@""".*?""", RegexOptions.Compiled), registry.GetClassificationType(Classification.SolString)),
                (new Regex($@"\b({string.Join("|", VerboseConstant.BuiltinTypes.Concat(VerboseConstant.Keywords))})\b", RegexOptions.Compiled), registry.GetClassificationType(Classification.SolKeyword)),
                (new Regex($@"\b({VerboseConstant.Operators})\b", RegexOptions.Compiled), registry.GetClassificationType(Classification.SolKeyword)),
                (new Regex(@"\b-?(\d+(\.\d*)*)|(\.\d+)", RegexOptions.Compiled), registry.GetClassificationType(Classification.SolNumber)),
            };

            _typeClassification = registry.GetClassificationType(Classification.SolType);
        }

        #region IClassifier

#pragma warning disable 67

        /// <summary>
        /// An event that occurs when the classification of a span of text has changed.
        /// </summary>
        /// <remarks>
        /// This event gets raised if a non-text change would affect the classification in some way,
        /// for example typing /* would cause the classification to change in C# without directly
        /// affecting the span.
        /// </remarks>
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

#pragma warning restore 67

        /// <summary>
        /// Gets all the <see cref="ClassificationSpan"/> objects that intersect with the given range of text.
        /// </summary>
        /// <remarks>
        /// This method scans the given SnapshotSpan for potential matches for this classification.
        /// In this instance, it classifies everything and returns each span as a new ClassificationSpan.
        /// </remarks>
        /// <param name="span">The span currently being classified.</param>
        /// <returns>A list of ClassificationSpans that represent spans identified to be of this classification.</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            IList<ClassificationSpan> list = new List<ClassificationSpan>();
            ITextSnapshotLine line = span.Start.GetContainingLine();
            string text = line.GetText();

            foreach (var tuple in _map)
            {
                AddMatchingHighlighting(tuple.Item1, text, line, list, tuple.Item2);
            }

            string fullFileText = span.Snapshot.GetText();
            var contracts = Regex.Matches(fullFileText, @"(?:contract|struct)\W+([\w_]+)", RegexOptions.Compiled).Cast<Match>().Select(x => x.Groups[1].Value);
            var matchingItems = new Regex($@"\b({string.Join("|", contracts)})\b");
            AddMatchingHighlighting(matchingItems, text, line, list, _typeClassification);

            return list;
        }

        private static void AddMatchingHighlighting(Regex regex, string text, ITextSnapshotLine line, ICollection<ClassificationSpan> list, IClassificationType classificationType)
        {
            foreach (Match match in regex.Matches(text))
            {
                var str = new SnapshotSpan(line.Snapshot, line.Start.Position + match.Index, match.Length);

                if (list.Any(s => s.Span.IntersectsWith(str)))
                    continue;

                list.Add(new ClassificationSpan(str, classificationType));
            }
        }

        #endregion
    }
}
