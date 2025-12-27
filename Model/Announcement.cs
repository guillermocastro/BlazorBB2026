using BlazorBB2026.Components.Pages;
using Humanizer;
using System.ComponentModel;

namespace BlazorBB2026.Model
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public DateTime AnnouncementDate { get; set; }
        public string TitleES { get; set; } = null!;
        public string TitleEN { get; set; } = null!;
        public string DescriptionES { get; set; } = null!;
        public string DescriptionEN { get; set; } = null!;
    }
}
