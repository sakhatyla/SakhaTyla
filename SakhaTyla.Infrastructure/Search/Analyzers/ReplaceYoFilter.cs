using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;

namespace SakhaTyla.Infrastructure.Search.Analyzers
{
    internal sealed class ReplaceYoFilter : TokenFilter
    {
        private readonly ICharTermAttribute _termAtt;

        public ReplaceYoFilter(TokenStream input) : base(input)
        {
            _termAtt = AddAttribute<ICharTermAttribute>();
        }

        public override bool IncrementToken()
        {
            if (!m_input.IncrementToken())
                return false;
            var chArray = _termAtt.Buffer;
            var num = _termAtt.Length;
            for (var index = 0; index < num; ++index)
            {
                if (chArray[index] == 'ё')
                    chArray[index] = 'е';
            }
            return true;
        }
    }
}
