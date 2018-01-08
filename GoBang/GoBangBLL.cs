using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoBang
{
    public class GoBangBLL
    {

        /// <summary>
        /// 对象实例
        /// </summary>
        private static GoBangBLL _goBangInstance;

        /// <summary>
        /// 对象实例
        /// </summary>
        public static GoBangBLL GoBangInstance
        {
            get
            {
                if (_goBangInstance == null)
                {
                    return new GoBangBLL();
                }
                return _goBangInstance;
            }
        }

        /// <summary>
        /// 初始化棋盘
        /// </summary>
        /// <returns></returns>
        public static List<GoBangEntity> GetInitGoBangList()
        {
            List<GoBangEntity> list = new List<GoBangEntity>();
            for (int i = -14; i <= 14; i++)
            {
                for (int j = -14; j <= 14; j++)
                {
                    GoBangEntity entity = new GoBangEntity();
                    entity.PositionX = i;
                    entity.PositionY = j;
                    entity.Status = eStatusType.空;
                    list.Add(entity);
                }
            }
            return list;
        }

        /// <summary>
        /// 设置坐标状态
        /// </summary>
        /// <param name="currentEnt"></param>
        /// <param name="list"></param>
        public static void SetStaus(GoBangEntity currentEnt, ref List<GoBangEntity> list)
        {
            foreach (var item in list)
            {
                if (item.PositionX == currentEnt.PositionX && item.PositionY == currentEnt.PositionY)
                {
                    item.Status = currentEnt.Status;
                }
            }
        }

        /// <summary>
        /// 判断是否赢
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool JudgeIsWin(List<GoBangEntity> list, GoBangEntity currentEnt)
        {
            bool result = false;
            List<eDirectionType> typeList = new List<eDirectionType>() {
                eDirectionType.横轴,
                eDirectionType.斜45,
                eDirectionType.纵轴,
                eDirectionType.斜135
            };
            foreach (var item in typeList)
            {
                result = CatulateByDirection(list, currentEnt,item);
                if (result)
                {
                    break;
                }
            }
            return result;

        }

        /// <summary>
        ///按方向计算是否赢
        /// </summary>
        /// <param name="list"></param>
        /// <param name="currentEnt"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool CatulateByDirection(List<GoBangEntity> list, GoBangEntity currentEnt, eDirectionType type)
        {
            bool result = false;
            int count = 0;
            int beginX = currentEnt.PositionX - 5;
            int beginY = currentEnt.PositionY - 5;
            int cx = 0;
            int cy = 0;
            for (int i = 0; i < 11; i++)
            {
                if (type == eDirectionType.横轴)
                {
                    cx = beginX + i;
                    cy = currentEnt.PositionY;
                }
                else if (type == eDirectionType.斜45 || type == eDirectionType.斜135)
                {
                    cx = beginX + i;
                    cy = beginY + i;
                }
                else if (type == eDirectionType.纵轴)
                {
                    cx = currentEnt.PositionX;
                    cy = beginY + i;
                }

                GoBangEntity selectEnt = list.Where(x => x.PositionX == cx && x.PositionY == cy).FirstOrDefault();
                if (selectEnt.Status == currentEnt.Status)
                {
                    count++;
                    if (count >= 5)
                    {
                        result = true;
                        break;
                    }
                }
                else {
                    count = 0;
                    continue;
                }
            }
            return result;
        }

    }
    /// <summary>
    /// 棋子状态
    /// </summary>
    public enum eStatusType
    {
        空 = 0,
        黑 = 1,
        白 = 2
    }

    /// <summary>
    /// 计算结果方向类型
    /// </summary>
    public enum eDirectionType
    {
        横轴 = 1,
        斜45 = 2,
        纵轴 = 3,
        斜135 = 4
    }

    public class GoBangEntity
    {
        /// <summary>
        /// 横坐标
        /// </summary>
        public int PositionX { get; set; }
        /// <summary>
        /// 纵坐标
        /// </summary>
        public int PositionY { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public eStatusType Status { get; set; }
    }
}
