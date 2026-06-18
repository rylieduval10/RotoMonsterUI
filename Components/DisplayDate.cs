using System;

namespace RotoMonsterUI
{
    public class DisplayDate
    {
        private DisplayDateInput _input;

        public DisplayDate(DisplayDateInput input)
        {
            _input = input;
        }

        public string Render()
        {
            return _input.Date.ToString(_input.Format);
        }
    }
}