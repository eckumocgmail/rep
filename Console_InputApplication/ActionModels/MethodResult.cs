﻿
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// 
/// </summary>
internal class MethodResult : MethodResult<object>
{


}



/// <summary>
/// 
/// </summary>
/// <typeparam name="TResult"></typeparam>
 
public class MethodResult<TResult> where TResult : class
{
    public bool Succeeded { get; set; } = false;
    public bool IsCompleted { get; set; } = false;
    public TResult Result { get; set; } = null;
    public string Exception { get; set; } = null;
    public Dictionary<string, object> Args { get; set; }
    public DateTime Started { get; set; }
    public DateTime Completed { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public static MethodResult<TResult> OnError(Exception ex)
    {
        return new MethodResult<TResult>()
        {
            Succeeded = false,
            IsCompleted = true,
            Exception = ex.Message
        };
    }

    public static MethodResult<TResult> OnError(string ex)
    {
        return new MethodResult<TResult>()
        {
            Succeeded = false,
            IsCompleted = true,
            Exception = ex
        };
    }
    public static MethodResult<TResult> OnFailed(Exception ex, DateTime started)
    {
        return OnError(ex);
    }

    /// <summary>
    /// 
    /// </summary>
    public static MethodResult<TResult> OnResult(TResult result)
    {
        return new MethodResult<TResult>()
        {
            Result = result,
            Succeeded = true,
            IsCompleted = true,
        };
    }

    /// <summary>
    /// 
    /// </summary>
    public static MethodResult<TResult> OnComplete(TResult result, System.Collections.Generic.Dictionary<string, object> parameters, DateTime started)
    {
        return new MethodResult<TResult>()
        {
            Args = parameters,
            Started = started,
            Completed = DateTime.Now,
            Result = result,
            Succeeded = true,
            IsCompleted = true,
        };
    }


    /// <summary>
    /// 
    /// </summary>
    public static MethodResult<TResult> OnAsyncResult(IAsyncResult todo)
    {
        return new MethodResult<TResult>()
        {
            Succeeded = false,
            IsCompleted = true,

        };
    }


}