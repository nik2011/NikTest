using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SZHome.Entity
{
    public class HandleResult
    {
        public HandleResult(int stats, string message)
        {
            this.StatsCode = stats;
            this.Message = message;
        }

        public HandleResult(int stats, string message, object data)
        {
            this.StatsCode = stats;
            this.Message = message;
            this.Data = data;
        }

        public HandleResult()
        {
            this.StatsCode = 500;
            this.Message = "失败";
        }

        public int StatsCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string Other { get; set; }

    }
}
