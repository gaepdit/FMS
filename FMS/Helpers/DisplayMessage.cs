namespace FMS
{
    public class DisplayMessage
    {
        private Context Context { get; }
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