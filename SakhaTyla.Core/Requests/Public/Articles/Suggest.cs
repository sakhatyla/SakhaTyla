﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Requests.Public.Articles.Models;

namespace SakhaTyla.Core.Requests.Public.Articles
{
    public class Suggest : IRequest<List<ArticleSuggestModel>>
    {
        public const int MinLength = 2;

        public string? Query { get; set; }
    }
}
