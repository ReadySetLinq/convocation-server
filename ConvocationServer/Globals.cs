﻿
namespace ConvocationServer
{
    static class Globals
    {
        static bool _debug_mode = false;
        public static bool DEBUG_MODE
        {
            set { _debug_mode = value; }
            get { return _debug_mode; }
        }

        public static int INVALID_INT = -763281543;
    }
}
