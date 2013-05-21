namespace Roomie.Web.Website.ViewModels
{
    public class WidgetData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Target { get; set; }
        public WidgetStatus Status { get; set; }
        public string DivId { get; set; }
        public string DebugText { get; set; }
        public string Location { get; set; }

        public string CssClass
        {
            get
            {
                if (Status == WidgetStatus.None)
                {
                    return null;
                }

                var result = Status.ToString().ToLower();

                return result;
            }
        }

    public static WidgetStatus ConnectedOrDisconnected(bool? status)
        {
            if (status == true)
            {
                return WidgetStatus.Connected;
            }

            return WidgetStatus.Disconnected;
        }
    }
}