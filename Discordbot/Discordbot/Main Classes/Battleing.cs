using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discordbot
{


    struct Effects
    {
        public string Title;
        public float Mult;
        public int Turns;
        public int Buffer;
        public int Counter;
    };

    class Fighter
    {
        public int NUM = 0;
        public string ListNUM;

        public string Name;
        public bool Foe = false;

        public int Aura = 100;
        public Effects[] Encountors;
        public Effects[] AuraEF;
        public Effects[] Effects;

        public bool FReg = false;
        public bool Dead = false;

        public string SLine;

        public Fighter(int ID,string name, bool foe, int HP)
        {
            NUM = ID;
            ListNUM = "[" + ID + "]";
            Name = name;
            Foe = foe;
            Aura = HP;
            ListingLine();


        }
        public string ListingLine()
        {
            SLine = ListNUM + Name + " " + Aura + "%" + "::";
            return SLine;
        }
    }



    class BattleSYS
    {


        public BattleSYS()
        {
            /*
            FileStream fileStream = new FileStream(@"C:\Users\dee\Documents\GitHub\Discord_Bot_WV\Discordbot\Discordbot\txt\FBoard.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            string[] lines = File.ReadAllLines(@"C:\Users\dee\Documents\GitHub\Discord_Bot_WV\Discordbot\Discordbot\txt\FBoard.txt", Encoding.UTF8);


            lines[0] = "```";
            File.WriteAllLines(@"C:\Users\dee\Documents\GitHub\Discord_Bot_WV\Discordbot\Discordbot\txt\FBoard.txt", lines);
            */

        }

        public static Fighter MakeFighter(Fighter[] List, string name, bool foe, int HP)
        {
            for (int i = 0; i < List.Length; i++)
            {
                if (List[i].FReg == true)
                {
                    i++;
                }
                else
                {
                    List[i] = new Fighter(i,name,foe,HP);
                }

            }
            return List[List.Length];
        }


        public static string ShowList(int Round, Fighter[] List)
        {
            string L1 = "```\n" + "Round #" + Round + "\n___________________________";

            string PC = null;
            for (int i = 0; i < List.Length; i++)
            {
                if (List[i].FReg == false)
                {
                    break;
                }
                else if(List[i].Foe == true)
                {
                    continue;
                }
                else
                {
                    PC += List[i].SLine + "\n";
                }

            }

            string L2 = "-------------------------------------\n";

            string Foe = null;
            for (int i = 0; i < List.Length; i++)
            {
                if (List[i].FReg == false)
                {
                    break;
                }
                else if (List[i].Foe == false)
                {
                    continue;
                }
                else
                {
                    Foe += List[i].SLine + "\n";
                }

            }
            string L3 = "```";

            string Overview = L1 + PC + L2 + Foe + L3;
            return Overview;
        }
       



    }
}
