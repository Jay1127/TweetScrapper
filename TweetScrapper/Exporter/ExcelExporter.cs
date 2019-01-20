using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    /// <summary>
    /// 엑셀 내보내기
    /// </summary>
    public class ExcelExporter : IExportable
    {
        /// <summary>
        /// 엑셀의 헤더(헤더 없는 경우 처리 필요)
        /// </summary>
        public IEnumerable<string> Header { get; set; }

        /// <summary>
        /// 엑셀 생성자
        /// </summary>
        /// <param name="header">헤더</param>
        public ExcelExporter(IEnumerable<string> header)
        {
            Header = header;
        }

        /// <summary>
        /// 엑셀로 내보내기
        /// </summary>
        /// <param name="path">파일 내보내기 경로</param>
        /// <param name="scrapItems">내보낼 scrap item</param>
        public void Export(string path, List<IScrapItem> scrapItems)
        {
            if (!Directory.Exists(Path.GetPathRoot(path)))
            {
                throw new DirectoryNotFoundException();
            }

            var itemsByDate = (from item in scrapItems
                               group item by item.CreationTime into g
                               select new
                               {
                                   sheetName = g.Key.ToShortDateString(),
                                   items = g.Select(val => val).ToArray()
                               }).ToArray();

            using (var spreadsheet = CreateSpreadSheet(path))
            {
                for (int sheetIndex = 0; sheetIndex < itemsByDate.Length; sheetIndex++)
                {
                    AddSheet(spreadsheet, itemsByDate[sheetIndex].sheetName);

                    InsertTextToRow(spreadsheet, sheetIndex, 0, Header);

                    for (int rowIndex = 1; rowIndex < itemsByDate[sheetIndex].items.Count() + 1; rowIndex++)
                    {
                        InsertTextToRow(spreadsheet, sheetIndex, rowIndex, itemsByDate[sheetIndex].items[rowIndex - 1].ComposePrintItmes());
                    }
                }

                Save(spreadsheet);
            }
        }

        /// <summary>
        /// SpreadSheet를 생성함.
        /// </summary>
        /// <param name="path">생성할 엑셀 파일 경로</param>
        /// <returns>SpreadsheetDocument</returns>
        private SpreadsheetDocument CreateSpreadSheet(string path)
        {
            var spreadSheet = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook);

            WorkbookPart workbookPart = spreadSheet.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            SharedStringTablePart sharedStringTablePart = spreadSheet.WorkbookPart.AddNewPart<SharedStringTablePart>();
            sharedStringTablePart.SharedStringTable = new SharedStringTable();

            return spreadSheet;
        }

        /// <summary>
        /// 엑셀 파일에 Sheet를 생성함.
        /// </summary>
        /// <param name="spreadsheet">SpreadSheet</param>
        /// <param name="sheetName">생성할 sheet이름</param>
        private void AddSheet(SpreadsheetDocument spreadsheet, string sheetName)
        {
            WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
            newWorksheetPart.Worksheet = new Worksheet(new SheetData());

            Sheets sheets = spreadsheet.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
            string relationshipId = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart);

            // Get a unique ID for the new worksheet.
            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Count() > 0)
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            // Append the new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
            sheets.Append(sheet);
        }

        /// <summary>
        /// 주어진 Row에 텍스트를 입력함.
        /// </summary>
        /// <param name="spreadSheet">SpreadSheet</param>
        /// <param name="sheetIndex">sheet 번호{0부터 시작(엑셀은 1부터 시작)}</param>
        /// <param name="rowIndex">행 번호{0부터 시작(엑셀은 1부터 시작)}</param>
        /// <param name="insertedTexts">입력할 문자열(각 아이템마다 셀에 추가)</param>
        private void InsertTextToRow(SpreadsheetDocument spreadSheet, int sheetIndex, int rowIndex, IEnumerable<string> insertedTexts)
        {
            var sharedStringTablePart = spreadSheet.WorkbookPart.SharedStringTablePart;

            var worksheetPart = spreadSheet.WorkbookPart.WorksheetParts.ElementAt(sheetIndex);
            var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            var row = new Row() { RowIndex = (uint)(rowIndex + 1) };
            
            foreach (string text in insertedTexts)
            {
                int i = 0;

                // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
                foreach (SharedStringItem item in sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>())
                {
                    if (item.InnerText == text)
                    {
                        break;
                    }

                    i++;
                }

                sharedStringTablePart.SharedStringTable.AppendChild(new SharedStringItem(new Text(text)));
                sharedStringTablePart.SharedStringTable.Save();

                Cell cell = new Cell()
                {
                    CellValue = new CellValue(i.ToString()),
                    DataType = new EnumValue<CellValues>(CellValues.SharedString)
                };

                row.Append(cell);
            }

            sheetData.Append(row);
        }

        /// <summary>
        /// SpreadSheet 저장
        /// </summary>
        /// <param name="spreadsheet">SpreadSheet</param>
        private void Save(SpreadsheetDocument spreadsheet)
        {
            spreadsheet.Save();
        }
    }
}
