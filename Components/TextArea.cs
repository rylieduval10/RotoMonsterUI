using HtmlTags;

namespace RotoMonsterUI
{
    public class TextAreaInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Placeholder { get; set; } = "";
        public string InitialValue { get; set; } = "";
        public int MinHeight { get; set; } = 100;
        public int? MaxLength { get; set; }
    }

    public class TextArea
    {
        private readonly TextAreaInput _input;

        public TextArea(TextAreaInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var id = _input.Id;
            var name = _input.Name ?? id;

            var wrapper = new HtmlTag("div").AddClass("bm-textarea-wrapper").Attr("id", id);

            var textarea = new HtmlTag("textarea")
                .AddClass("bm-textarea")
                .Attr("id", $"{id}-input")
                .Attr("name", name)
                .Attr("placeholder", _input.Placeholder)
                .Attr("style", $"min-height:{_input.MinHeight}px;");

            if (_input.MaxLength.HasValue)
                textarea.Attr("maxlength", _input.MaxLength.Value.ToString());

            if (!string.IsNullOrEmpty(_input.InitialValue))
                textarea.Text(_input.InitialValue);

            wrapper.Append(textarea);

            return wrapper.ToString();
        }
    }
}