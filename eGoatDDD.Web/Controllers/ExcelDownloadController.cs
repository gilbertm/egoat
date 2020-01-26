using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Domain.Constants;
using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style.XmlAccess;

namespace eGoatDDD.Web.Controllers
{

    public class ExcelFileDto
    {
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }

        public ExcelFileDto(string fileName, byte[] fileBytes)
        {
            FileName = fileName;
            FileBytes = fileBytes;
        }
    }

    [Authorize(Policy = "CanEdits")]
    public class ExcelDownloadController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> Goats()
        {
            using (ExcelPackage goatPackage = new ExcelPackage())
            {
                GoatsListNonDtoViewModel goatsLisNonDtotViewModel = await _mediator.Send(new GetAllGoatsQuery
                {
                    PageNumber = 0,
                    Filter = "all",
                    PageSize = 0
                });


                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = goatPackage.Workbook.Worksheets.Add("Goats");

                //First add the headers
                worksheet.Cells[1, 1].Value = "#";
                worksheet.Cells[1, 1].Style.Font.Bold = true;

                worksheet.Cells[1, 2].Value = "Status";
                worksheet.Cells[1, 2].Style.Font.Bold = true;

                worksheet.Cells[1, 3].Value = "Color";
                worksheet.Cells[1, 3].Style.Font.Bold = true;
                worksheet.Cells[1, 4].Value = "Code";
                worksheet.Cells[1, 4].Style.Font.Bold = true;
                worksheet.Cells[1, 5].Value = "Birthdate";
                worksheet.Cells[1, 5].Style.Font.Bold = true;
                worksheet.Cells[1, 6].Value = "Age";
                worksheet.Cells[1, 6].Style.Font.Bold = true;
                worksheet.Cells[1, 7].Value = "Gender";
                worksheet.Cells[1, 7].Style.Font.Bold = true;
                worksheet.Cells[1, 8].Value = "Breed";
                worksheet.Cells[1, 8].Style.Font.Bold = true;
                worksheet.Cells[1, 9].Value = "Parents";
                worksheet.Cells[1, 9].Style.Font.Bold = true;


                if (goatsLisNonDtotViewModel.Goats.Count() > 0)
                {
                    var ictr = 1;

                    string StyleName = "HyperStyle";
                    ExcelNamedStyleXml HyperStyle = worksheet.Workbook.Styles.CreateNamedStyle(StyleName);
                    HyperStyle.Style.Font.UnderLine = true;
                    HyperStyle.Style.Font.Size = 12;
                    HyperStyle.Style.Font.Color.SetColor(Color.Blue);

                    foreach (var goat in goatsLisNonDtotViewModel.Goats)
                    {
                        // worksheet.Cells[ictr + 1, 1].Value = $"<a href='https://egoatdddweb.azurewebsites.net/Goat/Get/{goat.Goat.Id}'>{ictr}</a>";
                        
                        //------HYPERLINK to a website.  
                        using (ExcelRange range = worksheet.Cells[ictr + 1, 1])
                        {
                            range.Hyperlink = new Uri($"https://egoatdddweb.azurewebsites.net/Goat/Get/{goat.Goat.Id}", UriKind.Absolute);
                            range.Value = ictr;
                            range.StyleName = StyleName;
                        }

                        worksheet.Cells[ictr + 1, 2].Value = goat.Goat.DisposalId.HasValue ? (goat.Goat.DisposalId > 0 ? $"{Enum.GetName(typeof(DisposeType), goat.Goat.Disposal.Type)}" : "Alive") : "Alive";

                        worksheet.Cells[ictr + 1, 3].Value = goat.Goat.Color.Name;

                        worksheet.Cells[ictr + 1, 4].Value = goat.Goat.Code;

                        worksheet.Cells[ictr + 1, 5].Value = goat.Goat.BirthDate?.ToString("yyyy-MM-dd");

                        var birthdate = goat.Goat.BirthDate.HasValue ? goat.Goat.BirthDate.Value : DateTime.Now;

                        DateTime date1 = birthdate;
                        DateTime date2 = DateTime.Now;

                        int oldMonth = date2.Month;
                        while (oldMonth == date2.Month)
                        {
                            date1 = date1.AddDays(-1);
                            date2 = date2.AddDays(-1);
                        }

                        int years = 0, months = 0, days = 0;

                        while (date2.CompareTo(date1) >= 0)
                        {
                            years++;
                            date2 = date2.AddYears(-1);
                        }

                        date2 = date2.AddYears(1);
                        years--;


                        oldMonth = date2.Month;
                        while (date2.CompareTo(date1) >= 0)
                        {
                            days++;
                            date2 = date2.AddDays(-1);
                            if ((date2.CompareTo(date1) >= 0) && (oldMonth != date2.Month))
                            {
                                months++;
                                days = 0;
                                oldMonth = date2.Month;
                            }
                        }

                        date2 = date2.AddDays(1);

                        worksheet.Cells[ictr + 1, 6].Value = string.Concat(years, " years", months, " months", days, " days");

                        worksheet.Cells[ictr + 1, 7].Value = goat.Goat.Gender == 'M' ? "Male" : "Female";

                        var breeds = string.Empty;

                        if (goat.Goat.GoatBreeds != null)
                        {
                            foreach (var breed in goat.Goat.GoatBreeds)
                            {
                                breeds += string.Concat(breed.Percentage, " ", breed.Breed.Name, ";");
                            }
                        }

                        worksheet.Cells[ictr + 1, 8].Value = breeds;

                        var parents = string.Empty;

                        if (goat.Goat.Parents != null)
                        {
                            foreach (var parent in goat.Goat.Parents)
                            {
                                parents += string.Concat(parent.Parent.Color.Name, " ", parent.Parent.Code, ";");
                            }
                        }

                        worksheet.Cells[ictr + 1, 9].Value = parents;

                        ictr++;
                    }
                }
                
                var excelfile = new ExcelFileDto($"Goats-{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.xlsx", goatPackage.GetAsByteArray());

                return File(excelfile.FileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelfile.FileName);
            }

        }
    }
}