using System.Collections.Generic;
using System.Linq;
using HtmlTags;

namespace RotoMonsterUI
{
    public class NewsCard
    {
        private readonly NewsCardInput _input;

        public NewsCard(NewsCardInput input)
        {
            _input = input;
        }

        private static string NormalizeColor(string color)
        {
            if (string.IsNullOrEmpty(color)) return color;
            return color.StartsWith("#") ? color : "#" + color;
        }

        private static string TintBackground(string color, double alpha = 0.12)
        {
            var hex = NormalizeColor(color).TrimStart('#');
            if (hex.Length != 6) return $"rgba(148,163,184,{alpha})";
            var r = System.Convert.ToInt32(hex.Substring(0, 2), 16);
            var g = System.Convert.ToInt32(hex.Substring(2, 2), 16);
            var b = System.Convert.ToInt32(hex.Substring(4, 2), 16);
            return $"rgba({r},{g},{b},{alpha})";
        }

        private static IconType? StatusTagIcon(string tagName)
        {
            if (string.IsNullOrEmpty(tagName)) return null;
            var n = tagName.Trim().ToLower();
            switch (n)
            {
                case "injury": return IconType.Injury;
                case "illness": return IconType.Illness;
                case "rest": return IconType.Rest;
                case "personal": return IconType.Personal;
                case "coach's decision":
                case "coachs decision": return IconType.CoachsDecision;
                case "dental": return IconType.Dental;
                case "possible suspension": return IconType.PossibleSuspension;
                case "suspended": return IconType.Suspended;
                case "trade pending": return IconType.TradePending;
                case "contract": return IconType.Contract;
                case "new contract": return IconType.NewContract;
                case "injury maintenance": return IconType.InjuryMaintenance;
                case "out for season": return IconType.OutForSeason;
                case "free agent": return IconType.FreeAgent;
                case "new team": return IconType.NewTeam;
                default: return IconType.Other;
            }
        }

        private static (IconType icon, string color) NewsTagDefaults(string tagName)
        {
            var n = (tagName ?? "").ToLower();

            if (n.Contains("shootaround"))
                return (n.Contains("no") || n.Contains("missed")) ? (IconType.MissedShootaround, "#ef4444") : (IconType.MadeShootaround, "#22c55e");
            if (n.Contains("practice"))
                return (n.Contains("no") || n.Contains("missed")) ? (IconType.MissedPractice, "#ef4444") : (IconType.Practiced, "#22c55e");
            if (n.Contains("game") && n.Contains("decision"))
                return (IconType.GameTimeDecision, "#3b82f6");
            if (n.Contains("spot start"))
                return (IconType.SpotStart, "#22c55e");
            if (n.Contains("limited minutes"))
                return (IconType.LimitedMinutes, "#3b82f6");
            if (n.Contains("team update"))
                return (IconType.IsTeamUpdate, "#3b82f6");

            return (IconType.Note, "#94a3b8");
        }

        private static string LevelColor(NewsLevel level)
        {
            switch (level)
            {
                case NewsLevel.Medium: return "#3b82f6";
                case NewsLevel.High: return "#f59e0b";
                case NewsLevel.Monster: return "#a855f7";
                default: return null;
            }
        }

