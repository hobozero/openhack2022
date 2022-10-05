using System;
using System.Collections.Generic;
using System.Text;

namespace OpenHack2022.Application
{
    public class Result : IEnumerable<KeyValuePair<string, string>>
    {
        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }
        public Dictionary<string, string> MetaData { get; set; }

        public Result(
            bool success,
            string errorMessage = "",
            Dictionary<string, string> metaData = null)
        {
            Success = success;
            ErrorMessage = errorMessage ?? string.Empty;
            MetaData = metaData ?? new Dictionary<string, string>();
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return MetaData.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return MetaData.GetEnumerator();
        }
    }

    public class Result<TData> : Result
    {
        public TData Data { get; private set; }

        public Result(
            bool success,
            TData data,
            string errorMessage = "",
            Dictionary<string, string> metaData = null)
            : base(success, errorMessage, metaData)
        {
            Data = data;
        }
    }

    public static class ResultExtensions
    {
        /// <summary>
        /// Necessary to support collection initializer.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="metadataKey"></param>
        /// <param name="metadataValue"></param>
        public static void Add(this Result result, string metadataKey, string metadataValue)
        {
            result.MetaData.Add(metadataKey, metadataValue);
        }

        public static Result<T> ToSuccessResult<T>(this T data)
        {
            return new Result<T>(
                success: true,
                data: data);
        }

        public static Result<T> ToErrorResult<T>(this T data, string error, Dictionary<string, string> metaData = null)
        {
            return new Result<T>(
                success: false,
                data: data,
                errorMessage: error,
                metaData: metaData);
        }

        public static Result<T> ToErrorResult<T>(this string error, Dictionary<string, string> metaData = null)
        {
            return new Result<T>(
                success: false,
                data: default(T),
                errorMessage: error,
                metaData: metaData);
        }

        public static Result ToErrorResult(this string error, Dictionary<string, string> metaData)
        {
            return new Result(
                success: false,
                errorMessage: error,
                metaData: metaData);
        }
    }
}
