using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LockWindow
{
    static class Program
    {
        private static Boolean lockState = false; //保存当前的锁定状态
        private static System.Windows.Forms.Timer appTimer = new Timer(); //计时器
        private static int iTimeLen = 0; //时间计数

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new LockDesk());
            RegisterAppLockProcess();
        }

        /// <summary>
        /// 注册应用程序的锁定处理
        /// </summary>
        private static void RegisterAppLockProcess()
        {
            appTimer.Interval = 1000; //每间隔一秒执行一次检查
            appTimer.Tick += new EventHandler(CheckLockState);
            appTimer.Start();

            LockMessager lm = new LockMessager();

            Application.AddMessageFilter(lm);
        }

        /// <summary>
        /// 循环检查锁定状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CheckLockState(object sender, EventArgs e)
        {
            if (lockState) return;

            iTimeLen = iTimeLen + 1;

            //当五秒内没有接收到鼠标和键盘的按键，则进行锁定
            if (iTimeLen >= 5)
            {
                LockProcess();

                ShowLockWindow();
            }
        }

        /// <summary>
        /// 显示锁定后的界面
        /// </summary>
        private static void ShowLockWindow()
        {
            //这里简单显示出锁定后的界面
            LockDesk frmLock = new LockDesk();
            frmLock.ShowDialog();
        }

        public static void LockProcess()
        {
            lockState = true; //使当前状态为加锁状态

            //简单的锁定处理....
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }
        }

        public static void UnLockProcess()
        {
            lockState = false; //使当前状态为解锁状态

            //简单的解锁处理....
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Show();
            }
            //解锁后时间计数归0，重新开始计数
            iTimeLen = 0;
        }


        internal class LockMessager : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                //如果检测到有鼠标或则键盘被按下的消息，则使计数为0.....
                //0x0201  WM_LBUTTONDOWN   0x0100  WM_KEYDOWN   0x0204  WM_RBUTTONDOWN  0x0207  WM_MBUTTONDOWN
                //0x0216  WM_MOVING  0x0200 WM_MOUSEMOVE
                if (m.Msg == 0x0201 
                    || m.Msg == 0x0100 
                    || m.Msg == 0x0204 
                    || m.Msg == 0x0207 
                    || m.Msg == 0x0216 
                    || m.Msg == 0x0200)
                    iTimeLen = 0;

                return false;
            }
        }

    }
}
