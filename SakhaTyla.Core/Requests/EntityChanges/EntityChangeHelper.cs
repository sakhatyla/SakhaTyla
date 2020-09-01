using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.EntityChanges.Models;

namespace SakhaTyla.Core.Requests.EntityChanges
{
    public class EntityChangeHelper
    {
        private readonly IMapper _mapper;
        private readonly Type _entityType;
        private readonly Type _entityModelType;

        public EntityChangeHelper(string entityName, IMapper mapper)
        {
            _entityType = GetEntityType(entityName);
            _entityModelType = GetEntityModelType(entityName);
            _mapper = mapper;
        }

        public IList<EntityPropertyChange> GetPropertyChanges(EntityChangeModel entityChange)
        {
            var fromEntity = JsonSerializer.Deserialize(entityChange.From, _entityType, JsonSerializerHelper.JsonSerializerOptions);
            var toEntity = JsonSerializer.Deserialize(entityChange.To, _entityType, JsonSerializerHelper.JsonSerializerOptions);
            var fromEntityModel = _mapper.Map(fromEntity, _entityType, _entityModelType);
            var toEntityModel = _mapper.Map(toEntity, _entityType, _entityModelType);
            var propertyChanges = new List<EntityPropertyChange>();
            foreach (var property in _entityModelType.GetProperties())
            {
                var fromValue = property.GetValue(fromEntityModel);
                var toValue = property.GetValue(toEntityModel);
                if (!Equal(fromValue, toValue))
                {
                    propertyChanges.Add(new EntityPropertyChange()
                    {
                        Name = property.Name,
                        DisplayName = GetDisplayName(property),
                        FromValue = fromValue,
                        ToValue = toValue,
                    });
                }
            }
            return propertyChanges;
        }

        private bool Equal(object val1, object val2)
        {
            if (val1 != null && val2 != null)
            {
                return val1.Equals(val2);
            }
            else if (val1 == null && val2 == null)
            {
                return true;
            }
            return false;
        }

        private string GetDisplayName(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<DisplayNameAttribute>();
            return attribute?.DisplayName ?? property.Name;
        }

        private Type GetEntityType(string entityName)
        {
            return CoreHelper.GetPlatformAndAppAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(TrackedEntity).IsAssignableFrom(t) && t.Name == entityName)
                .FirstOrDefault();
        }

        private Type GetEntityModelType(string entityName)
        {
            return CoreHelper.GetPlatformAndAppAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.Name == $"{entityName}Model")
                .FirstOrDefault();
        }
    }

    public class EntityPropertyChange
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public object FromValue { get; set; }

        public object ToValue { get; set; }
    }
}
