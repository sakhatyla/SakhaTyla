using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Indexers;

namespace SakhaTyla.Core.Messaging.Articles
{
    public class UpdateArticleIndex : IRequest
    {
        public static string QueueName => nameof(UpdateArticleIndex);

        public int Id { get; set; }

        public IndexAction Action { get; set; }
    }
}
