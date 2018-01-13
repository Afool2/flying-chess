using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fly_chess
{
    class Program
    {
        //静态字段模拟全局变量
        static int[] _Maps = new int[100];
        //静态数组存储玩家A和B的位置
        static int[] _PlayerPos = { 0, 0 };
        // 静态数组存储2个玩家的姓名
        static string[] _PlayersNames = { null, null };
        // 用于判断玩家是否暂停一回合
        static bool[] _PlayersFlag = { true, true };
        static void Main(string[] args)
        { 
            ShowHeadText();
            #region 输入玩家名称
            Console.WriteLine("请输入玩家A的名称");
            _PlayersNames[0] = Console.ReadLine();
            while (_PlayersNames[0] == "")
            {
                Console.WriteLine("玩家A的名称不能为空！请重新输入");
                _PlayersNames[0] = Console.ReadLine();
            }
            Console.WriteLine("请输入玩家B的名称");
            _PlayersNames[1] = Console.ReadLine();
            while (_PlayersNames[1] == "" || _PlayersNames[1] == _PlayersNames[0])
            {
                Console.WriteLine("玩家B的名称不能{0}！请重新输入", _PlayersNames[1] == "" ? "为空" : "与玩家A相同");
                _PlayersNames[1] = Console.ReadLine();
            }
            #endregion
            InitialMap();
            DrawMap();

            while (true)
            {
                if (_PlayersFlag[0]) PlayGame(0);
                else _PlayersFlag[0] = true;
                if (_PlayerPos[0] >= 99)
                {
                    Console.WriteLine("玩家{0}无耻地赢了玩家{1}", _PlayersNames[0], _PlayersNames[1]);
                    break;
                }

                if (_PlayersFlag[1]) PlayGame(1);
                else _PlayersFlag[1] = true;
                if (_PlayerPos[1] >= 99)
                {
                    Console.WriteLine("玩家{0}无耻地赢了玩家{1}", _PlayersNames[1], _PlayersNames[0]);
                    break;
                }
            }
            Win(); // 显示胜利文字
            Console.ReadKey();
        }

        /// <summary>
        /// 显示头部文本
        /// </summary>
        public static void ShowHeadText()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("****************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("****************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("********** 飞行棋 **********");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("****************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("****************************");

        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitialMap()
        {
            //数组存储特殊关卡在_Maps中的索引
            int[] luckyTurn = { 6, 23, 40, 55, 69, 83 };//幸运轮盘◎
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷☆
            int[] pause = { 9, 27, 60, 93 };//暂停▲
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//时空隧道卐

            //_Maps中，元素的值1,2,3,4分别表示幸运轮盘，地雷，暂停，时空隧道;0为默认普通关卡
            for (int i = 0; i < luckyTurn.Length; i++) _Maps[luckyTurn[i]] = 1;
            for (int i = 0; i < landMine.Length; i++) _Maps[landMine[i]] = 2;
            for (int i = 0; i < pause.Length; i++) _Maps[pause[i]] = 3;
            for (int i = 0; i < timeTunnel.Length; i++) _Maps[timeTunnel[i]] = 4;

        }

        /// <summary>
        /// 获取地图索引对应的图案字符串
        /// </summary>
        /// <param name="i">地图索引</param>
        /// <returns>字符串</returns>
        public static string GetPattenStr(int i)
        {
            //玩家A和玩家B的位置相同，并且位置都在地图上时，画<>
            if (_PlayerPos[0] == _PlayerPos[1] && _PlayerPos[0] == i) return "<>";
            // 如果i等于玩家A的位置,画A.(全角状态下的字符，占2个半角字符)
            else if (_PlayerPos[0] == i) return "Ａ";
            else if (_PlayerPos[1] == i) return "Ｂ"; // 如果i等于玩家B的位置，画B
            else // 画地图
            {
                switch (_Maps[i])
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        return "◎";
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        return "☆";
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Red;
                        return "▲";
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        return "卐";
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        return "□";
                }
            }
        }

        /// <summary>
        /// 画地图
        /// </summary>
        public static void DrawMap()
        {
            Console.WriteLine("图例:   幸运轮盘◎   地雷☆   暂停▲   时空隧道卐");
            // 画第一横行
            for (int i = 0; i < 30; i++) Console.Write(GetPattenStr(i));
            
            #region 画第一竖行
            for (int i = 30; i < 35; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < 29; j++)
                {
                    Console.Write("　");
                }
                Console.Write(GetPattenStr(i));
            }
            Console.WriteLine();
            #endregion

            // 画第二横行
            for (int i = 64; i >= 35; i--) Console.Write(GetPattenStr(i));
            Console.WriteLine();

            // 画第二竖行
            for (int i = 65; i < 70; i++) Console.WriteLine(GetPattenStr(i));

            // 画第三横行
            for (int i = 70; i < 100; i++) Console.Write(GetPattenStr(i));
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// 游戏主逻辑
        /// </summary>
        /// <param name="playerIndex">玩家在_PlayerPos中的索引</param>
        public static void PlayGame(int playerIndex)
        {
            Console.WriteLine("玩家{0}按任意键开始掷骰子", _PlayersNames[playerIndex]);
            Console.ReadKey(true);
            Random r = new Random();
            int x = r.Next(1, 7);
            Console.WriteLine("玩家{0}掷出了{1},按任意键开始行动", _PlayersNames[playerIndex], x);
            Console.ReadKey(true);
            _PlayerPos[playerIndex] += x;
            if(_PlayerPos[playerIndex]== _PlayerPos[1 - playerIndex]) // 踩到了对方
            {
                Console.WriteLine("玩家{0}踩到了玩家{1},玩家{1}退6格", _PlayersNames[playerIndex], _PlayersNames[1 - playerIndex]);
                _PlayerPos[1 - playerIndex] -= 6;
            }
            else //没有踩到对方
            {
                try
                {
                    switch (_Maps[_PlayerPos[playerIndex]])
                    {
                        case 1: //踩到幸运轮盘
                            Console.WriteLine("玩家{0}踩到了幸运轮盘\n请选择： 1---和玩家{1}交换位置  2---轰炸对方，使之退6格", _PlayersNames[playerIndex], _PlayersNames[1 - playerIndex]);
                            string option = Console.ReadLine();
                            while (option != "1" && option != "2")
                            {
                                Console.WriteLine("输入错误！请重新输入: 1---交换位置  2---轰炸对方");
                                option = Console.ReadLine();
                            }
                            if (option == "1")
                            {
                                int temp = _PlayerPos[playerIndex];
                                _PlayerPos[playerIndex] = _PlayerPos[1 - playerIndex];
                                _PlayerPos[1 - playerIndex] = temp;
                            }
                            else _PlayerPos[1 - playerIndex] -= 6;
                            break;
                        case 2:
                            Console.WriteLine("玩家{0}踩到了地雷，退6格", _PlayersNames[playerIndex]);
                            _PlayerPos[playerIndex] -= 6;
                            break;
                        case 3:
                            Console.WriteLine("玩家{0}踩到了暂停，暂停一回合", _PlayersNames[playerIndex]);
                            _PlayersFlag[playerIndex] = false;
                            break;
                        case 4:
                            Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", _PlayersNames[playerIndex]);
                            _PlayerPos[playerIndex] += 10;
                            break;
                        default:
                            Console.WriteLine("玩家{0}踩到了普通方块", _PlayersNames[playerIndex]);
                            break;
                    }
                }
                catch { }
            }
            Console.ReadKey(true);
            #region 任一玩家的位置超出地图时，回到正确位置
            // 后退超出地图(位置<0)
            if (_PlayerPos[playerIndex] < 0) _PlayerPos[playerIndex] = 0;
            if (_PlayerPos[1 - playerIndex] < 0) _PlayerPos[1 - playerIndex] = 0;

            //前进超出地图(位置>99)
            if (_PlayerPos[playerIndex] > 99) _PlayerPos[playerIndex] = 99;
            if (_PlayerPos[1 - playerIndex] > 99) _PlayerPos[1 - playerIndex] = 99;
            #endregion
            Console.Clear();
            DrawMap();
        }

        /// <summary>
        /// "胜利"文字
        /// </summary>
        public static void Win()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                          ◆                      ");
            Console.WriteLine("                    ■                  ◆               ■        ■");
            Console.WriteLine("      ■■■■  ■  ■                ◆■         ■    ■        ■");
            Console.WriteLine("      ■    ■  ■  ■              ◆  ■         ■    ■        ■");
            Console.WriteLine("      ■    ■ ■■■■■■       ■■■■■■■   ■    ■        ■");
            Console.WriteLine("      ■■■■ ■   ■                ●■●       ■    ■        ■");
            Console.WriteLine("      ■    ■      ■               ● ■ ●      ■    ■        ■");
            Console.WriteLine("      ■    ■ ■■■■■■         ●  ■  ●     ■    ■        ■");
            Console.WriteLine("      ■■■■      ■             ●   ■   ■    ■    ■        ■");
            Console.WriteLine("      ■    ■      ■            ■    ■         ■    ■        ■");
            Console.WriteLine("      ■    ■      ■                  ■               ■        ■ ");
            Console.WriteLine("     ■     ■      ■                  ■           ●  ■          ");
            Console.WriteLine("    ■    ■■ ■■■■■■             ■              ●         ●");
            Console.ResetColor();
        }

    }
}