        public string Render()
        {
            var card = new HtmlTag("div").AddClass("news-card");

            // ---- Header row ----
            var headerRow = new HtmlTag("div").AddClass("news-card-header");

            if (_input.UserCanEdit)
            {
                var editCheckbox = new Checkbox()
                    .WithName($"editnews_{_input.NewsId}")
                    .WithChecked(_input.IsEditing)
                    .WithPostBack()
                    .Render();
                headerRow.AppendHtml(editCheckbox);
            }

            headerRow.Append(RenderStatusBadge());

            if (_input.DisplayPlayerInput != null)
            {
                headerRow.AppendHtml(new DisplayPlayer(_input.DisplayPlayerInput).Render());
            }
            else if (_input.DisplayTeamInput != null)
            {
                var teamSpan = new HtmlTag("span")
                    .AddClass("news-card-team")
                    .Attr("style", $"color:{NormalizeColor(_input.DisplayTeamInput.ColorCode)}")
                    .Text(_input.DisplayTeamInput.Code);
                headerRow.Append(teamSpan);
            }

            var headerRight = new HtmlTag("div").AddClass("news-card-header-right");

            if (!string.IsNullOrEmpty(_input.SourceURL))
            {
                var sourceLink = new HtmlTag("a")
                    .AddClass("news-card-source")
                    .Attr("href", _input.SourceURL)
                    .Attr("target", "_blank")
                    .Attr("rel", "noopener noreferrer")
                    .Text("source");
                headerRight.Append(sourceLink);
            }

            if (_input.TimeSinceCreated.HasValue)
                headerRight.AppendHtml(new TimeSince(_input.TimeSinceCreated.Value).Render());

            headerRow.Append(headerRight);
            card.Append(headerRow);

            // ---- Status text / team roster row ----
            if (_input.DisplayTeamInput != null && _input.TeamPlayerNames.Any())
            {
                var rosterRow = new HtmlTag("div").AddClass("news-card-roster");
                rosterRow.Append(new HtmlTag("span").Text(string.Join(", ", _input.TeamPlayerNames)));

                var teamLevelColor = LevelColor(_input.NewsLevel);
                if (teamLevelColor != null)
                {
                    var teamLevelBadge = new HtmlTag("span")
                        .AddClass("news-card-level-badge")
                        .Attr("style", $"background:{teamLevelColor}")
                        .Text($"{_input.NewsLevel.ToString().ToLower()} level");
                    rosterRow.Append(teamLevelBadge);
                }

                card.Append(rosterRow);
            }
            else if (!string.IsNullOrEmpty(_input.StatusTypeText))
            {
                var statusRow = new HtmlTag("div").AddClass("news-card-status-row");
                statusRow.Append(new HtmlTag("span").AddClass("news-card-status-text").Text(_input.StatusTypeText));
                if (!string.IsNullOrEmpty(_input.NewsTitle))
                    statusRow.Append(new HtmlTag("span").AddClass("news-card-news-title").Text(_input.NewsTitle));

                var levelColor = LevelColor(_input.NewsLevel);
                if (levelColor != null)
                {
                    var levelBadge = new HtmlTag("span")
                        .AddClass("news-card-level-badge")
                        .Attr("style", $"background:{levelColor}")
                        .Text($"{_input.NewsLevel.ToString().ToLower()} level");
                    statusRow.Append(levelBadge);
                }

                card.Append(statusRow);
            }

            // ---- More details ----
            if (!_input.IsEditing && !string.IsNullOrEmpty(_input.NewsDetails))
            {
                card.Append(new HtmlTag("div").AddClass("news-card-more-details-label").Text("More details"));
                card.Append(new HtmlTag("div").AddClass("news-card-details").Text(_input.NewsDetails));
            }

            // ---- Applied news tags ----
            if (_input.NewsTags != null && _input.NewsTags.Count > 0)
            {
                var tagList = new HtmlTag("div").AddClass("news-card-tag-list");
                foreach (var tag in _input.NewsTags)
                {
                    var defaults = NewsTagDefaults(tag.Name);
                    var icon = tag.Icon ?? defaults.icon;
                    var color = tag.Color ?? defaults.color;

                    var tagRow = new HtmlTag("div")
                        .AddClass("news-card-tag")
                        .Attr("style", $"color:{NormalizeColor(color)}; background:{TintBackground(color)};");
                    tagRow.AppendHtml(new Icon(new IconInput { Type = icon, Size = 14, Color = "currentColor" }).Render());
                    tagRow.Append(new HtmlTag("span").Text(tag.Name));
                    tagList.Append(tagRow);
                }
                card.Append(tagList);
            }

            // ---- Owned / free agent counts ----
            if (_input.UserOwnCount.HasValue || _input.UserFreeAgentCount.HasValue)
            {
                var countsRow = new HtmlTag("div").AddClass("news-card-counts");
                if (_input.UserOwnCount.HasValue)
                    countsRow.AppendHtml($"yours {_input.UserOwnCount.Value}");
                if (_input.UserFreeAgentCount.HasValue)
                    countsRow.AppendHtml($" fa {_input.UserFreeAgentCount.Value}");
                card.Append(countsRow);
            }

            // ---- Edit form ----
            if (_input.IsEditing)
                card.Append(RenderEditForm());

            return card.ToString();
        }

        private HtmlTag RenderStatusBadge()
        {
            var badge = new HtmlTag("span")
                .AddClass("news-card-status-badge")
                .Attr("style", $"background:{NormalizeColor(_input.StatusTypeColorCode)}");

            badge.Append(new HtmlTag("span").Text(_input.StatusTypeAbbreviation));

            var tagIcon = StatusTagIcon(_input.StatusTypeTag);
            if (tagIcon.HasValue)
                badge.AppendHtml(new Icon(new IconInput { Type = tagIcon.Value, Size = 12, Color = "white" }).Render());

            if (_input.IsUnofficial)
                badge.AppendHtml(new Icon(new IconInput { Type = IconType.Error, Size = 12, Color = "white" }).Render());

            return badge;
        }

