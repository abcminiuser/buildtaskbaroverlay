// Guids.cs
// MUST match guids.h
using System;

namespace FourWalledCubicle.BuildTaskbarOverlay
{
    static class GuidList
    {
        public const string guidBuildTaskbarOverlayPkgString = "b5963c5d-2ceb-49a5-8dd1-dee8f3bb1e81";
        public const string guidBuildTaskbarOverlayCmdSetString = "f7fe8ecd-5915-421b-874c-0912cfe50921";

        public static readonly Guid guidBuildTaskbarOverlayCmdSet = new Guid(guidBuildTaskbarOverlayCmdSetString);
    };
}