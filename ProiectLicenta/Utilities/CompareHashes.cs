using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProiectLicenta.Utilities
{
    public class CompareHashes
    {
        /*
         * functie care vrerifica daca parola introdusa de utilizator este corecta
         * parametrii: parola corecta stocata in DB si parola introdusa in fereastra de logare
         * daca coincid return true else return fals
         */
        public static bool ComparePasswords(string PasswordFromUser, string PassWordFromDB)
        {
            if (string.Compare(PasswordFromUser, PassWordFromDB) == 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}