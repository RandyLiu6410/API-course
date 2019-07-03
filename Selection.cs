using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace lab1
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Selection : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document document = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            ICollection<ElementId> ids = uidoc.Selection.GetElementIds();

            string prompt = "選取元件數量: ";
            prompt += ids.Count.ToString() + "\n";

            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            FilteredElementCollector collector = new FilteredElementCollector(document, ids);
            IList<Element> walls = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            prompt += "牆數量: " + walls.Count.ToString() + "\n";

            filter = new ElementCategoryFilter(BuiltInCategory.OST_Columns);
            collector = new FilteredElementCollector(document, ids);
            IList<Element> columns = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            prompt += "柱數量: " + columns.Count.ToString() + "\n";

            filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            collector = new FilteredElementCollector(document, ids);
            IList<Element> beams = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            prompt += "梁數量: " + beams.Count.ToString() + "\n";

            filter = new ElementCategoryFilter(BuiltInCategory.OST_Doors);
            collector = new FilteredElementCollector(document, ids);
            IList<Element> doors = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            prompt += "門數量: " + doors.Count.ToString() + "\n";

            TaskDialog.Show("總計", prompt);

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
