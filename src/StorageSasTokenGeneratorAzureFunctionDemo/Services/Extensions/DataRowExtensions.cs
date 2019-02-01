using System;
using System.Data;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Extensions
{
    /// <summary>
    ///     Extension methods for ADO.NET DataRows (DataTable / DataSet)
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        ///     Gets the record value casted as bool or the specified default value.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <param name="defaultValue"> The default value. </param>
        /// <returns> The record value </returns>
        public static bool GetBoolean(this DataRow row, string columnName, bool defaultValue)
        {
            var value = row[columnName];
            return value as bool? ?? defaultValue;
        }

        /// <summary>
        ///     Gets the record value casted as bool or false.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static bool GetBoolean(this DataRow row, string columnName)
        {
            return GetBoolean(row, columnName, false);
        }

        /// <summary>
        ///     Gets the record value casted as byte array.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static byte[] GetBytes(this DataRow row, string columnName)
        {
            return row != null ? row[columnName] as byte[] : new byte[] { };
        }

        /// <summary>
        ///     Gets the record value casted as DateTime or the specified default value.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <param name="defaultValue"> The default value. </param>
        /// <returns> The record value </returns>
        public static DateTime GetDateTime(this DataRow row, string columnName, DateTime defaultValue)
        {
            var value = row[columnName];
            return value as DateTime? ?? defaultValue;
        }

        /// <summary>
        ///     Gets the record value casted as DateTime or DateTime.MinValue.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static DateTime GetDateTime(this DataRow row, string columnName)
        {
            return GetDateTime(row, columnName, DateTime.MinValue);
        }

        /// <summary>
        ///     Gets the record value casted as DateTimeOffset (UTC) or the specified default value.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <param name="defaultValue"> The default value. </param>
        /// <returns> The record value </returns>
        public static DateTimeOffset GetDateTimeOffset(this DataRow row, string columnName, DateTimeOffset defaultValue)
        {
            var datetime = row.GetDateTime(columnName);
            return datetime != DateTime.MinValue ? new DateTimeOffset(datetime, TimeSpan.Zero) : defaultValue;
        }

        /// <summary>
        ///     Gets the record value casted as DateTimeOffset (UTC) or DateTime.MinValue.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static DateTimeOffset GetDateTimeOffset(this DataRow row, string columnName)
        {
            return new DateTimeOffset(GetDateTime(row, columnName), TimeSpan.Zero);
        }

        /// <summary>
        ///     Gets the record value casted as decimal or the specified default value.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <param name="defaultValue"> The default value. </param>
        /// <returns> The record value </returns>
        public static decimal GetDecimal(this DataRow row, string columnName, long defaultValue)
        {
            var value = row[columnName];
            return value as decimal? ?? defaultValue;
        }

        /// <summary>
        ///     Gets the record value casted as decimal or 0.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static decimal GetDecimal(this DataRow row, string columnName)
        {
            return GetDecimal(row, columnName, 0);
        }

        /// <summary>
        ///     Gets the record value casted as Guid or Guid.Empty.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static Guid GetGuid(this DataRow row, string columnName)
        {
            var value = row[columnName];
            return value as Guid? ?? Guid.Empty;
        }

        /// <summary>
        ///     Gets the record value casted as int or the specified default value.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <param name="defaultValue"> The default value. </param>
        /// <returns> The record value </returns>
        public static int GetInt32(this DataRow row, string columnName, int defaultValue)
        {
            if (row.IsNull(columnName))
            {
                return defaultValue;
            }
            int convertedValue;
            var result = int.TryParse(row[columnName].ToString(), out convertedValue) ? convertedValue : defaultValue;
            return result;
        }

        /// <summary>
        ///     Gets the record value casted as int or 0.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static int GetInt32(this DataRow row, string columnName)
        {
            return GetInt32(row, columnName, 0);
        }

        /// <summary>
        ///     Gets the record value casted as long or the specified default value.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <param name="defaultValue"> The default value. </param>
        /// <returns> The record value </returns>
        public static long GetInt64(this DataRow row, string columnName, int defaultValue)
        {
            var value = row[columnName];
            return value as long? ?? defaultValue;
        }

        /// <summary>
        ///     Gets the record value casted as long or 0.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static long GetInt64(this DataRow row, string columnName)
        {
            return GetInt64(row, columnName, 0);
        }

        /// <summary>
        ///     Gets the record value casted as string or the specified default value.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <param name="defaultValue"> The default value. </param>
        /// <returns> The record value </returns>
        public static string GetString(this DataRow row, string columnName, string defaultValue)
        {
            var value = row[columnName];
            return value as string ?? defaultValue;
        }

        /// <summary>
        ///     Gets the record value casted as string or null.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static string GetString(this DataRow row, string columnName)
        {
            return GetString(row, columnName, string.Empty);
        }

        /// <summary>
        ///     Gets the record value as Type class instance or the specified default value.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <param name="defaultValue"> The default value. </param>
        /// <returns> The record value </returns>
        public static Type GetType(this DataRow row, string columnName, Type defaultValue)
        {
            var classType = row.GetString(columnName);
            if (string.IsNullOrEmpty(classType))
            {
                return defaultValue;
            }
            var type = Type.GetType(classType);
            return type ?? defaultValue;
        }

        /// <summary>
        ///     Gets the record value as Type class instance or null.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static Type GetType(this DataRow row, string columnName)
        {
            return GetType(row, columnName, null);
        }

        /// <summary>
        ///     Gets the record value as class instance from a type name or the specified default type.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <param name="defaultValue"> The default value. </param>
        /// <returns> The record value </returns>
        public static object GetTypeInstance(this DataRow row, string columnName, Type defaultValue)
        {
            var type = row.GetType(columnName, defaultValue);
            return type != null ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        ///     Gets the record value as class instance from a type name or null.
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static object GetTypeInstance(this DataRow row, string columnName)
        {
            return GetTypeInstance(row, columnName, null);
        }

        /// <summary>
        ///     Gets the record value as class instance from a type name or null.
        /// </summary>
        /// <typeparam name="T"> The type to be casted to </typeparam>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static T GetTypeInstance<T>(this DataRow row, string columnName) where T : class
        {
            return GetTypeInstance(row, columnName, null) as T;
        }

        /// <summary>
        ///     Gets the record value as class instance from a type name or the specified default type.
        /// </summary>
        /// <typeparam name="T"> The type to be casted to </typeparam>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <param name="type"> The type. </param>
        /// <returns> The record value </returns>
        public static T GetTypeInstanceSafe<T>(this DataRow row, string columnName, Type type) where T : class
        {
            var instance = row.GetTypeInstance(columnName, null) as T;
            return instance ?? Activator.CreateInstance(type) as T;
        }

        /// <summary>
        ///     Gets the record value as class instance from a type name or an instance from the specified type.
        /// </summary>
        /// <typeparam name="T"> The type to be casted to </typeparam>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> The record value </returns>
        public static T GetTypeInstanceSafe<T>(this DataRow row, string columnName) where T : class, new()
        {
            var instance = row.GetTypeInstance(columnName, null) as T;
            return instance ?? new T();
        }

        /// <summary>
        ///     Gets the record value casted to the specified data type or the specified default value.
        /// </summary>
        /// <typeparam name="T"> The return data type </typeparam>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the column. </param>
        /// <returns> The record value </returns>
        public static T? GetValue<T>(this DataRow row, string columnName) where T : struct
        {
            return GetValue(row, columnName, default(T));
        }

        /// <summary>
        ///     Gets the record value casted to the specified data type or the specified default value.
        /// </summary>
        /// <typeparam name="T"> The return data type </typeparam>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the column. </param>
        /// <param name="defaultValue"> The default value. </param>
        /// <returns> The record value </returns>
        public static T? GetValue<T>(this DataRow row, string columnName, T defaultValue) where T : struct
        {
            if (row.IsNull(columnName))
            {
                return defaultValue;
            }
            return row[columnName] as T?;
        }

        /// <summary>
        ///     Determines whether the record value is DBNull.Value
        /// </summary>
        /// <param name="row"> The data row. </param>
        /// <param name="columnName"> The name of the record columnName. </param>
        /// <returns> <c>true</c> if the value is DBNull.Value; otherwise, <c>false</c> . </returns>
        public static bool IsDbNull(this DataRow row, string columnName)
        {
            var value = row[columnName];
            return value == DBNull.Value;
        }
    }
}