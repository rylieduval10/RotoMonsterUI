using System.Collections.Generic;
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
            // Nothing to show: no favorites AND the current page can't be
            // favorited - hide the whole bar.
            bool hasCurrent = _input.CurrentPage != null && !string.IsNullOrEmpty(_input.CurrentPage.PageId);
            if ((_input.Pages == null || _input.Pages.Count == 0) && !hasCurrent)
                return "";

            // Drag reorder only makes sense with 2+ pages.
            bool allowReorder = _input.AllowReorder && _input.Pages != null && _input.Pages.Count >= 2;

            var wrapper = new HtmlTag("div").AddClass("favorites-toolbar");
            if (allowReorder)
                wrapper.Attr("data-favorites-id", _input.Id);

            var starIcon = new Icon(new IconInput { Type = IconType.Favorite, Size = 20 }).Render();
            var label = new HtmlTag("span")
                .AddClass("favorites-toolbar-label")
                .AppendHtml(new CustomTooltip(starIcon, "Favorites").Render());
            wrapper.Append(label);

            // Favorited pages - links only, no per-pill remove button. Removal
            // happens via the current-page toggle when the user is on that page.
            foreach (var page in _input.Pages)
            {
                var pill = new HtmlTag("span")
                    .AddClass("favorites-toolbar-pill")
                    .Attr("data-pageid", page.PageId);

                if (allowReorder)
                {
                    var handle = new HtmlTag("span")
                        .AddClass("favorites-toolbar-handle")
                        .Attr("draggable", "true")
                        .Attr("aria-label", $"Drag to reorder {page.Name}");
                    handle.AppendHtml(new Icon(new IconInput { Type = IconType.DragHandle, Size = 14, Color = "currentColor" }).Render());
                    pill.Append(handle);
                }

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
                        .AddClass("modern-filter-btn favorites-toolbar-current-btn favorites-toolbar-remove")
                        .Attr("type", "button")
                        .Attr("name", $"{_input.Id}_hide_{_input.CurrentPage.PageId}")
                        .Attr("aria-label", $"Remove {_input.CurrentPage.Name} from favorites")
                        .Attr("onclick", $"TriggerPostBack(this, '{_input.Id}_hide_', 'data-pageid')")
                        .Attr("data-pageid", _input.CurrentPage.PageId);
                    removeBtn.AppendHtml(new Icon(new IconInput { Type = IconType.Close, Size = 12, Color = "currentColor" }).Render());
                    removeBtn.AppendHtml($"<span style='margin-left:0.35rem;'>Remove {System.Net.WebUtility.HtmlEncode(_input.CurrentPage.Name)}</span>");
                    wrapper.Append(removeBtn);
                }
                else if (_input.Pages.Count < _input.MaxPages)
                {
                    var addBtn = new HtmlTag("button")
                        .AddClass("modern-filter-btn favorites-toolbar-current-btn favorites-toolbar-add")
                        .Attr("type", "button")
                        .Attr("name", $"{_input.Id}_addcurrent_{_input.CurrentPage.PageId}")
                        .Attr("aria-label", $"Add {_input.CurrentPage.Name} to favorites")
                        .Attr("onclick", $"TriggerPostBack(this, '{_input.Id}_addcurrent_', 'data-pageid')")
                        .Attr("data-pageid", _input.CurrentPage.PageId);
                    addBtn.AppendHtml(new Icon(new IconInput { Type = IconType.Plus, Size = 12, Color = "currentColor" }).Render());
                    addBtn.AppendHtml($"<span style='margin-left:0.35rem;'>Add {System.Net.WebUtility.HtmlEncode(_input.CurrentPage.Name)}</span>");
                    wrapper.Append(addBtn);
                }
                // else: at MaxPages and not favorited - no add toggle shown.
            }

            // Hidden field holding the current page order, updated by the drag JS
            // on drop. Pre-seeded with the rendered order so a no-op drag (drop in
            // the same spot) can be detected client-side and skip the postback.
            if (allowReorder)
            {
                var ids = new List<string>();
                foreach (var p in _input.Pages)
                    ids.Add(p.PageId);

                var orderField = new HtmlTag("input")
                    .Attr("type", "hidden")
                    .Attr("id", $"{_input.Id}_order")
                    .Attr("name", $"{_input.Id}_order")
                    .Attr("value", string.Join(",", ids));
                wrapper.Append(orderField);
            }

            return wrapper.ToString();
        }
    }
}