using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finist.MyContacts.Service
{
    public interface IWindowService
    {
       void DragMove();
       void MaximizeHeight();
       void MinimizeWindow();
       void CloseWindow();
       void MaximizeWindow();
    }
}
