using System;

namespace Games
{
    class Program
    {
        static void Main(string[] args)
        {
            string plaerName = "Plaer";
            string npsName = "Тюркпигун";
            string[] spellName = { "Пронзание 21 копьём", "Развернём и нагнем ", "Пояс верности      ", "Жажда крови        ", "Великий шёрц       " };
            int[] spellStrike = { 100, 50, 25, 10, 500 };
            int[] spellStrikePlaer = { 1, 2, 3, 4, 5 };
            int[] spellStrikeNps = { 1, 2, 3, 4, 5 };
            string[] spellLegend = { "Наносит урон = "+spellStrike[0], "Усиливает удар "+spellName[0]+" на - "+spellStrike[2],
                                           "Игнорирует урон "+spellName[0]+" и возвращает "+spellStrike[2], "Возвращяет "+spellStrike[3]+" %Hp ", 
                                        "Супер удар производится после накопления Силы великого Шорца" };
            int healthPlaerStart = 1000;
            int healthNpsStart = 5000;
            int powerShorca = 1;
            int powerShorcaMax = 100;
            int powerShorcaIncrement = 10;
            int plaerplaerHpPercent = 100;
            int NpsHealthPercent = 100;
            bool plaerStartBool = false;
            int percent100 = 100;

            Console.WriteLine("Пошаговая игра Бой с босом");
            Console.Write("Выберите себе имя ");
            plaerName = Console.ReadLine();

            #region//Кто первый

            Console.ResetColor();
            Console.WriteLine("Вы великий "+plaerName+" шли по лесу и вдруг перед вами выскочил " + npsName);
            Console.ReadLine();
           
            int randomStart = 0;
            int randomStop = 9;
            Random randomNps = new Random();
            int plaerStart = randomNps.Next(randomStart, randomStop);
            int npsRandom = randomNps.Next(randomStart, randomStop);

            if(plaerStart>=npsRandom)
            {
                plaerStartBool = true;
                Console.WriteLine("Ваше быстры и вниметельны право первого хода ваше");
            }else
            {
                plaerStartBool = false;
                Console.WriteLine("Вы слепенький и немного тормоз " + npsName + " увидел вас первым");
            }

            Console.WriteLine("Готовтесь к бою");
            Console.ReadLine();
            #endregion

            bool endFight = true;

            while(endFight)
            {
                Console.ResetColor();
                Console.Clear();
                
                #region //Уровень жизни
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("Уровень вашей жизни = " + plaerplaerHpPercent+"%");
                int fitUnderWindow=2;

                for(int i=0;i<plaerplaerHpPercent / fitUnderWindow; i++)
                {
                    Console.Write(" ");
                }

                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("\nСила вашего Шорца   = " + powerShorca+"%");

                for (int i=0;i<powerShorca/2;i++)
                {
                    Console.Write(" ");
                }

                Console.Write("\n");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Уровень жизни босса = " + NpsHealthPercent + "%");

                for(int i = 0; i < NpsHealthPercent / 2; i++)
                {
                    Console.Write(" ");
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\n");

                #endregion
                #region//Памятка ударов

                Console.Write("\n");

                for(int i=0;i<spellName.Length;i++)
                {
                    Console.Write(spellName[i] + " " + spellLegend[i] +"\n");
                }

                Console.ReadLine();

                #endregion
                #region//запрос ударов

                int[] plaerHit = { 1, 2, 3 };
                int[] npsHit = { 1, 2, 3 };

                for(int i=0; i< plaerHit.Length;i++)
                {
                    Console.Write("Выберите действие " + i + " ");
                    string temp= Console.ReadLine();
                    plaerHit[i] = Convert.ToInt32(temp);
                    spellStrikePlaer[i] = spellStrike[plaerHit[i]];

                    if (plaerHit[i] == 5)
                    { 

                        if (powerShorca < powerShorcaMax)
                        {
                            plaerHit[i] = 1;
                            Console.Write("Cила Шорца не достаточна для удара, вы нанесли простой удар" + "\n");
                        }
                    }

                    if (plaerHit[i] == 3)
                    {
                        spellStrikePlaer[i] -= (spellStrike[0] - spellStrike[0]);
                    }

                    if (plaerHit[i] == 4)
                    {
                        plaerplaerHpPercent += (plaerplaerHpPercent / percent100 * spellStrike[4]);
                    }
                }
                randomStart = 0;
                randomStop = 2;

                for(int i=0; i< npsHit.Length;i++)
                {
                    npsHit[i] = randomNps.Next(randomStart, randomStop);
                    spellStrikeNps[i] = spellStrike[npsHit[i]];

                    if (npsHit[i] == 5)
                    {

                        if (powerShorca < powerShorcaMax)
                        {
                            plaerHit[i] = 1;
                        }
                    }

                    if (npsHit[i] == 3)
                    {
                        spellStrikeNps[i] -= (spellStrike[0] - spellStrike[0]);
                    }

                    if (npsHit[i] == 4)
                    {
                        NpsHealthPercent += (NpsHealthPercent / percent100 * spellStrike[4]);
                    }
                }


                #endregion
                #region//бой
                
                int npsOnePercent = healthNpsStart / percent100;
                int plaerOnePercent = healthPlaerStart / percent100;

                if(plaerStartBool)
                {
                    int tempHpPlaer = healthPlaerStart/percent100*plaerplaerHpPercent;
                    int tempHpNps = healthNpsStart/percent100*NpsHealthPercent;

                    for(int i=0;i< plaerHit.Length;i++)
                    {

                        if(powerShorca<powerShorcaMax)
                        {
                            powerShorca += powerShorcaIncrement;
                        }

                        tempHpNps -= spellStrikePlaer[npsHit[i]];

                        if (tempHpNps<=0)
                        {
                            endFight = false;
                            Console.WriteLine("Ха вы победили, и пошли дальше насвистывая ");
                            return;
                        }

                        tempHpPlaer -= spellStrike[plaerHit[i]];

                        if (tempHpPlaer <= 0)
                        {
                            endFight = false;
                            Console.WriteLine("Вы умерли, пусть земля вам будет гвозьдями");
                            return;
                        }

                        plaerplaerHpPercent = tempHpPlaer / plaerOnePercent;
                        NpsHealthPercent = tempHpNps / npsOnePercent;

                    }
                }else 
                {

                    int tempHpPlaer = healthPlaerStart / percent100 * plaerplaerHpPercent;
                    int tempHpNps = healthNpsStart / percent100 * NpsHealthPercent;

                    for (int i=0; i<npsHit.Length;i++)
                    {

                        tempHpPlaer -= spellStrikeNps[npsHit[i]];

                        if (tempHpPlaer <= 0)
                        {
                            endFight = false;
                            Console.WriteLine("Вы умерли, пусть земля вам будет гвозьдями");
                            return;
                        }

                        tempHpNps -= spellStrikePlaer[npsHit[i]];

                        if (tempHpNps <= 0)
                        {
                            endFight = false;
                            Console.WriteLine("Ха вы победили, и пошли дальше насвистывая ");
                            return;
                        }

                        plaerplaerHpPercent = tempHpPlaer / plaerOnePercent;
                        NpsHealthPercent = tempHpNps / npsOnePercent;
                    }
                }
                #endregion
            }
            #region
            Console.ResetColor();
            Console.WriteLine("");
            #endregion
        }
    }
}
