using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace lab1
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class SetParameter : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document document = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Reference reference = uidoc.Selection.PickObject(ObjectType.Element, "請選擇一個元件");
            Element elem = document.GetElement(reference);

            try
            {
                Transaction trans = new Transaction(document, "更改頂部偏移");
                trans.Start();
                elem.LookupParameter("頂部偏移").Set(0);

                trans.Commit();
            }
            catch
            {
                message = "沒有頂部偏移的參數";
                elements.Insert(elem);
                return Result.Failed;
            }

            return Result.Succeeded;
        }
    }
}
