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
    class ParameterFilter : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document document = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            ICollection<ElementId> ids = uidoc.Selection.GetElementIds();

            string prompt = "";

            BuiltInParameter volParam = BuiltInParameter.HOST_VOLUME_COMPUTED;
            // provider
            ParameterValueProvider pvp = new ParameterValueProvider(new ElementId((int)volParam));
            // evaluator
            FilterNumericRuleEvaluator fnrv = new FilterNumericGreater();
            // 篩選面體積大於500立方呎的元件
            double ruleValue = 500f;
            // rule 規則
            FilterRule fRule = new FilterDoubleRule(pvp, fnrv, ruleValue, 1E-6);
            // Create an ElementParameter filter 創建ElementParameter篩選器
            ElementParameterFilter filter = new ElementParameterFilter(fRule);
            // Apply the filter to the elements in the active document 使用這個篩選器到作用文件檔案中的元件
            FilteredElementCollector collector = new FilteredElementCollector(document);
            IList<Element> elems = collector.WherePasses(filter).ToElements();
            prompt += "篩選後的元件數量:" + elems.Count.ToString() + "\n";

            ElementCategoryFilter filterWall = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            IList<Element> walls = collector.WherePasses(filter).WherePasses(filterWall).ToElements();
            prompt += "篩選後的牆數量:" + walls.Count.ToString() + "\n";

            TaskDialog.Show("篩選", prompt);

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
