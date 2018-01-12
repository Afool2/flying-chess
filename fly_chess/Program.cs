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

        static void Main(string[] args)
        {
            ShowHeadText();
            InitialMap();
            DrawMap();
            
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
        /// 画地图
        /// </summary>
        public static void DrawMap()
        {
            // 画第一横行
            for (int i = 0; i < 30; i++) GetPattenStr(i);

            // 画第一竖行
            for (int i = 30; i < 35; i++)
            {
                Console.WriteLine();
                for(int j = 0; j < 29; j++)
                {
                    Console.Write("　");
                }
                GetPattenStr(i);
            }
            
        }

        /// <summary>
        /// 画图案
        /// </summary>
        /// <param name="i">地图数组的索引</param>
        public static string GetPattenStr(int i)
        {
            //玩家A和玩家B的位置相同，并且位置都在地图上时，画<>
            if (_PlayerPos[0] == _PlayerPos[1] && _PlayerPos[0] == i)
            {
                return "<>";
            }
            else if (_PlayerPos[0] == i) // 如果i等于玩家A的位置,画A
            {
                return "Ａ"; // 全角状态下的字符，占2个半角字符
            }
            else if (_PlayerPos[1] == i) // 如果i等于玩家B的位置，画B
            {
                return "Ｂ";
            }
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
    }
}
