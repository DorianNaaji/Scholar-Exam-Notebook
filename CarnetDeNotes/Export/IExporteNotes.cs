using System;
using System.Collections.Generic;
using System.Text;
using metier;

namespace Export
{
    public interface IExporteNotes
    {
        void Exporter(List<Module> mod);
    }
}
