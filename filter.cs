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
    public class Filter : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document document = commandData.Application.ActiveUIDocument.Document;
            // Find all Wall instances in the document by using category filter
            // 使用品類篩選器在文件檔案中找出所有『牆』實作元件
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            // Apply the filter to the elements in the active document
            // 對作用文件檔案中的元件使用這個篩選器
            // Use shortcut WhereElementIsNotElementType() to find wall instances only
            // 使用WhereElementIsNotElementType()來找尋牆實作元件
            FilteredElementCollector collector = new FilteredElementCollector(document);
            IList<Element> walls = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            String prompt = "目前執行中之文件檔案中的『牆』有 : \n";
            foreach (Element e in walls)
            {
                prompt += e.Name + "\n";
            }
            TaskDialog.Show("牆", prompt);

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
