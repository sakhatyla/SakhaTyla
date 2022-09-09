using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.TranslateChatBot
{
    internal class ArticleInlineInfo
    {
        public Guid Id { get; set; }
        public ArticleInlineInfoType Type { get; set; }
        public int GroupNumber { get; set; }
        public int ArticleNumber { get; set; }
        public int TotalArticles { get; set; }

        public string GetCode(int articleNumber)
        {
            return Id + ":" +
                   (int)Type + ":" +
                   (Type == ArticleInlineInfoType.Articles ? GroupNumber + "" : "") + ":" +
                   articleNumber;
        }

        public ArticleInlineButton[]? GetButtons()
        {
            if (TotalArticles == 1)
                return null;
            return new[]
            {
                    ArticleNumber == 0
                        ? new ArticleInlineButton(" ", "-")
                        : new ArticleInlineButton("<", GetCode(ArticleNumber - 1)),
                    ArticleNumber == TotalArticles - 1
                        ? new ArticleInlineButton(" ", "-")
                        : new ArticleInlineButton(">", GetCode(ArticleNumber + 1)),
                };
        }

        public static ArticleInlineInfo? ParseCode(string code)
        {
            if (code == null)
                return null;
            var items = code.Split(':');
            if (items.Length != 4)
                return null;
            if (!Guid.TryParse(items[0], out var id))
                return null;
            if (!int.TryParse(items[1], out var type))
                return null;
            if (type > 1)
                return null;
            var group = 0;
            if (!string.IsNullOrEmpty(items[2]))
            {
                if (!int.TryParse(items[2], out group))
                    return null;
            }
            if (!int.TryParse(items[3], out var article))
                return null;
            return new ArticleInlineInfo()
            {
                Id = id,
                Type = (ArticleInlineInfoType)type,
                GroupNumber = group,
                ArticleNumber = article,
            };
        }
    }

    internal enum ArticleInlineInfoType
    {
        Articles,
        MoreArticles
    }

    internal class ArticleInlineButton
    {
        public ArticleInlineButton(string displayText, string code)
        {
            DisplayText = displayText;
            Code = code;
        }

        public string DisplayText { get; set; }
        public string Code { get; set; }
    }
}
