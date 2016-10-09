using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StallionSuppyChain
{
    public static class Global
    {

         static Global()
         {
             UserId = 1;
         }

        public static int UserId { get; set; }
    }
}
