using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SakhaTyla.Core.Infrastructure
{
    public static class MapperExtensions
    {
        public static void UpdateCollection<TSource, TDestination, TId>(this IMapperBase mapper,
            ICollection<TSource>? sourceCollection,
            ICollection<TDestination> destinationCollection,
            Func<TSource, TId> sourceIdFunc,
            Func<TDestination, TId> destinationIdFunc)
        {
            if (sourceCollection == null)
            {
                return;
            }
            var comparer = EqualityComparer<TId>.Default;
            destinationCollection.Where(d => sourceCollection.All(s => !comparer.Equals(sourceIdFunc(s), destinationIdFunc(d))))
                .ToList()
                .ForEach(d => destinationCollection.Remove(d));

            foreach (var sourceItem in sourceCollection)
            {
                var destinationItem = destinationCollection.FirstOrDefault(d => comparer.Equals(destinationIdFunc(d), sourceIdFunc(sourceItem)));
                if (destinationItem != null)
                {
                    mapper.Map(sourceItem, destinationItem);
                }
                else
                {
                    destinationItem = mapper.Map<TDestination>(sourceItem);
                    destinationCollection.Add(destinationItem);
                }
            }
        }
    }
}
