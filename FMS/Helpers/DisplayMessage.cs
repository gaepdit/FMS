﻿namespace FMS
{
    public class DisplayMessage
    {
        public Context Context { get; set; }
        public string Message { get; set; }

        // parameterless constructor required for deserialization in GetDisplayMessage method
        // ReSharper disable once UnusedMember.Global
        public DisplayMessage() { }

        public DisplayMessage(Context context, string message)
        {
            Context = context;
            Message = message;
        }

        public string AlertClass => Context switch
        {
            Context.Primary => "alert-primary",
            Context.Secondary => "alert-secondary",
            Context.Success => "alert-success",
            Context.Danger => "alert-danger",
            _ => throw new System.NotImplementedException(),
        };
    }

    public enum Context
    {
        Primary,
        Secondary,
        Success,
        Danger
    }
}