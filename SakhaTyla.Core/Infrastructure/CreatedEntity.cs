using System;
using System.Collections.Generic;
using System.Text;

namespace SakhaTyla.Core.Infrastructure
{
    public class CreatedEntity<T>
    {
        public CreatedEntity(T id)
        {
            Id = id;
        }

        public T Id { get; set; }
    }
}
