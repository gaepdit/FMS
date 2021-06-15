namespace FMS
{
    public class DisplayMessage
    {
        // ReSharper disable once MemberCanBePrivate.Global
        // Context must be public so it works with deserialization in TempDataExtensions class
        public Context Context { get; }
        public string Message { get; }

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
            Context.Info => "alert-info",
            _ => string.Empty
        };
    }

    public enum Context
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Info
    }
}