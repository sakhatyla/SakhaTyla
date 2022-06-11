using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Migration.SourceDatabase;
using SakhaTyla.Core.Requests.Categories;

namespace SakhaTyla.Migration.Migrations
{
    class CategoryMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;

        public CategoryMigration(SourceLoader sourceLoader, IMediator mediator)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
        }

        public async Task MigrateCategories()
        {
            var categories = await _sourceLoader.GetCategoriesAsync();
            foreach (var category in categories)
            {
                var createCategory = new CreateCategory()
                {
                    Name = category.Name,
                };
                await _mediator.Send(createCategory);
            }
        }
    }
}
