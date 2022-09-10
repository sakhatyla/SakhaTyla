using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SakhaTyla.Core.ChatBots;
using SakhaTyla.Core.Requests.Public.Articles;
using SakhaTyla.Core.Requests.Public.Articles.Models;

namespace SakhaTyla.Core.TranslateChatBot
{
    public class TranslateChatBotMessageHandler : IChatBotMessageHandler
    {
        private readonly IChatBotMessageSender _chatBotMessageSender;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        private const string SadFace = "\uD83D\uDE1E";
        private const string FlushedFace = "\uD83D\uDE33";
        private const string RandomButtonText = "\uD83C\uDFB2 Случайное слово";
        private const int MaxCacheSize = 10;
        private const int MaxQuerySize = 250;

        public TranslateChatBotMessageHandler(IChatBotMessageSender chatBotMessageSender,
            IMediator mediator,
            IMemoryCache cache)
        {
            _chatBotMessageSender = chatBotMessageSender;
            _mediator = mediator;
            _cache = cache;
        }

        public async Task ProcessMessage(ChatBotMessage message, CancellationToken cancellationToken)
        {
            var text = message.Text;

            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            if (text == "/start")
            {
                await _chatBotMessageSender.SendMessage(message.Chat.Id, "Чтобы получить перевод слов, просто напишите их мне");
            }
            else if (text == RandomButtonText)
            {
                await SendRandomArticle(message.Chat.Id);
            }
            else
            {
                if (!message.Chat.Private)
                {
                    text = Regex.Replace(text, "^@[^\\s]+", "");
                }
                await Translate(message.Chat.Id, message.Chat.Private, text);
            }            
        }

        public async Task ProcessCallbackQuery(ChatBotCallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            if (callbackQuery.Message != null && !string.IsNullOrEmpty(callbackQuery.Data))
            {
                var info = ArticleInlineInfo.ParseCode(callbackQuery.Data);
                if (info != null)
                {
                    var articleTranslate = GetTranslateFromCache(info.Id);
                    if (articleTranslate != null)
                    {
                        if (info.Type == ArticleInlineInfoType.Articles)
                        {
                            var group = articleTranslate.Articles.Skip(info.GroupNumber).FirstOrDefault();
                            if (@group != null)
                            {
                                var articles = @group.Articles.ToList();
                                var article = articles.Skip(info.ArticleNumber).FirstOrDefault();
                                if (article != null)
                                {
                                    info.TotalArticles = articles.Count;
                                    await EditArticle(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId, article, info);
                                }
                            }                            
                        }
                        else if (info.Type == ArticleInlineInfoType.MoreArticles)
                        {
                            if (articleTranslate.MoreArticles != null)
                            {
                                var article = articleTranslate.MoreArticles.Skip(info.ArticleNumber).FirstOrDefault();
                                if (article != null)
                                {
                                    info.TotalArticles = articleTranslate.MoreArticles.Count;
                                    await EditArticle(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId, article, info);
                                }
                            }   
                        }
                    }
                }
            }            
            await _chatBotMessageSender.AnswerCallbackQuery(callbackQuery.Id);
        }

        private async Task SendRandomArticle(string chatId)
        {
            var article = await _mediator.Send(new GetRandomArticle());
            await SendArticle(chatId, article);
        }

        private async Task SendArticle(string chatId, ArticleModel article, ArticleInlineInfo? articleInlineInfo = null)
        {
            await _chatBotMessageSender.SendMessage(chatId, article.GetPreparedText(), html: true, replyButtons: GetReplyButtons(articleInlineInfo));
        }

        private async Task EditArticle(string chatId, string messageId, ArticleModel article, ArticleInlineInfo? articleInlineInfo = null)
        {
            await _chatBotMessageSender.EditMessage(chatId, messageId, article.GetPreparedText(), html: true, replyButtons: GetReplyButtons(articleInlineInfo));
        }

        private ReplyButton[]? GetReplyButtons(ArticleInlineInfo? articleInlineInfo)
        {
            if (articleInlineInfo == null)
            {
                return null;
            }
            var buttons = articleInlineInfo.GetButtons();
            if (buttons != null)
            {
                return buttons
                    .Select(b => new ReplyButton(b.DisplayText, b.Code))
                    .ToArray();
            }
            return null;
        }

        private async Task Translate(string chatId, bool @private, string query)
        {
            if (query.Length > MaxQuerySize)
            {
                await _chatBotMessageSender.SendMessage(chatId, $"Ой, слишком длинный запрос! {FlushedFace}");
                return;
            }
            var result = await _mediator.Send(new Translate()
            {
                Query = query,
            });
            if (result.Articles.Count > 0)
            {
                var id = CacheTranslate(result);
                var groupIndex = 0;
                foreach (var articleGroup in result.Articles)
                {
                    var articles = articleGroup.Articles.ToList();
                    var inlineInfo = @private ? new ArticleInlineInfo()
                    {
                        Id = id,
                        Type = ArticleInlineInfoType.Articles,
                        GroupNumber = groupIndex,
                        ArticleNumber = 0,
                        TotalArticles = articles.Count,
                    } : null;
                    await SendArticle(chatId, articles.First(), inlineInfo);
                    groupIndex++;
                }
            }
            else if (result.MoreArticles != null && result.MoreArticles.Count > 0)
            {
                var id = CacheTranslate(result);
                var inlineInfo = @private ? new ArticleInlineInfo()
                {
                    Id = id,
                    Type = ArticleInlineInfoType.MoreArticles,
                    ArticleNumber = 0,
                    TotalArticles = result.MoreArticles.Count,
                } : null;
                await SendArticle(chatId, result.MoreArticles.First(), inlineInfo);
            }
            else
            {
                await _chatBotMessageSender.SendMessage(chatId, $"Ничего не нашел {SadFace}");
            }
        }

        private Guid CacheTranslate(TranslateModel translate)
        {
            var id = Guid.NewGuid();
            _cache.Set(id, translate, TimeSpan.FromHours(1));
            return id;
        }

        private TranslateModel? GetTranslateFromCache(Guid id)
        {
            if (_cache.TryGetValue(id, out TranslateModel value))
            {
                return value;
            }
            return null;
        }
    }
}
