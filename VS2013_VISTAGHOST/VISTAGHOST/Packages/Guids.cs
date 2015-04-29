// Guids.cs
// MUST match guids.h
using System;

namespace Vistaghost.VISTAGHOST
{
    static class GuidList
    {
        public const string guidVISTAGHOSTPkgString = "1c746f75-ef46-4b33-8640-04468e0af8a3";
        public const string guidVISTAGHOSTCmdSetString = "327bd6eb-f9a6-4fce-9733-7e3e8815ac11";

        //for window pane
        public const string guidVISTAGHOSTWindow = "4813b710-088a-4605-b79e-c3681fb89b4a";

        public static readonly Guid guidVISTAGHOSTCmdSet = new Guid(guidVISTAGHOSTCmdSetString);
    };
}