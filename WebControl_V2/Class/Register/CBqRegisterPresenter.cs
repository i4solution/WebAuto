using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bqInet;

namespace WebControl_V2
{
    public class LoginArgs : EventArgs
    {
        public string username;
        public string pass;
        public string email;
        public string phone;
    }
    public class CBqRegisterPresenter
    {
        IRegister _view;

        public CBqRegisterPresenter()
        {
            _view = new frmRegister();
            _view.OnLogin += new EventHandler<LoginArgs>(OnLogin);
            _view.OnRegister += new EventHandler<LoginArgs>(OnRegister);
        }
        public void Show()
        {
            if (_view == null)
                _view = new frmRegister();
            _view.Show();
        }
        public void OnRegister(object sender, LoginArgs e)
        {
            if (bqInet.CbqInet.RequestPOSTPhpDBWrite(e.username, e.pass, e.email, e.phone))
            {
                _view.UpdateStatus("OK");
            }
            else
            {
                _view.UpdateStatus("FAIL");
            }
        }
        public void OnLogin(object sender, LoginArgs e)
        {
            if (bqInet.CbqInet.RequestPOSTPhpDBRead(e.username, e.pass))
            {
                _view.UpdateStatus("OK");
            }
            else
            {
                _view.UpdateStatus("FAIL");
            }
        }
    }
}
