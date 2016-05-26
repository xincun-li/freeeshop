namespace Final_eshop_xincunli.Filters
{
    public class AllowViewOrderAttribute:AllowEditAttribute
    {
        protected override string GetCompareEmail(int id)
        {
            var order = db.Orders.Find(id);
            return order.Member.Email; 
        }
    }
}