using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using SZHomeDLL;
using SZHome.Entity;

namespace SZHome.Common
{
    public class MSMQHelp : IDisposable
    {
        #region 字段与属性
        /// <summary>
        /// 消息对象实例
        /// </summary>
        private MessageQueue _msmq = null;
        ///// <summary>
        ///// 路径
        ///// </summary>
        //private string _path;

        ///// <summary>
        ///// 队列名称
        ///// </summary>
        //public string msmqName
        //{
        //    get;
        //    set;
        //}
        ///// <summary>
        ///// 远程队列ip 
        ///// </summary>
        //public string tcp { get; set; }

        #endregion

        /// <summary>
        /// 实例化本机消息队列
        /// </summary>
        /// <param name="msmqName">队列名称</param>
        public MSMQHelp(string msmqName)
        {
            string path = @".\private$\" +msmqName;
            _msmq = new MessageQueue(path);
        }
        /// <summary>
        /// 实例化远程消息队列
        /// </summary>
        /// <param name="msmqName">队列名称</param>
        /// <param name="tcp">tcp</param>
        public MSMQHelp(string msmqName, string tcp)
        {
             string path = @"FormatName:DIRECT=TCP:" + tcp + "\\private$\\" + msmqName;
             _msmq = new MessageQueue(path);
        }


        /// <summary>
        /// 创建本地队列
        /// </summary>
        /// <param name="msmqName">队列名称</param>
        /// <param name="transactional">是否启用事务</param>
        /// <returns></returns>
        public static bool CreateQueue(string msmqName, bool transactional)
        {
            string path = @".\private$\" +msmqName;
            if (MessageQueue.Exists(path))
            {
                return true;
            }
            else
            {
                if (MessageQueue.Create(path) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 创建远程队列
        /// </summary>
        /// <param name="msmqName">队列名称</param>
        /// <param name="tcp">tcp</param>
        /// <param name="transactional">是否启用事务</param>
        /// <returns></returns>
        public static bool CreateQueue(string msmqName, string tcp, bool transactional)
        {
            string path = @"FormatName:DIRECT=TCP:" + tcp + "\\private$\\" + msmqName;
            if (MessageQueue.Exists(path))
            {
                return true;
            }
            else
            {
                if (MessageQueue.Create(path) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 发送消息队列
        /// </summary>
        /// <param name="msmqIndex">消息队列实体</param>
        /// <returns></returns>
        public void Send(msmqEntity msmqEnt)
        {
            try
            {
                _msmq.Send(new Message(msmqEnt, new BinaryMessageFormatter()));
            }
            catch (Exception ex)
            {
                LogHelper.LogTrace(ex.Message);
            }

        }

        /// <summary>
        /// 接收消息队列,删除队列
        /// </summary>
        /// <returns></returns>
        public msmqEntity ReceiveAndRemove()
        {
            msmqEntity msmqEnt = null;
            _msmq.Formatter = new BinaryMessageFormatter();
            Message msg = null;
            try
            {
                msg = _msmq.Receive(new TimeSpan(0, 0, 1));
            }
            catch (Exception ex)
            {
                LogHelper.LogTrace(ex.Message);
            }
            if (msg != null)
            {
                msmqEnt = msg.Body as msmqEntity;
            }
            return msmqEnt;
        }
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<msmqEntity> GetAllMessage()
        {
            List<msmqEntity> list = new List<msmqEntity>();
            try
            {
                Message[] message = _msmq.GetAllMessages();
                if (message != null && message.Length > 0)
                {
                    foreach (Message item in message)
                    {
                        list.Add((msmqEntity)item.Body);
                    }
                }
            }
            catch (Exception ex)
            {

                LogHelper.LogTrace(ex.Message);
            }
           
            return list;
        }

        #region IDisposable Members
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_msmq != null)
            {
                _msmq.Close();
                _msmq.Dispose();
                _msmq = null;
            }
        }

        #endregion
    }
}
