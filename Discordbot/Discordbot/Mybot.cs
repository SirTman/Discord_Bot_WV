using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Ame's Battle Bot
namespace Discordbot
{
    /*
    class StatusEffects
    {
        private StatusEffects()
        {
            string S_Name = "";
            string Type = "";//::Type
            float EffectMul = 1.0F;//(x1.0)
            bool haseffect = false;
            int Turncounter = 1;//[1]
            int Buffer = 0; //{1}
            

        }
    }

    class Fighter
    {
        public Fighter()
        {
            string F_Name = "";
            bool Player = false;
            bool Alive = false;
            int HP = 100;
            StatusEffects[] CE = new StatusEffects[10];
            //Dodge table
            if (Player == true)
            {
                int MaxDodgeNum = 10;
                int NumNeededToDodge = 5;
            }
            else
            {
                int MaxDodgeNum = 5;
                int NumNeededToDodge = 3;
            }
        }
        public Fighter(Fighter[] List, string a_name, bool a_player, int a_health)
        {
            for (int i = 0; i >= 100; i++)
            {

                if (List[i].Alive == true)
                {
                    continue;
                }
            }

        }
    }
    */
    



    class Mybot
    {
        DiscordClient discord;
        CommandService commands;

        public Mybot()
        {
            //Command exicution
            //bool Fight = false;
           // Fighter[] RosterList = new Fighter[100];

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });
            
            //Call Initialiser
            discord.UsingCommands(x => 
            {
                x.PrefixChar = '+';
                x.AllowMentionPrefix = true;
            });
            commands = discord.GetService<CommandService>();

            //Commands Classes go here
            Ping();
            RefisterDMGGenCommand();
            RefisterDodgeGenCommand();




            //Stuff used to connect it to the server
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzAyNTExOTY0OTcyNzc3NDgz.C9NOVA.intlnTR8dIUFEFgW9Y09iaoPS7Y", TokenType.Bot);
            });


        }
       
        
        //Commands
        //Check if the bot is online
        private void Ping()
        {
            commands.CreateCommand("Ping") .Do(async (e) =>
            {
                 await e.Channel.SendMessage("```Hiya~ I'm online.```");
            });
        }
        
        //Roll ATK
        private void RefisterDMGGenCommand()
        {
            commands.CreateCommand("ATK")
                .Parameter("Who", ParameterType.Optional)
                .Do(async (e) =>
                {
                    Random rnd = new Random();
                    var dmgvalue = rnd.Next(1, 50);
                    string DMG = dmgvalue.ToString();
                    await e.Channel.SendMessage(DMG + " " + e.GetArg("Who"));

                });
        }
        
        //Roll Dodge
        private void RefisterDodgeGenCommand()
        {
            commands.CreateCommand("Dodge")
                .Parameter("Who", ParameterType.Optional)
                .Do(async (e) =>
            {
                Random rnd = new Random();
                var dmgvalue = rnd.Next(1, 10);
                string DMG = dmgvalue.ToString();
                await e.Channel.SendMessage(DMG + " " + e.GetArg("Who"));

            });
            commands.CreateCommand("D,E")
                .Parameter("Who", ParameterType.Optional)
                .Do(async (e) =>
            {
                Random rnd = new Random();
                var dmgvalue = rnd.Next(1, 5);
                string DMG = dmgvalue.ToString();
                await e.Channel.SendMessage(DMG + " " + e.GetArg("Who"));

            });
        }


        //End
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
