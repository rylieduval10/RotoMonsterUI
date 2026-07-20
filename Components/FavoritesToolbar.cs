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

            // Favorited pages - links only, no per-pill remove button. Removal
            // happens via the current-page toggle when the user is on that page.
            foreach (var page in _input.Pages)
            {
                var pill = new HtmlTag("span").AddClass("favorites-toolbar-pill");
                var link = new HtmlTag("a")
                    .AddClass("favorites-toolbar-link")
                    .Attr("href", page.Url)
                    .Text(page.Name);
                pill.Append(link);
                wrapper.Append(pill);
            }

            // Current-page toggle: add if it's not favorited (and there's room),
            // remove if it is. Nothing shows if the page can't be favorited.
            if (_input.CurrentPage != null && !string.IsNullOrEmpty(_input.CurrentPage.PageId))
            {
                bool isFavorited = _input.Pages.Exists(p => p.PageId == _input.CurrentPage.PageId);

                if (isFavorited)
                {
                    var removeBtn = new HtmlTag("button")
                        .AddClass("modern-filter-btn modern-filter-btn-secondary favorites-toolbar-current-btn")
                        .Attr("type", "button")
                        .Attr("name", $"{_input.Id}_hide_{_input.CurrentPage.PageId}")
                        .Attr("aria-label", "Remove this page from favorites")
                        .Attr("onclick", $"TriggerPostBack(this, '{_input.Id}_hide_', 'data-pageid')")
                        .Attr("data-pageid", _input.CurrentPage.PageId);
                    removeBtn.AppendHtml(new Icon(new IconInput { Type = IconType.Close, Size = 14, Color = "currentColor" }).Render());
                    removeBtn.AppendHtml("<span style='margin-left:0.35rem;'>Remove from favorites</span>");
                    wrapper.Append(removeBtn);
                }
                else if (_input.Pages.Count < _input.MaxPages)
                {
                    var addBtn = new HtmlTag("button")
                        .AddClass("modern-filter-btn modern-filter-btn-secondary favorites-toolbar-current-btn")
                        .Attr("type", "button")
                        .Attr("name", $"{_input.Id}_addcurrent_{_input.CurrentPage.PageId}")
                        .Attr("aria-label", "Add this page to favorites")
                        .Attr("onclick", $"TriggerPostBack(this, '{_input.Id}_addcurrent_', 'data-pageid')")
                        .Attr("data-pageid", _input.CurrentPage.PageId);
                    addBtn.AppendHtml(new Icon(new IconInput { Type = IconType.Plus, Size = 14, Color = "currentColor" }).Render());
                    addBtn.AppendHtml("<span style='margin-left:0.35rem;'>Add to favorites</span>");
                    wrapper.Append(addBtn);
                }
                // else: at MaxPages and not favorited - no add toggle shown.
            }

            return wrapper.ToString();
        }
    }
}