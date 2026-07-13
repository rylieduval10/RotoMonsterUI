using HtmlTags;

namespace RotoMonsterUI
{
    public class PollOptionPlayerPicker
    {
        private readonly PollOptionPlayerPickerInput _input;

        public PollOptionPlayerPicker(PollOptionPlayerPickerInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("poll-player-picker").Attr("id", _input.Id);

            if (_input.AddedPlayers.Count > 0)
            {
                var list = new HtmlTag("div").AddClass("poll-player-picker-list");
                foreach (var player in _input.AddedPlayers)
                {
                    var chip = new HtmlTag("div").AddClass("poll-player-picker-chip");
                    chip.AppendHtml(new DisplayPlayer(player).Render());

                    if (_input.CanEdit)
                    {
                        var removeBtn = new HtmlTag("button")
                        .AddClass("poll-player-picker-remove")
                        .Attr("type", "submit")
                        .Attr("name", $"{_input.Id}-remove-{player.PlayerId}")
                        .Attr("aria-label", "Remove player")
                        .AppendHtml(new Icon(new IconInput { Type = IconType.Error, Size = 14 }).Render())
                        .AppendHtml("<span class='sr-only'>Remove</span>");
                        chip.Append(removeBtn);
                    }

                    list.Append(chip);
                }
                wrapper.Append(list);
            }

            if (_input.CanEdit)
            {
                var searchRow = new HtmlTag("div").AddClass("poll-player-picker-search-row");

                var searchBox = new SearchBox()
                    .WithId($"{_input.Id}-search")
                    .WithName($"{_input.Id}-search")
                    .WithPlaceholder("Search players...")
                    .Render();

                var hiddenSelected = new HtmlTag("input")
                    .Attr("type", "hidden")
                    .Attr("id", $"{_input.Id}-selected")
                    .Attr("name", $"{_input.Id}-selected");

                var addBtn = new HtmlTag("button")
                    .AddClass("modern-filter-btn modern-filter-btn-secondary")
                    .Attr("type", "submit")
                    .Attr("name", $"{_input.Id}-add")
                    .Text("Add");

                searchRow.AppendHtml(searchBox);
                searchRow.Append(hiddenSelected);
                searchRow.Append(addBtn);
                wrapper.Append(searchRow);
            }

            return wrapper.ToString();
        }
    }
}