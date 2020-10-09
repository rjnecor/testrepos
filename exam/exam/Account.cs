using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam
{
    public class Account
    {
        public string AccountId;
        public string Amount;
        public string Currency;
        public int DateCreated;


        public Account()
        {
            AccountId = "User" + Randomizer(5);
            Amount = Randomizer(1000) + "." + Randomizer(99); //removed (Randomizer(2)*2-1)*  generates too much negativity
            Currency = "EUR";
            DateCreated = 1598016617 + Randomizer(900000);
        }

        public static int Randomizer(int range)
        {
            Random rnd = new Random();
            return rnd.Next(range - 1);
        }

        public string GeneratePostBody()
        {
            return "{\"statement\": " +
                "{\"account_id\": \"" + AccountId + "\"," +
                "\"amount\": \"" + Amount + "\"," +
                "\"currency\": \"EUR\",\"date\": " + DateCreated + "}}";
        }


    }
}
