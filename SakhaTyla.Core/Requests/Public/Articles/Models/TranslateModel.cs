using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.Requests.Public.Articles.Models
{
    public class TranslateModel
    {
        public TranslateModel(string query)
        {
            Query = query;
        }

        public string Query { get; set; }

        public List<ArticleGroupModel> Articles { get; set; } = null!;

        public List<ArticleModel>? MoreArticles { get; set; }
    }
}
