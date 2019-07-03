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
    class GetParameter : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document document = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Reference reference = uidoc.Selection.PickObject(ObjectType.Element, "請選擇一個元件");
            Element elem = document.GetElement(reference);

            string prompt = "";

            try
            {
                string value = elem.LookupParameter("頂部偏移").AsValueString();
                prompt += elem.Name + "的頂部偏移為" + value;

                TaskDialog.Show("篩選", prompt);
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
