using HtmlTags;

namespace RotoMonsterUI
{
    public class DataTable
    {
        private string _tableHtml;
        private string _id;

        public DataTable(string tableHtml)
        {
            _tableHtml = tableHtml;
        }

        public DataTable WithId(string id)
        {
            _id = id;
            return this;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("modern-table-container");

            if (!string.IsNullOrEmpty(_id))
                wrapper.Attr("id", _id);

            wrapper.AppendHtml(_tableHtml);

            return wrapper.ToString();
        }
    }
}