// Copyright (c) TomorrowsWorld.org. All rights reserved. 
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;

namespace StandardResponse
{
    public class Status
    {
        public IList<StatusMessage> Messages { get; set; } = new List<StatusMessage>();
        public string Text => string.Join("\n", Messages.Select(m => $"{m.Type.ToString()}: {m.Message}"));
        public bool HasError => Messages.Any(m => m.Type == StatusMessageType.Error);
        public bool IsOk => !HasError;

        public virtual void Add(Status status)
        {
            Messages = Messages.Concat(status.Messages).ToList();
        }

        public virtual void AddError(string message)
        {
            Messages.Add(new StatusMessage { Type = StatusMessageType.Error, Message = message });
        }

        public virtual void AddWarning(string message)
        {
            Messages.Add(new StatusMessage { Type = StatusMessageType.Warning, Message = message });
        }

        public virtual void AddInfo(string message)
        {
            Messages.Add(new StatusMessage { Type = StatusMessageType.Info, Message = message });
        }

        public virtual void AddSuccess(string message)
        {
            Messages.Add(new StatusMessage { Type = StatusMessageType.Success, Message = message });
        }

        public virtual void AddSuccessIfNoMessages(string message)
        {
            if (!Messages.Any())
            {
                Messages.Add(new StatusMessage { Type = StatusMessageType.Success, Message = message });
            }
        }
        public static TStatus Error<TStatus>(string message) where TStatus : Status, new()
        {
            var status = new TStatus();
            status.AddError(message);
            return status;
        }

        public static Status Error(string message)
        {
            return Error<Status>(message);
        }

        public static TStatus Warning<TStatus>(string message) where TStatus : Status, new()
        {
            var status = new TStatus();
            status.AddWarning(message);
            return status;
        }

        public static Status Warning(string message)
        {
            return Warning<Status>(message);
        }

        public static TStatus Info<TStatus>(string message) where TStatus : Status, new()
        {
            var status = new TStatus();
            status.AddInfo(message);
            return status;
        }

        public static Status Info(string message)
        {
            return Info<Status>(message);
        }

        public static TStatus Success<TStatus>(string message = null) where TStatus : Status, new()
        {
            var status = new TStatus();
            if (message != null)
            {
                status.AddSuccess(message);
            }
            return status;
        }

        public static Status Success(string message = null)
        {
            return Success<Status>(message);
        }
    }
}
