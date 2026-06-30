using HtmlTags;

namespace RotoMonsterUI
{
    public class TrackedLeagueBar
    {
        private readonly TrackedLeagueBarInput _input;

        public TrackedLeagueBar(TrackedLeagueBarInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("tracked-league-bar");

            var leagueSection = new HtmlTag("div").AddClass("tracked-league-bar-section");
            var leagueLabel = new HtmlTag("span").AddClass("tracked-league-bar-label").Text("LEAGUE");
            var leagueValue = new HtmlTag("span").AddClass("tracked-league-bar-value").Text(_input.LeagueName ?? "");
            leagueSection.Append(leagueLabel);
            leagueSection.Append(leagueValue);

            var divider = new HtmlTag("div").AddClass("tracked-league-bar-divider");

            var ownerSection = new HtmlTag("div").AddClass("tracked-league-bar-section");
            var ownerLabel = new HtmlTag("span").AddClass("tracked-league-bar-label").Text("OWNER");
            var ownerValue = new HtmlTag("span").AddClass("tracked-league-bar-value").Text(_input.OwnerName ?? "");
            ownerSection.Append(ownerLabel);
            ownerSection.Append(ownerValue);

            wrapper.Append(leagueSection);
            wrapper.Append(divider);
            wrapper.Append(ownerSection);

            return wrapper.ToString();
        }
    }
}