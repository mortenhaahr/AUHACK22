using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GonnaCatchThemAll.Helpers
{
    public static class Delegates
    {
        public delegate void TransitionDelegate();

        public delegate void UserDelegate(WebAPI.User user);
    }
}
