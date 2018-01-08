using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using SZHomeDLL;
using SZHome.Entity;

namespace SZHome.Common
{
    public class MSMQManager:IDisposable
    {
         #region 字段与属性
        private MessageQueue _msmq = null;
        private string _path;

        private static MSMQManager _instanceLocalMSMQ = new MSMQManager(true);
        /// <summary>
        /// 本机消息队列实例
        /// </summary>
        public static MSMQManager InstanceLocalMSMQ
        {
            get { return MSMQManager._instanceLocalMSMQ; }
        }

        private static MSMQManager _instance = new MSMQManager(false);
        /// <summary>
        /// 远程消息队列实例
        /// </summary>
        public static MSMQManager Instance
        {
            get { return MSMQManager._instance; }
        }
        #endregion

        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="transactional">是否启用事务</param>
        /// <returns></returns>
        public bool Create(bool transactional)
        {
            if (MessageQueue.Exists(@".\private$\" + (ConfigHelper.GetConfigString("MSMQName") ?? "CSMSMQ")))
            {
                return true;
            }
            else
            {
                if (MessageQueue.Create(@".\private$\" + (ConfigHelper.GetConfigString("MSMQName") ?? "CSMSMQ"), transactional) != null)
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
        /// 实例化消息队列
        /// </summary>
        /// <param name="isLocalComputer">是否为本机</param>
        public MSMQManager(bool isLocalComputer)
        {
            if (isLocalComputer)
            {
                _path = @".\private$\" + (ConfigHelper.GetConfigString("MSMQName") ?? "CSMSMQ");
            }
            else
            {
                _path = @"FormatName:DIRECT=TCP:192.168.1.125\private$\" + (ConfigHelper.GetConfigString("MSMQName") ?? "CSMSMQ");
            }

            _msmq = new MessageQueue(_path);
        }


        /// <summary>
        /// 发送消息队列
        /// </summary>
        /// <param name="msmqIndex">消息队列实体</param>
        /// <returns></returns>
        public void Send(msmqEntity msmqIndex)
        {
            try
            {
                _msmq.Send(new Message(msmqIndex, new BinaryMessageFormatter()));
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
            msmqEntity msmqIndex = null;
            _msmq.Formatter = new BinaryMessageFormatter();
            Message msg = null;
            try
            {
                msg = _msmq.Receive(new TimeSpan(0, 0, 1));
            }
            catch(Exception ex)
            {
                LogHelper.LogTrace(ex.Message);
            }
            if (msg != null)
            {
                msmqIndex = msg.Body as msmqEntity;
            }
            return msmqIndex;
        }

        #region IDisposable Members

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
