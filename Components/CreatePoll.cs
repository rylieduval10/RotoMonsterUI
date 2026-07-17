using HtmlTags;

namespace RotoMonsterUI
{
    public class CreatePoll
    {
        private readonly CreatePollInput _input;

        public CreatePoll(CreatePollInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var card = new HtmlTag("div").AddClass("poll-card create-poll-card");

            // League selector + Refresh Rosters
            var leagueRow = new HtmlTag("div").AddClass("create-poll-league-row");
            var leagueDropdown = new Dropdown("Select League").WithName("create-poll-league");
            foreach (var league in _input.Leagues)
                leagueDropdown.AddItem(league.Label, league.Id);
            if (!string.IsNullOrEmpty(_input.SelectedLeagueId))
                leagueDropdown.WithSelectedValue(_input.SelectedLeagueId);
            leagueRow.AppendHtml(leagueDropdown.Render());

            var refreshBtn = new IconButton("Refresh Rosters", IconType.RefreshRosters)
                .WithStyle(ButtonStyle.Secondary)
                .WithName("create-poll-refresh-rosters")
                .Render();
            leagueRow.AppendHtml(refreshBtn);
            card.Append(leagueRow);

            // Expires in hours
            var expiresLabel = new HtmlTag("div").AddClass("create-poll-field-label")
                .Text($"Expires in hours - {_input.MinExpiresHours} to {_input.MaxExpiresHours} hours ({_input.MaxExpiresHours / 24} days)");
            card.Append(expiresLabel);

            var expiresBox = new TextBox()
                .WithName("create-poll-expires-hours")
                .WithType(TextBoxType.Number)
                .WithRange(_input.MinExpiresHours, _input.MaxExpiresHours)
                .WithValue(_input.ExpiresInHours.ToString())
                .Render();
            card.AppendHtml(expiresBox);

            // League settings
            var settingsLabel = new HtmlTag("div").AddClass("create-poll-field-label")
                .Text("League Settings - if the question depends on settings, which settings should be used?");
            card.Append(settingsLabel);

            var settingsDropdown = new Dropdown("Select Settings").WithName("create-poll-settings");
            foreach (var setting in _input.LeagueSettingsOptions)
                settingsDropdown.AddItem(setting.Label, setting.Id);
            if (!string.IsNullOrEmpty(_input.SelectedLeagueSettingsId))
                settingsDropdown.WithSelectedValue(_input.SelectedLeagueSettingsId);
            card.AppendHtml(settingsDropdown.Render());

            // Question
            var questionLabel = new HtmlTag("div").AddClass("create-poll-field-label")
                .Text("Question - what is being answered by the poll?");
            card.Append(questionLabel);

            var questionBox = new TextArea(new TextAreaInput
            {
                Id = "create-poll-question-area",
                Name = "create-poll-question",
                Placeholder = "",
                InitialValue = _input.Question ?? "",
                MinHeight = 140
            }).Render();
            card.AppendHtml(questionBox);

            // Poll options
            var optionsHeader = new HtmlTag("h3").AddClass("create-poll-options-header")
                .Text("Poll Options - there must be at least two options");
            card.Append(optionsHeader);

            if (_input.Options.Count < _input.MaxOptions)
            {
                var addOptionBtn = new HtmlTag("button")
                    .AddClass("modern-filter-btn modern-filter-btn-secondary")
                    .Attr("type", "submit")
                    .Attr("name", "create-poll-add-option")
                    .Text("Add Option");
                card.Append(addOptionBtn);
            }

        var optionsColumnLabels = new HtmlTag("div").AddClass("create-poll-column-labels");
        optionsColumnLabels.Append(new HtmlTag("span").AddClass("create-poll-column-label create-poll-column-label--tools").Text("Tools"));
        optionsColumnLabels.Append(new HtmlTag("span").AddClass("create-poll-column-label create-poll-column-label--players").Text("Players"));
        optionsColumnLabels.Append(new HtmlTag("span").AddClass("create-poll-column-label create-poll-column-label--comment").Text("Comment"));
        card.Append(optionsColumnLabels);

        var optionsList = new HtmlTag("div").AddClass("create-poll-options-list");

        for (int i = 0; i < _input.Options.Count; i++)
        {
            var option = _input.Options[i];
            var row = new HtmlTag("div").AddClass("create-poll-option-row");

            var toolsCell = new HtmlTag("div").AddClass("create-poll-tools-cell");
            var toolsWrap = new HtmlTag("div").AddClass("create-poll-tools");

            var upBtn = new HtmlTag("button").AddClass("create-poll-tool-btn")
                .Attr("type", "submit").Attr("name", $"create-poll-moveup-option-{option.OptionId}")
                .Attr("aria-label", "Move up");
            if (i == 0) upBtn.Attr("disabled", "disabled");
            upBtn.AppendHtml(new Icon(new IconInput { Type = IconType.ArrowUp, Size = 14 }).Render());
            toolsWrap.Append(upBtn);

            var downBtn = new HtmlTag("button").AddClass("create-poll-tool-btn")
                .Attr("type", "submit").Attr("name", $"create-poll-movedown-option-{option.OptionId}")
                .Attr("aria-label", "Move down");
            if (i == _input.Options.Count - 1) downBtn.Attr("disabled", "disabled");
            downBtn.AppendHtml(new Icon(new IconInput { Type = IconType.ArrowDown, Size = 14 }).Render());
            toolsWrap.Append(downBtn);

            if (_input.Options.Count > 2)
            {
                var deleteBtn = new HtmlTag("button").AddClass("create-poll-tool-btn create-poll-tool-btn--delete")
                    .Attr("type", "submit").Attr("name", $"create-poll-remove-option-{option.OptionId}")
                    .Attr("aria-label", "Delete option");
                deleteBtn.AppendHtml(new Icon(new IconInput { Type = IconType.Close, Size = 14 }).Render());
                toolsWrap.Append(deleteBtn);
            }

            toolsCell.Append(toolsWrap);
            row.Append(toolsCell);

            var playersCell = new HtmlTag("div").AddClass("create-poll-players-cell");
            var picker = new PollOptionPlayerPicker(new PollOptionPlayerPickerInput
            {
                Id = $"create-poll-option-{option.OptionId}-players",
                AddedPlayers = option.Players,
                CanEdit = true
            }).Render();
            playersCell.AppendHtml(picker);
            row.Append(playersCell);

            var commentCell = new HtmlTag("div").AddClass("create-poll-comment-cell");
            var commentArea = new TextArea(new TextAreaInput
            {
                Id = $"create-poll-option-{option.OptionId}-comment-area",
                Name = $"create-poll-option-{option.OptionId}-comment",
                InitialValue = option.Comment ?? "",
                MinHeight = 90
            }).Render();
            commentCell.AppendHtml(commentArea);
            row.Append(commentCell);

            optionsList.Append(row);
        }
        card.Append(optionsList);

            var note = new HtmlTag("p").AddClass("create-poll-note")
                .Text("Note: Once you create the poll, it cannot be modified. The only option you'll have is to delete it.");
            card.Append(note);

            var submitBtn = new HtmlTag("button")
                .AddClass("modern-filter-btn modern-filter-btn-primary")
                .Attr("type", "submit")
                .Attr("name", "create-poll-submit")
                .Text("Create Poll");
            card.Append(submitBtn);

            return card.ToString();
        }
    }
}