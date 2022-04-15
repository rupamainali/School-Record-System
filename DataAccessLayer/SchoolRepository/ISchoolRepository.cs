using System.Collections.Generic;
using SharedModel.Response;
using SharedModel.School;

namespace DataAccessLayer.SchoolRepository
{
    public interface ISchoolRepository
    {
        List<SchoolDetails> GetSchoolList();
        Response AddSchool(SchoolDetails schoolDetails);
        Response EditSchool(SchoolDetails schoolDetails);
        SchoolDetails GetSchoolDetails(string Id);
        Response DeleteSchool(string Id);
        List<SchoolDetails> SearchSchoolList(string searchText);
    }
}