        private HtmlTag RenderEditForm()
        {
            var form = new HtmlTag("div").AddClass("news-card-edit-form");

            var currentStatusRow = new HtmlTag("div").AddClass("news-card-field-row");
            currentStatusRow.Append(new HtmlTag("span").AddClass("news-card-field-label").Text("Current Status"));
            currentStatusRow.Append(RenderStatusBadge());
            form.Append(currentStatusRow);

            var statusDropdown = new Dropdown("Status").WithName($"status_{_input.NewsId}");
            foreach (var opt in _input.StatusTypeOptions)
                statusDropdown.AddItem(opt, opt);
            statusDropdown.WithSelectedValue(_input.StatusTypeText);
            form.AppendHtml($"<div class='news-card-field-row'><label>Status</label>{statusDropdown.Render()}</div>");

            var tagDropdown = new Dropdown("Tag").WithName($"tag_{_input.NewsId}");
            foreach (var opt in _input.StatusTypeTagOptions)
                tagDropdown.AddItem(opt, opt);
            tagDropdown.WithSelectedValue(_input.StatusTypeTag);
            var tagRow = new HtmlTag("div").AddClass("news-card-field-row");
            tagRow.AppendHtml($"<label>Tag</label>{tagDropdown.Render()}");
            var setBtn = new Button("Set").WithStyle(ButtonStyle.Secondary).WithName($"settag_{_input.NewsId}").Render();
            tagRow.AppendHtml(setBtn);
            form.Append(tagRow);

            var titleBox = new TextBox()
                .WithName($"newstitle_{_input.NewsId}")
                .WithValue(_input.NewsTitle)
                .Render();
            var titleLabel = string.IsNullOrEmpty(_input.StatusTypeTag) ? "Title" : _input.StatusTypeTag;
            form.AppendHtml($"<div class='news-card-field-row'><label>{titleLabel}</label>{titleBox}</div>");

            var sourceBox = new TextBox()
                .WithName($"source_{_input.NewsId}")
                .WithValue(_input.SourceURL)
                .Render();
            form.AppendHtml($"<div class='news-card-field-row'><label>Source</label>{sourceBox}</div>");

            var levelGroup = new RadioGroup($"level_{_input.NewsId}").WithSegmented();
            levelGroup.AddOption("L", "Low", _input.NewsLevel == NewsLevel.Low);
            levelGroup.AddOption("M", "Medium", _input.NewsLevel == NewsLevel.Medium);
            levelGroup.AddOption("H", "High", _input.NewsLevel == NewsLevel.High);
            levelGroup.AddOption("Monster", "Monster", _input.NewsLevel == NewsLevel.Monster);
            form.AppendHtml(levelGroup.Render());

            var saveRow = new HtmlTag("div").AddClass("news-card-field-row");
            saveRow.AppendHtml(new Button("Save").WithStyle(ButtonStyle.Primary).WithName($"savenews_{_input.NewsId}").Render());
            var unofficialCheckbox = new Checkbox()
                .WithLabel("Unofficial")
                .WithName($"unofficial_{_input.NewsId}")
                .WithChecked(_input.IsUnofficial)
                .Render();
            saveRow.AppendHtml(unofficialCheckbox);
            form.Append(saveRow);

            var detailsTextArea = new TextArea(new TextAreaInput
            {
                Id = $"newsdetails_{_input.NewsId}",
                Name = $"newsdetails_{_input.NewsId}",
                Placeholder = "More details",
                InitialValue = _input.NewsDetails ?? ""
            }).Render();
            form.AppendHtml(detailsTextArea);

            var appliedIds = new HashSet<int>(_input.NewsTags != null ? _input.NewsTags.Select(t => t.Id) : System.Array.Empty<int>());
            var checkGrid = new HtmlTag("div").AddClass("news-card-tag-checkbox-grid");
            foreach (var opt in _input.AvailableNewsTags)
            {
                var cb = new Checkbox()
                    .WithLabel(opt.Name)
                    .WithName($"newstag_{_input.NewsId}_{opt.Id}")
                    .WithChecked(appliedIds.Contains(opt.Id))
                    .Render();
                checkGrid.AppendHtml(cb);
            }
            form.Append(checkGrid);

            if (_input.UserCanDelete)
            {
                var deleteBtn = new HtmlTag("button")
                    .AddClass("news-card-delete-btn")
                    .Attr("name", $"deletenews_{_input.NewsId}")
                    .Text("Delete");
                form.Append(deleteBtn);
            }

            return form;
        }
    }
}