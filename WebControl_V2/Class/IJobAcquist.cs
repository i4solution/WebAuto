using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bqImplementWeb;

namespace WebControl_V2.Class
{
    public interface IJobAcquist
    {
        void DoJob(bqService service);

        void DoJobTest(bqService service);

        bool LogOutLinkAccount(bqService service);
        void Exit();

        bool LikeArticel
        {
            set;get; 
        }
        bool LikePage
        {
            set;
            get; 
        }
        bool FollowPage
        {
            set;
            get; 
        }
        CLinkAccount LinkAccount
        {
            set;
            get; 
        }
        IUpdateInterface UpdateGUI
        {
            set;
        }
    }
}
