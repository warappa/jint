﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Jint.Native;
using Jint.Runtime.Interop;

namespace Jint
{
    public class Options
    {
        private bool _discardGlobal;
        private bool _discardRecursion;
        private bool _strict;
        private bool _allowDebuggerStatement;
        private bool _allowClr;
        private readonly List<IObjectConverter> _objectConverters = new List<IObjectConverter>();
        private int _maxStatements;
        private int _maxRecursionDepth;
        private TimeSpan _timeoutInterval;
        private CultureInfo _culture = CultureInfo.CurrentCulture;
        private TimeZoneInfo _localTimeZone = TimeZoneInfo.Local;
        private List<Assembly> _lookupAssemblies = new List<Assembly>(); 

        /// <summary>
        /// When called, doesn't initialize the global scope.
        /// Can be useful in lightweight scripts for performance reason.
        /// </summary>
        public Options DiscardGlobal(bool discard = true)
        {
            _discardGlobal = discard;
            return this;
        }

        /// <summary>
        /// When called, doesn't allow to use recursion.
        /// Can be useful when you can not trust to author of the script and safety has higher priority. 
        /// </summary>
        public Options DiscardRecursion(bool discard = true)
        {
            _discardRecursion = discard;
            return this;
        }

        /// <summary>
        /// Run the script in strict mode.
        /// </summary>
        public Options Strict(bool strict = true)
        {
            _strict = strict;
            return this;
        }

        /// <summary>
        /// Allow the <code>debugger</code> statement to be called in a script.
        /// </summary>
        /// <remarks>
        /// Because the <code>debugger</code> statement can start the 
        /// Visual Studio debugger, is it disabled by default
        /// </remarks>
        public Options AllowDebuggerStatement(bool allowDebuggerStatement = true)
        {
            _allowDebuggerStatement = allowDebuggerStatement;
            return this;
        }

        /// <summary>
         /// Adds a <see cref="IObjectConverter"/> instance to convert CLR types to <see cref="JsValue"/>
        /// </summary>
        public Options AddObjectConverter(IObjectConverter objectConverter)
        {
            _objectConverters.Add(objectConverter);
            return this;
        }

        /// <summary>
        /// Allows scripts to call CLR types directly like <example>System.IO.File</example>
        /// </summary>
        public Options AllowClr(params Assembly[] assemblies)
        {
            _allowClr = true;
            _lookupAssemblies.AddRange(assemblies);
            _lookupAssemblies = _lookupAssemblies.Distinct().ToList();
            return this;
        }

        public Options MaxStatements(int maxStatements = 0)
        {
            _maxStatements = maxStatements;
            return this;
        }
        
        public Options TimeoutInterval(TimeSpan timeoutInterval)
        {
            _timeoutInterval = timeoutInterval;
            return this;
        }

        public Options MaxRecursionDepth(int maxRecursionDepth = 0)
        {
            _maxRecursionDepth = maxRecursionDepth;
            return this;
        }

        public Options Culture(CultureInfo cultureInfo)
        {
            _culture = cultureInfo;
            return this;
        }

        public Options LocalTimeZone(TimeZoneInfo timeZoneInfo)
        {
            _localTimeZone = timeZoneInfo;
            return this;
        }

        internal bool GetDiscardGlobal()
        {
            return _discardGlobal;
        }

        internal bool IsStrict()
        {
            return _strict;
        }

        internal bool IsDebuggerStatementAllowed()
        {
            return _allowDebuggerStatement;
        }

        internal bool IsClrAllowed()
        {
            return _allowClr;
        }
        
        internal IList<Assembly> GetLookupAssemblies()
        {
            return _lookupAssemblies;
        }

        internal IEnumerable<IObjectConverter> GetObjectConverters()
        {
            return _objectConverters;
        }

        internal int GetMaxStatements()
        {
            return _maxStatements;
        }

        internal int GetMaxRecursionDepth()
        {
            return _maxRecursionDepth;
        }

        internal bool IsRecursionAllowed()
        {
            return !_discardRecursion;
        }

        internal TimeSpan GetTimeoutInterval()
        {
            return _timeoutInterval;
        }

        internal CultureInfo GetCulture()
        {
            return _culture;
        }

        internal TimeZoneInfo GetLocalTimeZone()
        {
            return _localTimeZone;
        }
    }
}
