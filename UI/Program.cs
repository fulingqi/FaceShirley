using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UI
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        //public static MyMessageFilter msgFilter = new MyMessageFilter();
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Process instance = RunningInstance();
            if (instance == null)
            {
                //copyDLL();              
                Application.EnableVisualStyles();
                Application.Run(new FaceRegister());//刷脸注册外显



               //Application.Run(new FaceRegis());//刷脸注册
                //Application.Run(new TS_Mask());//刷脸注册
               // Application.Run(new BrushFace());//刷脸就医
                //Application.Run(new ManualInput());//刷脸就医—人工输入
                //Application.Run(new FaceBrush());
                //Application.Run(new MingYiPrivacyPolicy());
            }
            else
            {
                //MessageBox.Show("你已经运行了该程序！");
                HandleRunningInstance(instance);
            }
        }

        public static Process RunningInstance()
        {

            Process current = Process.GetCurrentProcess();

            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //遍历正在有相同名字运行的例程 

            foreach (Process process in processes)
            {

                //忽略现有的例程 

                if (process.Id != current.Id)
                {

                    //确保例程从EXE文件运行 

                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") ==

                         current.MainModule.FileName)
                    {

                        //返回另一个例程实例 

                        return process;

                    }

                }

            }

            //没有其它的例程，返回Null 

            return null;

        }

        public static void HandleRunningInstance(Process instance)
        {

            //确保窗口没有被最小化或最大化 

            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);



            //设置真实例程为foreground window 

            SetForegroundWindow(instance.MainWindowHandle);

        }
    }
}
