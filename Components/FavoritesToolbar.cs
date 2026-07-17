using HtmlTags;

namespace RotoMonsterUI
{
    public class FavoritesToolbar
    {
        private readonly FavoritesToolbarInput _input;

        public FavoritesToolbar(FavoritesToolbarInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("favorites-toolbar");

            var label = new HtmlTag("span").AddClass("favorites-toolbar-label").Text("Favorites");
            wrapper.Append(label);

            foreach (var page in _input.Pages)
            {
                var pill = new HtmlTag("span").AddClass("favorites-toolbar-pill");

                var link = new HtmlTag("a")
                    .AddClass("favorites-toolbar-link")
                    .Attr("href", page.Url)
                    .Text(page.Name);
                pill.Append(link);

                var hideBtn = new HtmlTag("button")
                    .AddClass("favorites-toolbar-hide-btn")
                    .Attr("type", "button")
                    .Attr("name", $"{_input.Id}_hide_{page.PageId}")
                    .Attr("aria-label", "Remove from favorites")
                    .Attr("onclick", $"TriggerPostBack(this, '{_input.Id}_hide_', 'data-pageid')")
                    .Attr("data-pageid", page.PageId);
                hideBtn.AppendHtml(new Icon(new IconInput { Type = IconType.Close, Size = 12, Color = "currentColor" }).Render());
                pill.Append(hideBtn);

                wrapper.Append(pill);
            }

            if (_input.AvailablePages != null && _input.AvailablePages.Count > 0)
            {
                var pickerId = $"{_input.Id}_addpage_picker";
                var selectName = $"{_input.Id}_addpage_select";

                var addBtn = new IconButton("Add a page", IconType.Plus)
                    .WithStyle(ButtonStyle.Secondary)
                    .WithIconOnly()
                    .WithOnClick($"var p=document.getElementById('{pickerId}'); p.style.display = p.style.display === 'none' ? 'inline-block' : 'none';")
                    .Render();
                wrapper.AppendHtml(addBtn);

                var picker = new Dropdown("Add a page")
                    .WithName(selectName);
                foreach (var page in _input.AvailablePages)
                    picker.AddItem(page.Name, page.PageId);

                var pickerWrap = new HtmlTag("span")
                    .AddClass("favorites-toolbar-add-picker")
                    .Attr("id", pickerId)
                    .Attr("style", "display:none;")
                    .AppendHtml(picker.Render());
                wrapper.Append(pickerWrap);
            }

            return wrapper.ToString();
        }
    }
}