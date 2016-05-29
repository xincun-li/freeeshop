using Final_eshop_entities.Models;

namespace Final_eshop_xincunli.Filters
{
    public class AllowEditMemberProfileAttribute:AllowEditAttribute
    {

        protected override string GetCompareEmail(int id)
        {
            
            Member member = db.MemberShips.Find(id);
            if (member == null) return "";
            else return member.Email;
        }
    }
}