namespace Nancy.ViewEngines.Spark
{
    using System.IO;
    using System.Web;
    using global::Spark;

    public abstract class NancySparkView : SparkViewBase
    {
        public object Model { get; set; }

        public ViewContext ViewContext { get; set; }

        public TextWriter Writer { get; set; }

        public void Execute()
        {
            base.RenderView(Writer);
        }

        public string H(object value)
        {
            return HttpUtility.HtmlEncode(value.ToString());
        }

        public object HTML(object value)
        {
            return value;
        }

        public string SiteResource(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            // TODO Lou: the ~ should be replaced with the appvdir or owin.RequestPathBase value
            if (value.StartsWith("~/"))
                return value.Substring(1);

            return value;
        }
    }

    public abstract class NancySparkView<TModel> : NancySparkView
    {
        public new TModel Model { get; private set; }

        public void SetModel(object model)
        {
            Model = (model is TModel) ? (TModel)model : default(TModel);
        }
    }
}