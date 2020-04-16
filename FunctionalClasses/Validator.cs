using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public class Validator{
        public static bool IsInt(string num)
        {
            return int.TryParse(num, out _);
        }
        public static bool IsInt(char num)
        {
            string numStr = "";
            numStr += num;
            return int.TryParse(numStr, out _);
        }

        public static bool IsAllDigits(string num){
            foreach (char c in num)
                if (!IsInt(c))
                    return false;
            return true;
        }

        public static bool IsID(string chk)
        {
            if(chk == ""){
                MessageBox.Show("The ID Field Should Not Be Empty.", "Wrong ID Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (IsInt(chk))
                return true;
            MessageBox.Show("The ID Should Be An Integer Number.", "Wrong ID Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public static bool IsArea(string chk)
        {
            if (chk == "")
            {
                MessageBox.Show("The Area Field Should Not Be Empty.", "Wrong Area Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (IsInt(chk))
                return true;
            MessageBox.Show("The Area Should Be An Integer Number.", "Wrong Area Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public static bool IsPrice(string chk)
        {
            if (chk == "")
            {
                MessageBox.Show("The Price Field Should Not Be Empty.", "Wrong Price Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (IsInt(chk))
                return true;
            MessageBox.Show("The Price Should Be An Integer Number.", "Wrong Price Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public static bool IsAddress(string chk){
            if (chk == "")
            {
                MessageBox.Show("The Address Field Should Not Be Empty.", "Wrong Address Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static bool IsStatus(string chk)
        {
            if (chk == "")
            {
                MessageBox.Show("The Status Field Should Not Be Empty.", "Wrong Status Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (chk == "Available")
                return true;
            if (chk == "Sold")
                return true;
            if (chk == "Rented")
                return true;
            MessageBox.Show("The Status Should Be Available/Sold/Rented.", "Wrong Status Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        public static bool IsName(string chk)
        {
            if (chk == "")
            {
                MessageBox.Show("The Name Field Should Not Be Empty.", "Wrong Name Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            return true;
        }
        public static bool IsNationalID(string chk)
        {
            if (chk == "")
            {
                MessageBox.Show("The National ID Field Should Not Be Empty.", "Wrong National ID Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (IsAllDigits(chk) && chk.Length == 14)
                return true;
            MessageBox.Show("The National ID Field Should Consist of 14 Digit.", "Wrong National ID Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }
}
