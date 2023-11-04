using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Finist.MyContacts.Service
{
    public class WindowService:IWindowService
    {
        private readonly Window _window;

      public   WindowService(Window window)
        {
            _window=window;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

      

        public void DragMove()
        {
            WindowInteropHelper helper = new WindowInteropHelper(_window);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        
        public void MaximizeHeight() => _window.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
       
           
        

        public void MaximizeWindow()
        {
            if (_window.WindowState == WindowState.Normal)
            {
                _window.WindowState = WindowState.Maximized;
            }
            else
            {
                _window.WindowState= WindowState.Normal;    
            }
        }
        public void CloseWindow() => Application.Current.Shutdown();
        public void MinimizeWindow()=> _window.WindowState=WindowState.Minimized;   
    }
}
