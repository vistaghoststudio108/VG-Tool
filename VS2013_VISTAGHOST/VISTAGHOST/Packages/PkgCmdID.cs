﻿// PkgCmdID.cs
// MUST match PkgCmdID.h
using System;

namespace Vistaghost.VISTAGHOST
{
    static class PkgCmdIDList
    {
        //public const uint cmdidSingle_tb = 0x0104;
        //public const uint cmdidSingle_mc = 0x0105;

        //public const uint cmdidMultiple_tb = 0x0106;
        //public const uint cmdidMultiple_mc = 0x0107;

        //public const uint cmdidDelete_tb = 0x0108;
        //public const uint cmdidDelete_mc = 0x0109;

        //public const uint cmdidCount_tb = 0x010a;
        //public const uint cmdidCount_mc = 0x010b;

        //public const uint cmdidConfig_mb = 0x010c;
        //public const uint cmdidAbout_mb = 0x010d;
        public const uint cmdidModTag = 0x0104;

        public const uint cmdidAddTag = 0x0105;

        public const uint cmdidDelTag = 0x0108;

        public const uint cmdidDelete = 0x0106;

        public const uint cmdidCount = 0x0107;

        public const uint cmdidConfig = 0x010c;

        public const uint cmdidAbout = 0x010d;

        public const uint cmdidMakeHeader = 0x0109;

        public const uint cmdidExportSettings = 0x010e;

        public const uint cmdidExportHistory = 0x010f;

        public const uint cmdidChangeInfo = 0x0110;

        public const uint cmdidCreateMultiHeader = 0x0111;

        public const uint cmdidCopyPrototype = 0x1110;

        public const uint cmdidExportFunc = 0x4440;
    };
}