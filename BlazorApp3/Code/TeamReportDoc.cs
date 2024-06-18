using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using BlazorApp3.Model;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using QuestPDF.Drawing;

namespace BlazorApp3.Code
{
    

    public class TeamReportDoc : IDocument
    {
        private List<TeamsMembers> members { get; set; }
        private string? LeagueName { get; set; }

        private int TeamSize;


        internal TeamReportDoc()
        {

        }

        public TeamReportDoc(int id)
        {
            var teamsMembers = new List<TeamsMembers>();
            using (var db = new TournamentContext())
            {
                var league = db.Leagues.Find(id);
                var teams = db.Teams.Where(x => x.Leagueid == id).ToList();
                foreach (var team in teams)
                {
                    var item = new TeamsMembers()
                    {
                        Division = team.DivisionId,
                        TeamNo = team.TeamNo
                    };
                    if (team.Skip != null)
                    {
                        var query = from m in db.Memberships
                                    join p in db.Players on m.Id equals p.MembershipId
                                    where p.Id == team.Skip
                                    select new { FullName = m.FullName };

                        item.Skip = query.First().FullName;
                    }
                    if (team.ViceSkip != null)
                    {
                        var query = from m in db.Memberships
                                    join p in db.Players on m.Id equals p.MembershipId
                                    where p.Id == team.ViceSkip
                                    select new { FullName = m.FullName };

                        item.ViceSkip = query.First().FullName;
                    }
                    if(team.Lead !=  null)
                    {
                        var query = from m in db.Memberships
                                    join p in db.Players on m.Id equals p.MembershipId
                                    where p.Id == team.Lead
                                    select new { FullName = m.FullName };

                        item.Lead = query.First().FullName;
                    }
                    teamsMembers.Add(item);
                }
                LeagueName = league.LeagueName;
                TeamSize = league.TeamSize;
                members = teamsMembers.OrderBy(x => x.Division * 100 + x.TeamNo).ToList();
            }
        }

        public DocumentMetadata GetMetadata() { return new DocumentMetadata(); }

        //
        // Summary:
        //     Configures the document content by specifying its layout structure and visual
        //     element.
        //
        // Parameters:
        //   container:
        //     The document container used for defining content via the FluentAPI.      
        [Obsolete]
        public void Compose(IDocumentContainer container)
        {
            container
            .Page(page =>
            {
                page.Margin(50);

                page.Header()
                    .Text(LeagueName)
                    .SemiBold().FontSize(24);
                    

                page.Content()
                .Table(table =>
                 {
                     // step 1
                     table.ColumnsDefinition(columns =>
                     {
                         columns.ConstantColumn(50);
                         columns.ConstantColumn(50);
                         columns.ConstantColumn(150);
                         if (TeamSize == 3)
                         {
                             columns.ConstantColumn(150);
                         }
                         if (TeamSize > 1)
                         {
                             columns.ConstantColumn(150);
                         }
                     });

                   
                    

                     // step 3
                     table.Cell().Element(CellStyle2).Text("Division").SemiBold();
                     table.Cell().Element(CellStyle2).Text("Team No").SemiBold();
                     table.Cell().Element(CellStyle2).Text("Skip").SemiBold();
                     if (TeamSize == 3)
                     {
                         table.Cell().Element(CellStyle2).Text("Vice Skip").SemiBold();
                     }
                     if (TeamSize > 1)
                     {
                         table.Cell().Element(CellStyle2).Text("Lead").SemiBold();
                     }

                     static IContainer CellStyle2(IContainer container)
                     {
                         return container.Border(1).BorderColor(Colors.Black).PaddingVertical(5).AlignCenter();
                     }
      
                     foreach (var item in members)
                     {
                         

                         table.Cell().Element(CellStyle).Text(item.Division);
                         table.Cell().Element(CellStyle).Text(item.TeamNo);
                         table.Cell().Element(CellStyle1).AlignRight().Text(item.Skip);
                         if (TeamSize == 3)
                         {
                             table.Cell().Element(CellStyle1).Text(item.ViceSkip);
                         }
                         if (TeamSize > 1)
                         {
                             table.Cell().Element(CellStyle1).Text(item.Lead);
                         }

                         static IContainer CellStyle(IContainer container)
                         {
                             return container.Border(1).BorderColor(Colors.Black).PaddingVertical(5).AlignCenter();
                         }
                         static IContainer CellStyle1(IContainer container)
                         {
                             return container.Border(1).BorderColor(Colors.Black).PaddingVertical(5).AlignRight();
                         }
                     }
                 });
            });
        }
    }

    internal class TeamsMembers
    {
        public int Division { get; set; }
        public int TeamNo { get; set; }
        public string? Skip = "";
        public string? ViceSkip = "";
        public string? Lead = "";
    }
}
