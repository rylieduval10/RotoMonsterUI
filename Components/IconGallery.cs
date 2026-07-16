using System;
using HtmlTags;

namespace RotoMonsterUI
{
    public class IconGallery
    {
        private readonly IconGalleryInput _input;

        public IconGallery(IconGalleryInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var grid = new HtmlTag("div").AddClass("icon-gallery");

            // Enum.GetValues walks every IconType automatically - nothing to update here
            // when new icon types get added later.
            foreach (IconType type in Enum.GetValues(typeof(IconType)))
            {
                var cell = new HtmlTag("div").AddClass("icon-gallery-item");

                var iconWrap = new HtmlTag("div").AddClass("icon-gallery-icon");
                iconWrap.AppendHtml(new Icon(new IconInput { Type = type, Size = _input.Size }).Render());
                cell.Append(iconWrap);

                var label = new HtmlTag("div").AddClass("icon-gallery-label").Text(type.ToString());
                cell.Append(label);

                grid.Append(cell);
            }

            return grid.ToString();
        }
    }
}