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
        int RoundNUM = 0;

        public Mybot()
        {
            //Command exicution
            //bool Fight = false;
           // Fighter[] RosterList = new Fighter[100];

            discord = new DiscordClient(x =>
            {
                x.AppName = "Minerva";
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });
            discord.Log.Message += (s, e) => Console.WriteLine($"[{e.Severity}] {e.Source}{ e.Message}");


            //Call Initialiser
            discord.UsingCommands(x => 
            {
                x.PrefixChar = '+';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
            });
            commands = discord.GetService<CommandService>();

            //Commands Classes go here
            Ping();
            ClearMSG();

            RefisterDMGGenCommand();
            RefisterDodgeGenCommand();

            //Fighter stuff
            RefisterFighterdodge();
            RefisterFighterattack();

            
            Round();

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
    
        private void ClearMSG()
        {
            commands.CreateCommand("ClearMSG")
                .Do(async (e) =>
                {
                    Message[] msgtodelete;
                    msgtodelete = await e.Channel.DownloadMessages(100);

                    await e.Channel.DeleteMessages(msgtodelete);
                });
        }

        //Roll ATK *NEW*
        private void RefisterFighterattack()
        {
            commands.CreateCommand("ATK")
                .Description("User [Base Power] Mod[Added Together]")
                .Parameter("User", ParameterType.Required)
                .Parameter("Basepower", ParameterType.Required)
                .Parameter("Mod", ParameterType.Optional)
                .Do(async (e) =>
                {
                    Random rnd = new Random();
                    float BasePower;
                    float Mod;
                    if (Single.TryParse(e.GetArg("Basepower"), out BasePower))
                    {
                        bool Powerincrease = false;
                        //Base Power of 50 or less
                        if (BasePower <= 50)
                        {
                            BasePower += rnd.Next(31);
                            Powerincrease = true;
                        }
                        //Base Power of 70 or less
                        if (BasePower <= 70 && Powerincrease == false)
                        {
                            BasePower += rnd.Next(21);
                            Powerincrease = true;
                        }
                        //Base Power of 70 or less
                        if (BasePower <= 90 && Powerincrease == false)
                        {
                            BasePower += rnd.Next(11);
                            Powerincrease = true;
                        }
                        //mod hanndler
                        if (e.GetArg("Mod") != null)
                        {
                            if (Single.TryParse(e.GetArg("Mod"), out Mod))
                            {
                                BasePower *= Mod;
                            }
                            else
                            {

                            }
                        }
                        //Critical hit & Message exicution
                        int ChanceOfCrit = rnd.Next(11);
                        if(ChanceOfCrit == 10)
                        {
                            int crit = rnd.Next(2, 5);
                            BasePower *= crit;
                            string Crittype = "";

                            //2=Nice, 3=Good, 4=Perfect
                            if (crit == 2)
                            {
                                Crittype = "Nice";
                            }
                            else if (crit == 3)
                            {
                                Crittype = "Good";
                            }
                            else if (crit == 4)
                            {
                                Crittype = "Perfect";
                            }
                            await e.Channel.SendMessage("```" + "Critical Hit " + Crittype + "```");
                        }
                        string DMG = BasePower.ToString();
                        await e.Channel.SendMessage("```" + e.GetArg("User") + " dealt " + DMG + "```");
                    }
                    //Failed
                    else
                    {
                        await e.Channel.SendMessage("```" + e.GetArg("Basepower") + " Isn't vaild```");
                    }
                        

                   

                });
        }

        //Roll Dodge *NEW*
        private void RefisterFighterdodge()
        {
            commands.CreateCommand("Dodge")
                .Description("Foe User Min2Dodge")
                .Parameter("Foe", ParameterType.Required)
                .Parameter("User", ParameterType.Required)
                .Parameter("Min", ParameterType.Optional)
                .Do(async (e) =>
                {
                    Random rnd = new Random();
                    int Min = 5;
                    if (Int32.TryParse(e.GetArg("Min"), out Min)) { }
                    int NUM = rnd.Next(11);
                    string Rolled = NUM.ToString();
                    if(NUM <= Min)
                    {
                        await e.Channel.SendMessage("```" + e.GetArg("Foe") + " failed to dodge " + e.GetArg("User") + "'s attack(" + Rolled + ")```");
                    }
                    else
                    {
                        await e.Channel.SendMessage("```" + e.GetArg("Foe") + " dodged " + e.GetArg("User") + "'s attack(" + Rolled + ")```");
                    }
                });
        }

        private void Round()
        {
            commands.CreateCommand("Round")
                .Description("Keeps track of rounds")
                .Do(async (e) =>
                {
                    RoundNUM += 1;
                    String NUM = RoundNUM.ToString();
                    await e.Channel.SendMessage("```ROUND: #" + NUM + "```");
                });
            commands.CreateCommand("CLRRound")
               .Description("Clears the Round number")
               .Do(async (e) =>
               {
                   RoundNUM = 0;
                   await e.Channel.SendMessage("```Rounds reset```");
               });
        }
        //------------------------------------------
        //Roll Dodge*Old*
        private void RefisterDodgeGenCommand()
        {
            commands.CreateCommand("DodgeOld")
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
        //Roll ATK*Old*
        private void RefisterDMGGenCommand()
        {
            commands.CreateCommand("DMG")
                .Parameter("Who", ParameterType.Optional)
                .Do(async (e) =>
                {
                    Random rnd = new Random();
                    var dmgvalue = rnd.Next(1, 50);
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
