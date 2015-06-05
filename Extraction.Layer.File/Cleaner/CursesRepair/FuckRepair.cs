
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileStructureDataExtraction.Cleaner.CursesRepair
{
    internal class FuckRepair : ICursesRepair
    {
        public bool Fixed { get; private set; }

        public string TryFix(string posFuck)
        {                // es fängt mit s an 
            if (posFuck.Length >= 4)
            {
                int rating = 0;
                List<char> usedLetters = new List<char>();
                if (posFuck[0] == 'f')
                {
                    rating++;
                    bool found = false;
                    for (int i = 1; i < 4; i++)
                    {
                        switch (posFuck[i])
                        {
                            case 'u': found = true; break;
                            case 'c': found = true; break;
                            case 'k': found = true; break;
                            case '*': found = true; break;
                        }
                        if (found)
                        {
                            if (usedLetters.Contains(posFuck[i]))
                                return posFuck;
                            else
                            {
                                rating++;
                                usedLetters.Add(posFuck[i]);
                            }
                        }
                    }
                    if (rating == 4)
                    {
                        Fixed = true;
                        return "fuck" + posFuck.Remove(0, 4);
                    }
                }
            }
            return posFuck;
        }
    }
}
