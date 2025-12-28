namespace BlazorBB2026.Model
{
    public class Article
    {
        public decimal ArticleId { get; set; }
        public string Page { get; set; } = null!;
        public string TitleES { get; set; } = null!;
        public string TitleEN { get; set; } = null!;
        public string Svg { get; set; } = null!;
        public string TextES { get; set; } = null!;
        public string TextEN { get; set; } = null!;
        public string DescriptionES { get; set; } = null!;
        public string DescriptionEN { get; set; } = null!;
        public string Url { get; set; } = null!;
        public bool IsEnabled { get; set; } = true;
    }
}
