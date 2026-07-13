using System.Linq;
using HtmlTags;

namespace RotoMonsterUI
{
    public class Poll
    {
        private readonly PollInput _input;

        public Poll(PollInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var card = new HtmlTag("div").AddClass("poll-card");

            // ---- Header row ----
            var headerRow = new HtmlTag("div").AddClass("poll-header");

            headerRow.Append(new HtmlTag("span").AddClass("poll-creator").Text(_input.CreatorUsername));

            if (_input.EndsInPercent.HasValue)
            {
                var endsBadge = new HtmlTag("span")
                    .AddClass("poll-ends-badge")
                    .Text($"Ends {_input.EndsInPercent.Value:0.0}%");
                headerRow.Append(endsBadge);
            }

            headerRow.Append(new HtmlTag("span").AddClass("poll-vote-count").Text($"{_input.TotalVoteCount} vote{(_input.TotalVoteCount == 1 ? "" : "s")}"));

            var headerRight = new HtmlTag("div").AddClass("poll-header-right");
            if (_input.TimeSinceCreated.HasValue)
                headerRight.AppendHtml(new TimeSince(_input.TimeSinceCreated.Value).Render());
            headerRow.Append(headerRight);

            card.Append(headerRow);

            // ---- Question ----
            card.Append(new HtmlTag("div").AddClass("poll-question").Text(_input.Question));

            // ---- League settings ----
            if (_input.LeagueSettings != null)
            {
                var settingsRow = new HtmlTag("div").AddClass("poll-league-settings");
                settingsRow.AppendHtml("<span class='poll-league-settings-label'>Settings:</span>");
                settingsRow.AppendHtml(new LeagueSettings(_input.LeagueSettings).Render());
                card.Append(settingsRow);
            }

            // ---- Options ----
            var selectedCount = _input.Options.Count(o => o.IsSelectedByUser);
            var optionsWrapper = new HtmlTag("div").AddClass("poll-options");

            foreach (var option in _input.Options)
            {
                var optionCard = new HtmlTag("div").AddClass("poll-option");
                if (option.IsSelectedByUser)
                    optionCard.AddClass("poll-option--selected");

                var percent = _input.TotalVoteCount > 0
                    ? (double)option.VoteCount / _input.TotalVoteCount * 100
                    : 0;

                optionCard.Append(new HtmlTag("div").AddClass("poll-option-label").Text(option.Label));
                optionCard.Append(new HtmlTag("div").AddClass("poll-option-percent").Text($"{percent:0}% ({option.VoteCount})"));

                if (option.IsSelectedByUser)
                {
                    optionCard.Append(new HtmlTag("button")
                        .AddClass("poll-option-btn poll-option-btn--yours")
                        .Attr("type", "button")
                        .Attr("disabled", "disabled")
                        .Text("Yours"));
                }
                else
                {
                    var wouldSelectAll = selectedCount == _input.Options.Count - 1;
                    var chooseBtn = new HtmlTag("button")
                        .AddClass("poll-option-btn poll-option-btn--choose")
                        .Attr("type", "submit")
                        .Attr("name", $"choosepoll_{_input.PollId}_{option.OptionId}");

                    if (wouldSelectAll)
                        chooseBtn.Attr("disabled", "disabled");

                    chooseBtn.Text("Choose");
                    optionCard.Append(chooseBtn);
                }

                // Players added to this option
                var isEditingThisOption = _input.IsEditingPlayers && _input.EditingOptionId == option.OptionId;

                if (option.Players.Count > 0 || isEditingThisOption)
                {
                    var playerPicker = new PollOptionPlayerPicker(new PollOptionPlayerPickerInput
                    {
                        Id = $"poll-{_input.PollId}-option-{option.OptionId}-players",
                        AddedPlayers = option.Players,
                        CanEdit = isEditingThisOption
                    }).Render();

                    var playersWrapper = new HtmlTag("div").AddClass("poll-option-players");
                    playersWrapper.AppendHtml(playerPicker);
                    optionCard.Append(playersWrapper);
                }

                if (_input.UserCanEditPlayers && !isEditingThisOption)
                {
                    var editPlayersBtn = new HtmlTag("button")
                        .AddClass("poll-option-edit-players-btn")
                        .Attr("type", "submit")
                        .Attr("name", $"editpollplayers_{_input.PollId}_{option.OptionId}")
                        .Text("Add players");
                    optionCard.Append(editPlayersBtn);
                }

                optionsWrapper.Append(optionCard);
            }

            card.Append(optionsWrapper);

            // ---- Footer actions ----
            var footerRow = new HtmlTag("div").AddClass("poll-footer");

            if (selectedCount > 0)
            {
                var clearVote = new HtmlTag("button")
                    .AddClass("poll-footer-link")
                    .Attr("type", "submit")
                    .Attr("name", $"clearpollvote_{_input.PollId}")
                    .Text("Clear Vote");
                footerRow.Append(clearVote);
            }

            if (_input.UserCanDelete)
            {
                var deletePoll = new HtmlTag("button")
                    .AddClass("poll-footer-link poll-footer-link--delete")
                    .Attr("type", "submit")
                    .Attr("name", $"deletepoll_{_input.PollId}")
                    .Text("Delete Poll");
                footerRow.Append(deletePoll);
            }

            card.Append(footerRow);

            return card.ToString();
        }
    }
}