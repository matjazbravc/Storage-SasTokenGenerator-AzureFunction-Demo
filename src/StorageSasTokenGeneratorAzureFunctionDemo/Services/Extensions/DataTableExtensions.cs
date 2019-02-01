using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Converts to DataTable;
        /// </summary>
        /// <param name="enumerable">The enumerable list.</param>
        /// <param name="columnName">Column name if Type is List of string</param>
        /// <typeparam name="T">Value type</typeparam>
        /// <returns>DataTable which contains data from list</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable, string columnName = "")
        {
            var table = CreateTable<T>(columnName);
            IEnumerable<T> itemArray = enumerable as T[] ?? enumerable.ToArray();
            if (itemArray.IsNullOrEmpty())
            {
                return table;
            }
            table.BeginLoadData();
            if (typeof(T) == Type.GetType("System.String"))
            {
                foreach (var item in itemArray)
                {
                    var itemValue = item as string;
                    if (string.IsNullOrWhiteSpace(itemValue))
                    {
                        continue;
                    }
                    var row = table.NewRow();
                    row.BeginEdit();
                    row[columnName] = item;
                    row.EndEdit();
                    table.Rows.Add(row);
                }
            }
            else
            {
                var properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (var item in itemArray)
                {
                    var row = table.NewRow();
                    row.BeginEdit();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    row.EndEdit();
                    table.Rows.Add(row);
                }
            }
            table.EndLoadData();
            return table;
        }

        /// <summary>
        /// Converts to IList.
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="table">The table.</param>
        /// <returns>A list which contains data from DataTable</returns>
        public static IList<T> ToList<T>(this DataTable table)
        {
            if (table == null)
            {
                return null;
            }
            var rows = table.Rows.Cast<DataRow>().ToList();
            return ToList<T>(rows);
        }

        /// <summary>
        /// Creates the item.
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="row">The data row.</param>
        /// <returns>A item which containd data from DataRow</returns>
        private static T CreateItem<T>(DataRow row)
        {
            var obj = default(T);
            if (row == null)
            {
                return obj;
            }

            var entityType = typeof(T);
            if (entityType.FullName == "System.String")
            {
                obj = (T)row[0];
            }
            else
            {
                obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    var prop = obj.GetType().GetProperty(column.ColumnName);
                    {
                        var value = row[column.ColumnName];
                        prop?.SetValue(obj, value, null);
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// Creates the table.
        /// </summary>
        /// <param name="columnName">Column name</param>
        /// <typeparam name="T">Value type</typeparam>
        /// <returns>DataTable with desired structure</returns>
        private static DataTable CreateTable<T>(string columnName = "Column")
        {
            var entityType = typeof(T);
            var table = new DataTable();
            if (entityType == Type.GetType("System.String"))
            {
                table.Columns.Add(columnName, typeof(string));
            }
            else
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                for (var i = 0; i < properties.Length; i++)
                {
                    table.Columns.Add(properties[i].Name, Nullable.GetUnderlyingType(properties[i].PropertyType) ?? properties[i].PropertyType);
                    table.Columns[i].AllowDBNull = true;
                }
            }
            return table;
        }

        /// <summary>
        /// Converts to IList.
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="rows">The data rows.</param>
        /// <returns>A List</returns>
        private static IList<T> ToList<T>(IList<DataRow> rows)
        {
            return rows?.Select(CreateItem<T>).ToList();
        }
    }
}