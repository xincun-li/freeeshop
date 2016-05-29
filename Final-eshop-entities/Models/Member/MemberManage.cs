using Final_eshop_entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Final_eshop_entities.Models
{
    public class MemberManage
    {
        public static int Create(MemberDTO m)
        {
            using (var db = new ShopContext())
            {
                Member mm = db.MemberShips.Create();
                mm.Name = m.Name;
                mm.Email = m.Email;
                mm.Password = m.Password;
                mm.ConfirmPassword = m.Password;
                mm.Role = m.Role == 2 ? Role.Seller : Role.Basic;
                db.MemberShips.Add(mm);
                db.SaveChanges();
                return mm.Id;
            }
        }

        public static List<Member> GetAllMembers()
        {
            using (var db = new ShopContext())
            {
                var query = from u in db.MemberShips
                            orderby u.RegisterDate descending
                            select u;

                return new List<Member>(query);
            }
        }

        public static Member Get(int Id)
        {
            using (var db = new ShopContext())
            {
                var user = (from u in db.MemberShips
                            where u.Id == Id
                            select u).FirstOrDefault();
                return user;
            }
        }

        public static Member Get(string email)
        {
            using (var db = new ShopContext())
            {
                var user = (from u in db.MemberShips
                            where u.Email == email
                            select u).FirstOrDefault();
                return user;
            }
        }

        public static bool UpgradePremium(Member m)
        {
            if (m == null)
            {
                throw new ArgumentNullException("Can't match a product.");
            }
            using (var db = new ShopContext())
            {

                var mm = (from u in db.MemberShips
                               where u.Id == m.Id
                               select u).FirstOrDefault();
                if (mm == null)
                {
                    return false;
                }
                db.MemberShips.Attach(mm);
                var entry = db.Entry(mm);
                entry.Entity.Role = Role.Premium;
                db.SaveChanges();

                return true;
            }
        }
    }
}