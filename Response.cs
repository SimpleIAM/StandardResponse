// Copyright (c) TomorrowsWorld.org. All rights reserved. 
// Licensed under the Apache License, Version 2.0.

using System.Net;

namespace StandardResponse
{
    public class Response<TResult, TStatus> where TStatus : Status, new()
    {
        public Response(TStatus status = null)
        {
            if (status != null)
            {
                Status = status;
            }
        }

        public Response(TResult result, TStatus status = null) 
        {
            Result = result;
            if(status != null)
            {
                Status = status;
            }
        }

        public TStatus Status { get; set; } = new TStatus();
        public TResult Result { get; set; }
        public bool HasError => Status.HasError;
        public bool IsOk => Status.IsOk;      
    }

    public static class Response
    {
        public static Response<TResult, TStatus> Success<TResult, TStatus>(TResult result, string message = null) where TStatus : Status, new()
        {
            var response = new Response<TResult, TStatus>(result);

            if (message != null)
            {
                response.Status.AddSuccess(message);
            }

            return response;
        }

        public static Response<TResult, Status> Success<TResult>(TResult result, string message = null)
        {
            return Success<TResult, Status>(result, message);
        }

        public static Response<TResult, TStatus> Error<TResult, TStatus>(string message) where TStatus : Status, new()
        {
            var response = new Response<TResult, TStatus>();
            response.Status.AddError(message);

            return response;
        }

        public static Response<TResult, Status> Error<TResult>(string message)
        {
            return Error<TResult, Status>(message);
        }

        public static class Web
        {
            public static Response<TResult, WebStatus> Success<TResult>(TResult result, string message = null)
            {
                return Success<TResult, WebStatus>(result, message);
            }

            public static Response<TResult, WebStatus> Error<TResult>(string message)
            {
                return Error<TResult, WebStatus>(message);
            }

            public static Response<TResult, WebStatus> Error<TResult>(string message, HttpStatusCode statusCode)
            {
                var response = Error<TResult, WebStatus>(message);
                response.Status.StatusCode = statusCode;
                return response;
            }
        }
    }
}
