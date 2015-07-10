using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace D3GemCalculatorWinPhone
{
    public class Gem // the class that does all the work, methods are self-explainatory for the most part
    {

        int[] GemChart;
        int[] GoldChart;
        int[] DeathsBreathChart;

        public Gem()
        { // this used to be 3 txt files read from assembly and stored in a multi-dimensional array, but this is more efficient
            GemChart = new int[9] { 2, 2, 2, 3, 3, 3, 3, 3, 3 };
            GoldChart = new int[9] { 2500, 5000, 10000, 20000, 25000, 200000, 300000, 400000, 500000 };
            DeathsBreathChart = new int[9] { 0, 0, 0, 0, 0, 0, 0, 1, 1 };
        }

        private string CalculateGold(int typeused, int typewanted, int amountwanted)
        {
            BigInteger goldresult = 0;


            for (int i = typeused; i < typewanted; i++)
            {
                goldresult = goldresult * GemChart[i] + GoldChart[i];
            }


            goldresult = BigInteger.Multiply(goldresult, amountwanted);
            return convertBigNumber(goldresult) + " Gold needed.";
        }
        private string CalculateGemsNeeded(int typeused, int typewanted, int amountwanted, string gemtype)
        {
            BigInteger gemresult = 0;

            for (int i = typeused; i < typewanted; i++)
            {
                gemresult = (gemresult > 0) ? gemresult * GemChart[i] : gemresult + GemChart[i];
            }

            gemresult = BigInteger.Multiply(gemresult, amountwanted);
            return convertBigNumber(gemresult) + " " + gemtype + " needed.";
        }
        private string CalculateDeathsBreath(int typeused, int typewanted, int amountwanted)
        {
            BigInteger deathsbreathresult = 0;

            for (int i = typeused; i < typewanted; i++)
            {
                deathsbreathresult = (deathsbreathresult > 0) ? deathsbreathresult * DeathsBreathChart[i] + GemChart[i] : DeathsBreathChart[i];
            } // I am not sure why I did it like this, but it seems to work, so I left it alone.

            deathsbreathresult = BigInteger.Multiply(deathsbreathresult, amountwanted);
            if (deathsbreathresult > 0)
                return convertBigNumber(deathsbreathresult) + " Death's Breath needed.";
            else
                return ""; //no need to render text for death's breath output if none are needed

        }
        //used to calculate time, but time calculation is now obsolete in D3, also did not want to rewrite it for winphone's api

        /*private string CalculateTime(int typeused, int typewanted, int amountwanted)
         { // fancy time calculations
            long seconds = 0;
            long minutes = 0;
            long hours = 0;
            long timepercombine = 3; //actually takes about 2.5 seconds but I rounded up to 3 for simplicity and accounting for player input
            long timeresult = 0;
            string Seconds, Minutes, Hours;

            for (int i = typeused; i < typewanted; i++)
            {
                timeresult = (timeresult > 0) ? timepercombine + timeresult * GemChart[i] : timepercombine;
            } //it works, that's all I know.
            timeresult = timeresult * amountwanted;
            minutes = Math.DivRem(timeresult, 60, out seconds); //some modulus/remainder division to get minutes and seconds
            hours = Math.DivRem(minutes, 60, out minutes); // same for hours/minutes
            Seconds = seconds.ToString(); //turn those longints into strings for final processing
            Minutes = minutes.ToString();
            Hours = hours.ToString("N0");

            return ("Total Time: " + Hours.PadLeft(2, '0') + ":" + Minutes.PadLeft(2, '0') + ":" + Seconds.PadLeft(2, '0')); //padded output
        }*/

        public string[] SanityCheck(int typeused, int typewanted, string amountwanted, string gemtypeused, string gemtypewanted)
        { // new and improved sanity check code. replaces the old error checking in buttonclick event method thing.
            string[] output;
            int quantity;

            if (typeused == -1 && typewanted == -1)
            {
                output = new string[1];
                output[0] = "Both gem types are blank.";
                return output;
            }
            else if (typeused == -1)
            {
                output = new string[1];
                output[0] = "Gemtype used not set.";
                return output;
            }
            else if (typewanted == -1)
            {
                output = new string[1];
                output[0] = "Gemtype wanted not set.";
                return output;
            }
            else if (typeused == typewanted)
            {
                output = new string[1];
                output[0] = "Both Gemtypes are the same.";
                return output;
            }
            else if (typeused > typewanted)
            {
                output = new string[1];
                output[0] = "Gem used is greather than Gem wanted.";
                return output;
            }
            else if (!amountwanted.All(char.IsDigit)) //used to do fancy checks for letters and such, now just checks if the entire string is numbers.
            {
                output = new string[1];
                output[0] = "Whole numbers only.";
                return output;
            }
            else
                quantity = int.Parse(amountwanted);
                output = new string[5];
            output[0] = CalculateGemsNeeded(typeused, typewanted, quantity, gemtypeused.ToString());
            output[1] = CalculateGold(typeused, typewanted, quantity);
            output[2] = CalculateDeathsBreath(typeused, typewanted, quantity);
            output[3] = "To make " + quantity + " " + gemtypewanted.ToString() + ".";
            output[4] = "Using " + gemtypeused.ToString() + ".";
            return output;
        }

        private string convertBigNumber(BigInteger input) //custom method for overcoming Mono biginteger's lack of Format method. only commas for now.
        {
            string output = "";
            string temp;
            char[] tempArray;
            char[] tempArrayTwo;

            temp = input.ToString(); // take big int and turn it into string
            tempArray = temp.ToCharArray(); // to char array
            Array.Reverse(tempArray); // reverse  it
            for (int i = 0; i != tempArray.Length; ++i)
            {
                if (i % 3 != 0 || i == 0)
                {
                    output = output + tempArray[i];
                }
                if (i != 0 && i % 3 == 0 && i != tempArray.Length)
                {
                    output = output + "," + tempArray[i];
                }
            }
            tempArrayTwo = output.ToCharArray();
            Array.Reverse(tempArrayTwo); // one final array reverse then reconversion
            output = new string(tempArrayTwo);
            return output;
        }
    }
}
