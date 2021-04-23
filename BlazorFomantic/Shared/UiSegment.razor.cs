using System.Text;

namespace BlazorFomantic.Shared
{
    public class SegmentBuilder
    {
        private SegmentOptions _segmentOptions = new SegmentOptions();

        

        public SegmentBuilder Raise()
        {
            _segmentOptions.Raised = true;
            return this;
        }

        public SegmentBuilder WithEmphasis(SegmentEmphasis emphasis)
        {
            _segmentOptions.Emphasis = emphasis;
            return this;
        }

        public SegmentOptions GetOptions() => _segmentOptions;

        public SegmentBuilder Disable()
        {
            _segmentOptions.Disabled = true;
            return this;
        }
    }

    public enum SegmentEmphasis
    {
        Primary,
        Secondary,
        Tertiary
    }

    public class SegmentOptions
    {
        public bool Raised { get; set; }
        public SegmentEmphasis Emphasis { get; set; }

        public string GetCss()
        {
            var sb = new StringBuilder();

            sb.Append(Raised.StringIf("raised").Spaciate());
            sb.Append(Disabled.StringIf("disabled").Spaciate());
            sb.Append((Emphasis != SegmentEmphasis.Primary).StringIf(Emphasis.ToString().ToLower().Spaciate()));

            return sb.ToString();
        }

        public bool Disabled { get; set; }

        private string TextIf()
        {
            return Raised ? "raised" : "";
        }
    }
}