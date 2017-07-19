using Discord;
using Discord.Commands;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Ame's Battle Bot
namespace Discordbot
{
    class Mybot
    {
        DiscordClient discord;
        CommandService commands;
        public int RoundNUM = 0;
        public string CONT = "```";
        // CONT + "Text" + CONT
        public Fighter[] FightList = new Fighter[100];

        public Mybot()
        {
            

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
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
            });
            commands = discord.GetService<CommandService>();

            //Commands Classes go here
            Ping();
            ClearMSG();
            
            //Fighter stuff
            RefisterFighterdodge();
            RefisterFighterattack();
            Percent();
            //VS();
            


            Round();

            //Stuff used to connect it to the server
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzAyNTExOTY0OTcyNzc3NDgz.C9NOVA.intlnTR8dIUFEFgW9Y09iaoPS7Y", TokenType.Bot);
                Game g = new Game("with something kinda lewd~", GameType.Default, "https://www.youtube.com/playlist?list=PLszcSON51lz1qLvyggPS0Xt51Y5XCIM5e");
                discord.SetGame(g);
            });
            

        }
       
        public void Comabt()
        {
            //string[] lines = File.ReadAllLines(@"C:\Users\dee\Documents\GitHub\Discord_Bot_WV\Discordbot\Discordbot\txt\FBoard.txt", Encoding.UTF8);
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
                    Random rnd = new Random((int)DateTime.Now.Ticks);
                    float BasePower;
                    float Mod;
                    if (Single.TryParse(e.GetArg("Basepower"), out BasePower))
                    {
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
                            int crit = rnd.Next(2, 7);
                            
                            string Crittype = "";
                            //2=Nice, 3=Good, 4=Perfect
                            if (crit == 2)
                            {
                                BasePower *= 1.25f;
                                Crittype = "Nice";
                            }
                            else if (crit == 3)
                            {
                                BasePower *= 1.5f;
                                Crittype = "Good";
                            }
                            else if (crit == 4)
                            {
                                BasePower *= 1.5f;
                                Crittype = "Excelent";
                            }
                            else if (crit == 5)
                            {
                                BasePower *= 1.75f;
                                Crittype = "Perfect";
                            }
                            else if (crit == 6)
                            {
                                BasePower *= 2f;
                                Crittype = "Row Row Fight the power";
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
                    Random rnd = new Random((int)DateTime.Now.Ticks);
                    int Min = 5;
                    if (Int32.TryParse(e.GetArg("Min"), out Min)) { }
                    int NUM = rnd.Next(11);
                    string Rolled = NUM.ToString();
                    if(NUM < Min)
                    {
                        await e.Channel.SendMessage(CONT + e.GetArg("Foe") + " failed to dodge " + e.GetArg("User") + "'s attack(" + Rolled + ")```");
                    }
                    else
                    {
                        await e.Channel.SendMessage(CONT + e.GetArg("Foe") + " dodged " + e.GetArg("User") + "'s attack(" + Rolled + ")```");
                    }
                });
        }
        //Pesentage Roller *NEW*
        private void Percent()
        {
            commands.CreateCommand("perc")
                .Description("Chance Roller, X% Condition Target")
                .Parameter("Chance", ParameterType.Required)
                .Parameter("Condition", ParameterType.Required)
                .Parameter("Target", ParameterType.Required)
                .Do(async (e) =>
                {
                    Random rnd = new Random((int)DateTime.Now.Ticks);
                    int Chance = 0;
                    if (Int32.TryParse(e.GetArg("Chance"), out Chance)) { }
                    int Roll = rnd.Next(1, 101);
                    if (Roll <= Chance)
                    {
                        await e.Channel.SendMessage("`" + e.GetArg("Target") + " became " + e.GetArg("Condition") + "`");
                    }
                    else
                    {
                        await e.Channel.SendMessage("`Nothing Happend`");
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
        private void VS()
        {
            /*
            commands.CreateCommand("VS")
                .Description("Keeps track of rounds")
                .Do(async (e) =>
                {
                    

                    await e.Channel.SendMessage("");
                });
                */
            commands.CreateCommand("AddPC")
                .Description("Name [T/F] HP")
                .Parameter("Name",ParameterType.Required)
                .Parameter("Foe", ParameterType.Required)
                .Parameter("HP",ParameterType.Required)
                .Do(async (e) =>
                {
                    bool IsFoe = e.GetArg("Foe") == "true";
                    int HP;

                    if (int.TryParse(e.GetArg("HP"), out HP))
                    {
                        BattleSYS.MakeFighter(FightList, e.GetArg("Name"), IsFoe, HP);

                        string REV = BattleSYS.ShowList(RoundNUM, FightList);
                        await e.Channel.SendMessage(REV);
                    }
                    else
                    {
                        await e.Channel.SendMessage("You broke it");
                    }

                        
                });

        }



        //End
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
