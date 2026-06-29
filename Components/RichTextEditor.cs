using HtmlTags;

namespace RotoMonsterUI
{
    public class RichTextEditor
    {
        private readonly RichTextEditorInput _input;

        public RichTextEditor(RichTextEditorInput input)
        {
            _input = input;
        }

        private string ToolbarBtn(string command, string title, string svgPath, string extraAttrs = "")
        {
            return $@"<button type=""button"" class=""rte-btn"" data-rte-cmd=""{command}"" title=""{title}"" {extraAttrs}>{svgPath}</button>";
        }

        private string Svg(string innerPath, double strokeWidth = 2)
        {
            return $@"<svg width=""14"" height=""14"" viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""{strokeWidth}"" stroke-linecap=""round"" stroke-linejoin=""round"">{innerPath}</svg>";
        }

        private string BuildEmojiPicker(string editorId)
        {
            var categories = new[]
            {
                ("Smileys", new[] { "😀","😂","😊","😍","🥲","😭","😤","🤔","😎","🥳","😬","🤯","😴","🤩","🥰" }),
                ("Sports", new[] { "⚾","🏀","🏈","⚽","🎯","🏆","🥇","🎮","⚡","🔥","💪","🏃","🤸","🧢","👟" }),
                ("Hands", new[] { "👍","👎","👋","🙌","🤞","✌️","🤙","👏","🙏","💪","👀","✅","❌","💯","❤️" }),
                ("Misc", new[] { "🌟","💥","🎉","🎊","💰","📈","📉","💡","🔑","⏰","📅","🗓️","📊","🚀","🎯" })
            };

            var sb = new System.Text.StringBuilder();
            sb.Append($@"<div class=""rte-emoji-panel"" id=""{editorId}-emoji-panel"" style=""display:none;"">");
            sb.Append(@"<div class=""rte-emoji-categories"">");

            foreach (var (label, emojis) in categories)
            {
                sb.Append($@"<div class=""rte-emoji-category"">");
                sb.Append($@"<div class=""rte-emoji-category-label"">{label}</div>");
                sb.Append(@"<div class=""rte-emoji-grid"">");
                foreach (var emoji in emojis)
                {
                    sb.Append($@"<button type=""button"" class=""rte-emoji-btn"" data-emoji=""{emoji}"" data-rte-editor=""{editorId}"" title=""{emoji}"">{emoji}</button>");
                }
                sb.Append("</div>");
                sb.Append("</div>");
            }

            sb.Append("</div>");
            sb.Append("</div>");

            return sb.ToString();
        }

