using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebControl_V2
{
    public interface IRegister
    {
        event EventHandler<LoginArgs> OnRegister;
        event EventHandler<LoginArgs> OnLogin;
        void Show();
        void UpdateStatus(string status);
    }
}
