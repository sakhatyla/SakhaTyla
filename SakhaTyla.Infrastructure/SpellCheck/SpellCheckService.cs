using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using SakhaTyla.Core.SpellCheck;
using SakhaTyla.Infrastructure.Search;
using WeCantSpell.Hunspell;

namespace SakhaTyla.Infrastructure.SpellCheck
{
    public class SpellCheckService : ISpellCheckService
    {
        private readonly LuceneOptions _options;
        private readonly ConcurrentDictionary<string, WordList> _dictionaries = new();

        private static readonly Dictionary<string, Dictionary<char, char>> ApplyMaps = new()
        {
            ["sah"] = new Dictionary<char, char>
            {
                ['5'] = 'ҕ',
                ['8'] = 'ө',
                ['е'] = 'ө',
                ['h'] = 'һ',
                ['ц'] = 'ҥ',
            }
        };

        private static readonly Dictionary<string, Dictionary<char, char>> FullMaps = new()
        {
            ["sah"] = new Dictionary<char, char>
            {
                ['5'] = 'ҕ',
                ['8'] = 'ө',
                ['е'] = 'ө',
                ['h'] = 'һ',
                ['ь'] = 'һ',
                ['у'] = 'ү',
                ['н'] = 'ҥ',
                ['ц'] = 'ҥ',
            }
        };

        public SpellCheckService(IOptions<LuceneOptions> options)
        {
            _options = options.Value;
        }

        private WordList GetDictionary(string language)
        {
            return _dictionaries.GetOrAdd(language, lang =>
            {
                var affPath = Path.Combine(_options.HunspellPath!, lang + ".aff");
                var dicPath = Path.Combine(_options.HunspellPath!, lang + ".dic");
                return WordList.CreateFromFiles(dicPath, affPath);
            });
        }

        public string FixSpelling(string language, string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var dictionary = GetDictionary(language);

            if (!FullMaps.TryGetValue(language, out var fullMap))
                return text;

            ApplyMaps.TryGetValue(language, out var applyMap);

            var extraMap = new Dictionary<char, char>();
            foreach (var kvp in fullMap)
            {
                if (applyMap == null || !applyMap.ContainsKey(kvp.Key))
                    extraMap[kvp.Key] = kvp.Value;
            }

            return Regex.Replace(text, @"[\p{L}\p{N}]+", match =>
            {
                var word = match.Value;
                if (dictionary.Check(word))
                    return word;

                if (!ContainsMapChar(word, fullMap))
                    return word;

                // Step 1: check suggestions of original word
                var suggestions = dictionary.Suggest(word);
                foreach (var suggestion in suggestions)
                {
                    if (suggestion.Length == word.Length && CanMapTo(word, suggestion, fullMap))
                        return suggestion;
                }

                // Step 2: generate all mapped variants, check each + its suggestions
                var variants = GenerateVariants(word, applyMap, extraMap);
                foreach (var variant in variants)
                {
                    if (variant == word)
                        continue;

                    if (dictionary.Check(variant))
                        return variant;

                    var variantSuggestions = dictionary.Suggest(variant);
                    foreach (var suggestion in variantSuggestions)
                    {
                        if (suggestion.Length == word.Length && CanMapTo(word, suggestion, fullMap))
                            return suggestion;
                    }
                }

                return word;
            });
        }

        private static List<string> GenerateVariants(string word, Dictionary<char, char>? applyMap, Dictionary<char, char> extraMap)
        {
            // Apply all level 1 mappings to get the base word
            var baseChars = new char[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                var ch = word[i];
                var lowerCh = char.ToLowerInvariant(ch);
                if (applyMap != null && applyMap.TryGetValue(lowerCh, out var replacement))
                    baseChars[i] = char.IsUpper(ch) ? char.ToUpperInvariant(replacement) : replacement;
                else
                    baseChars[i] = ch;
            }

            // Find positions where level 2 chars can be applied
            var extraPositions = new List<int>();
            for (int i = 0; i < baseChars.Length; i++)
            {
                if (extraMap.ContainsKey(char.ToLowerInvariant(baseChars[i])))
                    extraPositions.Add(i);
            }

            // Generate all combinations of level 2 substitutions
            var variants = new List<string>();
            int combinationCount = 1 << extraPositions.Count;
            for (int mask = 0; mask < combinationCount; mask++)
            {
                var variantChars = (char[])baseChars.Clone();
                for (int j = 0; j < extraPositions.Count; j++)
                {
                    if ((mask & (1 << j)) != 0)
                    {
                        var pos = extraPositions[j];
                        var ch = variantChars[pos];
                        var lowerCh = char.ToLowerInvariant(ch);
                        if (extraMap.TryGetValue(lowerCh, out var replacement))
                            variantChars[pos] = char.IsUpper(ch) ? char.ToUpperInvariant(replacement) : replacement;
                    }
                }
                variants.Add(new string(variantChars));
            }

            return variants;
        }

        private static bool ContainsMapChar(string word, Dictionary<char, char> charMap)
        {
            foreach (var ch in word)
            {
                if (charMap.ContainsKey(char.ToLowerInvariant(ch)))
                    return true;
            }
            return false;
        }

        private static bool CanMapTo(string original, string suggestion, Dictionary<char, char> charMap)
        {
            for (int i = 0; i < original.Length; i++)
            {
                var origChar = original[i];
                var sugChar = suggestion[i];

                if (origChar == sugChar)
                    continue;

                var origLower = char.ToLowerInvariant(origChar);
                if (charMap.TryGetValue(origLower, out var mapped))
                {
                    var expectedChar = char.IsUpper(origChar) ? char.ToUpperInvariant(mapped) : mapped;
                    if (sugChar == expectedChar)
                        continue;
                }

                return false;
            }
            return true;
        }

    }
}
