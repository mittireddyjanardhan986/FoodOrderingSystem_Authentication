namespace FoodOrderingApplicationFigma.Models
{
    public partial class Customer
    {
        public long customer_id { get; set; }

        public int user_id { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual User User { get; set; } = null!;
    }
}
