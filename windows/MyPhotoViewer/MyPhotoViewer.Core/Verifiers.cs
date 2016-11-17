﻿using System;

namespace MyPhotoViewer.Core
{
    public static class Verifiers
    {
        public static void Verify(bool b, string messageFormat, params object[] objects)
        {
            if (!b)
            {
                throw new Exception(string.Format(messageFormat, objects));
            }
        }

        public static void Verify(bool b, Func<Exception> exceptionCreator)
        {
            if (!b)
            {
                throw exceptionCreator();
            }
        }

        public static void ArgVerify(bool b, string paramName, string messageFormat = "", params object[] objects)
        {
            if (!b)
            {
                throw new ArgumentException(string.Format(messageFormat, objects), paramName);
            }
        }

        public static void ArgNullVerify(object arg, string argName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(argName));
            }
        }
    }
}