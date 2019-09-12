using System;
using System.Runtime.InteropServices;

namespace SharpSniper
{
    public static class DomainInformation
    {
        [DllImport("Netapi32.dll")]
        static extern int NetApiBufferFree(IntPtr Buffer);

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int NetGetJoinInformation(
          string server,
          out IntPtr domain,
          out NetJoinStatus status);

        // Win32 Result Code Constant
        const int ErrorSuccess = 0;

        public enum NetJoinStatus
        {
            NetSetupUnknownStatus = 0,
            NetSetupUnjoined,
            NetSetupWorkgroupName,
            NetSetupDomainName
        }

        // Returns the domain name the computer is joined to, or "" if not joined.
        public static string GetDomainOrWorkgroup()
        {
            int result = 0;
            string domain = null;
            IntPtr pDomain = IntPtr.Zero;
            NetJoinStatus status = NetJoinStatus.NetSetupUnknownStatus;
            try
            {
                result = NetGetJoinInformation(null, out pDomain, out status);
                if (result == ErrorSuccess)
                    switch (status)
                    {
                        case NetJoinStatus.NetSetupDomainName:
                        case NetJoinStatus.NetSetupWorkgroupName:
                            domain = Marshal.PtrToStringAuto(pDomain);
                            break;
                    }
            }
            finally
            {
                if (pDomain != IntPtr.Zero)
                    NetApiBufferFree(pDomain);
            }
            if (domain == null) domain = "";
            return domain;
        }
    }
}