﻿using System.Collections.Generic;
using MaterialIconsGenerator.Core;

namespace MaterialIconsGenerator.Providers.Google
{
    public class GoogleiOSIconsProvider : GoogleIconsProvider
    {
        private List<ISize> _sizes = new List<ISize>()
        {
            new GoogleSize{ Id = "18", Name ="18pt" },
            new GoogleSize{ Id = "24", Name ="24pt" },
            new GoogleSize{ Id = "36", Name ="36pt" },
            new GoogleSize{ Id = "48", Name ="48pt" }
        };

        private IEnumerable<string> _densities = new List<string>()
        {
            "1x",
                "2x",
                "3x"
        };


        public override IEnumerable<ISize> GetSizes() =>
            this._sizes;

        public override IEnumerable<string> GetDensities() =>
            this._densities;

        public override IProjectIcon CreateProjectIcon(IIcon icon, IIconColor color, ISize size, string density) =>
            new GoogleiOSProjectIcon(icon, color, size, density);
    }
}
