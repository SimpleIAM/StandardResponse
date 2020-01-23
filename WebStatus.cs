// Copyright (c) TomorrowsWorld.org. All rights reserved. 
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using System.Net;
using System.Text.Json.Serialization;

namespace StandardResponse
{
    public class WebStatus : Status
    {
        public WebStatus() : base() { }

        public WebStatus(Status status) : base()
        {
            Messages = status.Messages;
            if(status.HasError)
            {
                StatusCode = HttpStatusCode.InternalServerError;
            }
        }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        [JsonIgnore]
        public string RedirectUrl { get; private set; } // only used with HttpStatusCode.Redirect

        public void Add(WebStatus status)
        {
            Messages = Messages.Concat(status.Messages).ToList();
            if (status.StatusCode != HttpStatusCode.OK)
            {
                StatusCode = status.StatusCode;
            }
        }

        public override void AddError(string message)
        {
            base.AddError(message);
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public void AddError(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            base.AddError(message);
            StatusCode = statusCode;
        }


        public static WebStatus Error(string message, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            var status = new WebStatus();
            status.AddError(message, httpStatusCode);
            return status;
        }

        public new static WebStatus Success(string message = null)
        {
            return Success<WebStatus>(message);
        }

        public static WebStatus Redirect(string redirectUrl, string message = null)
        {
            var status = new WebStatus()
            {
                StatusCode = HttpStatusCode.Redirect,
                RedirectUrl = redirectUrl,
            };

            if (message != null)
            {
                status.AddSuccess(message);
            }
            return status;
        }
    }
}