        public string Render()
        {
            var id = _input.Id;
            var editorId = $"{id}-editor";
            var hiddenId = $"{id}-value";

            var wrapper = new HtmlTag("div").AddClass("rte-wrapper").Attr("id", id);

            // Toolbar
            var toolbar = new HtmlTag("div").AddClass("rte-toolbar");

            // Bold
            var boldSvg = Svg(@"<path d=""M6 4h8a4 4 0 0 1 4 4 4 4 0 0 1-4 4H6z""/><path d=""M6 12h9a4 4 0 0 1 4 4 4 4 0 0 1-4 4H6z""/>", 2.5);
            toolbar.AppendHtml(ToolbarBtn("bold", "Bold", boldSvg));

            // Italic
            var italicSvg = Svg(@"<line x1=""19"" y1=""4"" x2=""10"" y2=""4""/><line x1=""14"" y1=""20"" x2=""5"" y2=""20""/><line x1=""15"" y1=""4"" x2=""9"" y2=""20""/>", 2.5);
            toolbar.AppendHtml(ToolbarBtn("italic", "Italic", italicSvg));

            // Underline
            var underlineSvg = Svg(@"<path d=""M6 3v7a6 6 0 0 0 6 6 6 6 0 0 0 6-6V3""/><line x1=""4"" y1=""21"" x2=""20"" y2=""21""/>", 2.5);
            toolbar.AppendHtml(ToolbarBtn("underline", "Underline", underlineSvg));

            // Separator
            toolbar.AppendHtml("<div class='rte-sep'></div>");

            // Bullet list
            var bulletSvg = Svg(@"<line x1=""9"" y1=""6"" x2=""20"" y2=""6""/><line x1=""9"" y1=""12"" x2=""20"" y2=""12""/><line x1=""9"" y1=""18"" x2=""20"" y2=""18""/><circle cx=""4"" cy=""6"" r=""1.5"" fill=""currentColor"" stroke=""none""/><circle cx=""4"" cy=""12"" r=""1.5"" fill=""currentColor"" stroke=""none""/><circle cx=""4"" cy=""18"" r=""1.5"" fill=""currentColor"" stroke=""none""/>");
            toolbar.AppendHtml(ToolbarBtn("insertUnorderedList", "Bullet List", bulletSvg));

            // Numbered list
            var numberedSvg = Svg(@"<line x1=""10"" y1=""6"" x2=""21"" y2=""6""/><line x1=""10"" y1=""12"" x2=""21"" y2=""12""/><line x1=""10"" y1=""18"" x2=""21"" y2=""18""/><path d=""M4 6h1v4""/><path d=""M4 10h2""/><path d=""M6 18H4c0-1 2-2 2-3s-1-1.5-2-1""/>");
            toolbar.AppendHtml(ToolbarBtn("insertOrderedList", "Numbered List", numberedSvg));

            // Separator
            toolbar.AppendHtml("<div class='rte-sep'></div>");

            // Link
            var linkSvg = Svg(@"<path d=""M10 13a5 5 0 0 0 7.54.54l3-3a5 5 0 0 0-7.07-7.07l-1.72 1.71""/><path d=""M14 11a5 5 0 0 0-7.54-.54l-3 3a5 5 0 0 0 7.07 7.07l1.71-1.71""/>");
            toolbar.AppendHtml(ToolbarBtn("createLink", "Insert Link", linkSvg, $"data-rte-prompt=\"Enter URL:\""));

            // Unlink
            var unlinkSvg = Svg(@"<path d=""M10 13a5 5 0 0 0 7.54.54l3-3a5 5 0 0 0-7.07-7.07l-1.72 1.71""/><path d=""M14 11a5 5 0 0 0-7.54-.54l-3 3a5 5 0 0 0 7.07 7.07l1.71-1.71""/><line x1=""2"" y1=""2"" x2=""22"" y2=""22""/>");
            toolbar.AppendHtml(ToolbarBtn("unlink", "Remove Link", unlinkSvg));

            // Separator
            toolbar.AppendHtml("<div class='rte-sep'></div>");

            // Undo
            var undoSvg = Svg(@"<path d=""M3 7v6h6""/><path d=""M21 17a9 9 0 0 0-9-9 9 9 0 0 0-6 2.3L3 13""/>");
            toolbar.AppendHtml(ToolbarBtn("undo", "Undo", undoSvg));

            // Redo
            var redoSvg = Svg(@"<path d=""M21 7v6h-6""/><path d=""M3 17a9 9 0 0 1 9-9 9 9 0 0 1 6 2.3L21 13""/>");
            toolbar.AppendHtml(ToolbarBtn("redo", "Redo", redoSvg));

            // Separator
            toolbar.AppendHtml("<div class='rte-sep'></div>");

            // Emoji button
            toolbar.AppendHtml($@"<button type=""button"" class=""rte-btn"" id=""{editorId}-emoji-btn"" data-rte-emoji-panel=""{editorId}-emoji-panel"" title=""Emoji"">😊</button>");

            wrapper.Append(toolbar);

            // Emoji picker panel
            wrapper.AppendHtml(BuildEmojiPicker(editorId));

            // Editable area
            var editor = new HtmlTag("div")
                .AddClass("rte-body")
                .Attr("id", editorId)
                .Attr("contenteditable", "true")
                .Attr("data-placeholder", _input.Placeholder)
                .Attr("data-rte-target", hiddenId)
                .Attr("style", $"min-height:{_input.MinHeight}px;");

            if (!string.IsNullOrEmpty(_input.InitialValue))
                editor.AppendHtml(_input.InitialValue);

            wrapper.Append(editor);

            // Footer
            wrapper.Append(new HtmlTag("div").AddClass("rte-footer").Text(""));

            // Hidden input for form submission
            wrapper.Append(new HtmlTag("input")
                .Attr("type", "hidden")
                .Attr("id", hiddenId)
                .Attr("name", _input.Name ?? id)
                .Attr("value", _input.InitialValue ?? ""));

            return wrapper.ToString();
        }
    }
}