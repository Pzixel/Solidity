using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Solidity
{
    /// <summary>
    /// Classifier that classifies all text as an instance of the "SolKeyword" classification type.
    /// </summary>
    internal class SolKeyword : IClassifier
    {
        private readonly List<(Regex, IClassificationType)> _map;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolKeyword"/> class.
        /// </summary>
        /// <param name="registry">Classification registry.</param>
        internal SolKeyword(IClassificationTypeRegistryService registry)
        {
            _map = new List<(Regex, IClassificationType)>
            {
                (new Regex(@"\bcontract\b", RegexOptions.Compiled), registry.GetClassificationType("SolKeyword"))
            };
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
                foreach (Match match in tuple.Item1.Matches(text))
                {
                    var str = new SnapshotSpan(line.Snapshot, line.Start.Position + match.Index, match.Length);
                    list.Add(new ClassificationSpan(str, tuple.Item2));
                }
            }

            return list;
        }

        #endregion
    }
}
