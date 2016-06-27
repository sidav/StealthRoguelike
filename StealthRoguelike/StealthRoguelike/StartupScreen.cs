using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class StartupScreen
    {
        static string[] image = new string[]
        {
                @"           o+ommmy++:        ",
                @"       +mdmmmmmmmmmmms+      ",
                @"     +mmmmmmmmmmmmmmmmmm.    ",
                @"    hmmmmmmmmmmmmmmmmmmmm-   ",
                @"   smmmmmmmmmmmmmmmmmmmmmm   ",
                @"   mmmmmmmmmmmmmmmmmmmmmmdo  ",
                @"   mmmmmmmmmmmmmmmmmmmmmmmN  ",
                @"   dmmmmmdmd+++++smmmmmmmmm  ",
                @"   mmmmm+o+hmmmmmm+++sdmmmN  ",
                @"   mmm++ommdmmmmmdmmys.mmdh  ",
                @"  ymmh+h.   /mmmm\   .m mmm  ",
                @"  mmdysmmdmmmmmmmmmmmmm mmmm ",
                @" mmmmm`+oo/.``..``-+o+/hmmmd+",
                @"ymmmmmm`.............`ommmmmm",
                @":mmmmmm ............. dmmmmmm",
                @" ommmmN ..............-mmmmm ",
                @"  hmmmmd+`.........``+mmmmm. ",
                @"   +mmmmmm+`.....``+mmmmms+  ",
                @"     -+++++o......++++++     ",
        };

        static string[] image2 = new string[]
        {
                @"   ▄▄▄▄   ▄  █ ██   ██▄   ███▄   ▄ ▄      █ ▄▄  █▄▄▄▄ ▄█ ▄███▄     ▄▄▄▄     ▄▄▄▄",
                @"  █    ▀ █   █ █ █  █  █  █  █  █   █     █   █ █  ▄▀ ██ █▀   ▀   █    ▀▄ ▀▀ █  ",
                @"▄  ▀▀▀▄  ██▀▀█ █▄▄█ █   █ █  █ █ ▄   █    █▀▀▀  █▀▀▌  ██ ██▄▄   ▄  ▀▀▀▄      █  ",
                @" ▀▄▄▄▀   █   █ █  █ █  █  █  █ █  █  █    █     █  █  ▐█ █▄   ▄  ▀▄▄▄▀      █   ",
                @"            █     █ ███▀  ▀███  █ █ █      █      █    ▐ ▀███▀             ▀    ",
                @"           ▀     █               ▀ ▀        ▀    ▀                              ",
                @"                ▀                                                               ",
        };

        static string[] image3 = new string[]
        {
                @" @@@@ @  @@  @@@@  @@@@@   @@@@  @@ @@ @@     @@@@  @@@@@  @@ @@@@@  @@@@ @@@@",
                @"!@    @  @@ @!  @@ @!  @@ @!  @@ @@ @! @@     @! @@ @!  @! @! @@    !@     @! ",
                @" !@!  @!@!! @@!@!! @@  !! @@  !! @! !@ @!     @@!!  @@!!!  !@ @!!!   !@!   @! ",
                @"   !! :  !! !:  !! !:  !! !!  !! !: !: !!     !:    !! :.  !: !!       !!  !: ",
                @":::   :   : :   :: ::,,:   ::.    :.  ::      .     :   :.  :  :::  ..:::  !  ",
        };

        static string[] image4 = new string[]
        {
                @"    :    #                                                 :!                  ",
                @"@@###  :@           !@!           !                        #             : #   ",
                @"@!  /@  @  #   @#     @@:     :   @#  !  :      :  !  ! :  :    :     !  !@@## ",
                @"@! /    @@@@#   !@@  @# @@  @ #@# :@! @#::@    !@@@@# @!# #@  #@ @# @@###  @#  ",
                @" @!#@!  @  @@  @ ##  @# !@  @  ##  @: @@ :@     @  @@ @*  !#  !@  # @! /   @#  ",
                @" /  @@  @  @@ @  ##  @# !@  @  #!  @: @@ :@     @  @@ @   !#  !@@   #@!#@! @#  ",
                @"@   @!  @  @@ @  #@  @# :@  @  #!  @: @@ :@     @  @# @   !#  #@  #  /  @! @#  ",
                @"#@@@#  #@# @# @@*#@! !@@#: :#@@@   #@@@@@#:    !@@@@  @@! !@# @@@#@ #@@@#  @@# ",
                @"                                                @                              ",
                @"                                               !@#                             "
        };

        public static void DrawImage1StartupScreen()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string gameName = "---== SHADOW PRIEST ==---";
            int cursorXstart = Program.consoleWidth / 2 - gameName.Length / 2;
            Console.SetCursorPosition(cursorXstart, 1);
            Console.Write(gameName);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            cursorXstart = Program.consoleWidth / 2 - image[0].Length / 2;
            for (int i = 0; i < image.Length; i++)
            {
                Console.SetCursorPosition(cursorXstart, i + 3);
                Console.Write(image[i]);
            }
        }

        public static void DrawImage2StartupScreen()
        {
            string[] Art = image2;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            int cursorXstart = Program.consoleWidth / 2 - Art[0].Length / 2;
            int cursorYstart = Program.consoleHeight / 2 - Art.Length / 2 - 1;
            for (int i = 0; i < Art.Length; i++)
            {
                Console.SetCursorPosition(cursorXstart, i + cursorYstart);
                Console.Write(Art[i]);
            }
        }

        public static void DrawImage3StartupScreen()
        {
            string[] Art = image3;

            Console.ForegroundColor = ConsoleColor.Red;
            int cursorXstart = Program.consoleWidth / 2 - Art[0].Length / 2;
            int cursorYstart = Program.consoleHeight / 2 - Art.Length / 2 - 1;
            for (int i = 0; i < Art.Length; i++)
            {
                Console.SetCursorPosition(cursorXstart, i + cursorYstart);
                Console.Write(Art[i]);
            }
        }

        public static void DrawImage4StartupScreen()
        {
            string[] Art = image4;

            Console.ForegroundColor = ConsoleColor.Red;
            int cursorXstart = Program.consoleWidth / 2 - Art[0].Length / 2;
            int cursorYstart = Program.consoleHeight / 2 - Art.Length / 2 - 1;
            for (int i = 0; i < Art.Length; i++)
            {
                Console.SetCursorPosition(cursorXstart, i + cursorYstart);
                Console.Write(Art[i]);
            }
        }

        public static void ShowSplashScreen()
        {
            int WhatToDraw = Algorithms.getRandomInt(0, 4);
            if (WhatToDraw == 0)
                DrawImage1StartupScreen();
            if (WhatToDraw == 1)
                DrawImage2StartupScreen();
            if (WhatToDraw == 2)
                DrawImage3StartupScreen();
            if (WhatToDraw == 3)
                DrawImage4StartupScreen();

            //DrawImage4StartupScreen();

            Console.ForegroundColor = ConsoleColor.Gray;
            string anykey = "Press any key";
            int cursorXstart = Program.consoleWidth / 2 - anykey.Length / 2;
            Console.SetCursorPosition(cursorXstart, Program.consoleHeight-1);
            Console.Write(anykey);
            Console.ReadKey(true);

        }
    }
}
