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
        void Exit();
    }
}
