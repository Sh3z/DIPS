using Database.Connection;
using Database.Objects;
using DIPS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DicomHelper
{
    public class Logger
    {
        public void start()
        {
            Log.Created = false;
            Log.NeedUpdate = false;

            if (Log.CodecRegistration == false)
            {
                Codec codec = new Codec();
                codec.register();
                Log.CodecRegistration = true;
            }
        }
    }
}
