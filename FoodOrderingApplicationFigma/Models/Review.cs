namespace FoodOrderingApplicationFigma.Models
{
    public partial class Review
    {
        public long review_id { get; set; }

        public long customer_id { get; set; }

        public int restaurant_id { get; set; }

        public int rating { get; set; }

        public string? comment { get; set; }

        public DateTime? created_at { get; set; }

        public virtual Customer Customer { get; set; } = null!;

        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}
