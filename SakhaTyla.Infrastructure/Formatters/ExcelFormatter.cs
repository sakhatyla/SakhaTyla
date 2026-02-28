using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using SakhaTyla.Core;
using SakhaTyla.Core.Formatters;

namespace SakhaTyla.Infrastructure.Formatters
{
    public class ExcelFormatter : IExcelFormatter
    {
        const string DateFormat = "DD.MM.YYYY";
        const string DateTimeFormat = "DD.MM.YYYY HH:mm";
        const string TimeFormat = "HH:mm";
        private readonly IStringLocalizer<SharedResource> _localizer;
        private static readonly MethodInfo _excelRangeGetValueMethod = GetExcelRangeGetValueMethod();

        public ExcelFormatter(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public Task<IEnumerable<T>> LoadFromAsync<T>(Stream stream, bool withHeader) where T : new()
        {
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.First(w => w.Name == "Data");
            var properties = GetProperties<T>();
            IEnumerable<T> data = ReadCollection<T>(worksheet.Cells[worksheet.Dimension.Address], properties, withHeader).ToList();
            return Task.FromResult(data);
        }

        public async Task SaveToAsync<T>(Stream stream, IEnumerable<T> data, bool withHeader)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Data");
            var properties = GetProperties<T>();
            LoadFromCollection(worksheet.Cells, data, properties, withHeader);
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            await package.SaveAsAsync(stream);
        }

        private void LoadFromCollection<T>(ExcelRange cells, IEnumerable<T> data, IList<PropertyMetaData> properties, bool withHeader)
        {
            var i = 1;
            if (withHeader)
            {
                var j = 1;
                foreach (var property in properties)
                {
                    cells[i, j].Value = property.DisplayName;
                    j++;
                }
                cells[i, 1, i, j - 1].Style.Font.Bold = true;
                i++;
            }
            var dataList = data?.ToList();
            if (dataList != null && dataList.Any())
            {
                int j;
                var start = i;
                foreach (var row in dataList)
                {
                    j = 1;
                    foreach (var property in properties)
                    {
                        var value = property.Property.GetValue(row);
                        SetValue(cells[i, j], value);
                        j++;
                    }
                    i++;
                }
                var end = i - 1;

                j = 1;
                foreach (var property in properties)
                {
                    if (!string.IsNullOrEmpty(property.Format))
                    {
                        cells[start, j, end, j].Style.Numberformat.Format = property.Format;
                    }
                    j++;
                }
            }
        }

        private IEnumerable<T> ReadCollection<T>(ExcelRange cells, IList<PropertyMetaData> properties, bool withHeader) where T : new()
        {
            static object ObjectInit() => new T();
            return ReadCollection(cells, properties, withHeader, ObjectInit).Cast<T>();
        }

        private IEnumerable<object> ReadCollection(ExcelRange cells, IList<PropertyMetaData> properties, bool withHeader, Func<object> objectInit)
        {
            var columns = cells.Columns;
            var rows = cells.Rows;
            var propertyMap = new Dictionary<int, PropertyMetaData?>();
            if (withHeader)
            {
                for (var j = 1; j <= columns; j++)
                {
                    var propertyName = cells[1, j].GetValue<string>();
                    var property = properties.FirstOrDefault(p => p.DisplayName == propertyName);
                    propertyMap[j] = property;
                }
            }
            else
            {
                var j = 1;
                foreach (var property in properties)
                {
                    propertyMap[j++] = property;
                }
            }
            for (var i = 2; i <= rows; i++)
            {
                var row = objectInit();
                for (var j = 1; j <= columns; j++)
                {
                    var property = propertyMap[j];
                    if (property == null)
                    {
                        continue;
                    }
                    var value = GetValue(cells[i, j], property.Property.PropertyType);
                    property.Property.SetValue(row, value);
                }
                yield return row;
            }
        }

        private void SetValue(ExcelRange range, object? value)
        {
            if (value is Enum enumValue)
            {
                var description = enumValue.GetType()
                    .GetMember(enumValue.ToString())
                    .First()
                    .GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
                if (!string.IsNullOrEmpty(description))
                {
                    range.Value = _localizer[description];
                }
                else
                {
                    range.Value = value;
                }
            }
            else
            {
                range.Value = value;
            }
        }

        private static MethodInfo GetExcelRangeGetValueMethod()
        {
            return typeof(ExcelRange)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .First(m => m.Name == "GetValue" && m.GetGenericArguments().Length == 1);
        }

        private object? GetValue(ExcelRange range, Type type)
        {
            if (range.Value == null)
                return null;
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type.IsEnum)
            {
                var str = range.GetValue<string>();
                var enumValues = type.GetMembers()
                    .Select(m => new { m.Name, m.GetCustomAttribute<DescriptionAttribute>()?.Description });
                return enumValues.Where(v => v.Description == str)
                    .Select(v => Enum.Parse(type, v.Name))
                    .FirstOrDefault();
            }
            else
            {
                return _excelRangeGetValueMethod
                    .MakeGenericMethod(type)
                    .Invoke(range, null);
            }
        }

        private IList<PropertyMetaData> GetProperties<T>()
        {
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => new PropertyMetaData(p))
                .ToList();
            foreach (var property in properties)
            {
                var displayNameAttribute = property.Property.GetCustomAttribute<DisplayNameAttribute>();
                if (displayNameAttribute != null)
                    property.DisplayName = _localizer[displayNameAttribute.DisplayName];
                else
                    property.DisplayName = property.Property.Name;

                if (property.Property.PropertyType == typeof(DateTime)
                    || Nullable.GetUnderlyingType(property.Property.PropertyType) == typeof(DateTime))
                {
                    var format = DateTimeFormat;
                    var dataTypeAttribute = property.Property.GetCustomAttribute<DataTypeAttribute>();
                    if (dataTypeAttribute != null && dataTypeAttribute.DataType == DataType.Date)
                        format = DateFormat;
                    property.Format = format;
                }
                else if (property.Property.PropertyType == typeof(TimeSpan)
                    || Nullable.GetUnderlyingType(property.Property.PropertyType) == typeof(TimeSpan))
                {
                    property.Format = TimeFormat;
                }
            }
            return properties;
        }

        class PropertyMetaData
        {
            public PropertyMetaData(PropertyInfo property)
            {
                Property = property;
            }

            public PropertyInfo Property { get; set; }
            public string? DisplayName { get; set; }
            public string? Format { get; set; }
        }
    }
}
