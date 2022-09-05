using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.Requests.Public.Articles.Models
{
    public class ArticleGroupModel
    {
        public ArticleGroupModel()
        {
        }

        public Languages.Models.LanguageShortModel FromLanguage { get; set; } = null!;

        public Languages.Models.LanguageShortModel ToLanguage { get; set; } = null!;

        public List<ArticleModel> Articles { get; set; } = null!;
    }
}
